namespace VouwwandImages.Models.Products;

public class Window
{
    public Window(float width, float height)
    {
        Width = width;
        Height = height;
    }

    public Window(float width, float height,
        FoldHorizontal foldHorizontal)
    {
        Width = width;
        Height = height;
        FoldHorizontal = foldHorizontal;
    }

    public Window(float width, float height,
        SwingHorizontal swingHorizontal)
    {
        Width = width;
        Height = height;
        SwingHorizontal = swingHorizontal;
    }

    public Window(float width, float height,
        SwingHorizontal swingHorizontal, SwingVertical swingVertical)
    {
        Width = width;
        Height = height;
        SwingHorizontal = swingHorizontal;
        SwingVertical = swingVertical;
    }
    
    public Window(float width, float height, 
        SwingHorizontal swingHorizontal, SwingVertical swingVertical, 
        string uid, string name)
    {
        Width = width;
        Height = height;
        SwingHorizontal = swingHorizontal;
        SwingVertical = swingVertical;
        Uid = uid;
        Name = name;
    }

    public Window(float width, float height,
        SwingHorizontal swingHorizontal, SwingVertical swingVertical, SwingDirection direction,
        string uid, string name)
    {
        Width = width;
        Height = height;
        SwingHorizontal = swingHorizontal;
        SwingVertical = swingVertical;
        SwingDirection = direction;
        Uid = uid;
        Name = name;
    }

    public float Width { get; set; }
        
    public float Height { get; set; }

    public SwingHorizontal SwingHorizontal { get; set; }

    public SwingVertical SwingVertical { get; set; }

    public SwingDirection SwingDirection { get; set; }

    public SlideHorizontal SlideHorizontal { get; set; }

    public SlideVertical SlideVertical { get; set; }

    public FoldHorizontal FoldHorizontal { get; set; }
    public bool FirstFold { get; set; }

    public string Uid { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}