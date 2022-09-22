using SkiaSharp;

namespace VouwwandImages.Shapes;

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

    public override (float, float) GetMinimum()
    {
        return (Left,
            Top);
    }

}