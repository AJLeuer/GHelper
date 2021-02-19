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
		public  ObservableCollection<ApplicationViewModel>? Applications   { get; set; }
		private Action?                                     SaveFunction   { get; set; }
		private Action<GHubRecordViewModel>?                DeleteFunction { get; set; } 


		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void HandleSelectedGHubRecordChanged(TreeView sender, TreeViewSelectionChangedEventArgs info)
		{
			if (sender.SelectedItem is GHubRecordViewModel gHubRecord)
			{
				GHubDataDisplay.Content = RecordView.CreateViewForViewModel(gHubRecord, SaveFunction!, DeleteFunction!);
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