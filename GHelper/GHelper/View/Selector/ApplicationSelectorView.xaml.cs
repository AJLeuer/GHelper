using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Selector 
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

		public ApplicationSelectorView()
		{
			this.InitializeComponent();
		}

		public Visibility PosterImageVisibility
		{
			get 
			{
				if (Application.Application?.HasPoster == false)
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