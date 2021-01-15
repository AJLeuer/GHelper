using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using GHelper.Models;
using GHelperLogic.Models;
using Microsoft.UI.Xaml;

namespace GHelper.View
{
	public sealed partial class MainWindow : Window
	{
		private ObservableCollection<Context> Contexts
		{
			get;
		} = new ObservableCollection<Context> 
		{ 
			new Context()
			{
				ApplicationID = Guid.NewGuid(), Name = "Foo"
				,Profiles = new Collection<Profile>()
				{ 
					new Profile { Name = "fubar"}
				}
			},
			new Context() { ApplicationID = Guid.NewGuid(), Name = "Bar" 
				,Profiles = new Collection<Profile>()
				{ 
					new Profile() { Name = "Foo"},
					new Profile() { Name = "Bar"} 
				}
			}
		};
	

		public MainWindow()
        {
            this.InitializeComponent();
        }
	}
}
