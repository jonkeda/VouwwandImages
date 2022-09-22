using System;
using SkiaSharp;

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

    public override (float, float) GetMinimum()
    {
        float minX = 0;
        float minY = 0;
        foreach (SKPoint point in Points)
        {
            minX = Math.Min(minX, point.X);
            minY = Math.Min(minY, point.Y);
        }
        return (minX, minY);
    }

}