using CefSharp.Wpf;
using VouwwandImages.Database;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class MainViewModel : ViewModel
{

    public MainViewModel(ChromiumWebBrowser? chromiumWebBrowser)
    {
        VouwwandenDbContext dbContext = App.GetService<VouwwandenDbContext>();

        Web = new WebViewModel(chromiumWebBrowser, dbContext);

        Translator = App.GetService<TranslatorViewModel>();
        Images = App.GetService<ImagesViewModel>();
        Pdf = App.GetService<PdfViewModel>();
    }

    public TranslatorViewModel Translator { get; } 

    public ImagesViewModel Images { get; }

    public WebViewModel Web { get; }

    public PdfViewModel Pdf { get; }

}