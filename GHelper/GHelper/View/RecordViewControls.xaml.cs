using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		public Collection<Action> GHubRecordSavedCallbacks = new();

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

        public void RegisterForSaveNotification(Action saveFunction)
        {
	        GHubRecordSavedCallbacks.Add(saveFunction);
        }

        private void SendRecordSavedNotification(object sender, RoutedEventArgs e)
        {
	        foreach (Action callBack in GHubRecordSavedCallbacks)
	        {
		        callBack?.Invoke();
	        }
        }
	}
}