using System.Collections.ObjectModel;
using GHelper.View;
using GHelper.ViewModel;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Service
{
	public class ApplicationService
	{
		private readonly GHubSettingsFileReaderWriter gHubSettingsFileReaderWriter = new GHubSettingsFileReaderWriter();
		private ObservableCollection<ApplicationViewModel>? Applications;
		private readonly Reference<MainWindow> MainWindow;

		public ApplicationService(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Start()
		{
			Collection<Application> applications = gHubSettingsFileReaderWriter.ReadData().applications;
			this.Applications = new ObservableCollection<ApplicationViewModel>(ApplicationViewModel.CreateFromCollection(applications));
			MainWindow.Referent!.Applications = this.Applications;
			RegisterForNotifications();
		}

		private void Save()
		{
			//todo
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