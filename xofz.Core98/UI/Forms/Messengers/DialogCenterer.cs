// graciously taken from a StackOverflow answer by Hans Passant
// https://stackoverflow.com/questions/2576156/winforms-how-can-i-make-messagebox-appear-centered-on-mainform

namespace xofz.UI.Forms.Messengers
{
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class DialogCenterer
        : System.IDisposable
    {
        public DialogCenterer(
            Form owner)
        {
            if (owner == null)
            {
                return;
            }

            this.owner = owner;
            if (owner.WindowState == FormWindowState.Minimized)
            {
                // do not try to center on a minimized window
                return;
            }

            owner.BeginInvoke(
                new MethodInvoker(this.findDialog));
        }

        protected virtual void findDialog()
        {
            var tc = this.tryCount;
            const byte zero = 0;
            if (tc < zero)
            {
                return;
            }

            EnumThreadWndProc callback = this.checkWindow;
            if (!EnumThreadWindows(
                    GetCurrentThreadId(),
                    callback,
                    System.IntPtr.Zero))
            {
                return;
            }

            ++tc;
            this.setTryCount(tc);
            const byte maxTryCount = 10;
            if (tc < maxTryCount)
            {
                this.owner.BeginInvoke(
                    (Do)this.findDialog);
            }
        }

        protected virtual bool checkWindow(
            System.IntPtr windowHandle,
            System.IntPtr lp)
        {
            // Check if <windowHandle> is a dialog
            var sb = new StringBuilder(260);
            GetClassName(windowHandle, sb, sb.Capacity);
            if (sb.ToString() != @"#32770")
            {
                return true;
            }

            var formRectangle = new Rectangle(this.owner.Location, this.owner.Size);
            RECT dialogRectangle;
            GetWindowRect(windowHandle, out dialogRectangle);
            MoveWindow(
                windowHandle,
                formRectangle.Left + (formRectangle.Width - dialogRectangle.Right + dialogRectangle.Left) / 2,
                formRectangle.Top + (formRectangle.Height - dialogRectangle.Bottom + dialogRectangle.Top) / 2,
                dialogRectangle.Right - dialogRectangle.Left,
                dialogRectangle.Bottom - dialogRectangle.Top,
                true);
            return false;
        }

        void System.IDisposable.Dispose()
        {
            const short minusOne = -1;
            this.setTryCount(minusOne);
        }

        protected virtual void setTryCount(
            short tryCount)
        {
            this.tryCount = tryCount;
        }

        protected delegate bool EnumThreadWndProc(
            System.IntPtr hWnd,
            System.IntPtr lp);

        protected short tryCount;
        protected readonly Form owner;

        [DllImport(@"user32.dll")]
        protected static extern bool EnumThreadWindows(
            int tid,
            EnumThreadWndProc callback,
            System.IntPtr lp);

        [DllImport(@"kernel32.dll")]
        protected static extern int GetCurrentThreadId();

        [DllImport(@"user32.dll")]
        protected static extern int GetClassName(
            System.IntPtr hWnd,
            StringBuilder buffer,
            int buflen);

        [DllImport(@"user32.dll")]
        protected static extern bool GetWindowRect(
            System.IntPtr hWnd,
            out RECT rc);

        [DllImport(@"user32.dll")]
        protected static extern bool MoveWindow(
            System.IntPtr hWnd,
            int x,
            int y,
            int w,
            int h,
            bool repaint);

        protected struct RECT
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