using System;

namespace MobileDevice_Tunnel
{
    public unsafe class ConnectEventArgs : EventArgs
    {
        private readonly IntPtr device;
        private readonly NotificationMessage message;

        internal ConnectEventArgs(AMDeviceNotificationCallbackInfo cbi)
        {
            message = cbi.msg;
            device = cbi.dev;
        }

        public IntPtr Device
        {
            get { return device; }
        }

        public NotificationMessage Message
        {
            get { return message; }
        }
    }
}