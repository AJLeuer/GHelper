using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel
	{
		protected abstract GHubRecord? GHubRecord { get;}
		
		public abstract string Name { set; } 
		
		public string? DisplayName
		{
			get => GHubRecord?.DisplayName;
		}
		
		event GHubRecordSelectedEvent? GHubRecordSelected;

		public void NotifySelected()
		{
			GHubRecordSelected?.Invoke(this);
		}
	}
	
	public delegate void GHubRecordSelectedEvent(GHubRecordViewModel viewModel);
}