using GHelper.Annotations;
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
		public CustomApplicationViewModel([NotNull] CustomApplication application) 
			: base(application)
		{
		}

		public override void SetNewCustomPosterImage(Image customPoster)
		{
			if (Application is not null)
			{

				Option<IFilePath> potentialSavedImagePath = GHubImageStorageService.GHubImageCacheService.SavePosterImage(customPoster);

				if (potentialSavedImagePath.HasValue && potentialSavedImagePath.ValueOrDefault() is  { } savedImagePath)
				{
					Application.PosterPath = savedImagePath;
					retrievePosterImage();
					OnPropertyChanged(nameof(Poster));
					OnPropertyChanged(nameof(PosterPath));
				}
			}
		}
	}
}