using SkiaSharp;
using System.Windows.Media.Media3D;

namespace VouwwandImages.Shapes;

public class Rectangle : AbstractShape
{
    public Rectangle() { }

    public Rectangle(float left, float top, float width, float height, SKPaint paint)
    {
        Top = top;
        Left = left;
        Height = height;
        Width = width;
        Paint = paint;
    }

    public Rectangle(float left, float top, float width, float height, SKPaint paint, float strokeWidth)
        : this(left, top, width, height, paint)
    {
        StrokeWidth = strokeWidth;
    }

    public float Top { get; set; }
    public float Left { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }

    public SKPaint Paint { get; set; }
    public float StrokeWidth { get; set; }

    public override void Draw(SKCanvas canvas)
    {

        canvas.DrawRect(Left + StrokeWidth / 2, Top + StrokeWidth / 2, Width - StrokeWidth, Height - StrokeWidth, Paint);

    }

    public override (float, float) GetMaximum()
    {
        return (Left + StrokeWidth / 2 + Width - StrokeWidth,
                Top + StrokeWidth / 2 + Height - StrokeWidth
            );
    }

    public override (float, float) GetMinimum()
    {
        return (Left,
                Top
            );
    }
}