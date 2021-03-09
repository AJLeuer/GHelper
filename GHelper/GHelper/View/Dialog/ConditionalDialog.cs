using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View.Dialog
{
	public abstract class ConditionalDialog : ContentDialog
	{
		public abstract Task DisplayIfNeeded();

		protected void QuitApp(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
			Application.Current.Exit();
		}
	}
}