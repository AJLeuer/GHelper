using System;
using System.Threading.Tasks;
using GHelper.Service;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Dialog
{
    public partial class GHubRunningDialog : ConditionalDialog
    {
        public GHubRunningDialog()
        {
            this.InitializeComponent();
        }

        public override async Task DisplayIfNeeded()
        {
            while (GHubProcessService.GHubProcessState() == ProcessState.Running)
            {
                await this.EnqueueAndShowIfAsync();
            }
        }
    }
}
