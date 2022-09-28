namespace VouwwandImages.Robot;

public abstract class Page
{
    protected Context Context { get; }

    protected Page(Context context)
    {
        Context = context;
    }

}