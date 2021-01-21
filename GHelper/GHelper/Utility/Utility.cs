using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;
using SixLabors.ImageSharp;


namespace GHelper.Utility
{
	public static class Utility
	{
		public static BitmapImage ConvertToWindowsBitmapImage(this Image image)
		{
			using var memoryStream = new MemoryStream();
			image.SaveAsBmp(memoryStream);
			memoryStream.Seek(0, SeekOrigin.Begin);
			var bitmapImage = new BitmapImage();
			bitmapImage.SetSource(memoryStream.AsRandomAccessStream());
			return bitmapImage;
		}
	}
}