using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceRestoreNotificationCallback(ref AMRecoveryDevice callback_info);
}