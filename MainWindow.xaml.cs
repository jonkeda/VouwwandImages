using VouwwandImages.ViewModels;

namespace VouwwandImages
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(ChromiumBrowserEx.WebBrowser);
        }
    }
}
