using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class DimensionPage : Page
{
    public DimensionPage(Context context) : base(context)
    {
        DimensionTab = new Control(Context, new ByHref("#menu_sash_0-1"));

        Width = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][WIDTH]"));
        Height = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][HEIGHT]"));
        Apply = new Control(Context, new ById("sendConfiguratorModificationButton"));
        ErrorMessage = new Control(Context, new ById("configuratorErrorMessage"), false);
        // Price = new Control(Context, new ById());
        PriceNetto = new Control(Context, new ById("workshop_price"));

        WithRalColours = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][WITH_RAL_COLOURS]")); // 0
        Colour = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][COLOUR]")); // RAL_7016
        SashColour = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][SASH_COLOUR]"));

        FittingColour = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][FITTING_COLOUR]"));
        FillingInnerColour = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][FILLING_INNER_COLOUR]"));
        FillingOuterColour = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][FILLING_OUTER_COLOUR]"));

        RalInnerFrame = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_INNER_FRAME]"));
        RalOuterFrame = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_OUTER_FRAME]"));
        RalInnerSash = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_INNER_SASH]"));
        RalOuterSash = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_OUTER_SASH]"));

        RalInnerFitting = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_INNER_FITTING]"));
        RalOuterFitting = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_OUTER_FITTING]"));
        RalInnerPeripheral = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_INNER_PERIPHERAL]"));
        RalOuterPeripheral = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][RAL_OUTER_PERIPHERAL]"));
    }

    public Control DimensionTab { get; }

    public Control Width { get; }

    public Control Height { get; }

    public Control Apply { get; }

    public Control ErrorMessage { get; }

    // public Control Price { get; }

    public Control PriceNetto { get; }

    #region

    public Control WithRalColours { get; }
    public Control Colour { get; }
    public Control SashColour { get; }

    public Control FittingColour { get; }
    public Control FillingInnerColour { get; }
    public Control FillingOuterColour { get; }

    public Control RalInnerFrame { get; }
    public Control RalOuterFrame { get; }
    public Control RalInnerSash { get; }
    public Control RalOuterSash { get; }

    public Control RalInnerFitting { get; }
    public Control RalOuterFitting { get; }
    public Control RalInnerPeripheral { get; }
    public Control RalOuterPeripheral { get; }

    #endregion

}