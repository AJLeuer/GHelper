using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public partial class ApplicationView : UserControl 
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
	    
        public ApplicationView()
        {
	        InitializeComponent();
        }
    }
}