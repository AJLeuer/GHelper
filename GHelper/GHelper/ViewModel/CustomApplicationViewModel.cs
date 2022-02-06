using GHelper.Event;
using GHelper.Service;
using GHelperLogic.Model;
using NDepend.Path;
using Optional;
using Optional.Unsafe;
using Image = SixLabors.ImageSharp.Image;

namespace GHelper.ViewModel
{
	public class CustomApplicationViewModel : ApplicationViewModel
	{
		protected internal CustomApplicationViewModel(CustomApplication application)
			: base(application)
		{
		}
        
        public override void Delete()
        {
            InvokeDeleteEvent();
        }

		public override void SetNewCustomPosterImage(Image customPoster)
		{
			if (Application is not null)
			{
				Option<IFilePath> potentialSavedImagePath = GHubImageStorageService.GHubImageCacheService.SavePosterImage(customPoster);

				if (potentialSavedImagePath.HasValue && potentialSavedImagePath.ValueOrDefault() is  { } savedImagePath)
				{
					Application.PosterPath = savedImagePath;
					Application.LoadApplicationPosterImage(Application);
					RetrievePosterImage();
					OnPropertyChanged(nameof(Poster));
					OnPropertyChanged(nameof(PosterPath));
				}
			}
		}

		protected override void RestorePosterIfNeeded()
		{
			// Don't need to do anything because, unlike for standard applications, setting a new poster for a custom
			// application doesn't overwrite an existing file. RestoreInitialState() will just point our PosterPath
			// back to the old poster, which is all we need (though maybe we could clean up the new poster that the user
			// decided they didn't want after all, possibly a todo)
		}
	}
	
}