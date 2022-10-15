using CefSharp.Wpf;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class MainViewModel : ViewModel
{

    public MainViewModel(ChromiumWebBrowser? chromiumWebBrowser)
    {
        Web = new WebViewModel(chromiumWebBrowser);
    }

    public TranslatorViewModel Translator { get; set; } = new();

    public ImagesViewModel Images { get; set; } = new();

    public WebViewModel Web { get; set; }

}