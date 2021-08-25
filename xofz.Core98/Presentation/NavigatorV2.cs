﻿// ReSharper disable InconsistentlySynchronizedField
namespace xofz.Presentation
{
    using System.Collections.Generic;
    using xofz.Framework;
    using xofz.Framework.Lots;
    using EH = xofz.EnumerableHelpers;

    public class NavigatorV2
        : Navigator
    {
        public NavigatorV2()
        {
            this.locker = new object();
        }

        public NavigatorV2(
            MethodRunner runner)
            : base(runner)
        {
            this.locker = new object();
        }

        public NavigatorV2(
            MethodRunner runner,
            Do<Presenter> startPresenter)
            : base(runner, startPresenter)
        {
            this.locker = new object();
        }

        protected NavigatorV2(
            MethodRunner runner,
            object locker)
            : base(runner)
        {
            this.locker = locker;
        }

        protected NavigatorV2(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            object locker)
            : base(runner, startPresenter)
        {
            this.locker = locker;
        }

        protected NavigatorV2(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters)
            : base(runner, startPresenter, presenters)
        {
            this.locker = new object();
        }

        protected NavigatorV2(
            MethodRunner runner,
            Do<Presenter> startPresenter,
            ICollection<Presenter> presenters,
            object locker)
            : base(runner, startPresenter, presenters)
        {
            this.locker = locker;
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
                this.presenters?.Add(presenter);
            }

            return true;
        }

        public override bool IsRegistered<T>()
        {
            lock (this.locker)
            {
                return EH.Any(
                    EH.OfType<T>(
                        this.presenters));
            }
        }

        public override bool IsRegistered<T>(
            string name)
        {
            lock (this.locker)
            {
                return EH.Any(
                    EH.OfType<T>(
                        this.presenters),
                    p => p.Name == name);
            }
        }

        public virtual bool Unregister<T>()
            where T : Presenter
        {
            var ps = this.presenters;
            bool unregistered = falsity;
            lock (this.locker)
            {
                foreach (var p in EH.OfType<T>(ps))
                {
                    unregistered = ps.Remove(p);
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
            bool unregistered = falsity;
            lock (this.locker)
            {
                foreach (var p in EH.OfType<T>(ps))
                {
                    if (p.Name != name)
                    {
                        continue;
                    }

                    unregistered = ps.Remove(p);
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
                presenter = EH.FirstOrNull(
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

            this.startPresenter?.Invoke(presenter);
        }

        public override void Present<T>(
            string name)
        {
            var ps = this.presenters;
            NamedPresenter np;
            lock (this.locker)
            {
                var matchingPresenters = EH.OfType<T>(ps);
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

                    np = presenter;
                    goto start;
                }
            }

            return;
            start:
            this.startPresenter?.Invoke(np);
        }

        public override void PresentFluidly<T>()
        {
            Presenter presenter;
            lock (this.locker)
            {
                presenter = EH.FirstOrNull(
                    this.presenters,
                    p => p is T);
            }

            if (presenter == null)
            {
                return;
            }

            this.startPresenter?.Invoke(presenter);
        }

        public override void PresentFluidly<T>(
            string name)
        {
            NamedPresenter np;
            lock (this.locker)
            {
                var matchingPresenters = EH.OfType<T>(
                    this.presenters);
                foreach (var presenter in matchingPresenters)
                {
                    if (presenter.Name != name)
                    {
                        continue;
                    }

                    np = presenter;
                    goto start;
                }
            }

            return;
            start:
            this.startPresenter?.Invoke(np);
        }

        public override void StopPresenter<T>()
        {
            lock (this.locker)
            {
                foreach (var presenter in EH.OfType<T>(
                    this.presenters))
                {
                    presenter.Stop();
                    break;
                }
            }
        }

        public override void StopPresenter<T>(
            string name)
        {
            lock (this.locker)
            {
                foreach (var presenter in
                    EH.Where(
                        EH.OfType<T>(
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
            string fieldName = Presenter.DefaultUiFieldName)
        {
            Lot<Presenter> matchingPresenters;
            lock (this.locker)
            {
                matchingPresenters
                    = new LinkedListLot<Presenter>(
                        EH.Where(
                            this.presenters,
                            p => p is TPresenter));
            }

            const byte one = 1;
            if (matchingPresenters.Count < one)
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

        protected readonly object locker;
        protected const bool
            falsity = false;
    }
}
