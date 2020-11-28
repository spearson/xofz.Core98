namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Lots;
    using EH = xofz.EnumerableHelpers;

    public class Navigator
    {
        public Navigator()
            : this((MethodRunner)null)
        {
        }

        public Navigator(
            MethodRunner runner)
            : this(
                  runner,
                  p =>
                  {
                      ThreadPool.QueueUserWorkItem(
                          o => p?.Start());
                  })
        {
        }

        public Navigator(
            MethodRunner runner,
            Do<Presenter> startPresenter)
            : this(
                runner, 
                startPresenter, 
                new LinkedList<Presenter>())
        {
        }

        protected Navigator(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
        {
            this.runner = runner;
            this.startPresenter = startPresenter;
            this.presenters = presenters;
        }

        public virtual bool RegisterPresenter(
            Presenter presenter)
        {
            if (presenter == null)
            {
                return false;
            }

            this.presenters?.Add(presenter);
            return true;
        }

        public virtual bool IsRegistered<T>()
            where T : Presenter
        {
            return EH.Any(
                EH.OfType<T>(
                    this.presenters));
        }

        public virtual bool IsRegistered<T>(
            string name)
            where T : NamedPresenter
        {
            return EH.Any(
                EH.OfType<T>(
                    this.presenters),
                p => p.Name == name);
        }

        public virtual void Present<T>() 
            where T : Presenter
        {
            var ps = this.presenters;
            var presenter = EH.FirstOrDefault(
                ps,
                p => p is T);
            if (presenter == null)
            {
                return;
            }

            foreach (var p in ps)
            {
                p?.Stop();
            }

            this.startPresenter?.Invoke(presenter);
        }

        public virtual void Present<T>(
            string name)
            where T : NamedPresenter
        {
            var ps = this.presenters;
            var matchingPresenters = EH.OfType<T>(ps);
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                foreach (var p in ps)
                {
                    p?.Stop();
                }

                this.startPresenter?.Invoke(presenter);
                break;
            }
        }

        public virtual void PresentFluidly<T>()
            where T : Presenter
        {
            var presenter = EH.FirstOrDefault(
                this.presenters,
                p => p is T);
            if (presenter == null)
            {
                return;
            }

            this.startPresenter?.Invoke(presenter);
        }

        public virtual void PresentFluidly<T>(
            string name)
            where T : NamedPresenter
        {
            var matchingPresenters = EH.OfType<T>(
                this.presenters);
            foreach (var presenter in matchingPresenters)
            {
                if (presenter.Name != name)
                {
                    continue;
                }

                this.startPresenter?.Invoke(presenter);
                break;
            }
        }

        public virtual void LoginFluidly()
        {
            const string latchName =
                Framework.Login.DependencyNames.Latch;
            var r = this.runner;
            r?.Run<LatchHolder>(
                latch =>
                {
                    latch.Latch?.Reset();
                },
                latchName);
            this.PresentFluidly<LoginPresenter>();
            r?.Run<LatchHolder>(
                latch =>
                {
                    latch.Latch?.WaitOne();
                },
                latchName);
        }

        public virtual void StopPresenter<T>()
            where T : Presenter
        {
            foreach (var presenter in EH.OfType<T>(
                this.presenters))
            {
                presenter?.Stop();
                break;
            }
        }

        public virtual void StopPresenter<T>(
            string name)
            where T : NamedPresenter
        {
            foreach (var presenter in
                EH.Where(
                    EH.OfType<T>(
                        this.presenters),
                    p => p.Name == name))
            {
                presenter?.Stop();
                break;
            }
        }

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = Presenter.DefaultUiFieldName)
            where TPresenter : Presenter
        {
            Lot<Presenter> matchingPresenters
                = new LinkedListLot<Presenter>(
                    EH.Where(
                        this.presenters,
                        p => p is TPresenter));
            if (matchingPresenters.Count < 1)
            {
                return default;
            }

            if (presenterName == null)
            {
                return this.getUiProtected<TUi>(
                    EH.First(
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

            return default;
        }

        protected virtual TUi getUiProtected<TUi>(
            object presenter,
            string fieldName)
        {
            return (TUi)presenter
                ?.GetType()
                .GetField(
                    fieldName,
                    BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(presenter);
        }

        protected readonly ICollection<Presenter> presenters;
        protected readonly MethodRunner runner;
        protected readonly Do<Presenter> startPresenter;
    }
}