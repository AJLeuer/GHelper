using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using Microsoft.UI.Xaml;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window, INotifyPropertyChanged
	{
		public ObservableCollection<Application>? Applications { get; set; }
		
		private FrameworkElement? contentControlView;
		public FrameworkElement? ContentControlView
		{
			get { return contentControlView; }
			set
			{
				contentControlView = value;
				OnPropertyChanged(nameof(ContentControlView));
			}
		}

		public MainWindow()
		{
			this.InitializeComponent();
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}