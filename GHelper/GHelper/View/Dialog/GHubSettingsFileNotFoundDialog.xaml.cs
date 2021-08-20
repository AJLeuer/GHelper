using System.Threading.Tasks;
using GHelper.Service;
using GHelperLogic.IO;


namespace GHelper.View.Dialog
{
    public partial class GHubSettingsFileNotFoundDialog : ConditionalDialog
    {
        public GHubSettingsFileService? GHubSettingsFileService { get ; set ; }

        public GHubSettingsFileNotFoundDialog(GHubSettingsFileService? gHubSettingsFileService)
        {
            this.InitializeComponent();
            this.GHubSettingsFileService = gHubSettingsFileService;
        }

        public override async Task DisplayIfNeeded()
        {
            GHubSettingsIO.State? settingsFileState = GHubSettingsFileService?.CheckSettingsFileAvailability();

            if (settingsFileState == GHubSettingsIO.State.Unavailable)
            {
                await this.EnqueueAndShowIfAsync();
            }
        }
    }
}
