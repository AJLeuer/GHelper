using System.IO;
using System.Text;
using GHelperLogic.Model;
using GHelperLogic.Utility.JSONConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReaderWriter : GHubSettingsIO
	{
		public override GHubSettingsFile Read(Stream? settingsFileStream = null)
		{
			settingsFileStream ??= GHubSettingsStream;
			JObject parsedSettingsFile = parseSettingsFile(settingsFileStream);
			
			GHubSettingsFileObject = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter())!;
			GHubSettingsFileObject.AssociateProfilesToApplications();

			return GHubSettingsFileObject;
		}

		public override void Write(Stream? settingsFileStream = null, GHubSettingsFile? settingsFileObject = null)
		{
			settingsFileStream ??= GHubSettingsStream;
			settingsFileObject ??= GHubSettingsFileObject;

			using (StreamWriter settingsFileWriter = new (stream: settingsFileStream, encoding: new UTF8Encoding(), bufferSize: -1, leaveOpen: true))
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
			using TextReader reader = new StreamReader(stream: settingsFileStream, detectEncodingFromByteOrderMarks: true, bufferSize: -1, leaveOpen: true);
			using JsonReader settingsFileReader = new JsonTextReader(reader);
			JObject parsedSettingsFile = JObject.Load(settingsFileReader);
			return parsedSettingsFile;
		}

		protected override Stream InitializeGHubSettingsFileStream()
		{
			#if RELEASE || DEBUGRELEASE
				return new FileStream(Properties.Configuration.DefaultGHubSettingsFilePath.ToString()!,
														FileMode.Open,
														FileAccess.ReadWrite);

			#elif DEBUG
				return new FileStream(Properties.Configuration.DummyDebugGHubSettingsFilePath.ToString()!,
				                                        FileMode.Open,
				                                        FileAccess.ReadWrite);
			#endif
		}
	}
}