using Microsoft.UI.Xaml;

namespace GHelper.View
{
    public partial class DesktopApplicationView : ApplicationView
    {
        public DesktopApplicationView()
        {
            this.InitializeComponent();
            RecordViewControls.UserClickedSaveButton += () => { GHubRecordViewModel.FireSaveEvent(); };
            RecordViewControls.UserClickedDeleteButton += () => { GHubRecordViewModel.FireDeletedEvent(); };
            
            RecordViewControls.DeleteButton.Visibility = Visibility.Collapsed;
        }

        protected override void ResetAppearance()
        {
            RecordViewControls.ResetAppearance();
        }

        protected override void ChainApplicationChangedEventToControls()
        {
            Application.PropertyChanged += RecordViewControls.NotifyOfUserChange;
        }
    }
}
