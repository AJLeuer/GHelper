using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		public readonly Collection<Action> GHubRecordSavedCallbacks   = new();
		public readonly Collection<Action> GHubRecordDeletedCallbacks = new();

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

        public void RegisterForDeleteNotification(Action deleteFunction)
        {
	        GHubRecordDeletedCallbacks.Add(deleteFunction);
        }

        private void Save(object sender, RoutedEventArgs _)
        {
	        ResetAppearance();
	        SendRecordSavedNotifications();
        }

        private void Delete(object sender, RoutedEventArgs eventInfo)
        {
	        SendRecordDeletedNotifications();
        }

        private void SendRecordSavedNotifications()
        {
	        foreach (Action callBack in GHubRecordSavedCallbacks)
	        {
		        callBack.Invoke();
	        }
        }

        private void SendRecordDeletedNotifications()
        {
	        foreach (Action callBack in GHubRecordDeletedCallbacks)
	        {
		        callBack.Invoke();
	        }
        }
	}
}