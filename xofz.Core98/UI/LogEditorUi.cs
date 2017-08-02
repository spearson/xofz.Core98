namespace xofz.UI
{
    using System;

    public interface LogEditorUi : PopupUi
    {
        event Action AddKeyTapped;

        event Action TypeChanged;

        MaterializedEnumerable<string> Types { get; set; }

        string SelectedType { get; set; }

        string CustomType { get; set; }

        bool CustomTypeVisible { get; set; }

        MaterializedEnumerable<string> Content { get; set; }
    }
}
