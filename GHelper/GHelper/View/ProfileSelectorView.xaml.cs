using System;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinRT;

namespace GHelper.View
{
	public abstract partial class ProfileSelectorView : StackPanel, SelectableItem 
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

	    protected ProfileSelectorView()
	    {
		    InitializeComponent();
		    this.PointerReleased += new PointerEventHandler(HandleSelected);
	    }
	    
	    public void HandleSelected(object sender, PointerRoutedEventArgs e)
	    {
		    Selected?.Invoke(sender, e.As<EventArgs>());
	    }
    }
}