using System;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Application = GHelperLogic.Model.Application;

namespace GHelper.View.Utility
{
	public class GHubItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? ApplicationTemplate { get; set; }
		public DataTemplate? ProfileTemplate { get; set; }

		protected override DataTemplate? SelectTemplateCore(object item)
		{
			if (item is Application)
			{
				return ApplicationTemplate;
			}
			else if (item is Profile)
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