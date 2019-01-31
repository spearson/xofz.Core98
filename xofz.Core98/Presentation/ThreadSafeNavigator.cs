namespace xofz.Presentation
{
    using System.Collections.Generic;
    using xofz.Framework;
    using xofz.Framework.Lots;

    public class ThreadSafeNavigator : Navigator
    {
        public ThreadSafeNavigator(
            MethodWeb web)
            : base(web)
        {
            this.locker = new object();
        }

        public ThreadSafeNavigator(
            MethodWeb web,
            Do<Presenter> startPresenter)
            : base(web, startPresenter)
        {
            this.locker = new object();
        }

        protected ThreadSafeNavigator(
            MethodWeb web,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
            : base(web, startPresenter, presenters)
        {
            this.locker = new object();
        }

        public override bool RegisterPresenter(
            Presenter presenter)
        {
            if (presenter == null)
            {
                return false;
            }

            lock (this.locker)
            {
                this.presenters.Add(presenter);
            }

            return true;
        }

        public override bool IsRegistered<T>()
        {
            lock (this.locker)
            {
                return EnumerableHelpers.Any(
                    EnumerableHelpers.OfType<T>(
                        this.presenters));
            }
        }

        public override bool IsRegistered<T>(string name)
        {
            lock (this.locker)
            {
                return EnumerableHelpers.Any(
                    EnumerableHelpers.OfType<T>(
                        this.presenters),
                    p => p.Name == name);
            }
        }

        public virtual bool Unregister<T>()
            where T : Presenter
        {
            var ps = this.presenters;
            var unregistered = false;
            lock (this.locker)
            {
                foreach (var p in EnumerableHelpers.OfType<T>(ps))
                {
                    ps.Remove(p);
                    unregistered = true;
                    break;
                }
            }

            return unregistered;
        }

        public virtual bool Unregister<T>(
            string name)
            where T : NamedPresenter
        {
            var ps = this.presenters;
            var unregistered = false;
            lock (this.locker)
            {
                foreach (var p in EnumerableHelpers.OfType<T>(ps))
                {
                    if (p.Name != name)
                    {
                        continue;
                    }

                    ps.Remove(p);
                    unregistered = true;
                    break;
                }
            }

            return unregistered;
        }

        public override void Present<T>()
        {
            var ps = this.presenters;
            Presenter presenter;
            lock (this.locker)
            {
                presenter = EnumerableHelpers.FirstOrDefault(
                    ps,
                    p => p is T);
            }

            if (presenter == null)
            {
                return;
            }

            lock (this.locker)
            {
                foreach (var p in ps)
                {
                    p.Stop();
                }
            }

            this.startPresenter(presenter);
        }

        public override void Present<T>(string name)
        {
            var ps = this.presenters;
            lock (this.locker)
            {
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
        }

        public override void PresentFluidly<T>()
        {
            Presenter presenter;
            lock (this.locker)
            {
                presenter = EnumerableHelpers.FirstOrDefault(
                    this.presenters,
                    p => p is T);
            }

            if (presenter == null)
            {
                return;
            }

            this.startPresenter(presenter);
        }

        public override void PresentFluidly<T>(string name)
        {
            lock (this.locker)
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

        }

        public override void StopPresenter<T>()
        {
            lock (this.locker)
            {
                foreach (var presenter in EnumerableHelpers
                    .OfType<T>(this.presenters))
                {
                    presenter.Stop();
                    break;
                }
            }
        }

        public override void StopPresenter<T>(string name)
        {
            lock (this.locker)
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
        }

        public override TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = @"ui")
        {
            Lot<Presenter> matchingPresenters;
            lock (this.locker)
            {
                matchingPresenters
                    = new LinkedListLot<Presenter>(
                        EnumerableHelpers.Where(
                            this.presenters,
                            p => p is TPresenter));
            }

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

        protected readonly object locker;
    }
}
