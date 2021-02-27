using System;
using System.IO;
using System.Net;
using GHelperLogic.Utility.Wrappers;
using NDepend.Path;
using SixLabors.ImageSharp;

namespace GHelperLogic.IO
{
	public static class ImageIOHelper
	{
		public static WebClientInterface Client { get; set; } = new WebClient();

		public static Image? LoadFromHTTPURL(Uri imageFileURL)
		{
			Image? image = default;

			try
			{
				using (Stream? imageFile = Client.OpenRead(imageFileURL))
				{
					image = Image.Load(imageFile);
				}
			}
			catch (SystemException) {}

			return image;
		}
		
		public static Image? LoadFromFilePath(IPath imageFilePath)
		{
			Image? image = default;

			try
			{
				using (Stream imageFile = new FileStream(imageFilePath.ToString()!, FileMode.Open))
				{
					image = Image.Load(imageFile);
				}
			}
			catch (SystemException) {}

			return image;
		}
		
		public static Image? LoadFromStream(Stream imageFileStream)
		{
			Image? image = default;

			try
			{
				image = Image.Load(imageFileStream);
			}
			catch (SystemException) {}

			return image;
		}
	}
}