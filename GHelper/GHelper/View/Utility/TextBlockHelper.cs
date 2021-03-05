using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace GHelper.View.Utility
{
    /// <summary>
    /// Code credit StackOverflow user Vimes: https://stackoverflow.com/a/53661386/2562973 
    /// </summary>
    public class TextBlockHelper
    {
        public static bool GetTrimRuns(TextBlock textBlock)             => (bool)textBlock.GetValue(TrimRunsProperty);
        public static void SetTrimRuns(TextBlock textBlock, bool value) => textBlock.SetValue(TrimRunsProperty, value);

        public static readonly DependencyProperty TrimRunsProperty =
            DependencyProperty.RegisterAttached("TrimRuns", typeof(bool), typeof(TextBlockHelper),
                                                new PropertyMetadata(false, OnTrimRunsChanged));

        private static void OnTrimRunsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBlock = d as TextBlock;
            if (textBlock is not null) textBlock.Loaded += OnTextBlockLoaded;
        }

        static void OnTextBlockLoaded(object sender, RoutedEventArgs eventInfo)
        {
            var textBlock = sender as TextBlock;
            if (textBlock is not null)
            {
                textBlock.Loaded -= OnTextBlockLoaded;
                var runs = textBlock.Inlines.OfType<Run>().ToList();
                
                foreach (var run in runs)
                    run.Text = TrimOne(run.Text);
            }

        }

        private static string TrimOne(string text)
        {
            if (text.FirstOrDefault() == ' ')
                text = text.Substring(1);
            if (text.LastOrDefault() == ' ')
                text = text.Substring(0, text.Length - 1);

            return text;
        }
    }
}
