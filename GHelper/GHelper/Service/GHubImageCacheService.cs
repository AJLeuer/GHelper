using System;
using System.IO;
using NDepend.Path;
using SixLabors.ImageSharp;

namespace GHelper.Service
{
	public static class GHubImageCacheService
	{
		public static IFilePath SavePosterImage(Image poster)
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
				return destinationImageFilePath;
			}
			catch (Exception)
			{
				throw new IOException();
			}
		}
	}
}