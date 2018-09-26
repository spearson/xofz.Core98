namespace xofz.UI
{
    public interface ToggleUi : LabeledUi
    {
        event Do<ToggleUi> Tapped;

        event Do<ToggleUi> Pressed;

        event Do<ToggleUi> Released;

        bool Toggled { get; set; }

        bool Visible { get; set; }
    }
}
