using System.IO;
using SkiaSharp;
using VouwwandImages.Shapes;

namespace VouwwandImages.Models.ProductDrawings
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
            var (minWidth, minHeight) = GetMinimumAbs();
            canvas.Translate(minWidth, minHeight);
            foreach (var shape in Shapes)
            {
                shape.Draw(canvas);
            }
        }


        public void SaveImage(string filename)
        {
            var (maxWidth, maxHeight) = Shapes.GetMaximum();
            var (minWidth, minHeight) = GetMinimumAbs( );

            SKBitmap bitmap = new SKBitmap((int)maxWidth + (int)minWidth, 
                (int)maxHeight + (int)minHeight);
            SKCanvas canvas = new SKCanvas(bitmap);
            
            canvas.Clear(SKColors.White);
            Draw(canvas);
            using Stream s = File.Create(filename);
            SKData d = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
            d.SaveTo(s);
        }

        private (float, float) GetMinimumAbs()
        {
            var ( minWidth, minHeight) = Shapes.GetMinimum();

            if (minWidth < 0)
            {
                minWidth = -minWidth;
            }
            else
            {
                minWidth = 0;
            }

            if (minHeight < 0)
            {
                minHeight = -minHeight;
            }
            else
            {
                minHeight = 0;
            }

            return (minWidth, minHeight);
        }
    }
}
