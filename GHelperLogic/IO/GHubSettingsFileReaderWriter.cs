using System;
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
	public class GHubSettingsFileReaderWriter : GHubSettingsIO, IDisposable
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

		private Stream GHubSettingsFileStream { get;}

		public GHubSettingsFileReaderWriter()
		{
			GHubSettingsFileStream = InitializeDefaultGHubSettingsFileStream();
		}
		
		public GHubSettingsFileReaderWriter(Stream gHubSettingsFileStream)
		{
			this.GHubSettingsFileStream = gHubSettingsFileStream;
		}

		~GHubSettingsFileReaderWriter()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			GHubSettingsFileStream.Dispose();
		}
		
		public override State CheckSettingsAvailability()
		{
			try
			{
				if ((GHubSettingsFileStream.CanRead) && (GHubSettingsFileStream.CanWrite))
				{
					return State.Available;
				}
			}
			catch (IOException) {}
			
			return State.Unavailable;
		}
		
		public override Option<GHubSettingsFile> Read()
		{
			JObject parsedSettingsFile = parseSettingsFile(GHubSettingsFileStream);
			
			GHubSettingsFileObject = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter())!;
			GHubSettingsFileObject.AssociateProfilesToApplications();

			return Option.Some(GHubSettingsFileObject);
		}

		public override void Write(GHubSettingsFile? settingsFileObject = null)
		{
			settingsFileObject ??= GHubSettingsFileObject;

			using (StreamWriter settingsFileWriter = new (stream: GHubSettingsFileStream, encoding: new UTF8Encoding(), bufferSize: -1, leaveOpen: true))
			{
				//discard the old contents of the file
				GHubSettingsFileStream.SetLength(0); 
				
				string reSerializedGHubSettingsFile = Serialize(settingsFileObject);
				
				GHubSettingsFileStream.Position = 0;
				settingsFileWriter.Write(reSerializedGHubSettingsFile);
				settingsFileWriter.Flush();
				GHubSettingsFileStream.SetLength(GHubSettingsFileStream.Position);
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

		protected Stream InitializeDefaultGHubSettingsFileStream()
		{
			return new FileStream(GBHubSettingsFilePath.ToString()!,
			                      FileMode.Open,
			                      FileAccess.ReadWrite);
		}
	}
}