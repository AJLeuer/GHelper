using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.Event;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public partial class ProfileView : UserControl, RecordView
	{
		public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
			nameof (Profile),
			typeof (ProfileViewModel),
			typeof (ProfileView),
			new PropertyMetadata(null)
		);
	    
		public ProfileViewModel Profile
		{
			get { return (ProfileViewModel) GetValue(ProfileProperty); }
			set
			{
				SetValue(ProfileProperty, value);
				ResetAppearance();
				Profile.PropertyChanged += RecordViewControls.NotifyOfUserChange;
			}
		}

		public GHubRecordViewModel GHubRecord
		{
			get { return Profile; }
		}

		public event UserSavedEvent?              UserSaved;
		public event UserDeletedRecordEvent?      UserDeletedRecord;
		public event PropertyChangedEventHandler? PropertyChanged;
		
		public ProfileView( )
        {
            InitializeComponent();
	        RecordViewControls.UserClickedSaveButton += () => { UserSaved?.Invoke(); };
	        RecordViewControls.UserClickedDeleteButton += () => { UserDeletedRecord?.Invoke(GHubRecord); };
        }

		void RecordView.SendRecordChangedNotification()
	    {
		    OnPropertyChanged(nameof(GHubRecord));
	    }

	    private void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
		{
			RecordView.ChangeName(this, sender);
		}

	    private void ResetAppearance()
	    {
		    RecordViewControls.ResetAppearance();
	    }
	    
	    [NotifyPropertyChangedInvocator]
	    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
	}
}