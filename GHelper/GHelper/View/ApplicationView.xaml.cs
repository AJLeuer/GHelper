using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.Storage.Pickers;
using GHelper.Annotations;
using GHelper.Event;
using GHelper.View.Dialog;
using GHelper.ViewModel;
using GHelperLogic.IO;
using GHelperLogic.Utility;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Image = SixLabors.ImageSharp.Image;

namespace GHelper.View
{
	public partial class ApplicationView : UserControl, IApplicationView
	{
	    public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
		    nameof (Application),
		    typeof (ApplicationViewModel),
		    typeof (ApplicationView),
		    new PropertyMetadata(null)
	    );

	    public ApplicationViewModel Application
	    {
		    get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
		    set
		    {
			    SetValue(ApplicationProperty, value);
			    ResetAppearance();
			    Application.PropertyChanged += RecordViewControls.NotifyOfUserChange;
		    }
	    }

	    public GHubRecordViewModel GHubRecord
	    {
		    get { return Application; }
	    }

	    public event UserSavedEvent?              UserSaved;
	    public event UserDeletedRecordEvent?      UserDeletedRecord;
	    public event PropertyChangedEventHandler? PropertyChanged;

	    public ApplicationView()
        {
	        InitializeComponent();
	        RecordViewControls.UserClickedSaveButton += () => { UserSaved?.Invoke(); };
	        RecordViewControls.UserClickedDeleteButton += () => { UserDeletedRecord?.Invoke(GHubRecord); };
        }

	    void RecordView.SendRecordChangedNotification()
	    {
		    OnPropertyChanged(nameof(GHubRecord));
	    }

	    protected void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
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

	    public void ResetAppearance()
	    {
		    RecordViewControls.ResetAppearance();
	    }

	    
	    [NotifyPropertyChangedInvocator]
	    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
	}
}