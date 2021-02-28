using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			ClearAssociations();
			ICollection<Application>? applications = Applications?.Applications;
			ICollection<Profile>? profiles = Profiles?.Profiles;

			if (profiles != null)
			{
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

		private void ClearAssociations()
		{
			ClearApplicationAssociations();
			ClearProfileAssociations();
		}
		
		private void ClearApplicationAssociations()
		{
			ICollection<Application>? applications = Applications?.Applications;

			if (applications != null)
			{
				foreach (Application application in applications)
				{
					application.Profiles.Clear();
				}
			}
		}

		private void ClearProfileAssociations()
		{
			ICollection<Profile>? profiles = Profiles?.Profiles;

			if (profiles != null)
			{
				foreach (Profile profile in profiles)
				{
					profile.Application = null;
				}
			}
		}
	}

	public class ApplicationList
	{
		[JsonProperty("applications")]
		public Collection<Application>? Applications { get; set; }
		
		public static implicit operator ApplicationList (Collection<Application> applications)
		{
			return new ApplicationList { Applications = applications };
		}
	}

	public class ProfileList
	{
		[JsonProperty("profiles")]
		public Collection<Profile>? Profiles { get; set; }
		
		public static implicit operator ProfileList (Collection<Profile> profiles)
		{
			return new ProfileList { Profiles = profiles };
		}
	}
}
