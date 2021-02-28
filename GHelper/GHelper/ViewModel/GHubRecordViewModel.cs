using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel : INotifyPropertyChanged
	{
		public abstract GHubRecord? GHubRecord { get;}
		protected GHubRecord? GHubRecordBackup { get; set; }
		
		public abstract string? Name { get; set; }

		public string? DisplayName
		{
			get => GHubRecord?.DisplayName;
		}

		public State State
		{
			get
			{
				if (GHubRecord == GHubRecordBackup)
				{
					return State.Unchanged;
				}
				else
				{
					return State.Modified;
				}
			}
		}
		
		public abstract event PropertyChangedEventHandler? PropertyChanged;

		public void SaveBackup()
		{
			GHubRecordBackup = GHubRecord?.Clone();
		}

		public virtual void RestoreInitialState()
		{
			if ((GHubRecord != null) && (GHubRecordBackup != null))
			{
				GHubRecord.CopyStateFrom(GHubRecordBackup);
				foreach (PropertyInfo property in this.GetType().GetProperties())
				{
					OnPropertyChanged(property.Name);
				}
			}
		}

		protected abstract void OnPropertyChanged([CallerMemberName] string? propertyName = null);
	}
	
	public enum State
	{
		Unchanged,
		Modified
	}
}