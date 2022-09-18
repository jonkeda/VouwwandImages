using System;
using SkiaSharp;
using System.Windows.Media.Media3D;

namespace VouwwandImages.Shapes;

public class Path : AbstractShape
{
    public Path()
    {
        Points = new SKPoint[0];
    }

    public Path(SKPoint p1, SKPoint p2, SKPoint p3, SKPoint p4, 
        SKPaint strokePaint, SKPaint background)
    {
        Points = new SKPoint[4];
        Points[0] = p1;
        Points[1] = p2;
        Points[2] = p3;
        Points[3] = p4;
        StrokePaint = strokePaint;
        Background = background;
    }

    public SKPoint[] Points { get; set; }
    public SKPaint StrokePaint { get; set; }
    public SKPaint Background { get; set; }
    public float StrokeWidth { get; set; }

    public override void Draw(SKCanvas canvas)
    {
        SKPath path = new SKPath();
        path.AddPoly(Points);
        canvas.DrawPath(path, StrokePaint);
        canvas.DrawPath(path, Background);
    }

    public override (float, float) GetMaximum()
    {
        float maxX = 0;
        float maxY = 0;
        foreach (SKPoint point in Points)
        {
            maxX = Math.Max(maxX, point.X);
            maxY = Math.Max(maxY, point.Y);
        }
        return (maxX, maxY);
    }
}

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
}