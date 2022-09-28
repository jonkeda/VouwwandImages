using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CefSharp.Wpf;
using VouwwandImages.Robot;
using VouwwandImages.Robot.Pages;
using VouwwandImages.Robot.Scripts;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class WebViewModel : PriceCalculation
    {
        private readonly ChromiumWebBrowser _browser;
        private Context Context { get; }

        public WebViewModel(ChromiumWebBrowser browser)
        {
            _browser = browser;
            Context = new Context(browser);

            StandardDefaults();
        }


        public ICommand LoginCommand
        {
            get { return new TargetCommand(Login); }
        }

        private async void Login()
        {
            var script = new LoginScript(Context);
            await script.Run();
        }

        public ICommand ProductCommand
        {
            get { return new TargetCommand(Product); }
        }

        private async void Product()
        {
            var script = new ProductScript(Context);
            await script.Run();
        }

        public ICommand NoGlassCommand
        {
            get { return new TargetCommand(NoGlass); }
        }

        private async void NoGlass()
        {
            var script = new NoGlassScript(Context);
            await script.Run();
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
            var page = new DimensionPage(Context);

            await page.Width.Set(width.ToString());
            await page.Height.Set(height.ToString());
            await page.Apply.SetDisable(false);
            await page.Apply.Click();

            var waitPage = new ConstructorPage(Context);
            await waitPage.Wait();

            string? id = await page.ErrorMessage.GetId();
            if (id == null)
            {
                // string price = await GetValue("workshop_price");
                string? price = await page.PriceNetto.GetValue();

                sb.AppendLine($"{width}\t{height}\t{price?.Replace("€", "").Replace(",", "").Replace(".", ",")}");
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