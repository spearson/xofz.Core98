namespace xofz.UI
{
    public interface Messenger
    {
        Ui Subscriber { get; set; }

        string InfoCaption { get; set; }

        string WarningCaption { get; set; }

        string ErrorCaption { get; set; }

        string QuestionCaption { get; set; }

        Response Question(
            string question);

        Response QuestionWithCancel(
            string question);

        Response QuestionOKCancel(
            string question);

        void Inform(
            string message);

        void Warn(
            string warning);

        void GiveError(
            string error);
    }
}
