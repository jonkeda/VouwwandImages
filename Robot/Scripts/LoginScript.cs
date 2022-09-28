using System.Threading.Tasks;
using VouwwandImages.Robot.Pages;

namespace VouwwandImages.Robot.Scripts;

public class LoginScript : Script
{
    public LoginScript(Context context) : base(context)
    {
    }

    protected override async Task Steps()
    {
        LoginPage page = new LoginPage(Context);

        await page.CookieAccept.Click();
        await page.UserName.Set("info@kozijnkopen.com");

    }
}