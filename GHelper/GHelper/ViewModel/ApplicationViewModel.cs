using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.Properties;
using GHelper.Service;
using GHelper.Utility;
using GHelperLogic.Model;
using Microsoft.UI.Xaml.Media;
using SixLabors.ImageSharp;
using WindowsImage = Microsoft.UI.Xaml.Controls.Image;


namespace GHelper.ViewModel 
{
	public class ApplicationViewModel : GHubRecordViewModel
	{
        private static WindowsImage DefaultPosterImage { get ; } = new ();

		private ObservableCollection<ProfileViewModel>? profiles;

		public ObservableCollection<ProfileViewModel> Profiles 
		{
			get
			{
				if (profiles == null)
				{
					CreateProfileViewModelsFromApplicationProfiles();
				}
				return profiles!;
			}
		}

		public override GHubRecord? GHubRecord 
		{
			get { return this.Application; }
		}

		private Application? application;

		public Application? Application 
		{
			get => application;
			set
			{
				application = value;
				OnPropertyChanged(nameof(Application));
			}
		}

		public override string? Type
		{
			get
			{
				if ((Application is not null) && (Application.GetType().IsSubclassOf(typeof(Application))))
				{
					return Application.GetType().Name.ConvertPascalCaseToSentence();
				}
				else
				{
					return Resources.StandardApplicationTypeName;
				}
			}
		}
		
		public override string? Name
		{
			get => Application?.Name;
			set
			{
				if (Application != null)
				{
					Application.Name = value;
					OnPropertyChanged(nameof(DisplayName));
				}
			}
		}

		private WindowsImage? poster;

		public ImageSource Poster
		{
			get
			{
				if (poster != null)
				{
					return poster.Source;
				}
				else if (Application?.HasPoster == false)
				{
					return DefaultPosterImage.Source;
				}
				else
				{
					RetrievePosterImage();
					return poster?.Source ?? DefaultPosterImage.Source;
				}
			}
		}

		public string PosterPath 
		{
			get
			{
				if (Application?.PosterPath?.ToString() is {} posterPath)
				{
					return posterPath;
				}
				else if (Application?.PosterURL != null)
				{
					return Application.PosterURL.ToString();
				}
				else
				{
					return string.Empty;
				}
			}
		}

		public string InstallState 
		{
			get 
			{
				if (Application is DesktopApplication)
				{
					return nameof(ViewModel.InstallState.Installed);
				}
				else if (Application?.IsInstalled == null || (Application.IsInstalled == false))
				{
					return nameof(ViewModel.InstallState.NotInstalled).ConvertPascalCaseToSentence();
				}
				else 
				{
					return nameof(ViewModel.InstallState.Installed);
				}
			}
		}

		public override event PropertyChangedEventHandler? PropertyChanged;

        protected ApplicationViewModel(Application application)
		{
			this.Application = application;
			SaveBackup();
		}

        private static ApplicationViewModel CreateApplicationViewModelForApplicationModel(Application application)
        {
            ApplicationViewModel applicationViewModel;
			
            switch (application)
            {
                case DesktopApplication desktopApplication:
                    applicationViewModel = new DesktopApplicationViewModel(desktopApplication);
                    break;
                case CustomApplication customApplication:
                    applicationViewModel = new CustomApplicationViewModel(customApplication);
                    break;
                default:
                    applicationViewModel = new ApplicationViewModel(application);
                    break;
            }

            return applicationViewModel;
        }
        
        public override void Delete()
        {
            //Do nothing, we don't allow deleting the Desktop app, and deleting a standard app would
            //just result in G Hub reverting the change on start up. Deleting will only work on Custom Applications.
        }
        
        public override void DiscardUserChanges(GHubRecordViewModel? origin = null)
        {
            origin ??= this;
            base.DiscardUserChanges(origin);
            
            //just in case any of the other profiles were changed as a side effect, we'll revert all of them
            foreach (ProfileViewModel profile in Profiles)
            {
                if (profile != origin)
                {
                    profile.RestoreInitialState();
                }
            }
        }
		
        protected internal override void RestoreInitialState()
		{
			RestorePosterIfNeeded();
			base.RestoreInitialState();
			OnPropertyChanged(nameof(Application));
		}

		protected virtual void RestorePosterIfNeeded()
		{
			if (GHubRecordBackup is Application applicationBackup)
			{
				if ((applicationBackup.Poster is not null) && (Application?.Poster != applicationBackup.Poster))
				{
					SetNewCustomPosterImage(applicationBackup.Poster);
				}
			}
		}

		public virtual void SetNewCustomPosterImage(Image customPoster)
		{
			if ((Application?.Name is not null) && (Application?.PosterURL is not null))
			{
				try
				{
					GHubImageStorageService.GHubProgramDataImageStorageService.SavePosterImage(customPoster, Path.GetFileName(Application.PosterURL.ToString()));
					Application.LoadApplicationPosterImage(Application);
					RetrievePosterImage();
					OnPropertyChanged(nameof(Poster));
					OnPropertyChanged(nameof(PosterPath));
				}
				catch (IOException) {}
			}
		}

		private void CreateProfileViewModelsFromApplicationProfiles()
		{
			profiles = new ObservableCollection<ProfileViewModel>();

			if (Application?.Profiles != null)
			{
				foreach (Profile profile in Application.Profiles)
				{
					var profileViewModel = new ProfileViewModel(profile);
					profileViewModel.PropertyChanged += HandleProfilePropertyChanged;
                    profileViewModel.UserDiscardedChanges += DiscardUserChanges;
					profiles.Add(profileViewModel);
				}
			}
		}

		protected void RetrievePosterImage()
		{
			if (Application?.HasPoster != null && Application.HasPoster)
			{
				poster = new WindowsImage { Source = Application?.Poster?.ConvertToWindowsBitmapImage() };
			}
		}

		private void HandleProfilePropertyChanged(object? sender, PropertyChangedEventArgs eventInfo)
		{
			if (sender is ProfileViewModel profile)
			{
				HandleProfileSetActiveForApplication(profile, eventInfo);
			}
		}

		private void HandleProfileSetActiveForApplication(ProfileViewModel profileWithChangedActiveState, PropertyChangedEventArgs eventInfo)
		{
			// We only care about the Profile if the user set it to be active. We don't handle setting ActiveForApplication to false.
			if ((eventInfo.PropertyName == nameof(ProfileViewModel.ActiveForApplication)) && (profileWithChangedActiveState.ActiveForApplication == true))
			{
				foreach (ProfileViewModel profile in this.Profiles)
				{
					if (profile == profileWithChangedActiveState)
					{
						continue;
					}

					profile.ActiveForApplication = false;
                }
			}
		}

        [NotifyPropertyChangedInvocator]
		protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static IEnumerable<ApplicationViewModel> CreateFromCollection(IEnumerable<Application>? applications)
		{
			var applicationViewModels = new Collection<ApplicationViewModel>();

			if (applications != null)
			{
				foreach (Application application in applications)
				{
					ApplicationViewModel applicationViewModel = CreateApplicationViewModelForApplicationModel(application);
					applicationViewModels.Add(applicationViewModel);
				}
			}
			return applicationViewModels;
		}
    }

	public enum InstallState 
	{
		Installed,
		NotInstalled
	}
}