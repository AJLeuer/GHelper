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
		    set { SetValue(ApplicationProperty, value); }
	    }
	    
	    public GHubRecordViewModel GHubRecord
	    {
		    get { return Application; }
	    }

	    public ApplicationView()
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