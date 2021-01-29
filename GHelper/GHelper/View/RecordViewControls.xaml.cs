using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		public readonly Collection<Action> GHubRecordSavedCallbacks = new();

		public RecordViewControls()
        {
            InitializeComponent();
        }

        public void NotifyOfUserChange()
        {
	        SaveButton.Background = Application.Current.Resources[Properties.Resources.SystemAccentColorBrush] as SolidColorBrush;
        }

        public void ResetAppearance()
        {
	        var defaultButton = new Button { Style = Application.Current.Resources[Properties.Resources.SaveButtonStyle] as Style };

	        SaveButton.Background = defaultButton.Background;
	        SaveButton.Style = defaultButton.Style;
        }

        public void RegisterForSaveNotification(Action saveFunction)
        {
	        GHubRecordSavedCallbacks.Add(saveFunction);
        }

        private void Save(object sender, RoutedEventArgs _)
        {
	        ResetAppearance();
	        SendRecordSavedNotification();
        }

        private void SendRecordSavedNotification()
        {
	        foreach (Action callBack in GHubRecordSavedCallbacks)
	        {
		        callBack?.Invoke();
	        }
        }
	}
}