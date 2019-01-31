namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Lots;

    public class Navigator
    {
        public Navigator(
            MethodWeb web)
            : this(
                  web,
                  p => ThreadPool.QueueUserWorkItem(
                      o => p.Start()))
        {
        }

        public Navigator(
            MethodWeb web,
            Do<Presenter> startPresenter)
            : this(web, startPresenter, new LinkedList<Presenter>())
        {
        }

        protected Navigator(
            MethodWeb web,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
        {
            this.web = web;
            this.startPresenter = startPresenter;
            this.presenters = presenters;
        }

        public virtual bool RegisterPresenter(Presenter presenter)
        {
            if (presenter == null)
            {
                return false;
            }

            this.presenters.Add(presenter);
            return true;
        }

        public virtual bool IsRegistered<T>()
            where T : Presenter
        {
            return EnumerableHelpers.Any(
                EnumerableHelpers.OfType<T>(
                    this.presenters));
        }

        public virtual bool IsRegistered<T>(string name)
            where T : NamedPresenter
        {
            return EnumerableHelpers.Any(
                EnumerableHelpers.OfType<T>(
                    this.presenters),
                p => p.Name == name);
        }

        public virtual void Present<T>() where T : Presenter
        {
            var ps = this.presenters;
            var presenter = EnumerableHelpers.FirstOrDefault(
                ps,
                p => p is T);
            if (presenter == null)
            {
                return;
            }

            foreach (var p in ps)
            {
                p.Stop();
            }

            this.startPresenter(presenter);
        }

        public virtual void Present<T>(string name)
            where T : NamedPresenter
        {
            var ps = this.presenters;
            var matchingPresenters = EnumerableHelpers.OfType<T>(ps);
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                foreach (var p in ps)
                {
                    p.Stop();
                }

                this.startPresenter(presenter);
                break;
            }
        }

        public virtual void PresentFluidly<T>()
            where T : Presenter
        {
            var presenter = EnumerableHelpers.FirstOrDefault(
                this.presenters,
                p => p is T);
            if (presenter == null)
            {
                return;
            }

            this.startPresenter(presenter);
        }

        public virtual void PresentFluidly<T>(string name)
            where T : NamedPresenter
        {
            var matchingPresenters = EnumerableHelpers.OfType<T>(
                this.presenters);
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                this.startPresenter(presenter);
                break;
            }
        }

        public virtual void LoginFluidly()
        {
            var w = this.web;
            w.Run<LatchHolder>(
                latch => latch.Latch.Reset(),
                Framework.Login.DependencyNames.Latch);
            this.PresentFluidly<LoginPresenter>();
            w.Run<LatchHolder>(
                latch => latch.Latch.WaitOne(),
                Framework.Login.DependencyNames.Latch);
        }

        public virtual void StopPresenter<T>()
            where T : Presenter
        {
            foreach (var presenter in EnumerableHelpers
                .OfType<T>(this.presenters))
            {
                presenter.Stop();
                break;
            }
        }

        public virtual void StopPresenter<T>(string name)
            where T : NamedPresenter
        {
            foreach (var presenter in
                EnumerableHelpers.Where(
                    EnumerableHelpers.OfType<T>(
                        this.presenters),
                    p => p.Name == name))
            {
                presenter.Stop();
                break;
            }
        }

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = @"ui")
            where TPresenter : Presenter
        {
            Lot<Presenter> matchingPresenters
                = new LinkedListLot<Presenter>(
                    EnumerableHelpers.Where(
                        this.presenters,
                        p => p is TPresenter));
            if (matchingPresenters.Count < 1)
            {
                return default(TUi);
            }

            if (presenterName == null)
            {
                return this.getUiProtected<TUi>(
                    EnumerableHelpers.First(
                        matchingPresenters),
                    fieldName);
            }

            foreach (var p in matchingPresenters)
            {
                if (!(p is NamedPresenter np))
                {
                    continue;
                }

                if (np.Name == presenterName)
                {
                    return this.getUiProtected<TUi>(
                        np,
                        fieldName);
                }
            }

            return default(TUi);
        }

        protected virtual TUi getUiProtected<TUi>(
            object presenter,
            string fieldName)
        {
            return (TUi)presenter
                .GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(presenter);
        }

        protected readonly ICollection<Presenter> presenters;
        protected readonly MethodWeb web;
        protected readonly Do<Presenter> startPresenter;
    }
}