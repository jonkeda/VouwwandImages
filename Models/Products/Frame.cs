using System.Collections.Generic;

namespace VouwwandImages.Models.Products;

public class Frame
{
    public Frame()
    {

    }

    public Frame(string uid, string name)
    {
        Uid = uid;
        Name = name;
    }

    public Frame(string uid, string name,
        IEnumerable<Window> windows) : this(windows)
    {
        Uid = uid;
        Name = name;
    }

    public Frame(IEnumerable<Window> windows)
    {
        foreach (Window window in windows)
        {
            Name = window.Name;
            Uid = window.Uid;
            Sash row = new();
            row.WindowCollection.Add(window);
            Sashes.Add(row);
        }
    }

    public string Name { get; set; }
    public string Uid { get; set; }

    public Dimensions Dimension { get; set; }

    public SashCollection Sashes { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} ({Uid})";
    }

    public (float width, float height) GetMinimum()
    {
        return Sashes.GetMinimum();
    }

    public (float width, float height) GetMaximum()
    {
        return Sashes.GetMaximum();
    }
}