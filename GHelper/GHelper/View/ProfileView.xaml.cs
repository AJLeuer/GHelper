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
				ChainGHubRecordViewModelEventsToControls();
			}
		}

		public GHubRecordViewModel GHubRecordViewModel
		{
			get { return Profile; }
		} 
		
		public event PropertyChangedEventHandler? PropertyChanged;
		
		public ProfileView( )
        {
            InitializeComponent();
            RecordViewControls.UserClickedSaveButton += () => { GHubRecordViewModel.FireSaveEvent(); };
	        RecordViewControls.UserClickedDeleteButton += () => { GHubRecordViewModel.FireDeletedEvent(); };
        }

		void RecordView.SendRecordChangedNotification()
	    {
		    OnPropertyChanged(nameof(GHubRecordViewModel));
	    }

	    private void HandleNameEdit(object sender, KeyRoutedEventArgs routedEventInfo)
		{
			RecordView.ChangeName(this, sender);
			routedEventInfo.Handled = true;
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

	    private void ShowEditableName(UIElement uiElement, GettingFocusEventArgs eventInfo)
	    {
		    NameDisplay.Text = Profile.Name;
	    }

	    private void ShowDisplayName(UIElement uiElement, LosingFocusEventArgs eventInfo)
	    {
		    NameDisplay.Text = Profile.DisplayName;
	    }
	    
	    public void ChainGHubRecordViewModelEventsToControls()
	    {
		    Profile.PropertyChanged += RecordViewControls.SwitchToChangedAppearance;
		    Profile.UserSaved += RecordViewControls.ResetAppearance;
	    }
	}
}