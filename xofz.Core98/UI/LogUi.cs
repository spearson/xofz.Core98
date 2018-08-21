namespace xofz.UI
{
    using System.Collections.Generic;

    public interface LogUi : Ui
    {
        event Action StartDateChanged;

        event Action EndDateChanged;

        event Action AddKeyTapped;

        event Action ClearKeyTapped;

        event Action StatisticsKeyTapped;

        event Action FilterTextChanged;

        ICollection<Tuple<string, string, string>> Entries { get; set; }

        System.DateTime StartDate { get; set; }

        System.DateTime EndDate { get; set; }

        string FilterContent { get; set; }

        string FilterType { get; set; }

        bool AddKeyVisible { get; set; }

        bool ClearKeyVisible { get; set; }

        bool StatisticsKeyVisible { get; set; }

        void AddToTop(Tuple<string, string, string> entry);
    }
}
