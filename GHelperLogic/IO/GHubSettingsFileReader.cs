using System;
using System.Collections.ObjectModel;
using System.IO;
using GHelper.Models;
using GHelperLogic.Models;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.IO
{
	public class GHubSettingsFileReader
	{
		public static IFilePath DefaultFilePath { get ; } =
			PathHelpers.ToAbsoluteFilePath(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				Path.DirectorySeparatorChar + Properties.Resources.GHubConfigFileDirectoryName + 
				Path.DirectorySeparatorChar + Properties.Resources.GHubConfigFileName);

		private static Stream GHubSettingsFile;
		
		static GHubSettingsFileReader()
		{
			#if DEBUG
				GHubSettingsFile = new MemoryStream(Properties.Resources.DummyGHUBSettings, true);
			
			#elif RELEASE
				GHubSettingsFile = new FileStream(
						DefaultFilePath.ToString()!,
						FileMode.Open,
						FileAccess.ReadWrite);

			#endif
		}

		public Collection<Profile> ReadProfiles(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			return null!;
		}

		public Collection<Context> ReadContexts(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			JToken? contextsJSON = parsedSettingsFile["applications"]?["applications"];
			Collection<Context> contexts = JsonConvert.DeserializeObject<Collection<Context>>(contextsJSON!.ToString());
			return contexts;
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