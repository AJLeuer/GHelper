using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace GHelper.View.Text
{
    public sealed partial class ItalicizedTextBlock : ContentControl
    {
        public static readonly DependencyProperty StartingTextProperty = DependencyProperty.Register(
	         nameof (StartingText),
	         typeof (string),
	         typeof (ItalicizedTextBlock),
	         new PropertyMetadata(null));

        public static readonly DependencyProperty ItalicizedTextProperty = DependencyProperty.Register(
	         nameof (ItalicizedText),
	         typeof (string),
	         typeof (ItalicizedTextBlock),
	         new PropertyMetadata(null));
        
        public static readonly DependencyProperty FollowingTextProperty = DependencyProperty.Register(
	         nameof (FollowingText),
	         typeof (string),
	         typeof (ItalicizedTextBlock),
	         new PropertyMetadata(null));
        
        
        public string? StartingText   { get; set; }
        public string? ItalicizedText { get; set; }
        public string? FollowingText  { get; set; }

        public ItalicizedTextBlock()
        {
            this.InitializeComponent();
        }
    }
}
