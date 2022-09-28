namespace VouwwandImages.Robot.Selectors;

public class ById : By
{
    public string Id;

    public ById(string id)
    {
        Id = id;
    }

    public override string ExistsSelector()
    {
        return $"document.getElementById('{Id}') != null";

    }

    public override string GetSelector()
    {
        return $"document.getElementById('{Id}')";
    }
}