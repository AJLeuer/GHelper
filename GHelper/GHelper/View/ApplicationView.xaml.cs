using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public partial class ApplicationView : UserControl, RecordView 
    {
	    public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
		    nameof (ApplicationSelectorView.Application),
		    typeof (ApplicationViewModel),
		    typeof (ApplicationSelectorView),
		    new PropertyMetadata(null)
	    );
		
	    public ApplicationViewModel Application
	    {
		    get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
		    set
		    {
			    SetValue(ApplicationProperty, value);
			    ResetAppearance();
		    }
	    }

	    public GHubRecordViewModel GHubRecord
	    {
		    get { return Application; }
	    }

	    public ApplicationView()
        {
	        InitializeComponent();
        }
	    
	    public void RegisterForSaveNotification(Action saveFunction)
	    {
		    RecordViewControls.RegisterForSaveNotification(saveFunction);
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