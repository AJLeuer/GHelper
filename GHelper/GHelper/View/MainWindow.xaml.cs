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
		public  ObservableCollection<ApplicationViewModel>? Applications    { get;  set; }
		private Action?                                     SaveFunction    { get;  set; }
		private Action<GHubRecordViewModel>?                DeleteFunction  { get;  set; }
		public  GHubRecordViewModel?                        DisplayedRecord { get ; set ; }


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
		
		private async void HandleSelectedGHubRecordChanged(TreeView sender, TreeViewSelectionChangedEventArgs info)
		{
			if (sender.SelectedItem is GHubRecordViewModel gHubRecord)
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
							return;
					}
				}
				
				ChangeDisplayedRecord(gHubRecord);
			}
		}

		private async Task<ContentDialogResult> DisplaySaveDialog()
		{
            ContentDialog deleteFileDialog = new UnsavedChangeDialog { XamlRoot = MainView.XamlRoot };
            return await deleteFileDialog.ShowAsync().AsTask();
		}

		private void ChangeDisplayedRecord(GHubRecordViewModel gHubRecord)
		{
			if (DisplayedRecord != null)
			{
				DisplayedRecord.PropertyChanged -= HandleDisplayedRecordModified;
			}
			
			DisplayedRecord = gHubRecord;
			DisplayedRecord.PropertyChanged += HandleDisplayedRecordModified;
			
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