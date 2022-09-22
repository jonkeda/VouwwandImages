using System;
using System.Collections.ObjectModel;

namespace VouwwandImages.Shapes;

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

    public (float, float) GetMinimum()
    {
        float minWidth = 0;
        float minHeight = 0;

        foreach (var shape in this)
        {
            var (width, height) = shape.GetMinimum();
            minWidth = Math.Min(minWidth, width);
            minHeight = Math.Min(minHeight, height);
        }

        return (minWidth, minHeight);
    }
}