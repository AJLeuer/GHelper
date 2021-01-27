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
		private readonly GHubSettingsFileReader gHubSettingsFileReader = new GHubSettingsFileReader();
		private ObservableCollection<ApplicationViewModel>? Applications;
		private readonly Reference<MainWindow> MainWindow;

		public ApplicationService(Reference<MainWindow> mainWindow)
		{
			MainWindow = mainWindow;
			if (MainWindow.Referent is not null)
			{
				MainWindow.Referent.GHubRecordSaved += this.Save();
			}
		}

		public void Start()
		{
			Collection<Application> applications = gHubSettingsFileReader.ReadData().applications;
			this.Applications = new ObservableCollection<ApplicationViewModel>(ApplicationViewModel.CreateFromCollection(applications));
			MainWindow.Referent!.Applications = this.Applications;
		}

		private GHubRecordSavedEvent Save()
		{
			throw new System.NotImplementedException();
		}
	}
}