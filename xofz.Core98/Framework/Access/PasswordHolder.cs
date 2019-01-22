namespace xofz.Framework.Access
{
    using System.Collections.Generic;
    using System.Security;

    public class PasswordHolder
    {
        public virtual IDictionary<SecureString, AccessLevel> Passwords
        {
            get;
            set;
        }

        public static PasswordHolder Create(
            params SecureString[] passwords)
        {
            return Create(passwords as IEnumerable<SecureString>);
        }

        public static PasswordHolder Create(
            params string[] passwords)
        {
            return Create(passwords as IEnumerable<string>);
        }

        public static PasswordHolder Create(
            IEnumerable<SecureString> passwords)
        {
            var d = new Dictionary<SecureString, AccessLevel>(10);
            if (passwords == null)
            {
                goto finish;
            }

            byte counter = 1;
            foreach (var password in passwords)
            {
                if (counter > 10)
                {
                    break;
                }

                if (password == null)
                {
                    ++counter;
                    continue;
                }

                var level = nextLevel(counter);
                d.Add(password, level);
                ++counter;
            }

            finish:
            return new PasswordHolder
            {
                Passwords = d
            };
        }

        public static PasswordHolder Create(
            IEnumerable<string> passwords)
        {
            var d = new Dictionary<SecureString, AccessLevel>(10);
            if (passwords == null)
            {
                goto finish;
            }

            byte counter = 1;
            foreach (var password in passwords)
            {
                if (counter > 10)
                {
                    break;
                }

                if (password == null)
                {
                    ++counter;
                    continue;
                }

                var level = nextLevel(counter);
                var securePw = new SecureString();
                foreach (var c in password)
                {
                    securePw.AppendChar(c);
                }

                d.Add(securePw, level);
                ++counter;
            }

            finish:
            return new PasswordHolder
            {
                Passwords = d
            };
        }

        protected static AccessLevel nextLevel(byte counter)
        {
            switch (counter)
            {
                case 0:
                    return AccessLevel.None;
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
    }
}
