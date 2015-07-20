using System;
using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel
{
    /// <summary>
    ///     安装回调，暂未实现
    /// </summary>
    /// <param name="dict"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceInstallApplicationCallback(IntPtr dict);
}