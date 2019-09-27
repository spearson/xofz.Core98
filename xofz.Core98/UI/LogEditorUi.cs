namespace xofz.UI
{
    using System.Collections.Generic;

    public interface LogEditorUi 
        : PopupUi
    {
        event Do AddKeyTapped;

        event Do TypeChanged;

        ICollection<string> Types { get; set; }

        ICollection<string> Content { get; set; }

        string SelectedType { get; set; }

        string CustomType { get; set; }

        bool CustomTypeVisible { get; set; }
    }
}
