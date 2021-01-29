using System.IO;
using System.Reflection;
using GHelperLogic.Model;
using GHelperLogic.Utility.JSONConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GHelperLogic.Utility;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReaderWriter
	{
		private static readonly Stream            GHubSettingsFileStream;
		private static          GHubSettingsFile? GHubSettingsFileObject;
		
		static GHubSettingsFileReaderWriter()
		{
			#if DEBUG
				string settingFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"Properties\DummyGHUBSettings.json");
				GHubSettingsFileStream = new FileStream(settingFilePath,
														FileMode.Open,
														FileAccess.ReadWrite);

			#elif RELEASE || DEBUGRELEASE
				GHubSettingsFile = new FileStream(
						Properties.Configuration.DefaultFilePath.ToString()!,
						FileMode.Open,
						FileAccess.ReadWrite);

			#endif
		}

		public GHubSettingsFile Read(Stream? settingsFileStream = null)
		{
			settingsFileStream ??= GHubSettingsFileStream;
			JObject parsedSettingsFile = parseSettingsFile(settingsFileStream);
			
			GHubSettingsFileObject = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter(), new ProfileJSONConverter())!;
			GHubSettingsFileObject.AssociateProfilesToApplications();

			return GHubSettingsFileObject;
		}

		public void Write(Stream? settingsFileStream = null, GHubSettingsFile? settingsFileObject = null)
		{
			settingsFileStream ??= GHubSettingsFileStream;
			settingsFileObject ??= GHubSettingsFileObject;

			using (StreamWriter settingsFileWriter = new (settingsFileStream))
			{
				//discard the old contents of the file
				settingsFileStream.SetLength(0); 
				
				string reSerializedGHubSettingsFile = Serialize(settingsFileObject);
				
				settingsFileStream.Position = 0;
				settingsFileWriter.Write(reSerializedGHubSettingsFile);
				settingsFileWriter.Flush();
				settingsFileStream.SetLength(settingsFileStream.Position);
			}
		}

		public string Serialize(GHubSettingsFile? settingsFileObject = null)
		{
			settingsFileObject ??= GHubSettingsFileObject;
			string reSerializedGHubSettingsFile = JsonConvert.SerializeObject(settingsFileObject, Formatting.Indented);
			return reSerializedGHubSettingsFile;
		}

		private static JObject parseSettingsFile(Stream settingsFileStream)
		{
			using TextReader reader = new StreamReader(settingsFileStream);
			using JsonReader settingsFileReader = new JsonTextReader(reader);
			JObject parsedSettingsFile = JObject.Load(settingsFileReader);
			return parsedSettingsFile;
		}
	}
}