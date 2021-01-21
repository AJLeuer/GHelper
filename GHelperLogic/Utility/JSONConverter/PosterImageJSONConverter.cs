using System;
using System.IO;
using Newtonsoft.Json;
using SixLabors.ImageSharp;

namespace GHelperLogic.Utility.JSONConverter
{
	public class PosterImageJSONConverter : JsonConverter<Image>
	{
		public override bool CanWrite
		{
			get { return false; }
		}
		
		public override void WriteJson(JsonWriter writer, Image? value, JsonSerializer serializer) {}

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