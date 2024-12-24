namespace xofz.UI.Forms.Messengers
{
    using System.Windows.Forms;

    public sealed class FormsMessenger
        : Messenger
    {
        Ui Messenger.Subscriber { get; set; }

        string Messenger.InfoCaption { get; set; }
            = string.Empty;

        string Messenger.WarningCaption { get; set; }
            = @"Warning";

        string Messenger.ErrorCaption { get; set; }
            = @"Error";

        string Messenger.QuestionCaption { get; set; }
            = @"?";

        Response Messenger.Question(
            string question)
        {
            Messenger m = this;
            DialogResult result;
            if (m.Subscriber is Form subscriber)
            {
                using (new DialogCenterer(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        m.QuestionCaption,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                }

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
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

        Response Messenger.QuestionWithCancel(
            string question)
        {
            Messenger m = this;
            var subscriber = m.Subscriber as Form;
            DialogResult result;
            if (subscriber != null)
            {
                using (new DialogCenterer(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        m.QuestionCaption,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            handleResult:
            switch (result)
            {
                case DialogResult.Yes:
                    return Response.Yes;
                case DialogResult.No:
                    return Response.No;
                default:
                    return Response.Cancel;
            }
        }

        Response Messenger.QuestionOKCancel(
            string question)
        {
            Messenger m = this;
            DialogResult result;
            if (m.Subscriber is Form subscriber)
            {
                using (new DialogCenterer(subscriber))
                {
                    result = MessageBox.Show(
                        subscriber,
                        question,
                        m.QuestionCaption,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                }

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            handleResult:
            switch (result)
            {
                case DialogResult.OK:
                    return Response.OK;
                default:
                    return Response.Cancel;
            }
        }

        void Messenger.Inform(
            string message)
        {
            this.sendMessage(
                message,
                MessageBoxIcon.Information);
        }

        void Messenger.Warn(
            string warning)
        {
            this.sendMessage(
                warning,
                MessageBoxIcon.Warning);
        }

        void Messenger.GiveError(
            string error)
        {
            this.sendMessage(
                error,
                MessageBoxIcon.Error);
        }

        private void sendMessage(
            string message,
            MessageBoxIcon icon)
        {
            string caption;
            Messenger m = this;
            switch (icon)
            {
                case MessageBoxIcon.Warning:
                    caption = m.WarningCaption;
                    break;
                case MessageBoxIcon.Error:
                    caption = m.ErrorCaption;
                    break;
                case MessageBoxIcon.Information:
                    caption = m.InfoCaption;
                    break;
                default:
                    caption = string.Empty;
                    break;
            }

            Messenger messenger = this;
            if (messenger.Subscriber is Form subscriber)
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