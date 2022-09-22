using System;
using System.Collections.ObjectModel;

namespace VouwwandImages.Models.Products;

public class WindowCollection : Collection<Window>
{
    public (float width, float height) GetMaximum()
    {
        float width = 0;
        float height = 0;
        foreach (Window window in this)
        {
            width = Math.Max(width, window.Width);
            height += window.Height;
        }

        return (width, height);
    }

    public (float width, float height) GetMinimum()
    {
        float width = 0;
        float height = 0;
        foreach (Window window in this)
        {
            width = Math.Min(width, window.Width);
            height += window.Height;
        }
        return (width, height);
    }

}