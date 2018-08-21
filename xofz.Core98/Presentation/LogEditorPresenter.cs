namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.UI;

    public sealed class LogEditorPresenter : NamedPresenter
    {
        public LogEditorPresenter(
            LogEditorUi ui,
            MethodWeb web)
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            var subscriberRegistered = false;
            w.Run<EventSubscriber>(subscriber =>
            {
                subscriberRegistered = true;
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.TypeChanged),
                    this.ui_TypeChanged);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.AddKeyTapped),
                    this.ui_AddKeyTapped);
            });

            if (!subscriberRegistered)
            {
                this.ui.TypeChanged += this.ui_TypeChanged;
                this.ui.AddKeyTapped += this.ui_AddKeyTapped;
            }

            ICollection<string> types = new LinkedList<string>(
                    new[]
                    {
                        "Information",
                        "Warning",
                        "Error",
                        "Custom"
                    });
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Types = types;
                this.ui.SelectedType = "Information";
            });

            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            UiHelpers.Write(
                this.ui, 
                this.ui.Display);
        }

        public override void Stop()
        {
            UiHelpers.Write(
                this.ui, 
                this.ui.Hide);
        }

        private void ui_TypeChanged()
        {
            var customSelected = UiHelpers.Read(
                this.ui, 
                () => this.ui.SelectedType) == "Custom";
            UiHelpers.Write(
                this.ui, 
                () => this.ui.CustomTypeVisible = customSelected);
        }

        private void ui_AddKeyTapped()
        {
            var customSelected = UiHelpers.Read(
                this.ui, 
                () => this.ui.SelectedType) == "Custom";
            var type = customSelected
                ? UiHelpers.Read(this.ui, () => this.ui.CustomType)
                : UiHelpers.Read(this.ui, () => this.ui.SelectedType);

            this.web.Run<LogEditor>(le => le.AddEntry(
                    type,
                    UiHelpers.Read(this.ui, () => this.ui.Content)),
                this.Name);

            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Content = null;
                this.ui.SelectedType = "Information";
                this.ui.Hide();
            });
        }

        private long setupIf1;
        private readonly LogEditorUi ui;
        private readonly MethodWeb web;
    }
}
