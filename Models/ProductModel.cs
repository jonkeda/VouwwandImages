using System.Collections.ObjectModel;
using SkiaSharp;
using VouwwandImages.Shapes;

namespace VouwwandImages.Models
{
    public class ProductModelCollection : Collection<ProductModel>
    {
    }

    public class ProductModel
    {
        public ProductModel()
        {
            Shapes = new ShapeCollection();
        }

        public ShapeCollection Shapes { get; set; }

        public void Draw(SKCanvas canvas)
        {
            foreach (var shape in Shapes)
            {
                shape.Draw(canvas);
            }
        }
    }
}
