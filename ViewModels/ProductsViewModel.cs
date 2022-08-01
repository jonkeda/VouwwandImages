using VouwwandImages.Models;
using VouwwandImages.Products.FoldingDoors;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class ProductsViewModel : ViewModel
    {
        public ProductsViewModel()
        {
            Products = new ProductModelCollection();

            VouwwandFactory factory = new VouwwandFactory();
            Products = factory.CreateProducts();
        }

        public ProductModelCollection Products { get; set; }
    }
}
