namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.UI;

    public sealed class LogEditorPresenter 
        : PopupNamedPresenter
    {
        public LogEditorPresenter(
            LogEditorUi ui,
            MethodWeb web)
            : base(ui)
        {
            this.ui = ui;
            this.web = web;
        }

        /// <remarks>
        /// Requires a SetupHandler, EventSubscriber, and a Navigator
        /// registered with the MethodWeb.
        /// Also required is a TypeChangedHandler and an AddKeyTappedHandler
        /// </remarks>
        public void Setup()
        {
            if (Interlocked.CompareExchange(
                    ref this.setupIf1, 
                    1, 
                    0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Run<SetupHandler>(handler => handler.Handle(this.ui));

            w.Run<EventSubscriber>(subscriber =>
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

            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        private void ui_TypeChanged()
        {
            var w = this.web;
            w.Run<TypeChangedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void ui_AddKeyTapped()
        {
            var w = this.web;
            w.Run<AddKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui, this.Name);
            });
        }

        private long setupIf1;
        private readonly LogEditorUi ui;
        private readonly MethodWeb web;
    }
}
