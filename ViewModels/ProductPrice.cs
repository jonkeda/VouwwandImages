namespace VouwwandImages.ViewModels
{
    public class ProductPrice
    {
        public ProductPrice(int width, int height, double price)
        {
            Width = width / 1000d;
            Height = height / 1000d;
            Price = price;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
    }
}
