using System.Collections.ObjectModel;
using GHelper.View;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Controller
{
	public class ApplicationController
	{
		private readonly GHubSettingsFileReader gHubSettingsFileReader = new GHubSettingsFileReader();
		private ObservableCollection<Application>? Applications;
		private readonly Reference<MainWindow> MainWindow;

		public ApplicationController(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Start()
		{
			this.Applications = new ObservableCollection<Application>(gHubSettingsFileReader.ReadData().applications);
			MainWindow.Referent!.Applications = this.Applications;
		}
	}
}