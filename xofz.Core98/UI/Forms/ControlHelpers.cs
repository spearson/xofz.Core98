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

            var controls = container?.Controls;
            if (controls == null)
            {
                return;
            }

            long controlCount;
            Control otherControl;
            const byte
                zero = 0,
                one = 1;
            try
            {
                controlCount = controls.Count;
                otherControl = controls[zero];
            }
            catch
            {
                controlCount = zero;
                otherControl = null;
            }

            if (controlCount == one &&
                ReferenceEquals(
                    control, 
                    otherControl))
            {
                return;
            }

            controls.Clear();
            controls.Add(control);
        }

        public static void SafeReplaceV2(
            Control control,
            Control container)
        {
            if (control == null)
            {
                return;
            }

            var controls = container?.Controls;
            if (controls == null)
            {
                return;
            }

            controls.Clear();
            controls.Add(control);
        }
    }
}
