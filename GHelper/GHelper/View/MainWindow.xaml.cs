using System.Collections.ObjectModel;
using GHelper.ViewModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<ApplicationViewModel>? Applications { get; set; }

		public GHubRecord? CurrentDisplayedItem { get; private set; } = null;

		private ApplicationView ApplicationView { get; set; } = new();
		private ProfileView ProfileView { get; set; } = new();

		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void ApplicationSelected(ApplicationViewModel application)
		{
			ApplicationView.Application = application;
			GHubDataDisplay.Content = ApplicationView;
		}

		private void ProfileSelected(ProfileViewModel profile)
		{
			ProfileView.Profile = profile;
			GHubDataDisplay.Content = ProfileView;
		}

		private void HandleSelectedGHubRecordChanged(TreeView sender, TreeViewSelectionChangedEventArgs info)
		{
			object? selectedItem = sender.SelectedItem;
			
			if (selectedItem is GHubRecordViewModel gHubRecord)
			{
				switch (gHubRecord)
				{
					case ApplicationViewModel application:
						ApplicationSelected(application);
						break;
					case ProfileViewModel profile:
						ProfileSelected(profile);
						break;
				}	
			}
		}
	}
}