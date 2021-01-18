using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace GHelper.Utility
{
	public static class Utility
	{
		public static async Task<Image> CreateSingleColorImage(Color color, Size size)
		{
			var bitmap = new WriteableBitmap(size.Width, size.Height);
			var pixels = new byte[size.Width * size.Height * 4];
			
			for (uint i = 0; i < pixels.Length; i += 4)
			{
				//BGRA format
				pixels[i] = color.B;
				pixels[i + 1] = color.G;
				pixels[i + 2] = color.R;
				pixels[i + 3] = color.A;
			}

			await using (Stream stream = bitmap.PixelBuffer.AsStream())
			{
				//write to bitmap
				await stream.WriteAsync(pixels.AsMemory(0, pixels.Length));
			}
			
			return new Image { Source = bitmap };
		}
	}
}