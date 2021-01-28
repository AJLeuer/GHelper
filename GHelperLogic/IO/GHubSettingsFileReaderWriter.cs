using System.Collections.Generic;
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

		public (ICollection<Application>? applications, ICollection<Profile>? profiles) GetApplicationsData(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			GHubSettingsFile gHubSettingsFile = DeserializeData(settingsFile);
			
			ICollection<Application>? applications = gHubSettingsFile.Applications?.Applications;
			ICollection<Profile>? profiles = gHubSettingsFile.Profiles?.Profiles;
			
			return (applications, profiles);
		}

		public GHubSettingsFile DeserializeData(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			GHubSettingsFile gHubSettingsFile = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter(), new ProfileJSONConverter())!;

			gHubSettingsFile.AssociateProfilesToApplications();

			return gHubSettingsFile;
		}
		
		private static JObject readSettingsFile(Stream settingsFile)
		{
			using TextReader reader = new StreamReader(settingsFile);
			using JsonReader settingsFileReader = new JsonTextReader(reader);
			JObject parsedSettingsFile = JObject.Load(settingsFileReader);
			return parsedSettingsFile;
		}
	}
}