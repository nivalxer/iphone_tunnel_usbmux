using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using MobileDevice_Tunnel.CoreFundation;

namespace MobileDevice_Tunnel
{
    public class iPhoneRecoveryDevice
    {
        private readonly byte[] RecoveryHandle;
        private string IMEINum = "";
        private string SerialNum = "";

        /*public iPhoneRecoveryDevice(AMRecoveryDevice device)
        {
            this.RecoveryHandle = device.byte_0;
        }*/

        public iPhoneRecoveryDevice(byte[] device)
        {
            RecoveryHandle = device;
        }

        /// <summary>
        ///     暂未完善
        /// </summary>
        public object AuthlnstallPreflightOptions
        {
            get
            {
                object objectValue = null;
                try
                {
                    IntPtr sourceRef = MobileDevice.AMRecoveryModeDeviceCopyAuthlnstallPreflightOptions(RecoveryHandle,
                        IntPtr.Zero, IntPtr.Zero);
                    //if (sourceRef != IntPtr.Zero) objectValue = RuntimeHelpers.GetObjectValue(CoreFoundation.ManagedTypeFromCFType(ref sourceRef));
                }
                catch (Exception ex)
                {
                }
                return objectValue;
            }
        }

        public string IMEI
        {
            get
            {
                if (IMEINum.Length == 0)
                {
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero = MobileDevice.AMRecoveryModeDeviceCopyIMEI(RecoveryHandle);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            IMEINum = CoreFoundation.ReadCFStringFromIntPtr(zero);
                        }
                        else
                            IMEINum = "";
                    }
                }
                return IMEINum;
            }
        }

        public string LocationID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetLocationID(RecoveryDevice)); }
        }

        public string ProductID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductID(RecoveryDevice)); }
        }

        public string ProductType
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductType(RecoveryDevice)); }
        }

        public byte[] RecoveryDevice
        {
            get { return RecoveryHandle; }
        }

        public string SerialNumber
        {
            get
            {
                if (SerialNum.Length == 0)
                {
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero = MobileDevice.AMRecoveryModeDeviceCopySerialNumber(RecoveryDevice);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            SerialNum = CoreFoundation.ReadCFStringFromIntPtr(zero);
                        }
                        else
                            SerialNum = "";
                    }
                }
                return SerialNum;
            }
        }

        public string TypeID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetTypeID()); }
        }

        public string Version
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeGetSoftwareBuildVersion()); }
        }

        public bool exitRecovery()
        {
            if (setAutoBoot(true) == (int) kAMDError.kAMDSuccess)
            {
                reboot();
                return true;
            }
            return false;
        }

        public void reboot()
        {
            MobileDevice.AMRecoveryModeDeviceReboot(RecoveryHandle);
        }

        public int setAutoBoot(bool value)
        {
            return MobileDevice.AMRecoveryModeDeviceSetAutoBoot(RecoveryHandle,
                Conversions.ToByte(Interaction.IIf(value, 1, 0)));
        }
    }
}