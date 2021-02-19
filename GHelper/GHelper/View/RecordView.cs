using System;
using GHelper.ViewModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public interface RecordView
	{
		public static RecordView CreateViewForViewModel(GHubRecordViewModel gHubRecordViewModel, Action saveFunction, Action<GHubRecordViewModel> deleteFunction)
		{
			switch (gHubRecordViewModel)
			{
				case ProfileViewModel profileViewModel:
					return new ProfileView(saveFunction, deleteFunction) { Profile = profileViewModel };
				
				case ApplicationViewModel applicationViewModel:
					switch (applicationViewModel.Application)
					{
						case DesktopApplication:
							return new DesktopApplicationView(saveFunction, deleteFunction) { Application = applicationViewModel };
						default:
							return new ApplicationView(saveFunction, deleteFunction) { Application = applicationViewModel };
					}
					
				default:
					throw new ArgumentException("Unhandled subclass of GHubRecordViewModel in CreateViewForViewModel()");
			}
		}
		
		public GHubRecordViewModel? GHubRecord { get; }

		void RegisterForSaveNotification(Action saveFunction);
		void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction);
		
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
}