namespace xofz.UI
{
    public interface ToggleUiV2
        : ToggleUi
    {
        string ToggledColor { get; set; }

        string UntoggledColor { get; set; }

        string PressedColor { get; set; }
    }
}