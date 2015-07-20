using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMDeviceNotification
    {
        private readonly uint unknown0;
        private readonly uint unknown1;
        private readonly uint unknown2;
        private readonly DeviceNotificationCallback callback;
        private readonly uint unknown3;
    }
}