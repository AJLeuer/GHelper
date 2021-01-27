using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public partial class ProfileView : UserControl, RecordView
	{
		public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
			nameof (ProfileSelectorView.Profile),
			typeof (ProfileViewModel),
			typeof (ProfileSelectorView),
			new PropertyMetadata(null)
		);
	    
		public ProfileViewModel Profile
		{
			get { return (ProfileViewModel) GetValue(ProfileProperty); }
			set { SetValue(ProfileProperty, value); }
		}

		public GHubRecordViewModel GHubRecord
		{
			get { return Profile; }
		}
		
	    public ProfileView()
        {
            InitializeComponent();
        }

	    void RecordView.SaveRecord()
	    {
		    
	    }

	    void RecordView.HandleUserEdit()
	    {
		    
	    }

	    private void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
		{
			RecordView.ChangeName(this, sender, routedEventInfo);
		}

	    private void HandleNameChange(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
	    {
		    RecordView.ChangeName(this, sender, eventInfo);
	    }
	}
}