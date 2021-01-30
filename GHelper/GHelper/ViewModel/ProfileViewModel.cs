using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public class ProfileViewModel : GHubRecordViewModel, INotifyPropertyChanged
	{
		public override GHubRecord? GHubRecord 
		{
			get { return this.Profile; }
		}
		
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

		public override string Name 
		{
			set
			{
				if (Profile != null)
				{
					Profile.Name = value;
					OnPropertyChanged(nameof(DisplayName));
				}
			}
		}

		public bool ActiveForApplication
		{
			get { return Profile?.ActiveForApplication ?? false; }
			set
			{
				if (Profile != null)
				{
					Profile.ActiveForApplication = value;
				}
				OnPropertyChanged(nameof(ActiveForApplication));
			}
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