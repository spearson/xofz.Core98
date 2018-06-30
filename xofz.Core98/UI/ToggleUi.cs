namespace xofz.UI
{
    using System;

    public interface ToggleUi : LabeledUi
    {
        event Action<ToggleUi> Tapped;

        event Action<ToggleUi> Pressed;

        event Action<ToggleUi> Released;

        bool Toggled { get; set; }

        bool Visible { get; set; }
    }
}
