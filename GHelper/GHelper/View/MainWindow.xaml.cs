using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using GHelper.Models;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GHelper.View
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		private ObservableCollection<Context> Contexts
		{
			get;
		} = new ObservableCollection<Context>();
	

		public MainWindow()
        {
            this.InitializeComponent();
        }
	}
}
