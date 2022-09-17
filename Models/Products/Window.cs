namespace VouwwandImages.Models.Products;

public class Window
{
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

    public float Width { get; set; }
        
    public float Height { get; set; }

    public SwingHorizontal SwingHorizontal { get; set; }

    public SwingVertical SwingVertical { get; set; }

    public string Uid { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}