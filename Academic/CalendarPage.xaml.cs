using System;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Academic
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
            WebContent.Navigate(new Uri("https://www.baidu.com"));
        }

        private void WebContent_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            LoadingBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void WebContent_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            LoadingBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void WebContent_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            LoadingBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
