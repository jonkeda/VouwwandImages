using System;
using System.Collections.ObjectModel;

namespace VouwwandImages.Models.ProductDrawings;

public class SashCollection : Collection<Sash>
{
    public (float width, float height) GetMaximum()
    {
        float maxWidth = 0;
        float maxHeight = 0;
        foreach (Sash sash in this)
        {
            var (width, height) = sash.GetMaximum();
            maxHeight = Math.Max(maxHeight, height);
            maxWidth += width;
        }
        return (maxWidth, maxHeight);
    }

    public (float width, float height) GetMinimum()
    {
        float minWidth = 0;
        float minHeight = 0;
        foreach (Sash sash in this)
        {
            var (width, height) = sash.GetMinimum();
            minHeight = Math.Min(minHeight, height);
            minWidth += width;
        }
        return (minWidth, minHeight);
    }
}