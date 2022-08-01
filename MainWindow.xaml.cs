using System.Windows;
using VouwwandImages.ViewModels;

namespace VouwwandImages
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(ChromiumBrowserEx.WebBrowser);
        }
    }
}
