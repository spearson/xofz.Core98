namespace xofz.Framework
{
    using System.Security;
    using System.Threading;
    using xofz.Framework.Access;

    public class AccessController
    {
        public AccessController(
            MethodWeb web)
        {
            this.web = web;
            this.timerFinished = new ManualResetEvent(true);
        }

        public virtual void Setup()
        {
            if (Interlocked.CompareExchange(
                    ref this.setupIf1,
                    1,
                    0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Run<xofz.Framework.Timer, EventSubscriber>(
                (t, sub) =>
                {
                    sub.Subscribe(
                        t,
                        nameof(t.Elapsed),
                        this.timer_Elapsed);
                },
                DependencyNames.Timer);
            w.RegisterDependency(this);
        }

        public virtual event Do<AccessLevel> AccessLevelChanged;

        public virtual AccessLevel CurrentAccessLevel
            => this.currentLevel;

        public virtual System.TimeSpan TimeRemaining
        {
            get
            {
                var cal = this.currentLevel;
                if (cal == AccessLevel.None)
                {
                    return System.TimeSpan.Zero;
                }

                var lt = this.loginTime;
                return this.loginDuration - (System.DateTime.Now - lt);
            }
        }

        public virtual void InputPassword(
            SecureString password)
        {
            var w = this.web;
            w.Run<Access.SettingsHolder>(settings =>
            {
                this.InputPassword(
                    password,
                    settings.DefaultLoginDuration);
            });
        }

        public virtual void InputPassword(
            string password)
        {
            var w = this.web;
            w.Run<Access.SettingsHolder>(settings =>
            {
                this.InputPassword(
                    password,
                    settings.DefaultLoginDuration);
            });
        }

        public virtual void InputPassword(
            SecureString password,
            System.TimeSpan loginDuration)
        {
            var milliseconds = (long)loginDuration.TotalMilliseconds;
            this.InputPassword(
                password,
                milliseconds);
        }

        public virtual void InputPassword(
            string password,
            System.TimeSpan loginDuration)
        {
            var milliseconds = (long)loginDuration.TotalMilliseconds;
            this.InputPassword(
                password,
                milliseconds);
        }

        public virtual void InputPassword(
            SecureString password,
            long loginDurationMilliseconds)
        {
            if (loginDurationMilliseconds < 0)
            {
                return;
            }

            if (loginDurationMilliseconds > uint.MaxValue)
            {
                loginDurationMilliseconds = uint.MaxValue;
            }

            if (password == null)
            {
                this.setCurrentAccessLevel(
                    AccessLevel.None);
                return;
            }

            var w = this.web;
            var noAccess = AccessLevel.None;
            var newLevel = noAccess;
            w.Run<PasswordHolder, SecureStringToolSet>(
                (holder, ssts) =>
            {
                var ps = holder.Passwords;
                if (ps == null)
                {
                    return;
                }

                foreach (var kvp in ps)
                {
                    if (ssts.Decode(password) !=
                        ssts.Decode(kvp.Key))
                    {
                        continue;
                    }

                    newLevel = kvp.Value;
                    break;
                }
            });

            if (newLevel == noAccess)
            {
                this.setCurrentAccessLevel(noAccess);
                return;
            }

            w.Run<xofz.Framework.Timer>(t =>
                {
                    t.AutoReset = false;
                    t.Stop();
                    this.timerFinished.WaitOne();
                    this.setCurrentAccessLevel(newLevel);
                    this.setLoginDuration(
                        System.TimeSpan.FromMilliseconds(
                            loginDurationMilliseconds));
                    this.setLoginTime(
                        System.DateTime.Now);
                    t.Start(loginDurationMilliseconds);
                },
                DependencyNames.Timer);
        }

        public virtual void InputPassword(
            string password,
            long loginDurationMilliseconds)
        {
            if (loginDurationMilliseconds < 0)
            {
                return;
            }

            if (loginDurationMilliseconds > uint.MaxValue)
            {
                loginDurationMilliseconds = uint.MaxValue;
            }

            if (password == null)
            {
                this.setCurrentAccessLevel(
                    AccessLevel.None);
                return;
            }

            var w = this.web;
            var noAccess = AccessLevel.None;
            var newLevel = noAccess;
            w.Run<PasswordHolder, SecureStringToolSet>(
                (holder, ssts) =>
                {
                    var ps = holder.Passwords;
                    if (ps == null)
                    {
                        return;
                    }

                    foreach (var kvp in ps)
                    {
                        if (password == ssts.Decode(kvp.Key))
                        {
                            newLevel = kvp.Value;
                            break;
                        }
                    }
                });

            if (newLevel == noAccess)
            {
                this.setCurrentAccessLevel(noAccess);
                return;
            }

            w.Run<xofz.Framework.Timer>(t =>
                {
                    t.AutoReset = false;
                    t.Stop();
                    this.timerFinished.WaitOne();
                    this.setCurrentAccessLevel(newLevel);
                    this.setLoginDuration(
                        System.TimeSpan.FromMilliseconds(
                            loginDurationMilliseconds));
                    this.setLoginTime(
                        System.DateTime.Now);
                    t.Start(loginDurationMilliseconds);
                },
                DependencyNames.Timer);
        }

        protected virtual void setCurrentAccessLevel(
            AccessLevel currentAccessLevel)
        {
            var previousLevel = this.currentLevel;
            this.currentLevel = currentAccessLevel;
            if (previousLevel == currentAccessLevel)
            {
                return;
            }

            var alc = this.AccessLevelChanged;
            if (alc == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => alc.Invoke(currentAccessLevel));
        }

        protected virtual void setLoginTime(
            System.DateTime loginTime)
        {
            this.loginTime = loginTime;
        }

        protected virtual void setLoginDuration(
            System.TimeSpan loginDuration)
        {
            this.loginDuration = loginDuration;
        }

        protected virtual void timer_Elapsed()
        {
            var h = this.timerFinished;
            h.Reset();
            this.setCurrentAccessLevel(AccessLevel.None);
            h.Set();
        }

        protected long setupIf1;
        protected AccessLevel currentLevel;
        protected System.DateTime loginTime;
        protected System.TimeSpan loginDuration;
        protected readonly ManualResetEvent timerFinished;
        protected readonly MethodWeb web;
    }
}
