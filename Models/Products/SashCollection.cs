using System;
using System.Collections.ObjectModel;

namespace VouwwandImages.Models.Products;

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
}