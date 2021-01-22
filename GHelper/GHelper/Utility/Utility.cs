using System;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;
using SixLabors.ImageSharp;


namespace GHelper.Utility
{
	public static class Utility
	{
		public static BitmapImage? ConvertToWindowsBitmapImage(this Image image)
		{
			BitmapImage? bitmapImage;
			try
			{
				using var memoryStream = new MemoryStream();
				image.SaveAsBmp(memoryStream);
				memoryStream.Seek(0, SeekOrigin.Begin);
				bitmapImage = new BitmapImage();
				bitmapImage.SetSource(memoryStream.AsRandomAccessStream());
			}
			catch (Exception)
			{
				bitmapImage = null;
			}

			return bitmapImage;
		}
	}
}