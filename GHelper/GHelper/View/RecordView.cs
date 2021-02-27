using System;
using GHelper.Event;
using GHelper.ViewModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public interface RecordView
	{
		public static RecordView CreateViewForViewModel(GHubRecordViewModel gHubRecordViewModel)
		{
			switch (gHubRecordViewModel)
			{
				case ProfileViewModel profileViewModel:
					return new ProfileView { Profile = profileViewModel };
				
				case ApplicationViewModel applicationViewModel:
					switch (applicationViewModel.Application)
					{
						case DesktopApplication:
							return new DesktopApplicationView { Application = applicationViewModel };
						default:
							return new ApplicationView { Application = applicationViewModel };
					}
					
				default:
					throw new ArgumentException("Unhandled subclass of GHubRecordViewModel in CreateViewForViewModel()");
			}
		}
		
		public GHubRecordViewModel?          GHubRecord { get; }
		public event UserSavedEvent?         UserSaved;
		public event UserDeletedRecordEvent? UserDeletedRecord;

		protected void SendRecordChangedNotification();

		public static void ChangeName(RecordView recordView, object sender)
		{
			if (sender is TextBox textBox)
			{
				ChangeName(recordView, textBox);
			}
		}

		protected static void ChangeName(RecordView recordView, TextBox textBox)
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