using System;
using System.Collections.ObjectModel;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		public ObservableCollection<Application>? Applications { get; set; }

		public GHubRecord? CurrentDisplayedItem { get; private set; } = null;

		private ApplicationView ApplicationView { get; set; } = new();
		private ProfileView ProfileView { get; set; } = new();

		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void ApplicationSelected(object? sender, EventArgs e)
		{
			GHubDataDisplay.Content = ApplicationView;
		}

		private void ProfileSelected(object? sender, EventArgs e)
		{
			GHubDataDisplay.Content = ProfileView;
		}
	}
}