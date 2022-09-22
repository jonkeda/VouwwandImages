namespace VouwwandImages.Models.Products;

public class Sash
{
    public WindowCollection WindowCollection { get; set; } = new();

    public (float width, float height) GetMaximum()
    {
        return WindowCollection.GetMaximum();
    }

    public (float width, float height) GetMinimum()
    {
        return WindowCollection.GetMinimum();
    }
}