using System.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility.JSONConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReaderWriter
	{
		private static readonly Stream GHubSettingsFile;
		
		static GHubSettingsFileReaderWriter()
		{
			#if DEBUG 
				GHubSettingsFile = new MemoryStream(Properties.Resources.DummyGHUBSettings, true);
			
			#elif RELEASE || DEBUGRELEASE
				GHubSettingsFile = new FileStream(
						Properties.Configuration.DefaultFilePath.ToString()!,
						FileMode.Open,
						FileAccess.ReadWrite);

			#endif
		}

		public GHubSettingsFile Read(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			JObject parsedSettingsFile = parseSettingsFile(settingsFile);
			GHubSettingsFile gHubSettingsFile = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter(), new ProfileJSONConverter())!;

			gHubSettingsFile.AssociateProfilesToApplications();

			return gHubSettingsFile;
		}

		private static JObject parseSettingsFile(Stream settingsFile)
		{
			using TextReader reader = new StreamReader(settingsFile);
			using JsonReader settingsFileReader = new JsonTextReader(reader);
			JObject parsedSettingsFile = JObject.Load(settingsFileReader);
			return parsedSettingsFile;
		}
	}
}