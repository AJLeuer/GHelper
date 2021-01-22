using System;
using System.Collections.ObjectModel;
using System.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
using GHelperLogic.Utility.JSONConverter;
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

		private static readonly Stream GHubSettingsFile;
		
		static GHubSettingsFileReader()
		{
			#if DEBUG 
				GHubSettingsFile = new MemoryStream(Properties.Resources.DummyGHUBSettings, true);
			
			#elif RELEASE || DEBUGRELEASE
				GHubSettingsFile = new FileStream(
						DefaultFilePath.ToString()!,
						FileMode.Open,
						FileAccess.ReadWrite);

			#endif
		}

		public (Collection<Application> applications, Collection<Profile> profiles) ReadData(Stream? settingsFile = null)
		{
			settingsFile ??= GHubSettingsFile;
			(Stream firstSettingsFileCopy, Stream secondSettingsFileCopy) = settingsFile.Duplicate();
			Collection<Application> applications = ReadApplications(firstSettingsFileCopy);
			Collection<Profile> profiles = ReadProfiles(secondSettingsFileCopy);
			associateProfilesToApplications(applications, profiles);
			return (applications, profiles);
		}

		private static Collection<Profile> ReadProfiles(Stream settingsFile)
		{
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			JToken? profilesJSON = parsedSettingsFile["profiles"]?["profiles"];
			Collection<Profile> profiles = JsonConvert.DeserializeObject<Collection<Profile>>(profilesJSON!.ToString(), new ProfileJSONConverter())!;
			return profiles;
		}

		private static Collection<Application> ReadApplications(Stream settingsFile)
		{
			JObject parsedSettingsFile = readSettingsFile(settingsFile);
			JToken? applicationsJSON = parsedSettingsFile["applications"]?["applications"];
			Collection<Application> applications = JsonConvert.DeserializeObject<Collection<Application>>(applicationsJSON!.ToString(), new ApplicationJSONConverter())!;
			return applications;
		}

		private static void associateProfilesToApplications(Collection<Application> applications, Collection<Profile> profiles)
		{
			foreach (Profile profile in profiles)
			{
				if (profile.ApplicationID != null)
				{
					Application? application = applications.GetByID(profile.ApplicationID);
					if (application != null)
					{
						profile.Application = application;
						application.Profiles.Add(profile);
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