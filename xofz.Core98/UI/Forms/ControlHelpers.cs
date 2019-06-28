namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public static class ControlHelpers
    {
        public static void SafeReplace(
            Control control, 
            Control container)
        {
            if (control == null)
            {
                return;
            }

            if (container == null)
            {
                return;
            }

            if (container.Controls.Count == 1 &&
                ReferenceEquals(control, container.Controls[0]))
            {
                return;
            }

            container.Controls.Clear();
            container.Controls.Add(control);
        }
    }
}
