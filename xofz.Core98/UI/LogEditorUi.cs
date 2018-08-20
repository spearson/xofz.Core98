namespace xofz.UI
{
    using System.Collections.Generic;

    public interface LogEditorUi : PopupUi
    {
        event Action AddKeyTapped;

        event Action TypeChanged;

        ICollection<string> Types { get; set; }

        string SelectedType { get; set; }

        string CustomType { get; set; }

        bool CustomTypeVisible { get; set; }

        ICollection<string> Content { get; set; }
    }
}
