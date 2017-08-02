namespace xofz.UI
{
    using System;

    public interface ToggleUi : LabeledUi
    {
        event Action<ToggleUi> Tapped;

        bool Toggled { get; set; }

        bool Visible { get; set; }
    }
}
