using System;
using GHelper.Models;
using GHelperLogic.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.Utility
{
	public class GHubItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate ContextTemplate { get; set; }
		public DataTemplate ProfileTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item)
		{
			if (item is Context)
			{
				return ContextTemplate;
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