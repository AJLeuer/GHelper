using System.IO;
using GHelper.View;
using GHelper.ViewModel;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
using Optional;
using Optional.Unsafe;

namespace GHelper.Service
{
	public class GHubSettingsFileService
	{
		private readonly GHubSettingsIO        GHubSettingsIO = GHubSettingsIO.CreateAppropriateInstanceForGHubVersion();
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
			Option<GHubSettingsFile> gHubSettingsFile = GHubSettingsIO.Read();
			
			if (gHubSettingsFile.ValueOrDefault() is  { } settingsFile)
			{
				GHubViewModel = new GHubViewModel(settingsFile);
				MainWindow.Referent!.Applications = GHubViewModel.Applications;
			}
			else
			{
				throw new IOException("Couldn't read from G Hub settings file.");
			}
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