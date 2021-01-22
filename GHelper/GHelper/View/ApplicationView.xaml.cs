using GHelper.Utility;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Application = GHelperLogic.Model.Application;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace GHelper.View
{
	public partial class ApplicationView : StackPanel
	{
		public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
			nameof (ApplicationView.Application),
			typeof (Application),
			typeof (ApplicationView),
			new PropertyMetadata(null)
		);

		public static Image DefaultPosterImage { get ; } = new ();

		public Application Application
		{
			get { return (Application) GetValue(ApplicationProperty); }
			set { SetValue(ApplicationProperty, value); }
		}

		public Image Poster
		{
			get
			{
				if (poster != null)
				{
					return poster;
				}
				else if (Application.HasPoster == false)
				{
					return DefaultPosterImage;
				}
				else
				{
					retrievePosterImage();
					return poster ?? DefaultPosterImage;
				}
			}
		}

		private Image? poster = null;

		public ApplicationView()
		{
			this.InitializeComponent();
		}

		private void retrievePosterImage()
		{
			if (Application.Poster != null)
			{
				poster = new Image { Source = Application.Poster.ConvertToWindowsBitmapImage() };
			}
		}

		private Visibility posterImageVisibility
		{
			get 
			{
				if (this.Poster == DefaultPosterImage)
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