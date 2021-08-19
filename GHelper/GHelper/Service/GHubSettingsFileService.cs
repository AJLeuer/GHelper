using GHelper.View;
using GHelper.ViewModel;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Service
{
	public class GHubSettingsFileService
	{
		private readonly GHubSettingsIO        GHubSettingsIO = new GHubSettingsDatabaseIO();
		private          GHubViewModel?        GHubViewModel;
		private readonly Reference<MainWindow> MainWindow;

		public GHubSettingsFileService(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}
		
		public GHubSettingsIO.State CheckSettingsFileAvailability()
		{
			return GHubSettingsIO.CheckSettingsAvailability();
		}

		public void Start()
		{
			Load();
			RegisterForNotifications();
		}

		private void Load()
		{
			GHubSettingsFile gHubSettingsFile = GHubSettingsIO.Read();
			GHubViewModel = new GHubViewModel(gHubSettingsFile);
			MainWindow.Referent!.Applications = GHubViewModel.Applications;
		}

		private void Save()
		{
			GHubSettingsIO.Write(settingsFileObject: GHubViewModel?.GHubSettingsFile);
			GHubViewModel?.SetInitialRecordStates();
		}

		private void Delete(GHubRecordViewModel recordViewModel)
		{
			GHubViewModel?.Delete(recordViewModel);
			Save();
		}

		private void RegisterForNotifications()
		{
			if (MainWindow.Referent is not null)
			{
				MainWindow.Referent!.UserSaved += this.Save;
				MainWindow.Referent!.UserConfirmedDelete += this.Delete;
			}
		}
	}
}