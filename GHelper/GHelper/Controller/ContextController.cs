using System.Collections.ObjectModel;
using GHelper.View;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.Controller
{
	public class ContextController
	{
		private readonly GHubSettingsFileReader gHubSettingsFileReader = new GHubSettingsFileReader();
		private ObservableCollection<Context>? Contexts;
		private readonly Reference<MainWindow> MainWindow;

		public ContextController(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Start()
		{
			this.Contexts = new ObservableCollection<Context>(gHubSettingsFileReader.ReadData().contexts);
			MainWindow.Referent!.Contexts = this.Contexts;
		}
	}
}