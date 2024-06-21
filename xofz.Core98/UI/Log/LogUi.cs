namespace xofz.UI.Log
{
    using System.Collections.Generic;

    public interface LogUi
        : Ui
    {
        event Do DateRangeChanged;

        event Do AddKeyTapped;

        event Do ClearKeyTapped;

        event Do StatisticsKeyTapped;

        event Do FilterTextChanged;

        ICollection<XTuple<string, string, string>> Entries { get; set; }

        System.DateTime StartDate { get; set; }

        System.DateTime EndDate { get; set; }

        string FilterContent { get; set; }

        string FilterType { get; set; }

        bool AddKeyVisible { get; set; }

        bool ClearKeyVisible { get; set; }

        bool StatisticsKeyVisible { get; set; }

        void AddToTop(
            XTuple<string, string, string> entry);
    }
}