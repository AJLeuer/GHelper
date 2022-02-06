using System;
using System.ComponentModel;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View.Button
{
    public sealed partial class SaveButton : Microsoft.UI.Xaml.Controls.Button, StandardButton
    {
        private GHubRecordViewModel? gHubRecordViewModel;

        public GHubRecordViewModel? GHubRecordViewModel
        {
            get
            {
                return gHubRecordViewModel;
            }
            set
            {
                gHubRecordViewModel = value;
                ResetAppearance();
                WireToGHubRecord();
            }
        }

        public SaveButton()
        {
            this.InitializeComponent();
        }

        public void WireToGHubRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click += (object _, RoutedEventArgs _) => { GHubRecordViewModel?.Save(); };
                GHubRecordViewModel.PropertyChanged += SwitchToChangedAppearance;
                GHubRecordViewModel.UserSaved += ResetAppearance;
                GHubRecordViewModel.UserDiscardedChanges += (GHubRecordViewModel _) => { ResetAppearance(); };
                GHubRecordViewModel.UserDeletedRecord += (GHubRecordViewModel _) => { ResetAppearance(); };
            }
        }

        public void SwitchToChangedAppearance(object? sender, PropertyChangedEventArgs eventInfo)
        {
            this.Background = Application.Current.Resources[Properties.Resources.SystemAccentColorBrush] as SolidColorBrush;
        }
        
        public void ResetAppearance()
        {
            var defaultButton = new Microsoft.UI.Xaml.Controls.Button { Style = Application.Current.Resources[Properties.Resources.StandardButtonStyle] as Style };

            this.Background = defaultButton.Background;
            this.Style = defaultButton.Style;
        }
    }
}
