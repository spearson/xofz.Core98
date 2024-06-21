namespace xofz.UI
{
    public static class UiHelpers
    {
        public static T Read<T>(
            Ui ui,
            Gen<T> read)
        {
            if (read == null)
            {
                return default;
            }

            var root = ui?.Root;
            if (root == null)
            {
                return default;
            }

            if (root.InvokeRequired)
            {
                T t = default;
                root.Invoke((Do)(() => t = read()), null);
                return t;
            }

            return read();
        }

        public static void Write(
            Ui ui,
            Do write)
        {
            if (write == null)
            {
                return;
            }

            var root = ui?.Root;
            if (root == null)
            {
                return;
            }

            if (root.InvokeRequired)
            {
                root.BeginInvoke(write, null);
                return;
            }

            write();
        }

        public static void WriteSync(
            Ui ui,
            Do write)
        {
            if (write == null)
            {
                return;
            }

            var root = ui?.Root;
            if (root == null)
            {
                return;
            }

            if (root.InvokeRequired)
            {
                root.Invoke(write, null);
                return;
            }

            write();
        }
    }
}