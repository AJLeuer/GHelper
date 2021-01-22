using System;
using System.IO;
using System.Net;
using GHelperLogic.Utility.Wrappers;
using SixLabors.ImageSharp;

namespace GHelperLogic.Utility
{
	public static class IOHelper
	{
		public static WebClientInterface Client { get; set; } = new WebClient();

		public static Image? LoadFromURL(Uri imageFileURL)
		{
			Image? image = null;

			try
			{
				using (Stream? imageFile = Client.OpenRead(imageFileURL))
				{
					image = Image.Load(imageFile);
				}
			}
			catch (SystemException exception) { }

			return image;
		}
	}
}