namespace xofz.Presentation
{
    using System.Collections.Generic;
    using xofz.Framework;

    public class ThreadSafeNavigator
        : NavigatorV2
    {
        public ThreadSafeNavigator()
        {
        }

        public ThreadSafeNavigator(
            MethodRunner runner)
            : base(runner)
        {
        }

        public ThreadSafeNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter)
            : base(runner, startPresenter)
        {
        }

        protected ThreadSafeNavigator(
            MethodRunner runner,
            object locker)
            : base(runner, locker)
        {
        }

        protected ThreadSafeNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            object locker)
            : base(runner, startPresenter, locker)
        {
        }

        protected ThreadSafeNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
            : base(runner, startPresenter, presenters)
        {
        }

        protected ThreadSafeNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters,
            object locker)
            : base(runner, startPresenter, presenters, locker)
        {
        }
    }
}
