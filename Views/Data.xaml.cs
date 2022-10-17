using VouwwandImages.ViewModels;

namespace VouwwandImages.Views
{
    public partial class Data
    {
        public Data()
        {
            InitializeComponent();

            DataContext = App.GetService<DataViewModel>();
        }
    }
}
