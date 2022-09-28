using System;
using System.Threading.Tasks;

namespace VouwwandImages.Robot;

public abstract class Script
{
    protected Context Context { get; }

    protected Script(Context context)
    {
        Context = context;
    }

    public async Task Run()
    {
        try
        {
            await Steps();
        }
        catch (Exception e)
        {

        }
    }

    protected abstract Task Steps();

}