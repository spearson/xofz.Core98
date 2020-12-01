namespace xofz.Framework
{
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public class AlphanumericGen
    {
        public virtual string Generate(
            byte length)
        {
            var sb = new StringBuilder(length);
            var buffer = new byte[1];
            short counter = 0;
            var rng = new RNGCryptoServiceProvider();
            while (counter < length)
            {
                rng.GetBytes(buffer);
                var nextChar = (char)buffer[0];

                if (nextChar >= zero && nextChar <= nine)
                {
                    goto addChar;
                }

                if (nextChar >= capA && nextChar <= capZ)
                {
                    goto addChar;
                }

                if (nextChar >= a && nextChar <= z)
                {
                    goto addChar;
                }

                continue;

            addChar:
                sb.Append(nextChar);
                ++counter;
            }

            object disposable = rng;
            (disposable as IDisposable)?.Dispose();
            return sb.ToString();
        }

        public virtual SecureString GenerateSecure(
            byte length)
        {
            var s = new SecureString();
            var buffer = new byte[1];
            short counter = 0;
            var rng = new RNGCryptoServiceProvider();
            while (counter < length)
            {
                rng.GetBytes(buffer);
                var nextChar = (char)buffer[0];

                if (nextChar >= zero && nextChar <= nine)
                {
                    goto addChar;
                }

                if (nextChar >= capA && nextChar <= capZ)
                {
                    goto addChar;
                }

                if (nextChar >= a && nextChar <= z)
                {
                    goto addChar;
                }

                continue;

            addChar:
                s.AppendChar(nextChar);
                ++counter;
            }

            s.MakeReadOnly();
            object disposable = rng;
            (disposable as IDisposable)?.Dispose();

            return s;
        }

        protected const char a = 'a', z = 'z';
        protected const char capA = 'A', capZ = 'Z';
        protected const char zero = '0', nine = '9';
    }
}
