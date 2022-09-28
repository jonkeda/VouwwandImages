using VouwwandImages.Robot.Selectors;

namespace VouwwandImages.Robot.Pages;

public class GlassPage : Page
{
    public GlassPage(Context context) : base(context)
    {
        GlassTab = new Control(Context, new ByHref("#menu_sash_0-3"));
        Glazing = new Control(Context, new ByTagAttribute("div", "data-configurator-target", "[CONFIGS][CONFIG][0][GLAZING]"));
        NoGlass = new Control(Context, new ByTagAttribute("label", "for", "BEZ_SZYBY"));
        Filter = new Control(context, new ByClass("glass-filter-button"));
        NoGlass48Mm = new Control(Context, new ByTagAttribute("div", "data-value", "00/BEZ_SZYBY_48"));
        GlassWeight = new Control(Context, new ById("WORKSHOP[CONFIGS][CONFIG][0][GLAZING_OPTION][0][PSZ_waga_bsz]"));
        Apply = new Control(Context, new ById("sendConfiguratorModificationButton"));
    }

    public Control Apply { get; set; }

    public Control GlassTab { get; }
    public Control Glazing { get; }

    public Control Filter { get; }

    public Control GlassWeight { get; }

    /* glas types */
    public Control NoGlass { get; }

    /* glass sizes */
    public Control NoGlass48Mm { get; }

}