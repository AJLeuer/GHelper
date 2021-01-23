using System;
using GHelper.Utility;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Application = GHelperLogic.Model.Application;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace GHelper.View
{
	public partial class ApplicationSelectorView : StackPanel, SelectableItem
	{
		public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
			nameof (ApplicationSelectorView.Application),
			typeof (Application),
			typeof (ApplicationSelectorView),
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

		public event EventHandler? Selected;

		public ApplicationSelectorView()
		{
			this.InitializeComponent();
			this.PointerReleased += (sender, e) => HandleSelected(sender, e);
			this.Tapped += (sender, e) => HandleSelected(sender, e);
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

		public void HandleSelected(object sender, PointerRoutedEventArgs eventInfo)
		{
			Selected?.Invoke(sender, new EventArgs());
		}

		public void HandleSelected( object sender, TappedRoutedEventArgs eventInfo)
		{
			Selected?.Invoke(sender, new EventArgs());
		}
	}
}