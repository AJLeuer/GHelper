using System.ComponentModel;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.View
{
	public interface IApplicationView : RecordView, INotifyPropertyChanged
	{
		ApplicationViewModel Application { get ; set; }
	}
}