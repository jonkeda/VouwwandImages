using System.Collections.Generic;

namespace VouwwandImages.Models.Products;

public class Frames
{
    public Frames()
    {

    }

    public Frames(IEnumerable<Frame> frames)
    {
        foreach (Frame frame in frames)
        {
            FrameItems.Add(frame);
        }
    }

    public Frames(Dimensions dimension, IEnumerable<Window> windows)
    {
        foreach (Window window in windows)
        {
            Frame frame = new Frame
            {
                Dimension = dimension,
                Name = window.Name,
                Uid = window.Uid
            };
            Sash row = new();
            row.WindowCollection.Add(window);
            frame.Sashes.Add(row);
            FrameItems.Add(frame);
        }
    }

    public Frames(IEnumerable<Window> windows)
    {
        foreach (Window window in windows)
        {
            Frame frame = new Frame
            {
                Name = window.Name,
                Uid = window.Uid
            };
            Sash row = new();
            row.WindowCollection.Add(window);
            frame.Sashes.Add(row);
            FrameItems.Add(frame);
        }
    }

    public string Name { get; set; }
    public string Uid { get; set; }

    public FrameCollection FrameItems { get; set; } = new();
    public string Directory { get; set; }

    public override string ToString()
    {
        return Name;
    }

}