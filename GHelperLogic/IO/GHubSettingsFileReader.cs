﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
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

		public (Collection<Context> contexts, Collection<Profile> profiles) ReadData(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			(Stream firstSettingsFileCopy, Stream secondSettingsFileCopy) = settingsFile.Duplicate();
			Collection<Context> contexts = ReadContexts(firstSettingsFileCopy);
			Collection<Profile> profiles = ReadProfiles(secondSettingsFileCopy);
			associateProfilesToContexts(contexts, profiles);
			return (contexts, profiles);
		}

		private static Collection<Profile> ReadProfiles(Stream settingsFile)
		{
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			JToken? profilesJSON = parsedSettingsFile["profiles"]?["profiles"];
			Collection<Profile> profiles = JsonConvert.DeserializeObject<Collection<Profile>>(profilesJSON!.ToString());
			return profiles;
		}

		private static Collection<Context> ReadContexts(Stream settingsFile)
		{
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			JToken? contextsJSON = parsedSettingsFile["applications"]?["applications"];
			Collection<Context> contexts = JsonConvert.DeserializeObject<Collection<Context>>(contextsJSON!.ToString());
			return contexts;
		}

		private static void associateProfilesToContexts(Collection<Context> contexts, Collection<Profile> profiles)
		{
			foreach (Profile profile in profiles)
			{
				if (profile.ApplicationID != null)
				{
					Context? context = contexts.GetByID(profile.ApplicationID);
					if (context != null)
					{
						profile.Context = context;
						context.Profiles.Add(profile);
					}
				}
			}
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