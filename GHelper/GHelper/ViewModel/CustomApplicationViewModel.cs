using System.IO;
using GHelper.Annotations;
using GHelper.Service;
using GHelperLogic.Model;
using NDepend.Path;
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
				try
				{
					IFilePath imageSavedPath = GHubImageCacheService.SavePosterImage(customPoster);
					Application.PosterPath = imageSavedPath;
					retrievePosterImage();
					OnPropertyChanged(nameof(Poster));
					OnPropertyChanged(nameof(PosterPath));
				}
				catch (IOException) {}
			}
		}
	}
}