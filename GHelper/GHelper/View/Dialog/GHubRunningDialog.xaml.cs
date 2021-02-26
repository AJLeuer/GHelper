using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using GHelper.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Dialog
{
    public partial class GHubRunningDialog : ContentDialog
    {
        public GHubRunningDialog()
        {
            this.InitializeComponent();
        }

        public async Task DisplayIfNeeded()
        {
            while (GHubProcessService.GHubProcessState() == ProcessState.Running)
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
