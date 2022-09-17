using SkiaSharp;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using VouwwandImages.Shapes;

namespace VouwwandImages.Models.Products
{
    public class DrawingModel
    {
        public DrawingModel()
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

        public void SaveImage(string filename)
        {
            var (width, height) = Shapes.GetMaximum();

            SKBitmap bitmap = new SKBitmap((int)width, (int)height);
            SKCanvas canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);
            Draw(canvas);
            using Stream s = File.Create(filename);
            SKData d = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
            d.SaveTo(s);
        }

    }
}
