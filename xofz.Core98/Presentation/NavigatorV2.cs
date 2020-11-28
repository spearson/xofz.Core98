namespace xofz.Presentation
{
    using System.Collections.Generic;
    using xofz.Framework;

    public class NavigatorV2
        : ThreadSafeNavigator
    {
        public NavigatorV2()
        {
        }

        public NavigatorV2(
            MethodRunner runner) 
            : base(runner)
        {
        }

        public NavigatorV2(
            MethodRunner runner, 
            Do<Presenter> startPresenter) 
            : base(runner, startPresenter)
        {
        }

        protected NavigatorV2(
            MethodRunner runner, 
            object locker) 
            : base(runner, locker)
        {
        }

        protected NavigatorV2(
            MethodRunner runner, 
            Do<Presenter> startPresenter, 
            object locker) 
            : base(runner, startPresenter, locker)
        {
        }

        protected NavigatorV2(
            MethodRunner runner, 
            Do<Presenter> startPresenter, 
            ICollection<Presenter> presenters) 
            : base(runner, startPresenter, presenters)
        {
        }

        protected NavigatorV2(
            MethodRunner runner, 
            Do<Presenter> startPresenter, 
            ICollection<Presenter> presenters, 
            object locker) 
            : base(runner, startPresenter, presenters, locker)
        {
        }
    }
}
