using System;

namespace GHelper.View
{
	public interface SelectableItem
	{
		public event EventHandler? Selected;

		void NotifySelected(object? sender, EventArgs eventInfo);
	}
}