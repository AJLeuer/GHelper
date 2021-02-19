using System;
using System.Collections.ObjectModel;
using GHelper.ViewModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<ApplicationViewModel>?	Applications { get; set; }

		public  GHubRecord?                  CurrentDisplayedItem { get; private set; } = null;
		private Action?                      SaveFunction         { get; set; }
		private Action<GHubRecordViewModel>? DeleteFunction       { get; set; } 


		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void ApplicationSelected(ApplicationViewModel application)
		{
			GHubDataDisplay.Content = RecordView.CreateViewForViewModel(application, SaveFunction!, DeleteFunction!);
		}

		private void ProfileSelected(ProfileViewModel profile)
		{
			GHubDataDisplay.Content = RecordView.CreateViewForViewModel(profile, SaveFunction!, DeleteFunction!);
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

		public void RegisterForSaveNotification(Action saveFunction)
		{
			SaveFunction = saveFunction;
		}

		public void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction)
		{
			DeleteFunction = deleteFunction;
		}
	}
}