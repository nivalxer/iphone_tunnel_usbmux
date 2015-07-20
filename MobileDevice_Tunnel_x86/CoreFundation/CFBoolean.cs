using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MobileDevice_Tunnel.CoreFundation
{
    public class CFBoolean
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        public static IntPtr GetCFBoolean(bool flag)
        {
            string strEnumName = flag ? "kCFBooleanTrue" : "kCFBooleanFalse";

            IntPtr intptr_0 = GetModuleHandle("CoreFoundation.dll");
            if (intptr_0 == IntPtr.Zero)
            {
                string AppleApplicationSupportFolder =
                    Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Application Support", "InstallDir",
                        Environment.CurrentDirectory).ToString();
                intptr_0 = LoadLibrary(Path.Combine(AppleApplicationSupportFolder, "CoreFoundation.dll"));
            }
            IntPtr zero = IntPtr.Zero;
            if (intptr_0 != IntPtr.Zero) zero = GetProcAddress(intptr_0, strEnumName);
            return Marshal.ReadIntPtr(zero, 0);
        }
    }
}