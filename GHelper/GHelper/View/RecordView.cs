using System;
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
                            return new StandardApplicationView { Application = applicationViewModel };
                    }

                default:
                    throw new ArgumentException("Unhandled subclass of GHubRecordViewModel in CreateViewForViewModel()");
            }
        }

        public    GHubRecordViewModel? GHubRecordViewModel { get; }
        protected void                 SendRecordChangedNotification();

        public static void ChangeName(RecordView recordView, object sender)
        {
            if (sender is TextBox textBox)
            {
                ChangeName(recordView, textBox);
            }
        }

        protected static void ChangeName(RecordView recordView, TextBox textBox)
        {
            if (textBox.Text != recordView.GHubRecordViewModel?.DisplayName)
            {
                if (recordView.GHubRecordViewModel != null)
                {
                    recordView.GHubRecordViewModel.Name = textBox.Text;
                    recordView.SendRecordChangedNotification();
                }
            }
        }
    }
}