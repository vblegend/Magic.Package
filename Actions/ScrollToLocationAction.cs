using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Magic.Actions
{
    /// <summary>
    /// 定位位置
    /// </summary>
    public class ScrollToLocationAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToLocationAction), new PropertyMetadata(null));
        /// <summary>
        /// 目标 ScrollViewer
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            ScrollViewer.ScrollToHorizontalOffset(0);
            ScrollViewer.ScrollToVerticalOffset(0);
        }
    }
}
