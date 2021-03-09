using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View
{
	public partial class RecordViewControls : UserControl
	{
		public readonly Collection<Action> GHubRecordSavedCallbacks   = new();
		public readonly Collection<Action> GHubRecordDeletedCallbacks = new();

		public Button DeleteButton
		{
			get { return deleteButton; }
		}
		
		public event Action? UserClickedSaveButton;
		public event Action? UserClickedDeleteButton;

		public RecordViewControls()
        {
            InitializeComponent();
        }

		private void Save(object sender, RoutedEventArgs _)
        {
	        UserClickedSaveButton?.Invoke();
        }

        private void Delete(object sender, RoutedEventArgs eventInfo)
        {
	        UserClickedDeleteButton?.Invoke();
        }

        public void SwitchToChangedAppearance(object? sender, PropertyChangedEventArgs eventInfo)
        {
	        SaveButton.Background = Application.Current.Resources[Properties.Resources.SystemAccentColorBrush] as SolidColorBrush;
        }
        
        public void ResetAppearance()
        {
	        var defaultButton = new Button { Style = Application.Current.Resources[Properties.Resources.StandardButtonStyle] as Style };

	        SaveButton.Background = defaultButton.Background;
	        SaveButton.Style = defaultButton.Style;
        }
        
	}
}