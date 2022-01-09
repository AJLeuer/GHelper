using System;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using GHelper.View.Dialog;
using GHelperLogic.IO;
using GHelperLogic.Utility;
using Microsoft.UI.Xaml;
using Image = SixLabors.ImageSharp.Image;

namespace GHelper.View
{
	public partial class StandardApplicationView : ApplicationView
	{
		public StandardApplicationView()
        {
	        InitializeComponent();
	        RecordViewControls.UserClickedSaveButton += () => { GHubRecordViewModel.Save(); };
	        RecordViewControls.UserClickedDeleteButton += () => { GHubRecordViewModel.Delete(); };
        }

	    protected override void ResetAppearance()
	    {
		    RecordViewControls.ResetAppearance();
	    }

	    public override void ChainGHubRecordViewModelEventsToControls()
	    {
		    Application.PropertyChanged += RecordViewControls.SwitchToChangedAppearance;
		    Application.UserSaved += RecordViewControls.ResetAppearance;
	    }

	    private void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
        {
	        RecordView.ChangeName(this, sender);
        }

	    private async void SetNewCustomPosterImage(object sender, RoutedEventArgs routedEventInfo)
	    {
		    try
		    {
			    StorageFile? imageStorageFile = await FileDialog.ChooseFile(location: PickerLocationId.PicturesLibrary, filteredFileTypes: new [] { Properties.Resources.FileExtensionPNG, Properties.Resources.FileExtensionBMP, Properties.Resources.FileExtensionJPEG, Properties.Resources.FileExtensionJPEGAlternate, Properties.Resources.FileExtensionTGA });
			    if (imageStorageFile is not null)
			    {
				    Stream? imageFileStream = await imageStorageFile.OpenStreamForReadAsync();
				    Image? image = ImageIOHelper.LoadFromStream(imageFileStream);
				    if (image != null)
				    {
					    Application.SetNewCustomPosterImage(image);
				    }
			    }
		    }
		    catch (Exception exception)
		    {
			    LogManager.Log("Unable to set custom poster image");
			    LogManager.Log(exception);
		    }
	    }

	}
}