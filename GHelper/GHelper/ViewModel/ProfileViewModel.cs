using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class ProfileViewModel
	{
		public Profile Profile { get; set; }
		
		public string? DisplayName => Profile.DisplayName;

		public ProfileViewModel(Profile profile)
		{
			this.Profile = profile;
		}
	}
}