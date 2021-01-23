using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class ProfileViewModel : GHubRecordViewModel
	{
		public Profile Profile { get; set; }
		
		public string? DisplayName
		{
			get => Profile.DisplayName;
			set => Profile.Name = value;
		}

		public ProfileViewModel(Profile profile)
		{
			this.Profile = profile;
		}
	}
}