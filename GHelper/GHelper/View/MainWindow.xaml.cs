using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GHelper.Annotations;
using GHelper.Event;
using GHelper.Properties;
using GHelper.Service;
using GHelper.Utility;
using GHelper.View.Dialog;
using GHelper.View.Text;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View 
{
	public sealed partial class MainWindow : Window, INotifyPropertyChanged
	{
		public static readonly string AppName = Resources.ApplicationName;

		private ObservableCollection<ApplicationViewModel>? applications;

		public ObservableCollection<ApplicationViewModel>? Applications
		{
			get => applications;
			set
			{
				applications = value;
				OnPropertyChanged(nameof(Applications));
			}
		}

		public  GHubRecordViewModel?     DisplayedRecord         { get;  set; }
		private TreeViewNode?            LastSelectedNode        { get;  set; }
		public  GHubSettingsFileService? GHubSettingsFileService { get ; set ; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event UserSavedEvent?              UserSaved;
        public event UserDeletedRecordEvent?      UserPressedDelete;
		public event UserDeletedRecordEvent?      UserConfirmedDelete;

		public MainWindow()
		{
			this.InitializeComponent();
			this.UserPressedDelete += DisplayDeleteDialog;
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.TitleBar);
        }
        
        private async void HandleGHubRecordSelected(TreeView treeView, TreeViewItemInvokedEventArgs info)
        {
            LastSelectedNode = treeView.SelectedNode;
            if (info.InvokedItem is GHubRecordViewModel gHubRecord)
			{
				await HandleGHubRecordSelected(gHubRecord);
			}
		}

		private async Task HandleGHubRecordSelected(GHubRecordViewModel gHubRecord)
        {
            await CheckForUserChanges();
            ChangeDisplayedRecord(gHubRecord);
		}

        private async Task CheckForUserChanges()
        {
            if (DisplayedRecord?.State == State.Modified)
            {
                ContentDialogResult dialogResult = await DisplaySaveDialog();

                switch (dialogResult)
                {
                    // user elected to save
                    case ContentDialogResult.Primary:
                        DisplayedRecord.Save();
                        break;
                    // user doesn't want to save their changes
                    case ContentDialogResult.Secondary:
                        DisplayedRecord?.DiscardUserChanges();
                        break;
                    // user doesn't actually want to change the viewed record
                    case ContentDialogResult.None:
                        if (LastSelectedNode is not null)
                        {
                            TreeView.SelectedNode = LastSelectedNode;
                        }
                        return;
                    default:
                        return;
                }
            }
        }
        
        private void ChangeDisplayedRecord(GHubRecordViewModel gHubRecord)
        {
            RemoveOldDisplayedRecord();
            SetNewDisplayedRecord(gHubRecord);
        }

        private void RemoveOldDisplayedRecord()
        {
            if (DisplayedRecord is not null)
            {
                DisplayedRecord.UserSaved -= this.UserSaved;
                DisplayedRecord.UserDeletedRecord -= this.UserPressedDelete;
            }
        }

        private void SetNewDisplayedRecord(GHubRecordViewModel gHubRecord)
        {
            DisplayedRecord = gHubRecord;
            
            DisplayedRecord.UserSaved += this.UserSaved;
            DisplayedRecord.UserDeletedRecord += this.UserPressedDelete;
            
            RecordView view = RecordView.CreateViewForViewModel(gHubRecord);
            SaveButton.GHubRecordViewModel = gHubRecord;
            DeleteButton.GHubRecordViewModel = gHubRecord;
            GHubDataDisplay.Content = view;
            OnPropertyChanged(nameof(BorderVisibleForGHubContent));
        }

        public async Task DisplayGHubRunningDialogIfNeeded()
        {
            GHubRunningDialog gHubRunningDialog = new () { XamlRoot = MainView.XamlRoot };
            await gHubRunningDialog.DisplayIfNeeded();
        }

        public async Task DisplayGHubSettingsFileNotFoundDialogIfNeeded()
        {
            GHubSettingsFileNotFoundDialog fileNotFoundDialog = new (GHubSettingsFileService) { XamlRoot = MainView.XamlRoot };
            await fileNotFoundDialog.DisplayIfNeeded();
        }

		private async Task<ContentDialogResult> DisplaySaveDialog()
		{
            ContentDialog deleteFileDialog = new UnsavedChangeDialog { XamlRoot = MainView.XamlRoot };
            return await deleteFileDialog.EnqueueAndShowIfAsync();
		}

        private async void DisplayDeleteDialog(GHubRecordViewModel recordViewModel)
		{
			string? typeName = recordViewModel.GHubRecord?.GetType().Name.ConvertPascalCaseToSentence().ToLower();
			var confirmDialog = new WarnUserDialog
			                    {
				                    XamlRoot = MainView.XamlRoot,
				                    Title = new ItalicizedTextBlock
				                            {
					                            StartingText = $"Are you sure you want to delete the {typeName}  ", 
					                            ItalicizedText = recordViewModel.DisplayName, 
					                            FollowingText = "?"
				                            },

				                    PrimaryButtonText = "Delete"
			                    };

			ContentDialogResult dialogResult = await confirmDialog.EnqueueAndShowIfAsync();
			
			switch (dialogResult)
			{
				case ContentDialogResult.Primary:
					UserConfirmedDelete?.Invoke(recordViewModel);
					break;
			}
		}
        
        [NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        #region OtherProperties
        public Visibility BorderVisibleForGHubContent
        {
            get { return GHubDataDisplay.Content is RecordView ? Visibility.Visible : Visibility.Collapsed; }
        }
        #endregion
    }
}