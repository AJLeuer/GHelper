using System;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinRT;

namespace GHelper.View
{
	public partial class ProfileSelectorView : StackPanel, SelectableItem 
    {

	    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
		    nameof (ProfileSelectorView.Profile),
		    typeof (Profile),
		    typeof (ProfileSelectorView),
		    new PropertyMetadata(null)
	    );

	    public event EventHandler? Selected;
	    
	    public Profile Profile
	    {
		    get { return (Profile) GetValue(ProfileProperty); }
		    set { SetValue(ProfileProperty, value); }
	    }

	    public ProfileSelectorView()
	    {
		    InitializeComponent();
		    this.PointerReleased += (sender, e) => HandleSelected(sender, e);
	    }
	    
	    public void HandleSelected( object sender, PointerRoutedEventArgs eventInfo)
	    {
		    Selected?.Invoke(sender, new EventArgs());
	    }

	    public void HandleSelected( object sender, TappedRoutedEventArgs eventInfo)
	    {
		    Selected?.Invoke(sender, new EventArgs());
	    }
    }
}