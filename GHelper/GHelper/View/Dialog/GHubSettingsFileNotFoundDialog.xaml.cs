using System;
using System.Threading.Tasks;
using GHelper.Service;
using GHelperLogic.IO;
using GHelperLogic.Properties;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Dialog
{
    public partial class GHubSettingsFileNotFoundDialog : ContentDialog
    {
        public GHubSettingsFileService? GHubSettingsFileService { get ; set ; }

        public GHubSettingsFileNotFoundDialog(GHubSettingsFileService? gHubSettingsFileService)
        {
            this.InitializeComponent();
            this.GHubSettingsFileService = gHubSettingsFileService;
        }

        public async Task DisplayIfNeeded()
        {
            GHubSettingsFileReaderWriter.State? settingsFileState = GHubSettingsFileService?.CheckSettingsFileAvailability();

            if (settingsFileState == GHubSettingsFileReaderWriter.State.Unavailable)
            {
                await ShowAsync().AsTask();
            }
        }

        private void QuitApp(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }
    }
}
