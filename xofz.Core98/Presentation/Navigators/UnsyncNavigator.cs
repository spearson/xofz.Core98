namespace xofz.Presentation.Navigators
{
    using System.Collections.Generic;
    using xofz.Framework;

    public class UnsyncNavigator
        : Navigator
    {
        public UnsyncNavigator()
        {
        }

        public UnsyncNavigator(
            MethodRunner runner)
            : base(runner)
        {
        }

        public UnsyncNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter)
            : base(runner, startPresenter)
        {
        }

        protected UnsyncNavigator(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
            : base(runner, startPresenter, presenters)
        {
        }

        public virtual bool Unregister<T>()
            where T : Presenter
        {
            var ps = this.presenters;
            foreach (var p in EnumerableHelpers.OfType<T>(ps))
            {
                return ps.Remove(p);
            }

            return falsity;
        }

        public virtual bool Unregister<T>(
            string name)
            where T : NamedPresenter
        {
            var ps = this.presenters;
            foreach (var p in EnumerableHelpers.OfType<T>(ps))
            {
                if (p.Name != name)
                {
                    continue;
                }

                return ps.Remove(p);
            }

            return falsity;
        }
    }
}
