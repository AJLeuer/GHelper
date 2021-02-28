using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.Utility;
using GHelperLogic.IO;
using GHelperLogic.Model;
using Microsoft.UI.Xaml.Media;
using Image = SixLabors.ImageSharp.Image;
using WindowsImage = Microsoft.UI.Xaml.Controls.Image;


namespace GHelper.ViewModel 
{
	public class ApplicationViewModel : GHubRecordViewModel
	{
		public static WindowsImage DefaultPosterImage { get ; } = new ();

		private ObservableCollection<ProfileViewModel>? profiles;

		public ObservableCollection<ProfileViewModel> Profiles 
		{
			get
			{
				if (profiles == null)
				{
					createProfileViewModelsFromApplicationProfiles();
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

		public string? ApplicationType
		{
			get
			{
				if ((Application is not null) && (Application.GetType().IsSubclassOf(typeof(Application))))
				{
					return Application?.GetType().Name.ConvertPascalCaseToSentence();
				}
				else
				{
					return Properties.Resources.StandardApplicationTypeName;
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
					retrievePosterImage();
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
					return String.Empty;
				}
			}
		}

		public string InstallState 
		{
			get 
			{
				if (Application is DesktopApplication)
				{
					return nameof(GHelper.ViewModel.InstallState.Installed);
				}
				else if (Application?.IsInstalled == null || (Application.IsInstalled == false))
				{
					return nameof(GHelper.ViewModel.InstallState.NotInstalled).ConvertPascalCaseToSentence();
				}
				else 
				{
					return nameof(GHelper.ViewModel.InstallState.Installed);
				}
			}
		}

		public override event PropertyChangedEventHandler? PropertyChanged;

		public ApplicationViewModel(Application application)
		{
			this.Application = application;
			SaveBackup();
		}
		
		public override void RestoreInitialState()
		{
			base.RestoreInitialState();
			OnPropertyChanged(nameof(Application));
		}

		public virtual void SetNewCustomPosterImage(Image customPoster)
		{
		}

		private void createProfileViewModelsFromApplicationProfiles()
		{
			profiles = new ObservableCollection<ProfileViewModel>();

			if (Application?.Profiles != null)
			{
				foreach (Profile profile in Application.Profiles)
				{
					var profileViewModel = new ProfileViewModel(profile);
					profileViewModel.PropertyChanged += HandleProfilePropertyChanged;
					profiles.Add(profileViewModel);
				}
			}
		}

		protected void retrievePosterImage()
		{
			//If this is a application with a custom poster (and the poster bitmap was, therefore, serialized into the JSON)
			//... then it will have already been deserialized and stored into the 'poster' field
			//However if this is not a custom application and it has a posterURL available
			//... then on the first call to get Poster we initialize it by grabbing the image file from the URL
			//So basically Poster is used to store poster images that can be retrieved in two very different ways.
			//Update 25 Feb 2021: A new GHub update has made this situation even more complicated. Now there are 3 different
			//ways a poster could potentially be stored. The new method is a field "posterPath" which directs to a cached file
			//in the GHub AppData directory.

			if (Application?.HasPoster != null && Application.HasPoster)
			{
				if (Application.IsCustom == true)
				{
					if (Application.Poster != null)
					{
						poster = new WindowsImage { Source = Application?.Poster?.ConvertToWindowsBitmapImage()  };
					}
					else if (Application.PosterPath != null)
					{
						Image? posterImage = ImageIOHelper.LoadFromFilePath(Application?.PosterPath!);
						poster = new WindowsImage { Source = posterImage?.ConvertToWindowsBitmapImage() };
					}
				}
				else if (Application?.PosterURL != null)
				{
					Image? posterImage = ImageIOHelper.LoadFromHTTPURL(Application?.PosterURL!);
					poster = new WindowsImage { Source = posterImage?.ConvertToWindowsBitmapImage()  };
				}
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

		private static ApplicationViewModel CreateApplicationViewModelForApplicationModel(Application application)
		{
			ApplicationViewModel applicationViewModel;
			
			switch (application)
			{
				case CustomApplication customApplication:
					applicationViewModel = new CustomApplicationViewModel(customApplication);
					break;
				default:
					applicationViewModel = new ApplicationViewModel(application);
					break;
			}

			return applicationViewModel;
		}
	}

	public enum InstallState
	{
		Installed,
		NotInstalled
	}
}