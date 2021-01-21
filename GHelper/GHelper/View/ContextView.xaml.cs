using GHelper.Utility;
using GHelperLogic.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace GHelper.View
{
	public partial class ContextView : StackPanel
	{
		public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
			nameof (ContextView.Context),
			typeof (Context),
			typeof (ContextView),
			new PropertyMetadata(null)
		);

		public static Image DefaultPosterImage { get ; } = new ();

		public Context Context
		{
			get { return (Context) GetValue(ContextProperty); }
			set { SetValue(ContextProperty, value); }
		}

		public Image Poster
		{
			get
			{
				if (poster != null)
				{
					return poster;
				}
				else if (Context.HasPoster() == false)
				{
					return DefaultPosterImage;
				}
				else
				{
					retrievePosterImage();
					return poster!;
				}
			}
		}

		private Image? poster = null;

		public ContextView()
		{
			this.InitializeComponent();
		}

		private void retrievePosterImage()
		{
			if ((Context.IsCustom == true) && (Context.Poster != null))
			{
				poster = new Image { Source = Context.Poster.ConvertToWindowsBitmapImage() };
			}
			else if (Context.PosterURL != null)
			{
				poster = new Image { Source = new BitmapImage(Context.PosterURL) };
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