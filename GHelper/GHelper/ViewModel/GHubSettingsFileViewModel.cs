using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelperLogic.Model;
using GHelperLogic.Utility;

namespace GHelper.ViewModel
{
	public class GHubSettingsFileViewModel
	{
		public GHubSettingsFile                                      GHubSettingsFile { get; private set; }
		public Reference<ObservableCollection<ApplicationViewModel>> Applications     { get; private set; } = new();

		public GHubSettingsFileViewModel(GHubSettingsFile gHubSettingsFile)
		{
			GHubSettingsFile = gHubSettingsFile;
			ICollection<Application>? applications = GHubSettingsFile.Applications?.Applications;
			Applications.Referent = new ObservableCollection<ApplicationViewModel>(ApplicationViewModel.CreateFromCollection(applications));
		}
	}
}