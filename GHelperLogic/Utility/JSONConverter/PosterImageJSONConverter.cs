using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;

namespace GHelperLogic.Utility.JSONConverter
{
	public class PosterImageJSONConverter : JsonConverter<Image>
	{
		public override void WriteJson(JsonWriter writer, Image? value, JsonSerializer serializer)
		{
			using var imageStream = new MemoryStream();
			value.Save(imageStream, PngFormat.Instance);
			
			imageStream.Position = 0;
			byte[] imageBytes = imageStream.ToArray();
			
			JToken imageBase64JSON = Convert.ToBase64String(imageBytes);
			imageBase64JSON.WriteTo(writer);
		}

		public override Image ReadJson(JsonReader reader, Type objectType, Image? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			Image posterImage = default!;
			
			if (reader.Value is {} imageJSON)
			{
				string base64EncodedImage = imageJSON.ToString()!;
				Stream decodedImageStream = new MemoryStream(Convert.FromBase64String(base64EncodedImage));
				posterImage = Image.Load(decodedImageStream);
			}

			return posterImage;
		}
	}
}