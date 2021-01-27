using Windows.UI;
using GHelper.ViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		private static Color SaveButtonEnabledBackgroundColor { get; } = new Windows.UI.ViewManagement.UISettings().GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent);
		private static Color SaveButtonInactiveBackgroundColor { get; } = new Windows.UI.ViewManagement.UISettings().GetColorValue(Windows.UI.ViewManagement.UIColorType.Background);
        
	    public RecordViewControls()
        {
            InitializeComponent();
        }

        public void NotifyOfUserChange()
        {
	        SaveButton.Background = Application.Current.Resources["SystemAccentColorBrush"] as SolidColorBrush;
        }

        public void ResetAppearance()
        {
	        var defaultButton = new Button { Style = Application.Current.Resources["SaveButtonStyle"] as Style };

	        SaveButton.Background = defaultButton.Background;
	        SaveButton.Style = defaultButton.Style;
        }
	}
}