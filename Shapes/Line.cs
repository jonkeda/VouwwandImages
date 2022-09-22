using System;
using SkiaSharp;

namespace VouwwandImages.Shapes;

public class Line : AbstractShape
{
    public Line() { }

    public Line(float x1, float y1, float x2, float y2, SKPaint paint)
    {
        Y1 = y1;
        X1 = x1;
        Y2 = y2;
        X2 = x2;
        Paint = paint;
    }

    public SKPaint Paint { get; set; }
    public float X1 { get; set; }
    public float Y1 { get; set; }
    public float Y2 { get; set; }
    public float X2 { get; set; }

    public override void Draw(SKCanvas canvas)
    {
        canvas.DrawLine(X1, Y1, X2, Y2, Paint);
    }

    public override (float, float) GetMaximum()
    {
        return (Math.Max(X1, X2),
            Math.Max(Y1, Y2));
    }

    public override (float, float) GetMinimum()
    {
        return (Math.Min(X1, X2),
            Math.Min(Y1, Y2));
    }

}