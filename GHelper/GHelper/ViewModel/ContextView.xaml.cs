using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.ViewModel
{
    public partial class ContextView : StackPanel
    {
	    public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
		    nameof (ContextView.Context),
		    typeof (Context),
		    typeof (ContextView),
		    new PropertyMetadata(null)
	    );
	    
	    public Context Context
	    {
		    get { return (Context) GetValue(ContextProperty); }
		    set { SetValue(ContextProperty, value); }
	    }
	    
        public ContextView()
        {
            this.InitializeComponent();
        }
    }
}
