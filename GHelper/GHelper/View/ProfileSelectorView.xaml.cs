using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public partial class ProfileSelectorView : StackPanel 
    {
	    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
		    nameof (ProfileSelectorView.Profile),
		    typeof (Profile),
		    typeof (ProfileSelectorView),
		    new PropertyMetadata(null)
	    );
	    
	    public Profile Profile
	    {
		    get { return (Profile) GetValue(ProfileProperty); }
		    set { SetValue(ProfileProperty, value); }
	    }

	    public ProfileSelectorView()
	    {
		    InitializeComponent();
	    }
    }
}