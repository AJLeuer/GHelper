using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			public static Option<IFilePath> SavePosterImage(Image image, string imageFileName)
			{
				imageFileName += Properties.Resources.FileExtensionPNG;
				
				if (FindCurrentImageStorageDirectory().ValueOrDefault() is  { } imageStorageDirectory)
				{
					try
					{
						IAbsoluteDirectoryPath imageStorageDirectoryPath =
							PathHelpers.ToAbsoluteDirectoryPath(imageStorageDirectory.FullName);

						IFilePath destinationImageFilePath =
							imageStorageDirectoryPath.GetChildFileWithName(imageFileName);

						destinationImageFilePath = PathHelpers.ToAbsoluteFilePath(Utility.Utilities.AddIndexToFileNameIfNeeded(destinationImageFilePath.ToString()!));

						using FileStream posterFileStream = new (path: destinationImageFilePath.ToString()!,
						                                         mode: FileMode.Create);
						image.SaveAsPng(posterFileStream);

						return Option.Some(destinationImageFilePath);
					}
					catch (Exception)
					{
						return Option.None<IFilePath>();
					}
				}
				else
				{
					return Option.None<IFilePath>();
				}
			}
			
			private static Option<DirectoryInfo> FindCurrentImageStorageDirectory()
			{
				try
				{
					DirectoryInfo startingDirectory = new (Properties.Configuration.GHubProgramDataDepotsDirectoryPath.ToString()!);

					startingDirectory = GetMostRecentlyCreatedSubdirectory(parentDirectory: startingDirectory);

					IAbsoluteDirectoryPath? imageStorageDirectoryPath =
						PathHelpers.ToAbsoluteDirectoryPath(Path.Combine(startingDirectory.FullName,
						                                                 Properties.Resources.GHubProgramDataImagesStorageRelativePath));

					if (imageStorageDirectoryPath?.DirectoryInfo != null)
					{
						Option<DirectoryInfo> imageStorageDirectory = Option.Some(imageStorageDirectoryPath.DirectoryInfo);
						return imageStorageDirectory;
					}
					else
					{
						return Option.None<DirectoryInfo>();
					}
				}
				catch (Exception)
				{
					return Option.None<DirectoryInfo>();
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