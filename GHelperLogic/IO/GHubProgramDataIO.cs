using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GHelperLogic.Utility;
using NDepend.Path;
using Optional;
using Optional.Unsafe;
using SixLabors.ImageSharp;

namespace GHelperLogic.IO
{
	public static class GHubProgramDataIO
	{
		public static class DefaultApplicationImagesIO
		{
			public static void SavePosterImage(Image image, string imageFileName)
			{
				if (FindCurrentImageStorageDirectory().ValueOrDefault() is  { } imageStorageDirectoryPath)
				{
					IAbsoluteFilePath destinationImageFilePath = imageStorageDirectoryPath.GetChildFileWithName(imageFileName);
					
					Utilities.TakeOwnershipOf(file: destinationImageFilePath);

					using FileStream posterFileStream = new (path: destinationImageFilePath.ToString()!,
					                                         mode: FileMode.Create);
					image.SaveAsPng(posterFileStream);
				}
			}
			
			private static Option<IAbsoluteDirectoryPath> FindCurrentImageStorageDirectory()
			{
				try
				{
					DirectoryInfo startingDirectory = Properties.Configuration.GHubProgramDataDepotsDirectoryPath.DirectoryInfo;

					startingDirectory = GetMostRecentlyCreatedSubdirectory(parentDirectory: startingDirectory);

					IAbsoluteDirectoryPath? imageStorageDirectoryPath = PathHelpers.ToAbsoluteDirectoryPath(Path.Combine(startingDirectory.FullName, Properties.Resources.GHubProgramDataImagesStorageRelativePath));

					if (imageStorageDirectoryPath?.Exists is true)
					{
						return Option.Some(imageStorageDirectoryPath);
					}
					else
					{
						return Option.None<IAbsoluteDirectoryPath>();
					}
				}
				catch (Exception)
				{
					return Option.None<IAbsoluteDirectoryPath>();
				}
			}

			private static DirectoryInfo GetMostRecentlyCreatedSubdirectory(DirectoryInfo parentDirectory)
			{
				List<DirectoryInfo> subdirectories = parentDirectory.GetDirectories().ToList();
				
				subdirectories.Sort((DirectoryInfo firstDirectory, DirectoryInfo secondDirectory) =>
				{
					try
					{
						DateTime firstDirectoryCreationTime = Directory.GetCreationTimeUtc(firstDirectory.FullName);
						DateTime secondDirectoryCreationTime = Directory.GetCreationTimeUtc(secondDirectory.FullName);

						return firstDirectoryCreationTime.CompareTo(secondDirectoryCreationTime);
					}
					catch (SystemException)
					{
						return 0;
					}
				});

				return subdirectories.Last();
			}
		}
	}
}