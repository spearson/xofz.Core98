namespace xofz.Framework
{
    using System.Runtime.InteropServices;
    using System.Security;

    public class SecureStringDecoder
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
    }
}
