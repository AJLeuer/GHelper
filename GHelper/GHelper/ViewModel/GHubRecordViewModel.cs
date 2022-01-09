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

		public abstract string? Type { get; }
		
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
		
		public event UserSavedEvent?            UserSaved;
		public event UserDeletedRecordEvent?    UserDeletedRecord;
		public event UserDiscardedChangesEvent? UserDiscardedChanges;
		
		public abstract event PropertyChangedEventHandler? PropertyChanged;

		public void SaveBackup()
		{
			GHubRecordBackup = GHubRecord?.Clone();
		}

        public virtual void DiscardUserChanges(GHubRecordViewModel? origin = null)
        {
            origin ??= this;
            UserDiscardedChanges?.Invoke(origin);
            RestoreInitialState();
        }

		protected internal virtual void RestoreInitialState()
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

		public void Save()
		{
			UserSaved?.Invoke();
		}
		
		public virtual void Delete()
		{
			UserDeletedRecord?.Invoke(this);
		}
		
		public void HandleKeyboardInput(object sender, KeyRoutedEventArgs keyboardEventInfo)
		{
			switch (keyboardEventInfo.Key)
			{
				case VirtualKey.S:
					if (KeyboardState.GetModifierKeyState(VirtualKey.Control) == CoreVirtualKeyStates.Down)
					{
						Save();
					}
					break;
				
				case VirtualKey.Delete or VirtualKey.Back:
					Delete();
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