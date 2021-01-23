using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public partial class ProfileView : UserControl
	{
		public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
			nameof (ProfileSelectorView.Profile),
			typeof (ProfileViewModel),
			typeof (ProfileSelectorView),
			new PropertyMetadata(null)
		);
	    
		public ProfileViewModel Profile
		{
			get { return (ProfileViewModel) GetValue(ProfileProperty); }
			set { SetValue(ProfileProperty, value); }
		}
		
	    public ProfileView()
        {
            InitializeComponent();
        }
    }
}