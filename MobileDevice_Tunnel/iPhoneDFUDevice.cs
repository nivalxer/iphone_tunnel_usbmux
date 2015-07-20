using Microsoft.VisualBasic.CompilerServices;

namespace MobileDevice_Tunnel
{
    public class iPhoneDFUDevice
    {
        private readonly byte[] DFUHandle;

        /*public iPhoneDFUDevice(AMRecoveryDevice device)
        {
            this.DFUHandle = device.byte_0;
        }*/

        public iPhoneDFUDevice(byte[] device)
        {
            DFUHandle = device;
        }


        public string LocationID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetLocationID(DFUDevice)); }
        }

        public string ProductID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductID(DFUDevice)); }
        }

        public string ProductType
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductType(DFUDevice)); }
        }

        public byte[] DFUDevice
        {
            get { return DFUHandle; }
        }


        public string TypeID
        {
            get { return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetTypeID()); }
        }
    }
}