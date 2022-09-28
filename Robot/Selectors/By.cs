namespace VouwwandImages.Robot.Selectors;

public abstract class By
{
    protected By()
    {
    }

    public abstract string ExistsSelector();

    public abstract string GetSelector();
}