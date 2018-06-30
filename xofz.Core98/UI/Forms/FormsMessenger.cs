namespace xofz.UI.Forms
{
    using System.Windows.Forms;
    using xofz.UI.Forms.Internal;

    public sealed class FormsMessenger : Messenger
    {
        Ui Messenger.Subscriber { get; set; }

        Response Messenger.Question(string question)
        {
            Messenger messenger = this;
            var subscriber = messenger.Subscriber as Form;
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

        Response Messenger.QuestionWithCancel(string question)
        {
            Messenger messenger = this;
            var subscriber = messenger.Subscriber as Form;
            DialogResult result;
            if (subscriber != null)
            {
                using (new CenterWinDialog(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        @"?",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }
            }
            else
            {
                result = MessageBox.Show(
                    question,
                    @"?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
            }

            switch (result)
            {
                case DialogResult.Yes:
                    return Response.Yes;
                case DialogResult.No:
                    return Response.No;
                case DialogResult.Cancel:
                    return Response.Cancel;
                default:
                    return Response.Cancel;
            }
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

            Messenger messenger = this;
            var subscriber = messenger.Subscriber as Form;
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
