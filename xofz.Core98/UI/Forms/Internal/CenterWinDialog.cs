// graciously taken from a StackOverflow answer by Hans Passant
// https://stackoverflow.com/questions/2576156/winforms-how-can-i-make-messagebox-appear-centered-on-mainform

namespace xofz.UI.Forms.Internal
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    internal sealed class CenterWinDialog : IDisposable
    {
        public CenterWinDialog(Form owner)
        {
            this.owner = owner;
            owner.BeginInvoke(new MethodInvoker(this.findDialog));
        }

        private void findDialog()
        {
            if (this.numberOfTries < 0)
            {
                return;
            }

            EnumThreadWndProc callback = this.checkWindow;
            if (!EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
            {
                return;
            }

            ++this.numberOfTries;
            if (this.numberOfTries < 10)
            {
                this.owner.BeginInvoke(new MethodInvoker(this.findDialog));
            }
        }

        private bool checkWindow(IntPtr windowHandle, IntPtr lp)
        {
            // Check if <windowHandle> is a dialog
            var sb = new StringBuilder(260);
            GetClassName(windowHandle, sb, sb.Capacity);
            if (sb.ToString() != "#32770")
            {
                return true;
            }

            var formRectangle = new Rectangle(this.owner.Location, this.owner.Size);
            RECT dialogRectangle;
            GetWindowRect(windowHandle, out dialogRectangle);
            MoveWindow(
                windowHandle,
                formRectangle.Left + ((formRectangle.Width - dialogRectangle.Right + dialogRectangle.Left) / 2),
                formRectangle.Top + ((formRectangle.Height - dialogRectangle.Bottom + dialogRectangle.Top) / 2),
                dialogRectangle.Right - dialogRectangle.Left,
                dialogRectangle.Bottom - dialogRectangle.Top,
                true);
            return false;
        }

        public void Dispose()
        {
            this.numberOfTries = -1;
        }

        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        private int numberOfTries;
        private readonly Form owner;

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        private struct RECT
        {
#pragma warning disable 649
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
#pragma warning restore 649
        }
    }
}
