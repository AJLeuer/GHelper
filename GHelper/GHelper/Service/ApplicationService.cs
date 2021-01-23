using System.Collections.ObjectModel;
using GHelper.View;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Service
{
	public class ApplicationService
	{
		private readonly GHubSettingsFileReader gHubSettingsFileReader = new GHubSettingsFileReader();
		private ObservableCollection<Application>? Applications;
		private readonly Reference<MainWindow> MainWindow;

		public ApplicationService(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Start()
		{
			Collection<Application> applications = gHubSettingsFileReader.ReadData().applications;
			this.Applications = new ObservableCollection<Application>(applications);
			MainWindow.Referent!.Applications = this.Applications;
		}
	}
}