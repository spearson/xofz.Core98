﻿namespace xofz.Framework
{
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    public class AlphanumericGen
    {
        public virtual string Generate(
            byte length)
        {
            var sb = new StringBuilder(length);
            var buffer = new byte[oneN];
            short counter = zeroN;
            var rng = RandomNumberGenerator.Create();
            while (counter < length)
            {
                rng.GetBytes(buffer);
                var nextChar = (char)buffer[zeroN];

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
            (disposable as System.IDisposable)?.Dispose();
            return sb.ToString();
        }

        public virtual SecureString GenerateSecure(
            byte length)
        {
            var s = new SecureString();
            var buffer = new byte[oneN];
            short counter = zeroN;
            var rng = RandomNumberGenerator.Create();
            while (counter < length)
            {
                rng.GetBytes(buffer);
                var nextChar = (char)buffer[zeroN];

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
            (disposable as System.IDisposable)?.Dispose();

            return s;
        }

        protected const char a = 'a', z = 'z';
        protected const char capA = 'A', capZ = 'Z';
        protected const char zero = '0', nine = '9';

        protected const byte
            zeroN = 0,
            oneN = 1;
    }
}