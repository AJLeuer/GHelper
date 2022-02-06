using GHelperLogic.Model;
using SixLabors.ImageSharp;

namespace GHelper.ViewModel
{
    public class DesktopApplicationViewModel : ApplicationViewModel
    {
        protected internal DesktopApplicationViewModel(DesktopApplication application) : base(application)
        {
        }

        public override void SetNewCustomPosterImage(Image customPoster)
        {
            //Do nothing, we don't allow setting custom posters
        }

        protected override void RestorePosterIfNeeded()
        {
            //Do nothing, we don't allow setting custom posters anyway so there's no way our poster could
            //have been changed in the first place
        }
    }
}