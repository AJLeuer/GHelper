using Microsoft.UI.Xaml;
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
		private readonly Reference<MainWindow> window = new ();
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
            this.UnhandledException += HandleExceptions;
			gHubSettingsFileService = new GHubSettingsFileService(window);
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			LogManager.Log("Launching GHelper application.");
			window.Referent = new MainWindow { GHubSettingsFileService = gHubSettingsFileService};
			window.Referent.Activate();
            this.window.Referent.Closed += HandleImminentExit;
            
			gHubSettingsFileService.Start();

			if (window.Referent.Content is FrameworkElement frameworkElement)
            {
				frameworkElement.Loaded += FinishStartUp;
            }				
		}

		/// <summary>
		/// Performs any remaining launch tasks that require the main window to be loaded, and therefore couldn't be performed 
		/// by OnLaunched()
		/// </summary>
        private async void FinishStartUp(object sender, RoutedEventArgs routedEvent)
        {
            if (window.Referent is not null)
            {
                await window.Referent.DisplayGHubRunningDialogIfNeeded();
                await window.Referent.DisplayGHubSettingsFileNotFoundDialogIfNeeded();
            }
        }
        
        private void HandleImminentExit(object sender, WindowEventArgs windowEvent)
		{
            LogManager.Log("Shutting down GHelper application.");
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
