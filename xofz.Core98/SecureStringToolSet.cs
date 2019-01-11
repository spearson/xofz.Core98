namespace xofz
{
    using System.Runtime.InteropServices;
    using System.Security;

    public class SecureStringToolSet
    {
        public virtual string Decode(SecureString ss)
        {
            var binaryStringPointer = Marshal.SecureStringToBSTR(ss);
            try
            {
                return Marshal.PtrToStringBSTR(binaryStringPointer);
            }
            finally
            {
                Marshal.FreeBSTR(binaryStringPointer);
            }
        }

        public virtual SecureString Encode(string s)
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
