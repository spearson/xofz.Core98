namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    using xofz.UI.Forms.Internal;

    public sealed class FormsMessenger : Messenger
    {
        public Ui Subscriber { get; set; }

        Response Messenger.Question(string question)
        {
            var subscriber = this.Subscriber as Form;
            DialogResult result;
            if (subscriber != null)
            {
                using (new CenterWinDialog(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        @"?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                }
            }
            else
            {
                result = MessageBox.Show(
                    question,
                    @"?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            }

            return result == DialogResult.Yes ? Response.Yes : Response.No;
        }

        void Messenger.Inform(string message)
        {
            this.sendMessage(message, MessageBoxIcon.Information);
        }

        void Messenger.Warn(string warning)
        {
            this.sendMessage(warning, MessageBoxIcon.Warning);
        }

        void Messenger.GiveError(string error)
        {
            this.sendMessage(error, MessageBoxIcon.Error);
        }

        private void sendMessage(string message, MessageBoxIcon icon)
        {
            var caption = string.Empty;
            switch (icon)
            {
                case MessageBoxIcon.Warning:
                    caption = "Warning";
                    break;
                case MessageBoxIcon.Error:
                    caption = "Error";
                    break;
            }

            var subscriber = this.Subscriber as Form;
            if (subscriber != null)
            {
                using (new CenterWinDialog(subscriber))
                {
                    MessageBox.Show(
                        subscriber,
                        message,
                        caption,
                        MessageBoxButtons.OK,
                        icon);
                    return;
                }
            }

            MessageBox.Show(
                message,
                caption,
                MessageBoxButtons.OK,
                icon);
        }
    }
}
