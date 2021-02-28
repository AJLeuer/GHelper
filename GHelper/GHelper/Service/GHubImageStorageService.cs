using System;
using System.IO;
using GHelperLogic.IO;
using NDepend.Path;
using Optional;
using Optional.Unsafe;
using SixLabors.ImageSharp;

namespace GHelper.Service
{
	public static class GHubImageStorageService
	{
		public static class GHubImageCacheService
		{
			public static Option<IFilePath> SavePosterImage(Image poster)
			{
				string imageFileName = Guid.NewGuid().ToString("N");
				imageFileName += Properties.Resources.FileExtensionPNG;

				try
				{
					IFilePath destinationImageFilePath = GHelperLogic.Properties.Configuration.IconCacheDirectoryPath
						.GetChildFileWithName(imageFileName);
					using FileStream posterFileStream = new (path: destinationImageFilePath.ToString()!,
					                                         mode: FileMode.Create);
					poster.SaveAsPng(posterFileStream);
					return Option.Some(destinationImageFilePath);
				}
				catch (Exception)
				{
					return Option.None<IFilePath>();
				}
			}
		}
		
		
		
		public static class GHubProgramDataImageStorageService
		{
			public static Option<Uri> SavePosterImage(Image poster, string imageFileName)
			{
				Option<IFilePath> potentialImageFilePath = GHubProgramDataIO.DefaultApplicationImagesIO.SavePosterImage(image: poster, imageFileName: imageFileName);

				if (potentialImageFilePath.ValueOrDefault() is { } imageFilePath)
				{
					return Option.Some(DeterminePosterURLForSavedImageFile(imageFilePath));
				}
				else
				{
					return Option.None<Uri>();
				}
			}

			private static Uri DeterminePosterURLForSavedImageFile(IFilePath savedImagePath)
			{
				savedImagePath = PathHelpers.ToAbsoluteFilePath(savedImagePath.ToString()!);
				return new Uri(GHelperLogic.Properties.Resources.PosterURLStandardPrefix + savedImagePath.FileName);
			}
		}

	}
}