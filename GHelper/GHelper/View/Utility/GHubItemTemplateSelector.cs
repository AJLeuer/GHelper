using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Utility
{
	public class GHubItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? ApplicationTemplate { get; set; }
		public DataTemplate? ProfileTemplate { get; set; }

		protected override DataTemplate? SelectTemplateCore(object item)
		{
			if (item is ApplicationViewModel)
			{
				return ApplicationTemplate;
			}
			else if (item is ProfileViewModel)
			{
				return ProfileTemplate;
			}
			else
			{
				throw new ArgumentException("Unknown type of item passed to GHubItemTemplateSelector");
			}
		}
	}
}