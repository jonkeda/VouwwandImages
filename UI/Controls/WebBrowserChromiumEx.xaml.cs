using System;
using System.Windows;
using System.Windows.Controls;
using CefSharp;
using CefSharp.Wpf;

namespace VouwwandImages.UI.Controls
{
    public partial class WebBrowserChromiumEx
    {
        public ChromiumWebBrowser? WebBrowser { get; }


        public WebBrowserChromiumEx()
        {
            InitializeComponent();
            try
            {
                //throw new Exception();
                WebBrowser = new ChromiumWebBrowser();
                Grid.Children.Add(WebBrowser);
            }
            catch (Exception ex)
            {
                TextBlock tb = new TextBlock { Text = ex.ToString() };
                Grid.Children.Add(tb);
            }
        }

        public static readonly DependencyProperty WebUrlProperty = DependencyProperty.Register(
            nameof(WebUrl), typeof(string), typeof(WebBrowserChromiumEx), new PropertyMetadata(default(string), UrlCallback));

        public string WebUrl
        {
            get { return (string)GetValue(WebUrlProperty); }
            set { SetValue(WebUrlProperty, value); }
        }
        private static void UrlCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WebBrowserChromiumEx w && !string.IsNullOrEmpty(w.WebUrl))
            {
                try
                {
                    w.WebBrowser.Address = w.WebUrl;
                }
                catch
                {
                    // fail 
                }
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            WebBrowser?.Back();
        }

        private void ForwardClick(object sender, RoutedEventArgs e)
        {
            WebBrowser?.Forward();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            WebBrowser?.Reload();
        }


        private void GoClick(object sender, RoutedEventArgs e)
        {
            WebUrl = Url.Text;
        }

    }
}
