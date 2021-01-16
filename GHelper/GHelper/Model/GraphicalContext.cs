using GHelperLogic.Model;
using GHelperLogic.Utility;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.Model
{
	public class GraphicalContext : Context
	{
		public GraphicalContext(Context context)
		{
			context.CopyPropertiesTo(this);
		}
		
		public Image? PosterImage { get; }
	}
}