using System.Collections.ObjectModel;
using GHelper.Models;
using Microsoft.UI.Xaml;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		private ObservableCollection<Context> Contexts
		{
			get;
		}
	

		public MainWindow(ObservableCollection<Context> contexts)
        {
            this.InitializeComponent();
            this.Contexts = contexts;
        }
	}
}
