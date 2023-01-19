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
            // assuming 1 password per access level
            // and AccessLevel.None does not have a password
            var maxPwCount = System
                                 .Enum
                                 .GetNames(typeof(AccessLevel))
                                 .Length - one;
            IDictionary<SecureString, AccessLevel> d =
                new XDictionary<SecureString, AccessLevel>();
            if (maxPwCount < one || passwords == null)
            {
                goto finish;
            }

            long counter = one;
            foreach (var password in passwords)
            {
                if (counter > maxPwCount)
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
            // assuming 1 password per access level
            // and AccessLevel.None does not have a password
            var maxPwCount = System
                .Enum
                .GetNames(typeof(AccessLevel))
                .Length - one;

            IDictionary<SecureString, AccessLevel> d =
                new XDictionary<SecureString, AccessLevel>();
            if (maxPwCount < one || passwords == null)
            {
                goto finish;
            }

            long counter = 1;
            foreach (var password in passwords)
            {
                if (counter > maxPwCount)
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

        protected static AccessLevel nextLevel(
            long counter)
        {
            const int max = int.MaxValue;
            const int min = int.MinValue;

            int enumValue;
            if (counter > max)
            {
                enumValue = max;
                goto checkDef;
            }

            if (counter < min)
            {
                enumValue = min;
                goto checkDef;
            }

            enumValue = (int)counter;

            checkDef:
            if (!System.Enum.IsDefined(
                typeof(AccessLevel),
                enumValue))
            {
                const AccessLevel none = AccessLevel.None;
                return none;
            }

            return (AccessLevel)enumValue;
        }

        protected const byte one = 1;
    }
}
