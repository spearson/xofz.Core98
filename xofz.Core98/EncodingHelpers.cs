namespace xofz
{
    using System.Text;

    public static class EncodingHelpers
    {
        public static byte GetAsciiByte(
            char c)
        {
            const byte zero = 0;
            return Encoding.ASCII.GetBytes(
                new[] { c })[zero];
        }
    }
}
