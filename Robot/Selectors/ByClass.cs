namespace VouwwandImages.Robot.Selectors;

public class ByClass : By
{
    public string ClassName { get; }

    public ByClass(string className)
    {
        ClassName = className;
    }

    public override string ExistsSelector()
    {
        return $"document.getElementsByClassName(\"{ClassName}\").length > 0";
    }

    public override string GetSelector()
    {
        return $"document.getElementsByClassName(\"{ClassName}\")[0]";
    }
}