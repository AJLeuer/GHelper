using GHelperLogic.Model;

namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel
	{
		public abstract GHubRecord? GHubRecord { get;}
		
		public abstract string Name { set; } 
		
		public string? DisplayName
		{
			get => GHubRecord?.DisplayName;
		}
	}
}