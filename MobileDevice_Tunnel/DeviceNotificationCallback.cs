using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
}