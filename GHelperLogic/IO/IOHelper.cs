using System;
using System.IO;
using System.Net;
using GHelperLogic.Utility.Wrappers;
using SixLabors.ImageSharp;

namespace GHelperLogic.IO
{
	public static class IOHelper
	{
		public static WebClientInterface Client { get; set; } = new WebClient();

		public static Image? LoadFromURL(Uri imageFileURL)
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
	}
}