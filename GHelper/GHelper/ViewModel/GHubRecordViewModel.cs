using System.ComponentModel;
using GHelper.Utility;
using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel : INotifyPropertyChanged
	{
		public abstract GHubRecord? GHubRecord { get;}
		protected GHubRecord? GHubRecordBackup { get; set; }
		
		public abstract string Name { set; } 
		
		public string? DisplayName
		{
			get => GHubRecord?.DisplayName;
		}
		public abstract event PropertyChangedEventHandler? PropertyChanged;

		protected void SaveBackup()
		{
			GHubRecordBackup = GHubRecord?.Clone();
		}

		public virtual void RestoreInitialState()
		{
			if ((GHubRecord != null) && (GHubRecordBackup != null))
			{
				GHubRecord.CopyStateFrom(GHubRecordBackup);
			}
		}
	}
}