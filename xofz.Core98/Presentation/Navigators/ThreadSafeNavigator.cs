namespace xofz.Presentation.Navigators
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

        protected ThreadSafeNavigator(
            ICollection<Presenter> presenters)
            : base(null, null, presenters)
        {
        }

        protected ThreadSafeNavigator(
            ICollection<Presenter> presenters,
            object locker)
            : base(null, null, presenters, locker)
        {
        }

        protected ThreadSafeNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter)
            : base(runner, startPresenter, null)
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
            Do<Presenter> startPresenter = null,
            ICollection<Presenter> presenters = null,
            object locker = null)
            : base(runner, startPresenter, presenters, locker)
        {
        }
    }
}
