using Microsoft.UI.Xaml;
using Windows.ApplicationModel;
using GHelper.Service;
using GHelper.View;
using GHelperLogic.Utility;

namespace GHelper
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public partial class App : Application
	{
		private readonly Reference<MainWindow> window = new Reference<MainWindow>();
		private readonly GHubSettingsFileService gHubSettingsFileService;
		
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			LogManager.Start();
			LogManager.Log("Initializing GHelper application.");
			this.InitializeComponent();
			this.Suspending += OnSuspending;
			this.UnhandledException += HandleExceptions;
			gHubSettingsFileService = new GHubSettingsFileService(window);
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override async void OnLaunched(LaunchActivatedEventArgs args)
		{
			LogManager.Log("Launching GHelper application.");
			window.Referent = new MainWindow { GHubSettingsFileService = gHubSettingsFileService};
			window.Referent.Activate();
			await window.Referent.DisplayGHubSettingsFileNotFoundDialogIfNeeded();
			await window.Referent.DisplayGHubRunningDialogIfNeeded();
			gHubSettingsFileService.Start();
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			// Save application state and stop any background activity
			LogManager.Log("Suspending GHelper application.");
			LogManager.Stop();
		}
		
		
		private void HandleExceptions(object sender, UnhandledExceptionEventArgs exceptionInfo)
		{
			exceptionInfo.Handled = true;
			LogManager.Log($"Exception caught in {this.GetType()}");
			LogManager.Log(exceptionInfo.Exception);
		}
	}
}
