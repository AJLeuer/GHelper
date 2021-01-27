using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public interface RecordView
	{
		public GHubRecordViewModel? GHubRecord { get; }

		void RegisterForSaveNotification(Action saveFunction);
		
		protected void SendRecordChangedNotification();

		public static void ChangeName(RecordView recordView, object sender)
		{
			if (sender is TextBox textBox)
			{
				ChangeName(recordView, textBox);
			}
		}

		public static void ChangeName(RecordView recordView, KeyboardAcceleratorInvokedEventArgs eventInfo)
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
					recordView.SendRecordChangedNotification();
				}
			}
		}
	}

	public delegate void GHubRecordSavedEvent();
}