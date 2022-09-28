namespace VouwwandImages.Robot.Selectors;

public class ByTagAttribute : By
{
    public string TagName { get; }
    public string Attribute { get; }
    public string Value { get; }

    public ByTagAttribute(string attribute, string value)
    {
        Attribute = attribute;
        Value = value;
    }

    public ByTagAttribute(string tagName, string attribute, string value)
    {
        TagName = tagName;
        Attribute = attribute;
        Value = value;
    }
    public override string ExistsSelector()
    {
        return $"document.querySelectorAll(\"{TagName}[{Attribute}='{Value}']\").length > 0";
    }

    public override string GetSelector()
    {
        return $"document.querySelectorAll(\"{TagName}[{Attribute}='{Value}']\")[0]";
    }
}