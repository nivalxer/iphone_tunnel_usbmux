using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel.CoreFundation
{
    [StructLayout(LayoutKind.Sequential)]
    public class SECURITY_ATTRIBUTES
    {
        public int nLength;
        public string lpSecurityDescriptor;
        public bool bInheritHandle;
    }
}