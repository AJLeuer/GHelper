using Windows.System;
using Windows.UI.Core;

namespace GHelper.View.Utility
{
	public static class KeyboardState
	{
		public static CoreVirtualKeyStates ControlKeyState
		{
			get
			{
				CoreVirtualKeyStates controlKeyState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
			
				if ((controlKeyState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down)
				{
					return CoreVirtualKeyStates.Down;
				}
				else
				{
					return CoreVirtualKeyStates.None;
				}
			}
		}
	}
}