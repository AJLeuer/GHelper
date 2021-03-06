﻿using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Selector
{
	public partial class ProfileSelectorView : StackPanel
    {

	    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
		    nameof (Profile),
		    typeof (ProfileViewModel),
		    typeof (ProfileSelectorView),
		    new PropertyMetadata(null)
	    );
	    
	    public ProfileViewModel Profile
	    {
		    get { return (ProfileViewModel) GetValue(ProfileProperty); }
		    set { SetValue(ProfileProperty, value); }
	    }

	    public ProfileSelectorView()
	    {
		    InitializeComponent();
	    }
	    
    }
}