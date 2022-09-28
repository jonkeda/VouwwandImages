using System.Threading;
using System.Threading.Tasks;
using VouwwandImages.Robot.Pages;

namespace VouwwandImages.Robot.Scripts;

public class NoGlassScript : Script
{
    public NoGlassScript(Context context) : base(context)
    {
    }

    protected override async Task Steps()
    {
        var glass = new GlassPage(Context);
        var wait = new ConstructorPage(Context);

        await glass.GlassTab.Click();

        await glass.Glazing.Click();

        Thread.Sleep(500);

        await glass.NoGlass.Click();

        await glass.Filter.Click();

        Thread.Sleep(200);

        await glass.NoGlass48Mm.Click();

        // wait for error
        // wait for double calculation

        Thread.Sleep(500);

        await wait.Wait(); // glass.WorkshopWait.WaitForAttribute("style", "display: none;");

        Thread.Sleep(500);
        await glass.GlassWeight.Set("10");

        //await glass.Apply.Click();

        //Thread.Sleep(500);
        // await glass.WorkshopWait.WaitForAttribute("style", "display: none;");
        // await wait.Wait();

        //DimensionPage dimension = new DimensionPage(Context);
        //await dimension.DimensionTab.Click();
    }
}