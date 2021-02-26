using System.IO;
using System.Text;
using GHelperLogic.Model;
using GHelperLogic.Utility.JSONConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReaderWriter
	{
		private Stream? gHubSettingsFileStream;
		public  Stream GHubSettingsFileStream
		{
			get
			{
				if (gHubSettingsFileStream == null)
				{
					initializeGHubSettingsFileStream();
				}
				return gHubSettingsFileStream!;
			}
		}

		private GHubSettingsFile? GHubSettingsFileObject;

		public State CheckSettingsFileAvailability(Stream? settingsFileStream = null)
		{
			try
			{
				settingsFileStream ??= GHubSettingsFileStream;
				if (settingsFileStream.CanRead)
				{
					return State.Available;
				}
			}
			catch (IOException) {}
			
			return State.Unavailable;
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

		private void initializeGHubSettingsFileStream()
		{
			#if DEBUG
				gHubSettingsFileStream = new FileStream(Properties.Configuration.DummyDebugGHubSettingsFilePath.ToString()!,
				                                        FileMode.Open,
				                                        FileAccess.ReadWrite);

			#elif RELEASE || DEBUGRELEASE
				gHubSettingsFileStream = new FileStream(Properties.Configuration.DefaultGHubSettingsFilePath.ToString()!,
														FileMode.Open,
														FileAccess.ReadWrite);

			#endif
		}

		public enum State
		{
			Available,
			Unavailable
		}
	}
}