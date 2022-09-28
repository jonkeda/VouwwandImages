using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class LoginPage : Page
{
    public LoginPage(Context context) : base(context)
    {
        CookieAccept = new Control(Context, new ByClass("cookies__button--accept"));

        UserName = new Control(Context, new ById("user-login"));
    }

    public Control CookieAccept { get; }

    public Control UserName { get; }
}