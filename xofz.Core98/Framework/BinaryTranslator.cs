namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTranslator
    {
        public virtual IEnumerable<bool> GetBits(
            long number)
        {
            return this.GetBits(
                System.BitConverter.GetBytes(number));
        }

        public virtual IEnumerable<bool> GetBits(
            string s,
            Encoding encoding)
        {
            if (s == null || encoding == null)
            {
                return EnumerableHelpers.Empty<bool>();
            }

            return this.GetBits(encoding.GetBytes(s));
        }

        public virtual IEnumerable<bool> GetBits(
            IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                yield break;
            }

            const byte 
                six = 6,
                five = 5,
                four = 4,
                three = 3;
            foreach (var b in bytes)
            {
                yield return this.getBit(b, seven);
                yield return this.getBit(b, six);
                yield return this.getBit(b, five);
                yield return this.getBit(b, four);
                yield return this.getBit(b, three);
                yield return this.getBit(b, two);
                yield return this.getBit(b, one);
                yield return this.getBit(b, zero);
            }
        }

        public virtual long ReadNumber(
            IEnumerable<bool> bits)
        {
            var bytes = EnumerableHelpers.ToArray(
                this.GetBytes(bits));
            return System.BitConverter.ToInt64(bytes, zero);
        }

        public virtual string ReadString(
            IEnumerable<bool> bits, 
            Encoding encoding)
        {
            return encoding?.GetString(
                EnumerableHelpers.ToArray(
                    this.GetBytes(bits)));
        }

        public virtual IEnumerable<byte> GetBytes(
            IEnumerable<bool> bits)
        {
            var e = bits?.GetEnumerator();
            if (e == null)
            {
                yield break;
            }

            while (true)
            {
                if (!e.MoveNext())
                {
                    e.Dispose();
                    break;
                }

                var bit1 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1
                        });
                }

                var bit2 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2
                        });
                }

                var bit3 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3
                        });
                }

                var bit4 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4
                        });
                }

                var bit5 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5
                        });
                }

                var bit6 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5,
                            bit6
                        });
                }

                var bit7 = e.Current;
                if (!e.MoveNext())
                {
                    yield return this.getByte(
                        new[]
                        {
                            bit1,
                            bit2,
                            bit3,
                            bit4,
                            bit5,
                            bit6,
                            bit7
                        });
                }

                var bit8 = e.Current;
                yield return this.getByte(
                    new[]
                    {
                        bit1,
                        bit2,
                        bit3,
                        bit4,
                        bit5,
                        bit6,
                        bit7,
                        bit8
                    });
            }
        }

        protected virtual bool getBit(
            byte b, 
            byte shift)
        {
            return (b >> shift) % two == one;
        }

        protected virtual byte getByte(
            bool[] bits)
        {
            byte result = zero;
            if (bits == null)
            {
                return result;
            }

            var l = bits.Length;
            for (var i = zero; i < eight && i < l; ++i)
            {
                result += bits[i] ? (byte)(one << (seven - i)) : zero;
            }

            return result;
        }

        protected const byte
            zero = 0,
            one = 1,
            two = 2,
            seven = 7,
            eight = 8;
    }
}
