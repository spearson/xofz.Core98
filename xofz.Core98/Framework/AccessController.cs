namespace xofz.Framework
{
    using System.Security;
    using System.Threading;
    using xofz.Framework.Access;

    public class AccessController
    {
        public AccessController(
            MethodRunner runner)
        {
            this.runner = runner;
            const bool truth = true;
            this.timerFinished =
                new ManualResetEvent(truth);
        }

        public virtual void Setup()
        {
            const byte one = 1;
            if (Interlocked.Exchange(
                    ref this.setupIf1,
                    one) == one)
            {
                return;
            }

            var r = this.runner;
            r?.Run<xofz.Framework.Timer, EventSubscriber>(
                (t, sub) =>
                {
                    sub.Subscribe(
                        t,
                        nameof(t.Elapsed),
                        this.timer_Elapsed);
                },
                DependencyNames.Timer);
        }

        public virtual event Do<AccessLevel> AccessLevelChanged;

        public virtual AccessLevel CurrentAccessLevel
            => this.currentLevel;

        public virtual System.TimeSpan TimeRemaining
        {
            get
            {
                var r = this.runner;
                var cal = this.currentLevel;
                if (cal == zeroAccess)
                {
                    return System.TimeSpan.Zero;
                }

                System.DateTime
                    lt = this.loginTime,
                    now = System.DateTime.Now;
                r?.Run<TimeProvider>(provider =>
                {
                    now = provider.Now();
                });

                return this.loginDuration - (now - lt);
            }
        }

        public virtual void InputPassword(
            SecureString password)
        {
            var r = this.runner;
            r?.Run<Access.SettingsHolder>(settings =>
            {
                this.InputPassword(
                    password,
                    settings.DefaultLoginDuration);
            });
        }

        public virtual void InputPassword(
            string password)
        {
            var r = this.runner;
            r?.Run<Access.SettingsHolder>(settings =>
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
            if (loginDurationMilliseconds < min)
            {
                return;
            }

            if (loginDurationMilliseconds > max)
            {
                loginDurationMilliseconds = max;
            }

            if (password == null)
            {
                this.setCurrentAccessLevel(
                    zeroAccess);
                return;
            }

            var r = this.runner;
            var newLevel = zeroAccess;
            r?.Run<PasswordHolder, SecureStringToolSet>(
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

            if (newLevel == zeroAccess)
            {
                this.setCurrentAccessLevel(zeroAccess);
                return;
            }

            r?.Run<xofz.Framework.Timer>(t =>
                {
                    t.AutoReset = falsity;
                    t.Stop();
                    this.timerFinished.WaitOne();
                    this.setCurrentAccessLevel(
                        newLevel);
                    this.setLoginDuration(
                        System.TimeSpan.FromMilliseconds(
                            loginDurationMilliseconds));
                    var now = System.DateTime.Now;
                    r.Run<TimeProvider>(provider =>
                    {
                        now = provider.Now();
                    });
                    this.setLoginTime(
                        now);
                    t.Start(
                        loginDurationMilliseconds);
                },
                DependencyNames.Timer);
        }

        public virtual void InputPassword(
            string password,
            long loginDurationMilliseconds)
        {
            if (loginDurationMilliseconds < min)
            {
                return;
            }

            if (loginDurationMilliseconds > max)
            {
                loginDurationMilliseconds = max;
            }

            if (password == null)
            {
                this.setCurrentAccessLevel(
                    zeroAccess);
                return;
            }

            var r = this.runner;
            var newLevel = zeroAccess;
            r?.Run<PasswordHolder, SecureStringToolSet>(
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

            if (newLevel == zeroAccess)
            {
                this.setCurrentAccessLevel(
                    zeroAccess);
                return;
            }

            r?.Run<xofz.Framework.Timer>(t =>
                {
                    t.AutoReset = falsity;
                    t.Stop();
                    this.timerFinished.WaitOne();
                    this.setCurrentAccessLevel(
                        newLevel);
                    this.setLoginDuration(
                        System.TimeSpan.FromMilliseconds(
                            loginDurationMilliseconds));
                    var now = System.DateTime.Now;
                    r.Run<TimeProvider>(provider =>
                    {
                        now = provider.Now();
                    });
                    this.setLoginTime(
                        now);
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
                o =>
                    alc.Invoke(currentAccessLevel));
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
            var finished = this.timerFinished;
            finished.Reset();
            this.setCurrentAccessLevel(
                zeroAccess);
            finished.Set();
        }

        protected long setupIf1;
        protected AccessLevel currentLevel;
        protected System.DateTime loginTime;
        protected System.TimeSpan loginDuration;
        protected readonly ManualResetEvent timerFinished;
        protected readonly MethodRunner runner;
        protected const AccessLevel zeroAccess = AccessLevel.None;
        protected const long max = uint.MaxValue;
        protected const byte min = 0;
        protected const bool falsity = false;
    }
}