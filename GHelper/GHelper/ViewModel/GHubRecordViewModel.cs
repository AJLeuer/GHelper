namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel
	{
		event GHubRecordSelectedEvent? GHubRecordSelected;

		public void NotifySelected()
		{
			GHubRecordSelected?.Invoke(this);
		}
	}
	
	public delegate void GHubRecordSelectedEvent(GHubRecordViewModel viewModel);
}