using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public  ObservableCollection<ApplicationViewModel>? Applications     { get; set; }
		private Action?                                     SaveFunction     { get; set; }
		private Action<GHubRecordViewModel>?                DeleteFunction   { get; set; }
		public  GHubRecordViewModel?                        DisplayedRecord  { get; set; }
		private TreeViewNode?                               LastSelectedRecord { get; set; }

		private ushort SelectionProgrammaticResetLoops = 0;


		public MainWindow()
		{
			this.InitializeComponent();
		}

		public void RegisterForSaveNotification(Action saveFunction)
		{
			SaveFunction = saveFunction;
		}

		public void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction)
		{
			DeleteFunction = deleteFunction;
		}
		
		private async void HandleSelectedGHubRecordChanged(TreeView treeView, TreeViewSelectionChangedEventArgs info)
		{
			if (SelectionProgrammaticResetLoops > 0)
			{
				SelectionProgrammaticResetLoops--;
				return;
			}
			
			if (treeView.SelectedItem is GHubRecordViewModel gHubRecord)
			{
				await HandleSelectedGHubRecordChanged(gHubRecord);
			}
		}

		private async Task HandleSelectedGHubRecordChanged(GHubRecordViewModel gHubRecord)
		{
			if (DisplayedRecord?.State == State.Modified)
			{
				ContentDialogResult dialogResult = await DisplaySaveDialog();

				switch (dialogResult)
				{
					// user elected to save
					case ContentDialogResult.Primary:
						SaveFunction?.Invoke();
						break;
					// user doesn't want to save their changes
					case ContentDialogResult.Secondary:
						DisplayedRecord?.RestoreInitialState();
						break;
					// user doesn't actually want to change the viewed record
					case ContentDialogResult.None:
						// this is so stupid
						SelectionProgrammaticResetLoops = 2;
						TreeView.SelectedNode = LastSelectedRecord;
						return;
				}
			}
				
			ChangeDisplayedRecord(gHubRecord, TreeView.SelectedNode);
		}

		private async Task<ContentDialogResult> DisplaySaveDialog()
		{
            ContentDialog deleteFileDialog = new UnsavedChangeDialog { XamlRoot = MainView.XamlRoot };
            return await deleteFileDialog.ShowAsync().AsTask();
		}

		private void ChangeDisplayedRecord(GHubRecordViewModel gHubRecord, TreeViewNode selectedNode)
		{
			if (DisplayedRecord != null)
			{
				DisplayedRecord.PropertyChanged -= HandleDisplayedRecordModified;
			}

			DisplayedRecord = gHubRecord;
			DisplayedRecord.PropertyChanged += HandleDisplayedRecordModified;
			LastSelectedRecord = selectedNode;

			GHubDataDisplay.Content = RecordView.CreateViewForViewModel(gHubRecord, SaveFunction!, DeleteFunction!);
		}

		private void HandleDisplayedRecordModified(object? sender, PropertyChangedEventArgs eventInfo)
		{
			if (sender is GHubRecordViewModel)
			{
			}
		}
	}
}