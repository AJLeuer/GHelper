using GHelper.ViewModel;
using Microsoft.UI.Xaml;

namespace GHelper.View.Button
{
    public sealed partial class DeleteButton : Microsoft.UI.Xaml.Controls.Button
    {
        private GHubRecordViewModel? gHubRecordViewModel;

        public GHubRecordViewModel? GHubRecordViewModel
        {
            get
            {
                return gHubRecordViewModel;
            }
            set
            {
                RemovePreviousRecord();
                gHubRecordViewModel = value;
                WireToGHubRecord();
            }
        }

        public DeleteButton()
        {
            this.InitializeComponent();
        }

        private void WireToGHubRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click += InvokeGHubRecordDelete;
                Visibility = (GHubRecordViewModel is CustomApplicationViewModel or ProfileViewModel) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        
        private void RemovePreviousRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click -= InvokeGHubRecordDelete;
            }
        }

        private void InvokeGHubRecordDelete(object o, RoutedEventArgs routedEventArgs)
        {
            GHubRecordViewModel?.Delete();
        }
    }
}