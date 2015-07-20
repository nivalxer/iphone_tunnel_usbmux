using System;

namespace MobileDevice_Tunnel
{
    public class DeviceNotificationEventArgs : EventArgs
    {
        private readonly AMRecoveryDevice device;

        internal DeviceNotificationEventArgs(AMRecoveryDevice device)
        {
            this.device = device;
        }

        internal AMRecoveryDevice Device
        {
            get { return device; }
        }
    }
}