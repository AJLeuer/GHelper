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
			    DetermineNameViewStyle();
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

	    private void DetermineNameViewStyle()
	    {
		    Style editableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.StandardTextBox] as Style)!;
		    Style immutableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.ImmutableTextBox] as Style)!;

		    if ((Application.Application?.IsCustom != null) && (Application.Application?.IsCustom == true))
		    {
			    NameView.Style = editableTextBox;
		    }
		    else
		    {
			    NameView.Style = immutableTextBox;
		    }
	    }
    }
}