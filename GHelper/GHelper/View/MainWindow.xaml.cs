using System.Collections.ObjectModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<Context>? Contexts { get; set; }

		public MainWindow()
        {
            this.InitializeComponent();
        }
	}
}
