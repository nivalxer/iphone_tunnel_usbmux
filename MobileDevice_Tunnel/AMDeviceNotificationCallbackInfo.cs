using System;
using System.Runtime.InteropServices;

namespace MobileDevice_Tunnel
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMDeviceNotificationCallbackInfo
    {
        internal unsafe IntPtr dev_ptr;
        public NotificationMessage msg;

        public unsafe IntPtr dev
        {
            get { return dev_ptr; }
        }
    }
}