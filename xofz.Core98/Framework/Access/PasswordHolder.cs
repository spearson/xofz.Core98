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
            var maxPwCount = System
                .Enum
                .GetNames(typeof(AccessLevel))
                .Length;

            var d = new Dictionary<SecureString, AccessLevel>(
                maxPwCount);
            if (passwords == null)
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
            var maxPwCount = System
                .Enum
                .GetNames(typeof(AccessLevel))
                .Length;

            var d = new Dictionary<SecureString, AccessLevel>(
                maxPwCount);
            if (passwords == null)
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
            int enumValue;
            if (counter > int.MaxValue)
            {
                enumValue = int.MaxValue;
                goto checkDef;
            }

            if (counter < int.MinValue)
            {
                enumValue = int.MinValue;
                goto checkDef;
            }

            enumValue = (int)counter;

            checkDef:
            if (!System.Enum.IsDefined(
                typeof(AccessLevel),
                enumValue))
            {
                return AccessLevel.None;
            }

            return (AccessLevel)enumValue;
        }
    }
}
