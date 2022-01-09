using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.Utility;
using GHelperLogic.Model;

namespace GHelper.ViewModel 
{
	public class ProfileViewModel : GHubRecordViewModel
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

		public override string? Type
		{
			get
			{
				return Profile?.GetType().Name.ConvertPascalCaseToSentence();
			}
		}

		public override string? Name 
		{
			get => Profile?.Name;
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
					if (value == false)
					{
						Profile.ActiveForApplication = null;
					}
					else
					{
						Profile.ActiveForApplication = value;
					}
				}
				OnPropertyChanged(nameof(ActiveForApplication));
			}
		}

		public override event PropertyChangedEventHandler? PropertyChanged;

		public ProfileViewModel(Profile profile)
		{
			this.Profile = profile;
			SaveBackup();
		}

        protected internal override void RestoreInitialState()
		{
			base.RestoreInitialState();
			OnPropertyChanged(nameof(Profile));
		}

		[NotifyPropertyChangedInvocator]
		protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}