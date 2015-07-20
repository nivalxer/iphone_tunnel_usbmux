using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MobileDevice_Tunnel;

namespace iphone_tunnel_usbmux
{
    class Program
    {
        private static iPhone iphone;
        static int port = 22;
        private static int ThreadCount = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用由威锋技术组(WeiPhone Tech Team)出品的iOS SSH隧道映射工具");
            try
            {
                iphone = new iPhone();
                Console.WriteLine("初始化MobileDevice成功");
            }
            catch
            {
                Console.WriteLine("初始化MobileDevice失败。请检查iTunes是否正常安装");
                Console.Read();
                return;
            }
            Console.Write("请输入待映射的本机端口号，不输入则默认本机端口22：");
            string sport=Console.ReadLine();
            port = 22;
            try
            {
                port = Convert.ToInt32(sport);
            }
            catch
            {
                port = 22;
            }
            if (port < 0 || port > 65535)
            {
                Console.WriteLine("端口号不能小于0或大于65535，默认使用端口22");
                port = 22;
            }
            Console.WriteLine("本机映射端口号：{0}", port);
            Console.WriteLine("等待设备链接，如果设备已经连接，请重新拔插数据线，如需退出请输入quit回车");
            iphone.Connect += ConnectEventHandler;
            iphone.Disconnect += ConnectEventHandler;
            while (true)
            {
                string key = Console.ReadLine();
                if (key.ToLower().Trim() == "quit")
                {
                    return;
                }
            }
        }

        private static void ConnectEventHandler(object sender, EventArgs e)
        {
            if (iphone.IsConnected)
            {
                Console.WriteLine("设备(链接句柄{0})已连接，正在开启SSH隧道", iphone.Device.ToString());
                Thread thread1 = new Thread(() =>
                {
                    iphone.CreateUSBMuxConnect(port);
                });
                thread1.Start();
                ThreadCount++;
                Console.WriteLine("设备(链接句柄{0})SSH隧道已建立成功", iphone.Device.ToString());
            }
            else
            {
                Console.WriteLine("设备(链接句柄{0})断开链接", iphone.Device.ToString());
                ThreadCount--;
            }
        }
    }
}
