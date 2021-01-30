using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelper.Utility;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class GHubSettingsFileViewModel
	{
		public GHubSettingsFile                           GHubSettingsFile { get; }
		public ObservableCollection<ApplicationViewModel> Applications     { get; } = new();

		public GHubSettingsFileViewModel(GHubSettingsFile gHubSettingsFile)
		{
			GHubSettingsFile = gHubSettingsFile;
			InitializeApplications();
		}
		
		public void Delete(GHubRecordViewModel recordViewModel)
		{
			if (recordViewModel.GHubRecord is Application application)
			{
				DeleteApplication(application);
			}
			else if (recordViewModel.GHubRecord is Profile profile)
			{
				DeleteProfile(profile);
			}
			InitializeApplications();
		}

		private void DeleteApplication(Application application)
		{
			GHubSettingsFile.Applications?.Applications?.Remove(application);
		}

		private void DeleteProfile(Profile profile)
		{
			GHubSettingsFile.Profiles?.Profiles?.Remove(profile);
		}

		private void InitializeApplications()
		{
			GHubSettingsFile.AssociateProfilesToApplications();
			ICollection<Application>? applications = GHubSettingsFile.Applications?.Applications;
			Applications.ReplaceAll(ApplicationViewModel.CreateFromCollection(applications));
		}
	}
}