using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Annotations;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
    public partial class DesktopApplicationView : UserControl, IApplicationView
    {
        
        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
             nameof (Application),
             typeof (ApplicationViewModel),
             typeof (DesktopApplicationView),
             new PropertyMetadata(null));
        
        public ApplicationViewModel Application
        {
            get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
            set
            {
                SetValue(ApplicationProperty, value);
                ResetAppearance();
                DetermineNameViewStyle();
            }
        }
        
        public GHubRecordViewModel GHubRecord
        {
            get { return Application; }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public DesktopApplicationView(Action saveFunction, Action<GHubRecordViewModel> deleteFunction)
        {
            this.InitializeComponent();
            RegisterForSaveNotification(saveFunction);
            RegisterForDeleteNotification(deleteFunction);
            this.PropertyChanged += RecordViewControls.NotifyOfUserChange;
        }
        
        public void RegisterForSaveNotification(Action saveFunction)
        {
            RecordViewControls.RegisterForSaveNotification(saveFunction);
        }
        
        public void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction)
        {
            Action delete = () =>
            {
                deleteFunction(this.GHubRecord);
            };
		    
            RecordViewControls.RegisterForDeleteNotification(delete);
        }
        
        void RecordView.SendRecordChangedNotification()
        {
            OnPropertyChanged(nameof(GHubRecord));
        }
        
        protected void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
        {
            RecordView.ChangeName(this, sender);
        }

        protected void HandleNameChange(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
        {
            RecordView.ChangeName(this, eventInfo.Element);
        }

        public void ResetAppearance()
        {
            RecordViewControls.ResetAppearance();
        }

        public void DetermineNameViewStyle()
        {
            IApplicationView.DetermineNameViewStyle(Application, NameTextBox);
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
