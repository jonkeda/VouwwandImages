using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CefSharp.Wpf;
using VouwwandImages.Database;
using VouwwandImages.Robot;
using VouwwandImages.Robot.Pages;
using VouwwandImages.Robot.Scripts;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class WebViewModel : PriceCalculation
    {
        private Context Context { get; }

        public WebViewModel(ChromiumWebBrowser browser, 
            VouwwandenDbContext dbContext) : base(dbContext)
        {
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

            decimal maxHeight = ScrapeMaximumHeight;
            decimal minHeight = ScrapeMinimumHeight;
            decimal stepHeight = StepHeight;
            decimal maxWidth = ScrapeMaximumWidth;
            decimal minWidth = ScrapeMinimumWidth;
            decimal stepWidth = StepWidth;

            for (decimal width = minWidth; width <= maxWidth; width += stepWidth)
            {
                for (decimal height = minHeight; height <= maxHeight; height += stepHeight)
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

        private async Task ScrapePrice(StringBuilder sb, decimal width, decimal height)
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

        private decimal _scrapeMinimumWidth;
        private decimal _scrapeMaximumWidth;
        private decimal _stepWidth;

        private decimal _scrapeMinimumHeight;
        private decimal _scrapeMaximumHeight;
        private decimal _stepHeight;

        private volatile bool _stop;

        public decimal ScrapeMinimumWidth
        {
            get { return _scrapeMinimumWidth; }
            set { SetProperty(ref _scrapeMinimumWidth, value); }
        }

        public decimal ScrapeMaximumWidth
        {
            get { return _scrapeMaximumWidth; }
            set { SetProperty(ref _scrapeMaximumWidth, value); }
        }

        public decimal StepWidth
        {
            get { return _stepWidth; }
            set { SetProperty(ref _stepWidth, value); }
        }

        public decimal ScrapeMinimumHeight
        {
            get { return _scrapeMinimumHeight; }
            set { SetProperty(ref _scrapeMinimumHeight, value); }
        }

        public decimal ScrapeMaximumHeight
        {
            get { return _scrapeMaximumHeight; }
            set { SetProperty(ref _scrapeMaximumHeight, value); }
        }

        public decimal StepHeight
        {
            get { return _stepHeight; }
            set { SetProperty(ref _stepHeight, value); }
        }

        public ICommand ScrapePriceCommand
        {
            get { return new TargetCommand(ScrapePrice); }
        }

        private async void ScrapePrice()
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
            ScrapeMinimumWidth = 1000;
            ScrapeMaximumWidth = 3000;
            StepWidth = 1000;

            ScrapeMinimumHeight = 500;
            ScrapeMaximumHeight = 2500;
            StepHeight = 1000;

        }

        public ICommand StandardDefaultsCommand
        {
            get { return new TargetCommand(StandardDefaults); }
        }

        private async void StandardDefaults()
        {
            ScrapeMinimumWidth = 1000;
            ScrapeMaximumWidth = 3500;
            StepWidth = 500;

            ScrapeMinimumHeight = 1000;
            ScrapeMaximumHeight = 2500;
            StepHeight = 500;

            Bars = 2;
            Pillars = 2;

        }

        public ICommand StopCommand
        {
            get { return new TargetCommand(Stop); }
        }

        public bool CalculateMatrix { get; set; }

        public bool CalculateOutlines { get; set; }

        private void Stop()
        {
            _stop = true;
        }


        #endregion

    }
}