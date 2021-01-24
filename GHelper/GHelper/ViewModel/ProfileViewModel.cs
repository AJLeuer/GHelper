using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class ProfileViewModel : GHubRecordViewModel, INotifyPropertyChanged
	{
		private Profile? profile;

		public Profile? Profile
		{
			get => profile;
			set
			{
				profile = value;
				OnPropertyChanged(nameof(Profile));
			}
		}

		public string? DisplayName
		{
			get => Profile?.DisplayName;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public ProfileViewModel(Profile profile)
		{
			this.Profile = profile;
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}