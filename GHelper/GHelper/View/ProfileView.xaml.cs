using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

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
		
		public event PropertyChangedEventHandler? PropertyChanged;
		
		public ProfileView(Action saveFunction, Action<GHubRecordViewModel> deleteFunction)
        {
            InitializeComponent();
            RegisterForSaveNotification(saveFunction);
            RegisterForDeleteNotification(deleteFunction);
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
		    OnPropertyChanged(nameof(GHubRecord));
	    }

	    private void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
		{
			RecordView.ChangeName(this, sender);
		}

	    private void HandleNameChange(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
	    {
		    RecordView.ChangeName(this, eventInfo.Element);
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