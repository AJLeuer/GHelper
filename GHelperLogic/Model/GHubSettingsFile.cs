using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Model
{
	public class GHubSettingsFile
	{
		[JsonProperty("applications")]
		public ApplicationList? Applications { get; set; } 
		
		[JsonProperty("profiles")]
		public ProfileList? Profiles { get; set; }
		
		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }
		
		public void AssociateProfilesToApplications()
		{
			ICollection<Application>? applications = Applications?.Applications;
			ICollection<Profile>? profiles = Profiles?.Profiles;
			
			if (profiles != null)
				foreach (Profile profile in profiles)
				{
					if (profile.ApplicationID != null)
					{
						Application? application = applications?.GetByID(profile.ApplicationID);
						if (application != null)
						{
							profile.Application = application;
							application.Profiles.Add(profile);
						}
					}
				}
		}
	}

	public class ApplicationList
	{
		[JsonProperty("applications")]
		public Application[]? Applications { get; set; }
		
		public static implicit operator ApplicationList (Collection<Application> applications)
		{
			return new ApplicationList { Applications = applications.ToArray() };
		}
	}

	public class ProfileList
	{
		[JsonProperty("profiles")]
		public Profile[]? Profiles { get; set; }
		
		public static implicit operator ProfileList (Collection<Profile> profiles)
		{
			return new ProfileList { Profiles = profiles.ToArray() };
		}
	}
}
