namespace xofz.Presentation
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using xofz.Framework;

    public class Navigator
    {
        public Navigator(MethodWeb web)
        {
            this.web = web;
            this.presenters = new List<Presenter>();
        }

        public virtual void RegisterPresenter(Presenter presenter)
        {
            this.presenters.Add(presenter);
        }

        public virtual void Present<T>() where T : Presenter
        {
            var ps = this.presenters;
            Presenter p = default(Presenter);
            foreach (var presenter in ps)
            {
                if (presenter is T)
                {
                    p = presenter;
                    break;
                }
            }

            if (p == default(Presenter))
            {
                return;
            }

            foreach (var presenter in ps)
            {
                presenter.Stop();
            }

            new Thread(() =>
            {
                p.Start();
            }).Start();

        }

        public virtual void Present<T>(string name) where T : NamedPresenter
        {
            var ps = this.presenters;
            Presenter match = default(Presenter);
            foreach (var presenter in ps)
            {
                if (!(presenter is T))
                {
                    continue;
                }

                if (((T)presenter).Name != name)
                {
                    continue;
                }

                match = presenter;
                break;
            }

            if (match == default(Presenter))
            {
                return;
            }

            foreach (var presenter in ps)
            {
                presenter.Stop();
            }

            new Thread(() =>
            {
                match.Start();
            }).Start();
        }

        public virtual void PresentFluidly<T>() where T : Presenter
        {
            var ps = this.presenters;
            Presenter p = default(Presenter);
            foreach (var presenter in ps)
            {
                if (presenter is T)
                {
                    p = presenter;
                    break;
                }
            }

            if (p == default(Presenter))
            {
                return;
            }

            new Thread(() => p.Start()).Start();
        }

        public virtual void PresentFluidly<T>(string name) where T : NamedPresenter
        {
            var ps = this.presenters;
            Presenter match = default(Presenter);
            foreach (var presenter in ps)
            {
                if (!(presenter is T))
                {
                    continue;
                }

                if (((T)presenter).Name != name)
                {
                    continue;
                }

                match = presenter;
                break;
            }

            if (match == default(Presenter))
            {
                return;
            }

            new Thread(() =>
            {
                match.Start();
            }).Start();
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

        public virtual TUi GetUi<TPresenter, TUi>(
            string presenterName = null,
            string fieldName = "ui")
            where TPresenter : Presenter
        {
            var ps = this.presenters;
            var match = default(Presenter);
            if (presenterName == null)
            {
                foreach (var presenter in ps)
                {
                    if (presenter is TPresenter)
                    {
                        match = presenter;
                        break;
                    }
                }
            }
            else
            {
                foreach (var presenter in ps)
                {
                    if (!(presenter is NamedPresenter))
                    {
                        continue;
                    }

                    if (!(presenter is TPresenter))
                    {
                        continue;
                    }

                    if (((NamedPresenter)presenter).Name == presenterName)
                    {
                        match = presenter;
                        break;
                    }
                }
            }

            if (match == default(Presenter))
            {
                return default(TUi);
            }

            return this.getUi<TUi>(match, fieldName);
        }

        private TUi getUi<TUi>(object presenter, string fieldName)
        {
            return (TUi)presenter
                .GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(presenter);
        }

        private readonly List<Presenter> presenters;
        private readonly MethodWeb web;
    }
}
