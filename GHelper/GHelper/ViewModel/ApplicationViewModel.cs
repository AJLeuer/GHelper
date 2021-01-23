using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelper.Utility;
using GHelperLogic.Model;
using Image = Microsoft.UI.Xaml.Controls.Image;


namespace GHelper.ViewModel
{
	public class ApplicationViewModel : GHubRecordViewModel
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

		public Application Application { get ; set ; }
		
		private Image? poster = null;

		public Image Poster 
		{
			get
			{
				if (poster != null)
				{
					return poster;
				}
				else if (Application.HasPoster == false)
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

		public string? DisplayName => Application.DisplayName;

		public event GHubRecordSelectedEvent? GHubRecordSelected;

		public ApplicationViewModel(Application application)
		{
			this.Application = application;
		}

		private void createProfileViewModelsFromApplicationProfiles()
		{
			profiles = new ObservableCollection<ProfileViewModel>();
			
			foreach (Profile profile in Application.Profiles)
			{
				var profileViewModel = new ProfileViewModel(profile);
				profiles.Add(profileViewModel);
			}
		}
		
		private void retrievePosterImage()
		{
			if (Application.Poster != null)
			{
				poster = new Image { Source = Application.Poster.ConvertToWindowsBitmapImage() };
			}
		}

		public static IEnumerable<ApplicationViewModel> CreateFromCollection(IEnumerable<Application> applications)
		{
			var applicationViewModels = new Collection<ApplicationViewModel>();

			foreach (Application application in applications)
			{
				var applicationViewModel = new ApplicationViewModel(application);
				applicationViewModels.Add(applicationViewModel);
			}

			return applicationViewModels;
		}
	}
}