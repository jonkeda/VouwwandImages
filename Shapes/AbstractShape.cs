using System;
using System.Collections.ObjectModel;
using SkiaSharp;

namespace VouwwandImages.Shapes
{
    public class ShapeCollection : Collection<AbstractShape>
    {
        public (float, float) GetMaximum()
        {
            float maxWidth = 0;
            float maxHeight = 0;

            foreach (var shape in this)
            {
                var (width, height) = shape.GetMaximum();
                maxWidth = Math.Max(maxWidth, width);
                maxHeight = Math.Max(maxHeight, height);
            }

            return (maxWidth, maxHeight);
        }

    }

    public abstract class AbstractShape
    {
        public abstract void Draw(SKCanvas canvas);

        public abstract (float, float) GetMaximum();
    }

    public class Bitmap : AbstractShape
    {
        public Bitmap() { }

        public Bitmap(float left, float top, float width, float height, SKPaint paint, SKBitmap image)
        {
            Top = top;
            Left = left;
            Height = height;
            Width = width;
            Paint = paint;
            Image = image;
        }

        public SKBitmap Image { get; set; }
        public SKPaint Paint { get; set; }
        public float Top { get; set; }
        public float Left { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public override void Draw(SKCanvas canvas)
        {
            SKRect rect = new SKRect(Left, Top, Left + Width, Top + Height);
            SKRect rectBitmap = new SKRect(0, 0, Image.Width, Image.Height);

            canvas.DrawBitmap(Image, rectBitmap, rect, Paint);
            // canvas.DrawRect(Left + StrokeWidth / 2, Top + StrokeWidth / 2, Width - StrokeWidth, Height - StrokeWidth, Paint);

        }

        public override (float, float) GetMaximum()
        {
            return (Left + Width,
                    Top + Height);
        }

    }

}
