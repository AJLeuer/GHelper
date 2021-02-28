using System.ComponentModel;
using GHelper.ViewModel;

namespace GHelper.View
{
	public interface IApplicationView : RecordView, INotifyPropertyChanged
	{
		ApplicationViewModel Application { get ; set; }
	}
}