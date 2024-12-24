namespace xofz.Presentation.Presenters
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.UI.LogEditor;

    public sealed class LogEditorPresenter
        : PopupNamedPresenter
    {
        public LogEditorPresenter(
            LogEditorUi ui,
            MethodRunner runner)
            : base(ui)
        {
            this.ui = ui;
            this.runner = runner;
        }

        /// <remarks>
        /// Requires a SetupHandler, EventSubscriber, and a Navigator
        /// registered with the MethodRunner.
        /// Also required is a TypeChangedHandler and an AddKeyTappedHandler
        /// </remarks>
        public void Setup()
        {
            const long one = 1;
            if (Interlocked.Exchange(
                    ref this.setupIf1,
                    one) == one)
            {
                return;
            }

            var r = this.runner;
            r?.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui);
            });

            r?.Run<EventSubscriber>(subscriber =>
            {
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.TypeChanged),
                    this.ui_TypeChanged);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.AddKeyTapped),
                    this.ui_AddKeyTapped);
            });

            r?.Run<Navigator>(nav =>
                nav.RegisterPresenter(this));
        }

        private void ui_TypeChanged()
        {
            var r = this.runner;
            r?.Run<TypeChangedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private void ui_AddKeyTapped()
        {
            var r = this.runner;
            r?.Run<AddKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private long setupIf1;
        private readonly LogEditorUi ui;
        private readonly MethodRunner runner;
    }
}