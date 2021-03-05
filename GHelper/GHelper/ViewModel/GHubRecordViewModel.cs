using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.System;
using Windows.UI.Core;
using GHelper.Event;
using GHelper.View.Utility;
using GHelperLogic.Model;
using Microsoft.UI.Xaml.Input;

namespace GHelper.ViewModel
{
	public abstract class GHubRecordViewModel : INotifyPropertyChanged
	{
		public abstract GHubRecord? GHubRecord { get;}
		protected GHubRecord? GHubRecordBackup { get; set; }
		
		public abstract string? Name { get; set; }

		public string? DisplayName
		{
			get => GHubRecord?.DisplayName;
		}

		public State State
		{
			get
			{
				if (GHubRecord == GHubRecordBackup)
				{
					return State.Unchanged;
				}
				else
				{
					return State.Modified;
				}
			}
		}
		
		public event UserSavedEvent?         UserSaved;
		public event UserDeletedRecordEvent? UserDeletedRecord;
		
		public abstract event PropertyChangedEventHandler? PropertyChanged;

		public void SaveBackup()
		{
			GHubRecordBackup = GHubRecord?.Clone();
		}

		public virtual void RestoreInitialState()
		{
			if ((GHubRecord != null) && (GHubRecordBackup != null))
			{
				GHubRecord.CopyStateFrom(GHubRecordBackup);
				foreach (PropertyInfo property in this.GetType().GetProperties())
				{
					OnPropertyChanged(property.Name);
				}
			}
		}

		public void FireSaveEvent()
		{
			UserSaved?.Invoke();
		}
		
		public virtual void FireDeletedEvent()
		{
			UserDeletedRecord?.Invoke(this);
		}
		
		public void HandleKeyboardInput(object sender, KeyRoutedEventArgs keyboardEventInfo)
		{
			switch (keyboardEventInfo.Key)
			{
				case VirtualKey.S:
					if (KeyboardState.ControlKeyState == CoreVirtualKeyStates.Down)
					{
						FireSaveEvent();
					}
					break;
				
				case VirtualKey.Delete or VirtualKey.Back:
					FireDeletedEvent();
					break;
			}
		}

		protected abstract void OnPropertyChanged([CallerMemberName] string? propertyName = null);
	}
	
	public enum State
	{
		Unchanged,
		Modified
	}
}