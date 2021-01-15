using System.IO;

namespace GHelperLogic.Utility
{
	public static class StreamExtensions
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
	}
}