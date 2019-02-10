namespace xofz.Presentation
{
    using System.Collections.Generic;
    using xofz.Framework;

    public class NavigatorV2 : ThreadSafeNavigator
    {
        public NavigatorV2(
            MethodWeb web) 
            : base(web)
        {
        }

        public NavigatorV2(
            MethodWeb web, 
            Do<Presenter> startPresenter) 
            : base(web, startPresenter)
        {
        }

        protected NavigatorV2(
            MethodWeb web, 
            object locker) 
            : base(web, locker)
        {
        }

        protected NavigatorV2(
            MethodWeb web, 
            Do<Presenter> startPresenter, 
            object locker) 
            : base(web, startPresenter, locker)
        {
        }

        protected NavigatorV2(
            MethodWeb web, 
            Do<Presenter> startPresenter, 
            ICollection<Presenter> presenters) 
            : base(web, startPresenter, presenters)
        {
        }

        protected NavigatorV2(
            MethodWeb web, 
            Do<Presenter> startPresenter, 
            ICollection<Presenter> presenters, 
            object locker) 
            : base(web, startPresenter, presenters, locker)
        {
        }
    }
}
