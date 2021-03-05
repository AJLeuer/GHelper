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
			switch (item)
			{
				case ApplicationViewModel:
					return ApplicationTemplate;
				case ProfileViewModel:
					return ProfileTemplate;
				default:
					throw new ArgumentException("Unknown type of item passed to GHubItemTemplateSelector");
			}
		}
	}
}