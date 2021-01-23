using System;
using System.Collections.ObjectModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<Application>? Applications { get; set; }

		public GHubRecord? CurrentDisplayedItem { get; private set; } = null;

		private ApplicationView ApplicationView { get; set; } = new();
		private ProfileView ProfileView { get; set; } = new();

		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void ApplicationSelected(object? sender, EventArgs eventInfo)
		{
			GHubDataDisplay.Content = ApplicationView;
		}

		private void ProfileSelected(object? sender, EventArgs eventInfo)
		{
			GHubDataDisplay.Content = ProfileView;
		}

		private void SendNotificationsThatSelectedGHubRecordChanged(TreeView sender, TreeViewSelectionChangedEventArgs info)
		{
			object? selectedItem = sender.SelectedItem;
			
			switch (selectedItem)
			{
				case Application application:
					//somehow get the viewmodel, which has to notify its ApplicationSelectorView that it was selected
					//todo remove below line:
					ApplicationSelected(application, new EventArgs());
					break;
				case Profile profile:
					//somehow get the viewmodel, which has to notify its ProfileSelectorView that it was selected
					//todo remove below line:
					ProfileSelected(profile, new EventArgs());
					break;
			}
		}
	}
}