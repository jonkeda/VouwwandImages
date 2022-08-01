using CefSharp.Wpf;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class MainViewModel : ViewModel
{

    public MainViewModel(ChromiumWebBrowser? chromiumWebBrowser)
    {
        Images = new ProductsViewModel();

        Web = new WebViewModel(chromiumWebBrowser);

        PriceCalculation = new PriceCalculation();
    }

    public ProductsViewModel Images { get; set; }

    public WebViewModel Web { get; set; }

    public PriceCalculation PriceCalculation { get; set; }

}