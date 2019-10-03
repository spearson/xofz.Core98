namespace xofz
{
    using System.Text;

    public static class EncodingHelpers
    {
        public static byte GetAsciiByte(char c)
        {
            return Encoding.ASCII.GetBytes(
                new[] { c })[0];
        }
    }
}
