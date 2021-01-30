using System;
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
			set
			{
				SetValue(ProfileProperty, value);
				ResetAppearance();
			}
		}

		public GHubRecordViewModel GHubRecord
		{
			get { return Profile; }
		}

		public ProfileView()
        {
            InitializeComponent();
        }

		public void RegisterForSaveNotification(Action saveFunction)
		{
			RecordViewControls.RegisterForSaveNotification(saveFunction);
		}

		public void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction)
		{
			Action delete = () =>
			{
				deleteFunction(this.GHubRecord);
			};
		    
			RecordViewControls.RegisterForDeleteNotification(delete);
		}

		void RecordView.SendRecordChangedNotification()
	    {
		    RecordViewControls.NotifyOfUserChange();
	    }

	    private void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
		{
			RecordView.ChangeName(this, sender);
		}

	    private void HandleNameChange(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
	    {
		    RecordView.ChangeName(this, eventInfo);
	    }
	    
	    private void ResetAppearance()
	    {
		    RecordViewControls.ResetAppearance();
	    }
	}
}