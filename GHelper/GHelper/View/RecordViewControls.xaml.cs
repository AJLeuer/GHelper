using Windows.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		private static Color SaveButtonEnabledBackgroundColor { get; } = new Windows.UI.ViewManagement.UISettings().GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent);
        
	    public RecordViewControls()
        {
            InitializeComponent();
        }

        public void NotifyOfUserChange()
        {
	        SaveButton.Background = Application.Current.Resources["SystemAccentColorBrush"] as SolidColorBrush;
        }
    }
}