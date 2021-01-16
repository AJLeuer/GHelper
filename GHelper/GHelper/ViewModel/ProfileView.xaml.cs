using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.ViewModel
{
	public partial class ProfileView : StackPanel 
    {
	    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
		    nameof (ProfileView.Profile),
		    typeof (Profile),
		    typeof (ProfileView),
		    new PropertyMetadata(null)
	    );
	    
	    public Profile Profile
	    {
		    get { return (Profile) GetValue(ProfileProperty); }
		    set { SetValue(ProfileProperty, value); }
	    }

	    public ProfileView()
	    {
		    InitializeComponent();
	    }
    }
}