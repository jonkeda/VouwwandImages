using System.Threading.Tasks;
using VouwwandImages.Robot.Pages;

namespace VouwwandImages.Robot.Scripts;

public class ProductScript : Script
{
    public ProductScript(Context context) : base(context)
    {
    }

    protected override async Task Steps()
    {
        var page = new ProductSelectionPage(Context);
        await page.Product.Click();
        await page.Brand.Click();
        await page.Profile.Click();
    }
}