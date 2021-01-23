﻿using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class ProfileViewModel : GHubRecordViewModel
	{
		public Profile Profile { get; set; }
		
		public string? DisplayName => Profile.DisplayName;

		public event GHubRecordSelectedEvent? GHubRecordSelected;

		public ProfileViewModel(Profile profile)
		{
			this.Profile = profile;
		}
	}
}