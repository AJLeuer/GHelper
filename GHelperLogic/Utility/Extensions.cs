using System.IO;
using NDepend.Path;

namespace GHelperLogic.Utility
{
	public static class Extensions
	{
		public static (Stream, Stream) Duplicate(this Stream stream)
		{
			var firstCopy = new MemoryStream();
			var secondCopy = new MemoryStream();
			
			stream.CopyTo(firstCopy);
			stream.Position = 0;
			stream.CopyTo(secondCopy);

			firstCopy.Position = 0;
			secondCopy.Position = 0;
			
			return (firstCopy, secondCopy);
		}

		public static void CreateContainingDirectoryIfNeeded(this IFilePath filePath)
		{
			var file = new FileInfo(filePath.ToString()!);
			file.Directory?.Create();
		}
	}
}