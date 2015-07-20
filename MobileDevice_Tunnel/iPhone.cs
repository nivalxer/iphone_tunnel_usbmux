using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using MobileDevice_Tunnel.CoreFundation;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MobileDevice_Tunnel
{
    public unsafe class iPhone
    {
        private static readonly char[] path_separators = {'/'};
        private byte[] DFUHandle; //DFU和Recovery下识别暂时有问题
        private bool connected;
        private string current_directory;
        private DeviceNotificationCallback dnc;
        private DeviceRestoreNotificationCallback drn1;
        private DeviceRestoreNotificationCallback drn2;
        private DeviceRestoreNotificationCallback drn3;
        private DeviceRestoreNotificationCallback drn4;
        internal IntPtr hAFC; //afc链接句柄
        internal IntPtr hService; //afc服务句柄
        private iPhoneDFUDevice iPhoneDFU;
        internal IntPtr iPhoneHandle; //设备链接句柄
        private iPhoneRecoveryDevice iPhoneRecovery;
        private DeviceInstallApplicationCallback installCallBack;
        internal IntPtr installFD; //install安装服务句柄
        internal IntPtr notificationService; //通知事件句柄
        private bool wasAFC2;

        public iPhone()
        {
            wasAFC2 = false;
            doConstruction();
        }

        /// <summary>
        ///     实例化iPhone，需要传入对应处理事件
        /// </summary>
        /// <param name="myConnectHandler">当设备连接时事件</param>
        /// <param name="myDisconnectHandler">当设备断开连接时事件</param>
        public iPhone(ConnectEventHandler myConnectHandler, ConnectEventHandler myDisconnectHandler)
        {
            wasAFC2 = false;
            Connect = (ConnectEventHandler) Delegate.Combine(Connect, myConnectHandler);
            Disconnect = (ConnectEventHandler) Delegate.Combine(Disconnect, myDisconnectHandler);
            doConstruction();
        }

        public IntPtr AFCHandle
        {
            get { return hAFC; }
        }

        public IntPtr Device
        {
            get { return iPhoneHandle; }
        }

        public bool IsConnected
        {
            get { return connected; }
        }

        public event ConnectEventHandler Connect;

        public event DeviceNotificationEventHandler DfuConnect;

        public event DeviceNotificationEventHandler DfuDisconnect;

        public event ConnectEventHandler Disconnect;

        public event DeviceNotificationEventHandler RecoveryModeEnter;

        public event DeviceNotificationEventHandler RecoveryModeLeave;

        /// <summary>
        ///     链接到设备并开启相应服务，无需单独调用，在NotifyCallback已经调用
        /// </summary>
        /// <returns></returns>
        private bool ConnectToPhone()
        {
            if (MobileDevice.AMDeviceConnect(iPhoneHandle) == 1)
            {
                throw new Exception("Phone in recovery mode, support not yet implemented");
            }
            //if (MobileDevice.AMDeviceIsPaired(iPhoneHandle) == 0)
            //{
            //    return false;
            //}
            //if (MobileDevice.AMDeviceValidatePairing(iPhoneHandle) != 0)
            //{
            //    return false;
            //}
            if (MobileDevice.AMDeviceStartSession(iPhoneHandle) == 1)
            {
                return false;
            }
            connected = true;
            return true;
        }


        private void DfuConnectCallback(ref AMRecoveryDevice callback)
        {
            DFUHandle = callback.devicePtr;
            iPhoneDFU = new iPhoneDFUDevice(DFUHandle);
            OnDfuConnect(new DeviceNotificationEventArgs(callback));
        }

        private void DfuDisconnectCallback(ref AMRecoveryDevice callback)
        {
            DFUHandle = callback.devicePtr;
            iPhoneDFU = null;
            OnDfuDisconnect(new DeviceNotificationEventArgs(callback));
        }

        /// <summary>
        ///     配置链接，注册通知回调
        /// </summary>
        private void doConstruction()
        {
            IntPtr voidPtr=IntPtr.Zero;
            dnc = NotifyCallback;
            drn1 = DfuConnectCallback;
            drn2 = RecoveryConnectCallback;
            drn3 = DfuDisconnectCallback;
            drn4 = RecoveryDisconnectCallback;
            int num = MobileDevice.AMDeviceNotificationSubscribe(dnc, 0, 0, 0, ref voidPtr);
            if (num != 0)
            {
                throw new Exception("AMDeviceNotificationSubscribe failed with error " + num);
            }
            num = MobileDevice.AMRestoreRegisterForDeviceNotifications(drn1, drn2, drn3, drn4, 0, IntPtr.Zero);
            if (num != 0)
            {
                throw new Exception("AMRestoreRegisterForDeviceNotifications failed with error " + num);
            }
            current_directory = "/";
        }
      
        /// <summary>
        ///     获取完整路径
        /// </summary>
        /// <param name="path1">建议传入CurrentDirectory</param>
        /// <param name="path2">当前路径</param>
        /// <returns></returns>
        internal string FullPath(string path1, string path2)
        {
            string[] strArray;
            if ((path1 == null) || (path1 == string.Empty))
            {
                path1 = "/";
            }
            if ((path2 == null) || (path2 == string.Empty))
            {
                path2 = "/";
            }
            if (path2[0] == '/')
            {
                strArray = path2.Split(path_separators);
            }
            else if (path1[0] == '/')
            {
                strArray = (path1 + "/" + path2).Split(path_separators);
            }
            else
            {
                strArray = ("/" + path1 + "/" + path2).Split(path_separators);
            }
            var strArray2 = new string[strArray.Length];
            int count = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == "..")
                {
                    if (count > 0)
                    {
                        count--;
                    }
                }
                else if ((strArray[i] != ".") && !(strArray[i] == ""))
                {
                    strArray2[count++] = strArray[i];
                }
            }
            return ("/" + string.Join("/", strArray2, 0, count));
        }

        /// <summary>
        ///     获取UDID
        /// </summary>
        /// <returns></returns>
        public string GetCopyDeviceIdentifier()
        {
            return Marshal.PtrToStringAnsi(MobileDevice.AMDeviceCopyDeviceIdentifier(iPhoneHandle));
        }

        /// <summary>
        ///     获取设备所有信息
        /// </summary>
        /// <returns>Dictionary集合，第一个参数为键，第二个参数为键值</returns>
        public Dictionary<string, string> GetDeviceInfo()
        {
            var dictionary = new Dictionary<string, string>();
            IntPtr dict = IntPtr.Zero;
            if ((MobileDevice.AFCDeviceInfoOpen(hAFC, ref dict) == 0) && (dict != null))
            {
                IntPtr voidPtr2=IntPtr.Zero;
                IntPtr voidPtr3 = IntPtr.Zero;
                while (((MobileDevice.AFCKeyValueRead(dict, ref voidPtr2, ref voidPtr3) == 0) && (voidPtr2 != null)) &&
                       (voidPtr3 != null))
                {
                    string key = Marshal.PtrToStringAnsi(voidPtr2);
                    string str2 = Marshal.PtrToStringAnsi(voidPtr3);
                    dictionary.Add(key, str2);
                }
                MobileDevice.AFCKeyValueClose(dict);
            }
            return dictionary;
        }

        /// <summary>
        ///     获取恢复模式下设备序列号
        /// </summary>
        /// <returns></returns>
        public string GetRecoverySerialNumber()
        {
            return iPhoneRecovery.SerialNumber;
        }

        public string GetiPhoneStr(string str)
        {
            return MobileDevice.AMDeviceCopyValue(iPhoneHandle, str);
        }

        public bool EnterRecovery()
        {
            int i = MobileDevice.AMDeviceEnterRecovery(iPhoneHandle);
            if (i == (int) kAMDError.kAMDSuccess)
            {
                return true;
            }
            return false;
        }

        public bool GoOutRecovery()
        {
            return iPhoneRecovery.exitRecovery();
        }


        private void NotifyCallback(ref AMDeviceNotificationCallbackInfo callback)
        {
            if (callback.msg == NotificationMessage.Connected)
            {
                iPhoneHandle = callback.dev;
                if (ConnectToPhone())
                {
                    OnConnect(new ConnectEventArgs(callback));
                }
            }
            else if (callback.msg == NotificationMessage.Disconnected)
            {
                connected = false;
                OnDisconnect(new ConnectEventArgs(callback));
            }
        }

        protected void OnConnect(ConnectEventArgs args)
        {
            ConnectEventHandler connect = Connect;
            if (connect != null)
            {
                connect(this, args);
            }
        }

        protected void OnDfuConnect(DeviceNotificationEventArgs args)
        {
            DeviceNotificationEventHandler dfuConnect = DfuConnect;
            if (dfuConnect != null)
            {
                dfuConnect(this, args);
            }
        }

        protected void OnDfuDisconnect(DeviceNotificationEventArgs args)
        {
            DeviceNotificationEventHandler dfuDisconnect = DfuDisconnect;
            if (dfuDisconnect != null)
            {
                dfuDisconnect(this, args);
            }
        }

        protected void OnDisconnect(ConnectEventArgs args)
        {
            ConnectEventHandler disconnect = Disconnect;
            if (disconnect != null)
            {
                disconnect(this, args);
            }
        }

        protected void OnRecoveryModeEnter(DeviceNotificationEventArgs args)
        {
            DeviceNotificationEventHandler recoveryModeEnter = RecoveryModeEnter;
            if (recoveryModeEnter != null)
            {
                recoveryModeEnter(this, args);
            }
        }

        protected void OnRecoveryModeLeave(DeviceNotificationEventArgs args)
        {
            DeviceNotificationEventHandler recoveryModeLeave = RecoveryModeLeave;
            if (recoveryModeLeave != null)
            {
                recoveryModeLeave(this, args);
            }
        }

        public void ReConnect()
        {
            int num = MobileDevice.AFCConnectionClose(hAFC);
            num = MobileDevice.AMDeviceStopSession(iPhoneHandle);
            num = MobileDevice.AMDeviceDisconnect(iPhoneHandle);
            ConnectToPhone();
        }

        private void RecoveryConnectCallback(ref AMRecoveryDevice callback)
        {
            DFUHandle = callback.devicePtr;
            iPhoneRecovery = new iPhoneRecoveryDevice(DFUHandle);
            OnRecoveryModeEnter(new DeviceNotificationEventArgs(callback));
        }

        private void RecoveryDisconnectCallback(ref AMRecoveryDevice callback)
        {
            DFUHandle = callback.devicePtr;
            iPhoneRecovery = null;
            OnRecoveryModeLeave(new DeviceNotificationEventArgs(callback));
        }

        /// <summary>
        /// 开启AFC服务
        /// </summary>
        /// <param name="inSocket">socket句柄</param>
        /// <param name="blnInclude"></param>
        /// <returns></returns>
        public IntPtr OpenConnection(int inSocket)
        {
            IntPtr zero = IntPtr.Zero;
            try
            {
                if (MobileDevice.AFCConnectionOpen(new IntPtr(inSocket), 0, ref zero) != (int)kAMDError.kAMDSuccess)
                {
                    return zero;
                }
            }
            catch
            {
            }
            return zero;
        }

        /// <summary>
        /// iPhone端口本地映射
        /// </summary>
        public void CreateUSBMuxConnect(int port=2222)
        {
            //连接打开成功，建立双向通道负责数据转发
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建Socket对象
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint server = new IPEndPoint(serverIP, port);    //实例化服务器的IP和端口
            s.Bind(server);
            s.Listen(0);
            int socket = 0;
            while (true)
            {
                Socket cSocket;
                cSocket = s.Accept(); //用cSocket来代表该客户端连接
                int ret = MobileDevice.USBMuxConnectByPort(MobileDevice.AMDeviceGetConnectionID(this.Device),
                    MobileDevice.htons(22), ref socket);
                if (ret != 0)
                {
                    //连接错误，跳出
                    return;
                }
                IntPtr conn = IntPtr.Zero;
                conn = this.OpenConnection(socket); //socket链接通过AFC函数来打开，然后剩下工作交给映射的recv和send函数，函数模型和winsock相同
                if (conn != IntPtr.Zero)
                {
                    try
                    {
                        if (cSocket.Connected) //测试是否连接成功
                        {
                            Thread tread1 = new Thread(() =>
                            {
                                Conn_Forwarding_Thread(cSocket.Handle.ToInt32(), socket);
                                //USBMuxClientToPhone(cSocket, socket);
                            });
                            tread1.Start();
                            Thread tread2 = new Thread(() =>
                            {
                                Conn_Forwarding_Thread(socket, cSocket.Handle.ToInt32());
                                //USBMuxPhoneToClient(cSocket, socket);
                            });
                            tread2.Start();
                        }

                    }
                    catch
                    {

                    }
                }
            }
        }

        private void Conn_Forwarding_Thread(int from, int to)
        {
            byte[] recv_buf=new byte[256];
            int bytes_recv, bytes_send;

            while (true)
            {
                bytes_recv = MobileDevice.recv(from, recv_buf, 256, 0);
                if (bytes_recv == -1)
                {
                    //string errorMsg = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;//Winsock错误获取
                    int errorno = Marshal.GetLastWin32Error();
                    if (errorno == 10035) //10035 Socket无数据报错，可能函数原型为异步非阻塞，这里作为阻塞式调用而导致异常，可以忽略
                    {
                        continue;
                    }
                    MobileDevice.closesocket(from);
                    MobileDevice.closesocket(to);
                    break;
                }
                bytes_send = MobileDevice.send(to, recv_buf, bytes_recv, 0);

                if (bytes_recv == 0 || bytes_recv == -1 || bytes_send == 0 || bytes_send == -1)
                {
                    MobileDevice.closesocket(from);
                    MobileDevice.closesocket(to);

                    break;
                }
            }
        }
    }
}