using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public interface RecordView
	{
		public GHubRecordViewModel? GHubRecord { get; }
		
		protected void SaveRecord();
		protected void HandleUserEdit();

		public static void ChangeName(RecordView recordView, object sender, RoutedEventArgs routedEventInfo)
		{
			if (sender is TextBox textBox)
			{
				ChangeName(recordView, textBox);
			}
		}

		public static void ChangeName(RecordView recordView, KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
		{
			if (eventInfo.Element is TextBox textBox)
			{
				ChangeName(recordView, textBox);
			}
		}

		public static void ChangeName(RecordView recordView, TextBox textBox)
		{
			if (textBox.Text != recordView.GHubRecord?.DisplayName)
			{
				if (recordView.GHubRecord != null)
				{
					recordView.GHubRecord.Name = textBox.Text;
					recordView.HandleUserEdit();
				}
			}
		}
	}
}