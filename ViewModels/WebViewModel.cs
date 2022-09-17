using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class WebViewModel : PriceCalculation
    {
        private readonly ChromiumWebBrowser _browser;

        public WebViewModel(ChromiumWebBrowser browser)
        {
            _browser = browser;

            _minimumWidth = "2000";
            _maximumWidth = "4000";
            _stepWidth = "500";

            _minimumHeight = "2000";
            _maximumHeight = "2500";
            _stepHeight = "250";
        }


        public ICommand LoginCommand
        {
            get { return new TargetCommand(Login); }
        }

        private void Login()
        {
            ClickByClass("cookies__button--accept");
            Set("user-login", "Tvpbeheer@gmail.com");

            // SetTextbox("password", "");
        }

        private void Set(string id, string text)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').value= '{text}'");
        }

        private void Click(string id)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').click()");
        }

        private void Focus(string id)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').focus()");
        }

        private void Blur(string id)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').blur()");
        }

        private void ClickByClass(string className)
        {
            _browser.ExecuteScriptAsync($"document.getElementsByClassName('{className}')[0].click()");
        }

        private void Disabled(string id, bool value)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').disabled={value.ToString().ToLower()}");
        }

        private async Task<string> GetStyle(string id)
        {
            var response = await _browser.EvaluateScriptAsync($"document.getElementById('{id}').getAttribute('style')");

            return (string) response.Result;
        }

        private async Task<string?> GetId(string id)
        {
            var response = await _browser.EvaluateScriptAsync($"document.getElementById('{id}').getAttribute('id')");

            return (string?) response.Result;
        }

        private async Task<string> GetValue(string id)
        {
            var response = await _browser.EvaluateScriptAsync($"document.getElementById('{id}').innerText");

            return (string) response.Result;
        }

        private async Task WaitForStyle(string id, string style)
        {
            DateTime time = DateTime.Now;
            time = time.AddMinutes(1);
            while (time > DateTime.Now)
            {
                string foundStyle = await GetStyle(id);
                if (foundStyle == style)
                {
                    return;
                }

                Thread.Sleep(100);
            }
        }

        private async Task ScrapePrices()
        {
            PriceInput = "";
            _stop = false;

            StringBuilder sb = new StringBuilder();

            int maxHeight = int.Parse(MaximumHeight);
            int minHeight = int.Parse(MinimumHeight);
            int stepHeight = int.Parse(StepHeight);
            int maxWidth = int.Parse(MaximumWidth);
            int minWidth = int.Parse(MinimumWidth);
            int stepWidth = int.Parse(StepWidth);

            for (int width = minWidth; width <= maxWidth; width += stepWidth)
            {
                for (int height = minHeight; height <= maxHeight; height += stepHeight)
                {
                    await ScrapePrice(sb, width, height);
                    if (_stop)
                    {
                        return;
                    }
                }
            }

            PriceInput = sb.ToString();
        }

        private async Task ScrapePrice(StringBuilder sb, int width, int height)
        {
            Set("WORKSHOP[CONFIGS][CONFIG][0][WIDTH]", width.ToString());
            Set("WORKSHOP[CONFIGS][CONFIG][0][HEIGHT]", height.ToString());
            Disabled("sendConfiguratorModificationButton", false);
            Click("sendConfiguratorModificationButton");

            await WaitForStyle("workshop_wait", "display: none;");

            string? id = await GetId("configuratorErrorMessage");
            if (id == null)
            {
                string price = await GetValue("workshop_price");

                sb.AppendLine($"{width}\t{height}\t{price.Replace("€", "").Replace(",", "").Replace(".", ",")}");
            }

            Thread.Sleep(500);
        }

        #region Stepping

        private string _minimumWidth;
        private string _maximumWidth;
        private string _stepWidth;

        private string _minimumHeight;
        private string _maximumHeight;
        private string _stepHeight;

        private volatile bool _stop;

        public string MinimumWidth
        {
            get { return _minimumWidth; }
            set { SetProperty(ref _minimumWidth, value); }
        }

        public string MaximumWidth
        {
            get { return _maximumWidth; }
            set { SetProperty(ref _maximumWidth, value); }
        }

        public string StepWidth
        {
            get { return _stepWidth; }
            set { SetProperty(ref _stepWidth, value); }
        }

        public string MinimumHeight
        {
            get { return _minimumHeight; }
            set { SetProperty(ref _minimumHeight, value); }
        }

        public string MaximumHeight
        {
            get { return _maximumHeight; }
            set { SetProperty(ref _maximumHeight, value); }
        }

        public string StepHeight
        {
            get { return _stepHeight; }
            set { SetProperty(ref _stepHeight, value); }
        }

        public ICommand CalculateCommand
        {
            get { return new TargetCommand(Calculate); }
        }

        private async void Calculate()
        {
            await ScrapePrices();
            CalculatePrices();
        }

        public ICommand StopCommand
        {
            get { return new TargetCommand(Stop); }
        }

        private void Stop()
        {
            _stop = true;
        }


        #endregion

    }
}