namespace xofz.UI
{
    public interface SplitUi : Ui
    {
        void SwitchUi1(
            Ui newFirstUi);

        void SwitchUi2(
            Ui newSecondUi);
    }
}
