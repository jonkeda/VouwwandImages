using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class DimensionPage : Page
{
    public DimensionPage(Context context) : base(context)
    {
        DimensionTab = new Control(Context, new ByHref("#menu_sash_0-1"));

        Width = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][WIDTH]"));
        Height = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][HEIGHT]"));
        Apply = new  Control(Context, new ById("sendConfiguratorModificationButton"));
        ErrorMessage = new Control(Context, new ById("configuratorErrorMessage"), false);
        // Price = new Control(Context, new ById());
        PriceNetto = new Control(Context, new ById("workshop_price"));
    }

    public Control DimensionTab { get; }

    public Control Width { get; }

    public Control Height { get; }

    public Control Apply { get; }

    public Control ErrorMessage { get; }

    // public Control Price { get; }

    public Control PriceNetto { get; }
}