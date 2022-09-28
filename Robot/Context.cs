using CefSharp.Wpf;

namespace VouwwandImages.Robot
{
    public class Context
    {
        public readonly ChromiumWebBrowser Browser;

        public int WaitTime { get; } = 100;

        public int WaitCount { get; } = 10;

        public Context(ChromiumWebBrowser browser)
        {
            Browser = browser;
        }
    }
}
