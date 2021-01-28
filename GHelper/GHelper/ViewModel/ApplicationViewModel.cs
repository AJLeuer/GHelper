using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.Utility;
using GHelperLogic.IO;
using GHelperLogic.Model;
using Image = Microsoft.UI.Xaml.Controls.Image;


namespace GHelper.ViewModel
{
	public class ApplicationViewModel : GHubRecordViewModel, INotifyPropertyChanged
	{
		public static Image DefaultPosterImage { get ; } = new ();

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

		protected override GHubRecord? GHubRecord 
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
		
		public override string Name 
		{
			set
			{
				if (Application != null)
				{
					Application.Name = value;
					OnPropertyChanged(nameof(DisplayName));
				}
			}
		}

		private Image? poster;

		public Image Poster
		{
			get
			{
				if (poster != null)
				{
					return poster;
				}
				else if (Application?.HasPoster == false)
				{
					return DefaultPosterImage;
				}
				else
				{
					retrievePosterImage();
					return poster ?? DefaultPosterImage;
				}
			}
		}

		public string InstallState 
		{
			get 
			{
				if (Application is DesktopApplication)
				{
					return "Installed";
				}
				if ((Application == null) || (Application.IsInstalled == null) || (Application.IsInstalled == false))
				{
					return "Not Installed";
				}
				else 
				{
					return "Installed";
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public ApplicationViewModel(Application application)
		{
			this.Application = application;
		}

		private void createProfileViewModelsFromApplicationProfiles()
		{
			profiles = new ObservableCollection<ProfileViewModel>();

			if (Application?.Profiles != null)
			{
				foreach (Profile profile in Application.Profiles)
				{
					var profileViewModel = new ProfileViewModel(profile);
					profiles.Add(profileViewModel);
				}
			}
		}
		
		private void retrievePosterImage()
		{
			//If this is a application with a custom poster (and the poster bitmap was, therefore, serialized into the JSON)
			//... then it will have already been deserialized and stored into the 'poster' field
			//However if this is not a custom application and it has a posterURL available
			//... then on the first call to get Poster we initialize it by grabbing the image file from the URL
			//So basically Poster is used to store poster images that can be retrieved in two very different ways.

			if (Application?.HasPoster != null && Application.HasPoster)
			{
				if (Application.IsCustom == true)
				{
					poster = new Image { Source = Application?.Poster?.ConvertToWindowsBitmapImage()  };
				}
				else if (Application?.PosterURL != null)
				{
					SixLabors.ImageSharp.Image? posterImage = IOHelper.LoadFromURL(Application?.PosterURL!);
					poster = new Image { Source = posterImage?.ConvertToWindowsBitmapImage()  };
				}
			}
		}

		public static IEnumerable<ApplicationViewModel> CreateFromCollection(IEnumerable<Application>? applications)
		{
			var applicationViewModels = new Collection<ApplicationViewModel>();

			if (applications != null)
			{
				foreach (Application application in applications)
				{
					var applicationViewModel = new ApplicationViewModel(application);
					applicationViewModels.Add(applicationViewModel);
				}
			}
			return applicationViewModels;
		}


		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}