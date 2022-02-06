using System;
using System.ComponentModel;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.View.Button
{
    public sealed partial class SaveButton : Microsoft.UI.Xaml.Controls.Button
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
                RemovePreviousRecord();
                gHubRecordViewModel = value;
                ResetAppearance();
                WireToGHubRecord();
            }
        }

        public SaveButton()
        {
            this.InitializeComponent();
        }

        private void WireToGHubRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click += InvokeGHubRecordSave;
                GHubRecordViewModel.PropertyChanged += SwitchToChangedAppearance;
                GHubRecordViewModel.UserSaved += ResetAppearance;
                GHubRecordViewModel.UserDiscardedChanges += ResetAppearance;
                GHubRecordViewModel.UserDeletedRecord += ResetAppearance;
            }
        }

        private void RemovePreviousRecord()
        {
            if (GHubRecordViewModel is not null)
            {
                this.Click -= InvokeGHubRecordSave;
                GHubRecordViewModel.PropertyChanged -= SwitchToChangedAppearance;
                GHubRecordViewModel.UserSaved -= ResetAppearance;
                GHubRecordViewModel.UserDiscardedChanges -= ResetAppearance;
                GHubRecordViewModel.UserDeletedRecord -= ResetAppearance;
            }
        }

        private void InvokeGHubRecordSave(object o, RoutedEventArgs routedEventArgs)
        {
            GHubRecordViewModel?.Save();
        }

        private void SwitchToChangedAppearance(object? sender, PropertyChangedEventArgs eventInfo)
        {
            this.Background = Application.Current.Resources[Properties.Resources.SystemAccentColorBrush] as SolidColorBrush;
        }

        private void ResetAppearance()
        {
            var defaultButton = new Microsoft.UI.Xaml.Controls.Button { Style = Application.Current.Resources[Properties.Resources.StandardButtonStyle] as Style };

            this.Background = defaultButton.Background;
            this.Style = defaultButton.Style;
        }

        private void ResetAppearance(GHubRecordViewModel _) => ResetAppearance();
    }
}
