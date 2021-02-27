using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Microsoft.UI.Xaml;
using Nzr.ToolBox.Core;
using WinRT;

namespace GHelper.View.Dialog
{
	public static class FileDialog
	{
		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
		private static extern IntPtr GetActiveWindow();
 
		public static async Task<StorageFile?> ChooseFile(PickerLocationId location, params string[] filteredFileTypes)
        {
	        var picker = new FileOpenPicker { SuggestedStartLocation = location };
	        picker.FileTypeFilter.AddElements(filteredFileTypes);
	        
	        // When running on win32, FileOpenPicker needs to know the top-level hwnd via IInitializeWithWindow::Initialize.
	        if (Window.Current == null)
	        {
		        IInitializeWithWindow initializeWithWindowWrapper = picker.As<IInitializeWithWindow>();
		        IntPtr windowHandle = GetActiveWindow();
		        initializeWithWindowWrapper.Initialize(windowHandle);
	        }
        
	        StorageFile? selectedStorageFile = await picker.PickSingleFileAsync();
	        return selectedStorageFile;
        }
	}
	
	[ComImport, Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IInitializeWithWindow
	{
		void Initialize([In] IntPtr hwnd);
	}
}