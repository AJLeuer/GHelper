using System;
using GHelperLogic.Model;
using Newtonsoft.Json;

namespace GHelperLogic.Utility.JSONConverter
{
	public class ProfileJSONConverter : JsonConverter<Profile>
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, Profile? value, JsonSerializer serializer) { }
		
		public override Profile ReadJson(JsonReader reader, Type objectType, Profile? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			serializer.Converters.Clear();
			Profile profile = serializer.Deserialize<Profile>(reader)!;
			if (profile.Name == DefaultProfile.DefaultProfileDefaultName)
			{
				profile = new DefaultProfile(profile);
			}
			return profile;
		}

	}
}