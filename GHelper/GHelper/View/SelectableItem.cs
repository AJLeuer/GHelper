using System;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
	public interface SelectableItem
	{
		public event EventHandler? Selected;
		void HandleSelected(object sender, PointerRoutedEventArgs e);
	}
}