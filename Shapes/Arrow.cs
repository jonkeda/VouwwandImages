using SkiaSharp;

namespace VouwwandImages.Shapes;

public class Arrow : AbstractShape
{
    public Arrow() { }

    public Arrow(float left, float top, float length, ArrowDirection direction, SKPaint paint)
    {
        Left = left;
        Top = top;
        Length = length;
        Direction = direction;
        Paint = paint;
    }

    public ArrowDirection Direction { get; set; }
    public SKPaint Paint { get; set; }
    public float Left { get; set; }
    public float Top { get; set; }
    public float Length { get; set; }

    public override void Draw(SKCanvas canvas)
    {
        if (Direction == ArrowDirection.Left
            || Direction == ArrowDirection.Right)
        {
            canvas.DrawLine(Left, Top, Left + Length, Top, Paint);
        }
        else
        {
            canvas.DrawLine(Left, Top, Left , Top + Length, Paint);
        }

        float size = Length / 5;
        var path = new SKPath();
        if (Direction == ArrowDirection.Left)
        {
            path.AddPoly(new SKPoint[]
            {
                new (Left, Top),
                new (Left + size, Top - size),
                new (Left + size, Top + size),
            });
        }
        else if (Direction == ArrowDirection.Right)
        {
            path.AddPoly(new SKPoint[]
            {
                new (Left + Length, Top),
                new (Left + Length - size, Top - size),
                new (Left + Length - size, Top + size),
            });
        }
        else if (Direction == ArrowDirection.Up)
        {
            path.AddPoly(new SKPoint[]
            {
                new (Left, Top),
                new (Left + size, Top - size),
                new (Left - size, Top - size),
            });
        }
        else if (Direction == ArrowDirection.Down)
        {
            path.AddPoly(new SKPoint[]
            {
                new (Left, Top + Length),
                new (Left + size, Top + Length - size),
                new (Left - size, Top + Length - size),
            });
        }

        canvas.DrawPath(path, Paint);

    }

    public override (float, float) GetMaximum()
    {
        return (Left + Length,
            Top);
    }

    public override (float, float) GetMinimum()
    {
        return (Left,
            Top);
    }

}