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
			GHubDataDisplay.Content = ApplicationView;
		}

		private void ProfileSelected(ProfileViewModel profile)
		{
			GHubDataDisplay.Content = ProfileView;
		}

		private void SendNotificationsThatSelectedGHubRecordChanged(TreeView sender, TreeViewSelectionChangedEventArgs info)
		{
			object? selectedItem = sender.SelectedItem;
			
			switch (selectedItem)
			{
				case ApplicationViewModel application:
					//somehow get the viewmodel, which has to notify its ApplicationSelectorView that it was selected
					//todo remove below line:
					ApplicationSelected(application);
					break;
				case ProfileViewModel profile:
					//somehow get the viewmodel, which has to notify its ProfileSelectorView that it was selected
					//todo remove below line:
					ProfileSelected(profile);
					break;
			}
		}
	}
}