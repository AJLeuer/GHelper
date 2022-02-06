using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public abstract class ApplicationView : UserControl, RecordView, INotifyPropertyChanged
	{
		public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
		    nameof (Application),
		    typeof (ApplicationViewModel),
		    typeof (ApplicationView),
		    new PropertyMetadata(null));
        
		public ApplicationViewModel Application
		{
			get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
			set
			{
				SetValue(ApplicationProperty, value);
            }
		}
        
		public GHubRecordViewModel GHubRecordViewModel
		{
			get { return Application; }
		}

		public event PropertyChangedEventHandler? PropertyChanged;

        void RecordView.SendRecordChangedNotification()
		{
			OnPropertyChanged(nameof(GHubRecordViewModel));
		}

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}