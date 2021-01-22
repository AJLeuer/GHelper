using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<Application>? Applications { get; set; }

		public MainWindow()
		{
			this.InitializeComponent();
		}
	}
}