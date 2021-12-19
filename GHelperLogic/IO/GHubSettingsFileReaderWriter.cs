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
		private static readonly IFilePath GHubSettingsFilePath =
			#if RELEASE || DEBUGRELEASE
                Properties.Configuration.DefaultGHubSettingsFilePath;
			#elif DEBUG
				Properties.Configuration.DummyDebugGHubSettingsFilePath;
			#endif
		
		public static State CheckFileAvailability()
		{
			if (File.Exists(GHubSettingsFilePath.ToString()))
			{
				return State.Available;
			}
			else
			{
				return State.Unavailable;
			}
		}

		protected internal Stream GHubSettingsStream { get; private set; }

		public GHubSettingsFileReaderWriter()
		{
			GHubSettingsStream = InitializeDefaultGHubSettingsFileStream();
		}
		
		public GHubSettingsFileReaderWriter(Stream gHubSettingsStream)
		{
			this.GHubSettingsStream = gHubSettingsStream;
		}

		~GHubSettingsFileReaderWriter()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			GHubSettingsStream.Dispose();
		}
		
		public override State CheckSettingsAvailability()
		{
			try
			{
				if ((GHubSettingsStream.CanRead) && (GHubSettingsStream.CanWrite))
				{
					return State.Available;
				}
			}
			catch (IOException) {}
			
			return State.Unavailable;
		}
		
		public override Option<GHubSettingsFile> Read()
		{
			JObject parsedSettingsFile = parseSettingsFile(GHubSettingsStream);
			
			GHubSettingsFileObject = JsonConvert.DeserializeObject<GHubSettingsFile>(parsedSettingsFile.ToString(), new ApplicationJSONConverter())!;
			GHubSettingsFileObject.AssociateProfilesToApplications();

			return Option.Some(GHubSettingsFileObject);
		}

		public override void Write(GHubSettingsFile? settingsFileObject = null)
		{
			settingsFileObject ??= GHubSettingsFileObject;

			switch (GHubSettingsStream)
			{
				case FileStream:
				{
					WriteToFile(settingsFileObject: settingsFileObject);
					break;
				}
				case MemoryStream:
				{
					WriteToMemory(settingsFileObject: settingsFileObject);
					break;
				}
			}
		}
		private void WriteToFile(GHubSettingsFile? settingsFileObject)
		{
			using (StreamWriter settingsFileWriter = new (stream: GHubSettingsStream, encoding: new UTF8Encoding(), bufferSize: -1, leaveOpen: true))
			{
				//discard the old contents of the file
				GHubSettingsStream.SetLength(0); 
				
				string reSerializedGHubSettingsFile = Serialize(settingsFileObject);
				
				GHubSettingsStream.Position = 0;
				settingsFileWriter.Write(reSerializedGHubSettingsFile);
				settingsFileWriter.Flush();
				GHubSettingsStream.SetLength(GHubSettingsStream.Position);
			}
		}

		private void WriteToMemory(GHubSettingsFile? settingsFileObject)
		{
			GHubSettingsStream = new MemoryStream();
			
			using (StreamWriter settingsFileWriter = new (stream: GHubSettingsStream, encoding: new UTF8Encoding(), bufferSize: -1, leaveOpen: true))
			{
				string reSerializedGHubSettingsFile = Serialize(settingsFileObject);
				settingsFileWriter.Write(reSerializedGHubSettingsFile);
				settingsFileWriter.Flush();
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
			return new FileStream(GHubSettingsFilePath.ToString()!,
			                      FileMode.Open,
			                      FileAccess.ReadWrite);
		}
	}
}