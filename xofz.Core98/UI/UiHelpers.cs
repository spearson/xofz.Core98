namespace xofz.UI
{
    public static class UiHelpers
    {
        public static void Write(Ui ui, Action writer)
        {
            ui.WriteFinished.Reset();
            if (ui.Root.InvokeRequired)
            {
                ui.Root.BeginInvoke((Action)(() =>
                {
                    writer();
                    ui.WriteFinished.Set();
                }), new object[0]);
                return;
            }

            writer();
            ui.WriteFinished.Set();
        }

        public static T Read<T>(Ui ui, Func<T> valueReader)
        {
            var value = default(T);
            if (ui.Root.InvokeRequired)
            {
                ui.Root.Invoke((Action)(() => value = valueReader()), new object[0]);
                return value;
            }

            return valueReader();
        }
    }
}
