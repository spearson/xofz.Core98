namespace xofz.UI
{
    public interface Messenger
    {
        Ui Subscriber { get; set; }

        Response Question(string question);

        Response QuestionWithCancel(string question);

        void Inform(string message);

        void Warn(string warning);

        void GiveError(string error);
    }
}
