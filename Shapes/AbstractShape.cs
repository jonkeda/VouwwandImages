using SkiaSharp;

namespace VouwwandImages.Shapes
{
    public abstract class AbstractShape
    {
        public abstract void Draw(SKCanvas canvas);

        public abstract (float, float) GetMinimum();

        public abstract (float, float) GetMaximum();
    }
}
