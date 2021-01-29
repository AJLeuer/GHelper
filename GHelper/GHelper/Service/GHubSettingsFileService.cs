using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelper.View;
using GHelper.ViewModel;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Service
{
	public class GHubSettingsFileService
	{
		private readonly GHubSettingsFileReaderWriter GHubSettingsFileReaderWriter = new GHubSettingsFileReaderWriter();
		private          GHubSettingsFileViewModel?   GHubSettingsFileViewModel;
		private readonly Reference<MainWindow>        MainWindow;

		public GHubSettingsFileService(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Start()
		{
			GHubSettingsFile gHubSettingsFile = GHubSettingsFileReaderWriter.Read();
			GHubSettingsFileViewModel = new GHubSettingsFileViewModel(gHubSettingsFile);
			MainWindow.Referent!.Applications = GHubSettingsFileViewModel.Applications;
			RegisterForNotifications();
		}

		private void Save()
		{
			GHubSettingsFileReaderWriter.Write(settingsFileObject: GHubSettingsFileViewModel?.GHubSettingsFile);
		}

		private void RegisterForNotifications()
		{
			if (MainWindow.Referent is not null)
			{
				MainWindow.Referent!.RegisterForSaveNotification(this.Save);
			}
		}
	}
}