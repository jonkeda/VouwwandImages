using System.Windows.Input;
using CefSharp.Wpf;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class MainViewModel : ViewModel
{

    public MainViewModel(ChromiumWebBrowser? chromiumWebBrowser)
    {
        Web = new WebViewModel(chromiumWebBrowser);

        PriceCalculation = new PriceCalculation();
    }

    public ImagesViewModel Images { get; set; } = new();

    public WebViewModel Web { get; set; }

    public PriceCalculation PriceCalculation { get; set; }

}