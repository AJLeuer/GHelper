using Windows.System;
using Windows.UI.Core;
using Microsoft.UI.Input;

namespace GHelper.View.Utility
{
    public static class KeyboardState
    {
        public static CoreVirtualKeyStates GetModifierKeyState(VirtualKey modifierKey)
        {
            CoreVirtualKeyStates modifierKeyState = InputKeyboardSource.GetKeyStateForCurrentThread(modifierKey);

            if (modifierKeyState is CoreVirtualKeyStates.Down or CoreVirtualKeyStates.Locked)
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