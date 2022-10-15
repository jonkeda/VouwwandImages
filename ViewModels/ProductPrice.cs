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

        public ProductPrice(double width, double height, double price)
        {
            Width = width;
            Height = height;
            Price = price;
        }

        public double Width { get; set; }
        public float WidthF
        {
            get
            {
                return (float) Width;
            }
        }

        public double Height { get; set; }
        public float HeightF
        {
            get
            {
                return (float)Height;
            }
        }


        public double Price { get; set; }
        public float PriceF
        {
            get
            {
                return (float)Price;
            }
        }

        public double Length
        {
            get
            {
                return Height * 2 + Width * 2;
            }
        }
    }
}
