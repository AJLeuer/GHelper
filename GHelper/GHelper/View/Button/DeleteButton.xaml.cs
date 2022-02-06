using GHelper.ViewModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;

namespace GHelper.View.Button
{
    public sealed partial class DeleteButton : Microsoft.UI.Xaml.Controls.Button, StandardButton
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
                gHubRecordViewModel = value;
                WireToGHubRecord();
            }
        }

        public DeleteButton()
        {
            this.InitializeComponent();
        }

        public void WireToGHubRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click += (object _, RoutedEventArgs _) => { GHubRecordViewModel?.Delete(); };
                Visibility = (GHubRecordViewModel is DesktopApplicationViewModel) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}