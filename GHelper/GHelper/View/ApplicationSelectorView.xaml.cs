using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace GHelper.View 
{
	public partial class ApplicationSelectorView : StackPanel
	{
		public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
			nameof (Application),
			typeof (ApplicationViewModel),
			typeof (ApplicationSelectorView),
			new PropertyMetadata(null)
		);
		
		public ApplicationViewModel Application
		{
			get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
			set { SetValue(ApplicationProperty, value); }
		}

		public Image Poster
		{
			get { return Application.Poster; }
		}

		public ApplicationSelectorView()
		{
			this.InitializeComponent();
		}


		public Visibility PosterImageVisibility
		{
			get 
			{
				if (this.Poster == ApplicationViewModel.DefaultPosterImage)
				{
					return Visibility.Collapsed;
				}
				else
				{
					return Visibility.Visible;
				}
			}
		}
	}
}