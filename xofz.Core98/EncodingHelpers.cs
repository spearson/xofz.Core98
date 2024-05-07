namespace xofz
{
    using System.Text;

    public class EncodingHelpers
    {
        public static byte GetAsciiByte(
            char c)
        {
            return Encoding.ASCII.GetBytes(
                new[] { c })[zero];
        }

        protected const byte zero = 0;
    }
}
