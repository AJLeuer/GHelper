using System.ComponentModel;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public interface IApplicationView : RecordView, INotifyPropertyChanged
	{
		ApplicationViewModel Application { get ; set; }

		protected static void DetermineNameViewStyle(ApplicationViewModel application, TextBox textBoxToStyle)
		{
			Style editableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.StandardTextBox] as Style)!;
			Style immutableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.ImmutableTextBox] as Style)!;

			if ((application.Application?.IsCustom != null) && (application.Application?.IsCustom == true))
			{
				textBoxToStyle.Style = editableTextBox;
			}
			else
			{
				textBoxToStyle.Style = immutableTextBox;
			}
		}
	}
}