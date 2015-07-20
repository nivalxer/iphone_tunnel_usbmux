using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace MobileDevice_Tunnel
{
    internal class MobileDevice
    {
        private const string DLLName = "iTunesMobileDevice.dll";

        private static readonly DirectoryInfo ApplicationSupportDirectory =
            new DirectoryInfo(
                Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Application Support", "InstallDir",
                    Environment.CurrentDirectory).ToString());

        private static readonly FileInfo iTunesMobileDeviceFile =
            new FileInfo(
                Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Mobile Device Support\Shared",
                    "iTunesMobileDeviceDLL", "iTunesMobileDevice.dll").ToString());

        static MobileDevice()
        {
            string directoryName = iTunesMobileDeviceFile.DirectoryName;
            if (!iTunesMobileDeviceFile.Exists)
            {
                directoryName = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) +
                                @"\Apple\Mobile Device Support\bin";
                if (!File.Exists(directoryName + @"\iTunesMobileDevice.dll"))
                {
                    directoryName = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) +
                                @"\Apple\Mobile Device Support";
                    if (!File.Exists(directoryName + @"\iTunesMobileDevice.dll"))
                    {
                        directoryName = @"C:\Program Files\Apple\Mobile Device Support\bin";
                    }
                }
            }
            Environment.SetEnvironmentVariable("Path",
                string.Join(";",
                    new[]
                    {Environment.GetEnvironmentVariable("Path"), directoryName, ApplicationSupportDirectory.FullName}));
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr __CFStringMakeConstantString(byte[] s);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionClose(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionGetFSBlockSize(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionInvalidate(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionIsValid(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionOpen(IntPtr handle, uint io_timeout, ref IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDeviceInfoOpen(IntPtr conn, ref IntPtr dict);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceInstallApplication(IntPtr conn, IntPtr path, IntPtr options, IntPtr callback,
            IntPtr unknow1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceTransferApplication(IntPtr conn, IntPtr path, IntPtr options,
            DeviceInstallApplicationCallback callback, IntPtr unknow1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceUninstallApplication(IntPtr conn, IntPtr bundleIdentifier,
            IntPtr installOption, IntPtr unknown0, IntPtr unknown1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceArchiveApplication(IntPtr conn, IntPtr bundleIdentifier, IntPtr options,
            DeviceInstallApplicationCallback callback);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDPostNotification(IntPtr conn, IntPtr NoticeMessage, uint uint_0);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDObserveNotification(IntPtr conn, IntPtr NoticeMessage);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceSecureStartService(IntPtr conn, IntPtr sericename, IntPtr intptr_2,
            ref IntPtr socket);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionGetSocket(IntPtr intptr_0);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceLookupApplicationArchives(IntPtr conn, IntPtr AppType, ref IntPtr result);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceLookupApplications(IntPtr conn, IntPtr AppType, ref IntPtr result);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryClose(IntPtr conn, IntPtr dir);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryCreate(IntPtr conn, string path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefLock(IntPtr conn, long long_0);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryOpen(IntPtr conn, byte[] path, ref IntPtr dir);

        public static int AFCDirectoryOpen(IntPtr conn, string path, ref IntPtr dir)
        {
            return AFCDirectoryOpen(conn, Encoding.UTF8.GetBytes(path), ref dir);
        }

        public static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref string buffer)
        {
            int ret;
            IntPtr ptr = IntPtr.Zero;
            ret = AFCDirectoryRead(conn, dir, ref ptr);
            if ((ret == 0) && (ptr != IntPtr.Zero))
            {
                var ipPtr = ptr;
                var bufferArray = new ArrayList();
                int curr = 0;
                while (true)
                {
                    byte tmpByte = Marshal.ReadByte(ipPtr, curr);
                    if (tmpByte != 0)
                    {
                        bufferArray.Add(tmpByte);
                        curr++;
                    }
                    else
                    {
                        break;
                    }
                }
                buffer = Encoding.UTF8.GetString((byte[]) bufferArray.ToArray(typeof (byte)));
            }
            else
            {
                buffer = null;
            }
            return ret;
        }

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref IntPtr dirent);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileInfoOpen(IntPtr conn, byte[] path, ref IntPtr dict);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefClose(IntPtr conn, long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefOpen(IntPtr conn, byte[] path, int mode, int unknown, ref long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefRead(IntPtr conn, long handle, byte[] buffer, ref uint len);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefSeek(IntPtr conn, long handle, uint pos, uint origin);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefSetFileSize(IntPtr conn, long handle, uint size);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefTell(IntPtr conn, long handle, ref uint position);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefWrite(IntPtr conn, long handle, byte[] buffer, uint len);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFlushData(IntPtr conn, long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCKeyValueClose(IntPtr dict);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int AFCKeyValueRead(IntPtr dict, ref IntPtr key, ref IntPtr val);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCRemovePath(IntPtr conn, byte[] path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCRenamePath(IntPtr conn, byte[] old_path, byte[] new_path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceActivate(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceConnect(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMDeviceCopyDeviceIdentifier(IntPtr device);

        public static string AMDeviceCopyValue(IntPtr device, string name)
        {
            IntPtr ptr = AMDeviceCopyValue_IntPtr(device, 0, CFStringMakeConstantString(name));
            if (ptr != IntPtr.Zero)
            {
                return CoreFundation.CoreFoundation.ReadCFStringFromIntPtr(ptr);
            }
            return string.Empty;
        }

        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDeviceCopyValue",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMDeviceCopyValue_IntPtr(IntPtr device, uint unknown, IntPtr cfstring);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceSetValue(IntPtr device, IntPtr mbz, IntPtr cfstringkey, IntPtr cfstringname);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceDeactivate(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceDisconnect(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceEnterRecovery(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceGetConnectionID(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceIsPaired(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1,
            uint unused2, uint unused3, ref IntPtr am_device_notification_ptr);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceRelease(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStartService(IntPtr device, IntPtr service_name, ref IntPtr handle,
            IntPtr unknown);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStartSession(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStopSession(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceValidatePairing(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopyIMEI(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetLocationID(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetProductID(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetProductType(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetTypeID();

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeGetSoftwareBuildVersion();

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopySerialNumber(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopyAuthlnstallPreflightOptions(byte[] byte_0, IntPtr intptr_0,
            IntPtr intptr_1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceReboot(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceSetAutoBoot(byte[] device, byte paramByte);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string AMRestoreModeDeviceCopyIMEI(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRestoreModeDeviceCreate(uint unknown0, int connection_id, uint unknown1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRestoreRegisterForDeviceNotifications(
            DeviceRestoreNotificationCallback dfu_connect, DeviceRestoreNotificationCallback recovery_connect,
            DeviceRestoreNotificationCallback dfu_disconnect, DeviceRestoreNotificationCallback recovery_disconnect,
            uint unknown0, IntPtr user_info);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int USBMuxConnectByPort(int connectionID, uint iPhone_port_network_byte_order, ref int outSocket);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint htonl(uint hostlong);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint htons(uint hostshort);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint ntohl(uint netlong);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int send(int inSocket, IntPtr buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int send(int inSocket, byte[] buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", EntryPoint = "send", CallingConvention = CallingConvention.StdCall)]
        public static extern int send_UInt32(int inSocket, ref uint buffer, int bufferlen, int flags);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int recv(int inSocket, IntPtr buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int recv(int inSocket, byte[] buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", EntryPoint = "recv", CallingConvention = CallingConvention.StdCall)]
        public static extern int recv_UInt32(int inSocket, ref uint buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int closesocket(int inSocket);
        public static IntPtr CFStringMakeConstantString(string s)
        {
            //return __CFStringMakeConstantString(StringToCString(s));
            return CoreFundation.CoreFoundation.StringToCFString(s);
        }

        public static string CFStringToString(byte[] value)
        {
            return CoreFundation.CoreFoundation.CFStringToString(value);
        }

        public static byte[] StringToCFString(string value)
        {
            var bytes = new byte[value.Length + 10];
            bytes[4] = 140;
            bytes[5] = 7;
            bytes[6] = 1;
            bytes[8] = (byte) value.Length;
            Encoding.UTF8.GetBytes(value, 0, value.Length, bytes, 9);
            return bytes;
        }

        public static byte[] StringToCString(string value)
        {
            return Encoding.UTF8.GetBytes(value); ;
        }
    }
}