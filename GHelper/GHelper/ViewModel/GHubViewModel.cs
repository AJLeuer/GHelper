using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelper.Utility;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class GHubViewModel
	{
		public GHubSettingsFile                           GHubSettingsFile { get; }
		public ObservableCollection<ApplicationViewModel> Applications     { get; } = new ();

		public GHubViewModel(GHubSettingsFile gHubSettingsFile)
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

		public void SetInitialRecordStates()
		{
			foreach (ApplicationViewModel application in Applications)
			{
				application.SaveBackup();

				foreach (ProfileViewModel profile in application.Profiles)
				{
					profile.SaveBackup();
				}
			}
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