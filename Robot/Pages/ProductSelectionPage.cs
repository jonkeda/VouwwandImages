using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class ProductSelectionPage : Page
{
    public ProductSelectionPage(Context context) : base(context)
    {
        Product = new Control(Context, new ByTagAttribute("a", "data-item-id", "MENU_01_01_OKNA"));
        Brand = new Control(Context, new ByTagAttribute("a", "data-item-id", "MENU_01_02_ALU"));
        Profile = new Control(Context, new ByTagAttribute("a", "data-item-id", "M3_MB86N_O"));
    }

    public Control Product { get; }

    public Control Brand { get; }

    public Control Profile { get; }

}