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

            StandardDefaults();
        }


        public ICommand LoginCommand
        {
            get { return new TargetCommand(Login); }
        }

        private void Login()
        {
            ClickByClass("cookies__button--accept");
            Set("user-login", "info@kozijnkopen.com");

            // SetTextbox("password", "");
        }

        public ICommand NoGlassCommand
        {
            get { return new TargetCommand(NoGlass); }
        }

        private async void NoGlass()
        {
            ClickByHref("#menu_sash_0-3");

            ClickBy("div", "data-configurator-target", "[CONFIGS][CONFIG][0][GLAZING]");

            CheckById("BEZ_SZYBY");

            ClickByClass("glass-filter-button-container");

            ClickBy("div", "data-value", "00/BEZ_SZYBY_48");

            await WaitForStyle("workshop_wait", "display: none;");

            Set("WORKSHOP[CONFIGS][CONFIG][0][GLAZING_OPTION][0][PSZ_waga_bsz]", "20");

            // ClickBy( apply)

            ClickByHref("#menu_sash_0-3");

        }

        private void Set(string id, string text)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').value= '{text}'");
        }

        private void CheckById(string id)
        {
            _browser.ExecuteScriptAsync($"document.getElementById('{id}').check=true");
        }


        private void ClickById(string id)
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

        private void ClickByHref(string href)
        {
            _browser.ExecuteScriptAsync($"document.querySelectorAll(\"a[href='{href}']\")[0].click()");
        }

        private void ClickBy(string tagName, string attribute, string value)
        {
            _browser.ExecuteScriptAsync($"document.querySelectorAll(\"{tagName}[{attribute}='{value}']\")[0].click()");
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
            ClickById("sendConfiguratorModificationButton");

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

        public ICommand FastDefaultsCommand
        {
            get { return new TargetCommand(FastDefaults); }
        }

        private async void FastDefaults()
        {
            MinimumWidth = "1000";
            MaximumWidth = "3000";
            StepWidth = "1000";

            MinimumHeight = "500";
            MaximumHeight = "2500";
            StepHeight = "1000";

        }

        public ICommand StandardDefaultsCommand
        {
            get { return new TargetCommand(StandardDefaults); }
        }

        private async void StandardDefaults()
        {
            MinimumWidth = "1000";
            MaximumWidth = "3500";
            StepWidth = "500";

            MinimumHeight = "1000";
            MaximumHeight = "2500";
            StepHeight = "500";

            Bars = "2";
            Pillars = "2";

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