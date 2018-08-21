namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Threading;

    public class AccessController
    {
        public AccessController(IDictionary<string, AccessLevel> passwords)
            : this(passwords, new Timer())
        {
        }

        public AccessController(IEnumerable<string> passwords)
            : this(passwords, new Timer())
        {
        }

        public AccessController(params string[] passwords)
            : this(passwords, new Timer())
        {
        }

        public AccessController(
            IDictionary<string, AccessLevel> passwords,
            Timer timer)
        {
            this.passwords = passwords;
            this.timer = timer;
            this.timer.Elapsed += this.timer_Elapsed;
            this.timerHandlerFinished = new ManualResetEvent(true);
        }

        public AccessController(
            IEnumerable<string> passwords,
            Timer timer)
        {
            var levelCounter = 1;
            var dictionary = new Dictionary<string, AccessLevel>(10);
            foreach (var password in passwords)
            {
                dictionary.Add(password, this.getLevel(levelCounter));
                ++levelCounter;
            }

            this.passwords = dictionary;
            this.timer = timer;
            this.timer.Elapsed += this.timer_Elapsed;
            this.timerHandlerFinished = new ManualResetEvent(true);
        }

        private AccessLevel getLevel(int levelNumber)
        {
            if (levelNumber < 1)
            {
                return AccessLevel.None;
            }

            switch (levelNumber)
            {
                case 1:
                    return AccessLevel.Level1;
                case 2:
                    return AccessLevel.Level2;
                case 3:
                    return AccessLevel.Level3;
                case 4:
                    return AccessLevel.Level4;
                case 5:
                    return AccessLevel.Level5;
                case 6:
                    return AccessLevel.Level6;
                case 7:
                    return AccessLevel.Level7;
                case 8:
                    return AccessLevel.Level8;
                case 9:
                    return AccessLevel.Level9;
                case 10:
                    return AccessLevel.Level10;
                default:
                    return AccessLevel.Level10;
            }
        }

        public event Action<AccessLevel> AccessLevelChanged;

        public virtual AccessLevel CurrentAccessLevel
            => this.currentAccessLevel;

        public virtual System.TimeSpan TimeRemaining
        {
            get
            {
                var cal = this.currentAccessLevel;
                if (cal == AccessLevel.None)
                {
                    return System.TimeSpan.Zero;
                }

                var lt = this.loginTime;
                return this.loginDuration - (System.DateTime.Now - lt);
            }
        }

        public virtual void InputPassword(
            string password)
        {
            this.InputPassword(
                password,
                System.TimeSpan.FromMinutes(15));
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
            string password,
            long loginDurationMilliseconds)
        {
            if (loginDurationMilliseconds < 0)
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(loginDurationMilliseconds),
                    loginDurationMilliseconds,
                    @"The login duration milliseconds value must be positive.");
            }

            if (loginDurationMilliseconds > uint.MaxValue)
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(loginDurationMilliseconds),
                    loginDurationMilliseconds,
                    @"The maximum value for login duration milliseconds "
                    + @"is uint.MaxValue, or the maximum value "
                    + @"of an unsigned 32-bit integer.  That value is "
                    + uint.MaxValue + @", or "
                    + System.TimeSpan.FromMilliseconds(uint.MaxValue));
            }

            var p = this.passwords;
            if (password == null)
            {
                this.setCurrentAccessLevel(
                    AccessLevel.None);
                return;
            }

            if (!p.ContainsKey(password))
            {
                this.setCurrentAccessLevel(
                    AccessLevel.None);
                return;
            }

            var t = this.timer;
            t.AutoReset = false;
            t.Stop();
            this.timerHandlerFinished.WaitOne();
            this.setCurrentAccessLevel(p[password]);
            this.setLoginDuration(
                System.TimeSpan.FromMilliseconds(
                    loginDurationMilliseconds));
            this.setLoginTime(
                System.DateTime.Now);
            t.Start(loginDurationMilliseconds);
        }

        private void setCurrentAccessLevel(AccessLevel currentAccessLevel)
        {
            var previousLevel = this.currentAccessLevel;
            this.currentAccessLevel = currentAccessLevel;
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

        private void setLoginTime(System.DateTime loginTime)
        {
            this.loginTime = loginTime;
        }

        private void setLoginDuration(System.TimeSpan loginDuration)
        {
            this.loginDuration = loginDuration;
        }

        private void timer_Elapsed()
        {
            var h = this.timerHandlerFinished;
            h.Reset();
            this.setCurrentAccessLevel(AccessLevel.None);
            h.Set();
        }

        private AccessLevel currentAccessLevel;
        private System.DateTime loginTime;
        private System.TimeSpan loginDuration;
        private readonly IDictionary<string, AccessLevel> passwords;
        private readonly Timer timer;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
