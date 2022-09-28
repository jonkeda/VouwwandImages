namespace VouwwandImages.Robot.Selectors;

public class ByHref : ByTagAttribute
{
    public ByHref(string href) : base("a", "href", href)
    {
    }
}