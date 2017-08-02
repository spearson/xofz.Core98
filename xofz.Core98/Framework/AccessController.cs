namespace xofz.Framework
{
    using System.Collections.Generic;

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

        public AccessController(IDictionary<string, AccessLevel> passwords, Timer timer)
        {
            this.passwords = passwords;
            this.timer = timer;
            this.timer.Elapsed += this.timer_Elapsed;
        }

        public AccessController(IEnumerable<string> passwords, Timer timer)
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
        }

        private AccessLevel getLevel(int levelNumber)
        {
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
                    return AccessLevel.None;
            }
        }

        public virtual AccessLevel CurrentAccessLevel => this.currentAccessLevel;

        public virtual void InputPassword(string password)
        {
            this.InputPassword(password, 15 * 60 * 1000); // 15 minutes
        }

        public virtual void InputPassword(string password, int loginDurationInMs)
        {
            var p = this.passwords;
            if (p.ContainsKey(password))
            {
                this.setCurrentAccessLevel(p[password]);
                var t = this.timer;
                t.AutoReset = false;
                t.Stop();
                t.Start(loginDurationInMs);
            }
            else
            {
                this.setCurrentAccessLevel(AccessLevel.None);
            }
        }

        private void setCurrentAccessLevel(AccessLevel currentAccessLevel)
        {
            this.currentAccessLevel = currentAccessLevel;
        }

        private void timer_Elapsed()
        {
            this.setCurrentAccessLevel(AccessLevel.None);
        }

        private AccessLevel currentAccessLevel;
        private readonly IDictionary<string, AccessLevel> passwords;
        private readonly Timer timer;
    }
}
