namespace xofz.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using xofz.Framework;

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
        {
            this.web = web;
            this.startPresenter = startPresenter;

            // inherit from this class to override the type of collection
            this.presenters = new LinkedList<Presenter>();
        }

        public virtual void RegisterPresenter(Presenter presenter)
        {
            if (presenter == null)
            {
                throw new ArgumentNullException(
                    nameof(presenter));
            }

            this.presenters.Add(presenter);
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
                "LoginLatch");
            this.PresentFluidly<LoginPresenter>();
            w.Run<LatchHolder>(
                latch => latch.Latch.WaitOne(),
                "LoginLatch");
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
            foreach (var presenter in EnumerableHelpers.Where(
                EnumerableHelpers.OfType<T>(this.presenters),
                p => p.Name == name))
            {
                presenter.Stop();
                break;
            }
        }

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = "ui")
            where TPresenter : Presenter
        {
            ICollection<Presenter> matchingPresenters
                = new LinkedList<Presenter>(
                    EnumerableHelpers.Where(
                        this.presenters,
                        p => p is TPresenter));
            if (matchingPresenters.Count == 0)
            {
                return default(TUi);
            }

            if (presenterName == null)
            {
                return this.getUi<TUi>(
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
                    return this.getUi<TUi>(
                        np,
                        fieldName);
                }
            }

            return default(TUi);
        }

        private TUi getUi<TUi>(object presenter, string fieldName)
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
