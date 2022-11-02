using System.Threading.Tasks;
using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class ConstructorPage : Page
{
    public ConstructorPage(Context context) : base(context)
    {
        WorkshopWait = new Control(Context, new ById("workshop_wait"));

    }

    public Control WorkshopWait { get; }

    public Control Library { get; }

    public Control Item { get; }

    public async Task Wait()
    {
        await WorkshopWait.WaitForAttribute("style", "display: none;");
    }
}