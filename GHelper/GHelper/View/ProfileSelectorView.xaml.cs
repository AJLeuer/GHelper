using System;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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
	    }

	    public void NotifySelected(object? sender, EventArgs eventInfo)
	    {
		    this.Selected?.Invoke(sender, eventInfo);
	    }
    }
}