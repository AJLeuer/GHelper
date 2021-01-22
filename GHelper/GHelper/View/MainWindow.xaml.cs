using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<Application>? Applications { get; set; }
		public ApplicationView ApplicationView { get; }
		public ProfileView ProfileView { get; }
		public event EventHandler? GHubRecordSelected;

		public MainWindow()
		{
			this.InitializeComponent();
		}
	}
}