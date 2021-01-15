using System;
using System.Collections.Generic;
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
				Path.DirectorySeparatorChar + Properties.Resources.GHubConfigFileRelativePath + 
				Path.DirectorySeparatorChar + Properties.Resources.GHubConfigFileName);

		public Collection<Profile> ReadProfiles(IFilePath settingsFilePath = null)
		{
			settingsFilePath ??= DefaultFilePath;
			return ReadProfiles(new FileStream(
				settingsFilePath.ToString()!, 
				FileMode.Open, 
				FileAccess.Read));
		}

		public Collection<Profile> ReadProfiles(Stream settingsFile)
		{
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			return null;
		}
		
		public Collection<Context> ReadContexts(IFilePath settingsFilePath = null)
		{
			settingsFilePath ??= DefaultFilePath;
			return ReadContexts(new FileStream(
				settingsFilePath.ToString()!,
				FileMode.Open,
				FileAccess.Read));
		}
		
		public Collection<Context> ReadContexts(Stream settingsFile)
		{
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