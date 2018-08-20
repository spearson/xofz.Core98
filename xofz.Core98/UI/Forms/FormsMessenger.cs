namespace xofz.UI.Forms
{
    using System.Windows.Forms;

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
                using (new DialogCenterer(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        @"?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                }

                goto handleResult;
            }

            result = MessageBox.Show(
                    question,
                    @"?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            handleResult:
            switch (result)
            {
                case DialogResult.Yes:
                    return Response.Yes;
                default:
                    return Response.No;
            }
        }

        Response Messenger.QuestionWithCancel(string question)
        {
            Messenger messenger = this;
            var subscriber = messenger.Subscriber as Form;
            DialogResult result;
            if (subscriber != null)
            {
                using (new DialogCenterer(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        @"?",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }

                goto handleResult;
            }

            result = MessageBox.Show(
                    question,
                    @"?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

            handleResult:
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
            string caption;
            switch (icon)
            {
                case MessageBoxIcon.Warning:
                    caption = "Warning";
                    break;
                case MessageBoxIcon.Error:
                    caption = "Error";
                    break;
                default:
                    caption = string.Empty;
                    break;
            }

            Messenger messenger = this;
            var subscriber = messenger.Subscriber as Form;
            if (subscriber != null)
            {
                using (new DialogCenterer(subscriber))
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
