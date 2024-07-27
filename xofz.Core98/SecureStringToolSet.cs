namespace xofz
{
    using System.Runtime.InteropServices;
    using System.Security;

    public class SecureStringToolSet
    {
        // I think this was taken from Stack Overflow
        public virtual string Decode(
            SecureString ss)
        {
            if (ss == null)
            {
                return string.Empty;
            }

            var p = Marshal.SecureStringToBSTR(ss);
            try
            {
                return Marshal.PtrToStringBSTR(p);
            }
            finally
            {
                Marshal.FreeBSTR(p);
            }
        }

        public virtual SecureString Encode(
            string s)
        {
            var ss = new SecureString();
            if (s == null)
            {
                return ss;
            }

            foreach (var c in s)
            {
                ss.AppendChar(c);
            }

            return ss;
        }
    }
}