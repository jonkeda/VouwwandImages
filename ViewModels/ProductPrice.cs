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
        public int WidthI
        {
            get
            {
                return (int)(Width*1000d);
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
        public float HeightI
        {
            get
            {
                return (int)(Height*1000d);
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

        public double LengthI
        {
            get
            {
                return HeightI * 2 + WidthI * 2;
            }
        }

        public double Square
        {
            get
            {
                return Height * Width;
            }
        }

        public double SquareI
        {
            get
            {
                return HeightI * WidthI;
            }
        }

    }
}
