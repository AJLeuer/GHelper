using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
    public sealed partial class ApplicationDataView : StackPanel
    {
        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
             nameof (Application),
             typeof (ApplicationViewModel),
             typeof (ApplicationDataView),
             new PropertyMetadata(null)
            );

        public ApplicationViewModel Application
        {
            get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
            set
            {
                SetValue(ApplicationProperty, value);
                DetermineNameViewStyle();
            }
        }

        public event KeyEventHandler? NamedChanged;

        public ApplicationDataView()
        {
            this.InitializeComponent();
        }

        private void HandleNamedChanged(object sender, KeyRoutedEventArgs input)
        {
            NamedChanged?.Invoke(sender, input);
        }
        
        private void DetermineNameViewStyle()
        {
            Style editableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.StandardTextBox] as Style)!;
            Style immutableTextBox = (Microsoft.UI.Xaml.Application.Current.Resources[Properties.Resources.ImmutableTextBox] as Style)!;

            if (Application is CustomApplicationViewModel)
            {
                NameTextBox.Style = editableTextBox;
            }
            else
            {
                NameTextBox.Style = immutableTextBox;
            }
        }
    }
}
