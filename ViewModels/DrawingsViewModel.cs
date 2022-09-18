using VouwwandImages.Models.Products;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class DrawingsViewModel : ViewModel
    {
        public DrawingsViewModel()
        {
            Drawings = new DrawingModelCollection();

/*            DrawingFactory factory = new DrawingFactory();
            Drawings = factory.CreateProducts();
*/        }

        public DrawingModelCollection Drawings { get; set; }
    }
}
