using System.IO;
using System.Text;
using GHelperLogic.Model;
using GHelperLogic.Utility.JSONConverter;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Optional;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReaderWriter : GHubSettingsIO
	{
		private static readonly IFilePath GBHubSettingsFilePath =
			#if RELEASE || DEBUGRELEASE
                Properties.Configuration.DefaultGHubSettingsFilePath;
			#elif DEBUG
				Properties.Configuration.DummyDebugGHubSettingsFilePath;
			#endif
		
		public static State CheckFileAvailability()
		{
			if (File.Exists(GBHubSettingsFilePath.ToString()))
			{
				return State.Available;
			}
			else
			{
				return State.Unavailable;
			}
		}
		
		private Stream? gHubSettingsStream;

		private Stream GHubSettingsStream
		{
			get
			{
				if (gHubSettingsStream is null)
				{
					gHubSettingsStream = InitializeGHubSettingsFileStream();
				}
				return gHubSettingsStream!;
			}
		}
		
		public override State CheckSettingsAvailability(Stream? settingsStream = null)
		{
			try
			{
				settingsStream ??= GHubSettingsStream;
				if (settingsStream.CanRead)
				{
					return State.Available;
				}
			}
			catch (IOException) {}
			
			return State.Unavailable;
		}
		
		public override Option<GHubSettingsFile> Read(Stream? settingsFileStream = null)
		{
			settingsFileStream ??= GHubSettingsStream;
			JObject parsedSettingsFile = parseSettingsFile(settingsFileStream);
			
			GHubSettingsFileObject = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter())!;
			GHubSettingsFileObject.AssociateProfilesToApplications();

			return Option.Some(GHubSettingsFileObject);
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

		protected Stream InitializeGHubSettingsFileStream()
		{
			return new FileStream(GBHubSettingsFilePath.ToString()!,
			                      FileMode.Open,
			                      FileAccess.ReadWrite);
		}
	}
}