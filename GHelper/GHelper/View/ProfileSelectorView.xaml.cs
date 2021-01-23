using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public partial class ProfileSelectorView : StackPanel, SelectableItem 
    {

	    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
		    nameof (ProfileSelectorView.Profile),
		    typeof (ProfileViewModel),
		    typeof (ProfileSelectorView),
		    new PropertyMetadata(null)
	    );

	    public event EventHandler? Selected;
	    
	    public ProfileViewModel Profile
	    {
		    get { return (ProfileViewModel) GetValue(ProfileProperty); }
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