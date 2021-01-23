namespace GHelper.ViewModel
{
	public interface GHubRecordViewModel
	{
		event GHubRecordSelectedEvent? GHubRecordSelected;
	}
	
	public delegate void GHubRecordSelectedEvent(GHubRecordViewModel viewModel);
}