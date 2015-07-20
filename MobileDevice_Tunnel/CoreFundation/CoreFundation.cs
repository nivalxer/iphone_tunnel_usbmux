using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MobileDevice_Tunnel.CoreFundation
{
    public class CoreFoundation
    {
        public enum CFStringEncoding
        {
            kCFStringEncodingANSEL = 0x601,
            kCFStringEncodingASCII = 0x600,
            kCFStringEncodingBig5 = 0xa03,
            kCFStringEncodingBig5_E = 0xa09,
            kCFStringEncodingBig5_HKSCS_1999 = 0xa06,
            kCFStringEncodingCNS_11643_92_P1 = 0x651,
            kCFStringEncodingCNS_11643_92_P2 = 0x652,
            kCFStringEncodingCNS_11643_92_P3 = 0x653,
            kCFStringEncodingDOSArabic = 0x419,
            kCFStringEncodingDOSBalticRim = 0x406,
            kCFStringEncodingDOSCanadianFrench = 0x418,
            kCFStringEncodingDOSChineseSimplif = 0x421,
            kCFStringEncodingDOSChineseTrad = 0x423,
            kCFStringEncodingDOSCyrillic = 0x413,
            kCFStringEncodingDOSGreek = 0x405,
            kCFStringEncodingDOSGreek1 = 0x411,
            kCFStringEncodingDOSGreek2 = 0x41c,
            kCFStringEncodingDOSHebrew = 0x417,
            kCFStringEncodingDOSIcelandic = 0x416,
            kCFStringEncodingDOSJapanese = 0x420,
            kCFStringEncodingDOSKorean = 0x422,
            kCFStringEncodingDOSLatin1 = 0x410,
            kCFStringEncodingDOSLatin2 = 0x412,
            kCFStringEncodingDOSLatinUS = 0x400,
            kCFStringEncodingDOSNordic = 0x41a,
            kCFStringEncodingDOSPortuguese = 0x415,
            kCFStringEncodingDOSRussian = 0x41b,
            kCFStringEncodingDOSThai = 0x41d,
            kCFStringEncodingDOSTurkish = 0x414,
            kCFStringEncodingEBCDIC_CP037 = 0xc02,
            kCFStringEncodingEBCDIC_US = 0xc01,
            kCFStringEncodingEUC_CN = 0x930,
            kCFStringEncodingEUC_JP = 0x920,
            kCFStringEncodingEUC_KR = 0x940,
            kCFStringEncodingEUC_TW = 0x931,
            kCFStringEncodingGB_18030_2000 = 0x632,
            kCFStringEncodingGB_2312_80 = 0x630,
            kCFStringEncodingGBK_95 = 0x631,
            kCFStringEncodingHZ_GB_2312 = 0xa05,
            kCFStringEncodingISO_2022_CN = 0x830,
            kCFStringEncodingISO_2022_CN_EXT = 0x831,
            kCFStringEncodingISO_2022_JP = 0x820,
            kCFStringEncodingISO_2022_JP_1 = 0x822,
            kCFStringEncodingISO_2022_JP_2 = 0x821,
            kCFStringEncodingISO_2022_JP_3 = 0x823,
            kCFStringEncodingISO_2022_KR = 0x840,
            kCFStringEncodingISOLatin1 = 0x201,
            kCFStringEncodingISOLatin10 = 0x210,
            kCFStringEncodingISOLatin2 = 0x202,
            kCFStringEncodingISOLatin3 = 0x203,
            kCFStringEncodingISOLatin4 = 0x204,
            kCFStringEncodingISOLatin5 = 0x209,
            kCFStringEncodingISOLatin6 = 0x20a,
            kCFStringEncodingISOLatin7 = 0x20d,
            kCFStringEncodingISOLatin8 = 0x20e,
            kCFStringEncodingISOLatin9 = 0x20f,
            kCFStringEncodingISOLatinArabic = 0x206,
            kCFStringEncodingISOLatinCyrillic = 0x205,
            kCFStringEncodingISOLatinGreek = 0x207,
            kCFStringEncodingISOLatinHebrew = 520,
            kCFStringEncodingISOLatinThai = 0x20b,
            kCFStringEncodingJIS_C6226_78 = 0x624,
            kCFStringEncodingJIS_X0201_76 = 0x620,
            kCFStringEncodingJIS_X0208_83 = 0x621,
            kCFStringEncodingJIS_X0208_90 = 0x622,
            kCFStringEncodingJIS_X0212_90 = 0x623,
            kCFStringEncodingKOI8_R = 0xa02,
            kCFStringEncodingKOI8_U = 0xa08,
            kCFStringEncodingKSC_5601_87 = 0x640,
            kCFStringEncodingKSC_5601_92_Johab = 0x641,
            kCFStringEncodingMacArabic = 4,
            kCFStringEncodingMacArmenian = 0x18,
            kCFStringEncodingMacBengali = 13,
            kCFStringEncodingMacBurmese = 0x13,
            kCFStringEncodingMacCeltic = 0x27,
            kCFStringEncodingMacCentralEurRoman = 0x1d,
            kCFStringEncodingMacChineseSimp = 0x19,
            kCFStringEncodingMacChineseTrad = 2,
            kCFStringEncodingMacCroatian = 0x24,
            kCFStringEncodingMacCyrillic = 7,
            kCFStringEncodingMacDevanagari = 9,
            kCFStringEncodingMacDingbats = 0x22,
            kCFStringEncodingMacEthiopic = 0x1c,
            kCFStringEncodingMacExtArabic = 0x1f,
            kCFStringEncodingMacFarsi = 140,
            kCFStringEncodingMacGaelic = 40,
            kCFStringEncodingMacGeorgian = 0x17,
            kCFStringEncodingMacGreek = 6,
            kCFStringEncodingMacGujarati = 11,
            kCFStringEncodingMacGurmukhi = 10,
            kCFStringEncodingMacHebrew = 5,
            kCFStringEncodingMacHFS = 0xff,
            kCFStringEncodingMacIcelandic = 0x25,
            kCFStringEncodingMacInuit = 0xec,
            kCFStringEncodingMacJapanese = 1,
            kCFStringEncodingMacKannada = 0x10,
            kCFStringEncodingMacKhmer = 20,
            kCFStringEncodingMacKorean = 3,
            kCFStringEncodingMacLaotian = 0x16,
            kCFStringEncodingMacMalayalam = 0x11,
            kCFStringEncodingMacMongolian = 0x1b,
            kCFStringEncodingMacOriya = 12,
            kCFStringEncodingMacRoman = 0,
            kCFStringEncodingMacRomanian = 0x26,
            kCFStringEncodingMacRomanLatin1 = 0xa04,
            kCFStringEncodingMacSinhalese = 0x12,
            kCFStringEncodingMacSymbol = 0x21,
            kCFStringEncodingMacTamil = 14,
            kCFStringEncodingMacTelugu = 15,
            kCFStringEncodingMacThai = 0x15,
            kCFStringEncodingMacTibetan = 0x1a,
            kCFStringEncodingMacTurkish = 0x23,
            kCFStringEncodingMacUkrainian = 0x98,
            kCFStringEncodingMacVietnamese = 30,
            kCFStringEncodingMacVT100 = 0xfc,
            kCFStringEncodingNextStepJapanese = 0xb02,
            kCFStringEncodingNextStepLatin = 0xb01,
            kCFStringEncodingShiftJIS = 0xa01,
            kCFStringEncodingShiftJIS_X0213 = 0x628,
            kCFStringEncodingShiftJIS_X0213_00 = 0x628,
            kCFStringEncodingShiftJIS_X0213_MenKuTen = 0x629,
            kCFStringEncodingUTF7 = 0x4000100,
            kCFStringEncodingUTF7_IMAP = 0xa10,
            kCFStringEncodingVISCII = 0xa07,
            kCFStringEncodingWindowsArabic = 0x506,
            kCFStringEncodingWindowsBalticRim = 0x507,
            kCFStringEncodingWindowsCyrillic = 0x502,
            kCFStringEncodingWindowsGreek = 0x503,
            kCFStringEncodingWindowsHebrew = 0x505,
            kCFStringEncodingWindowsKoreanJohab = 0x510,
            kCFStringEncodingWindowsLatin1 = 0x500,
            kCFStringEncodingWindowsLatin2 = 0x501,
            kCFStringEncodingWindowsLatin5 = 0x504,
            kCFStringEncodingWindowsVietnamese = 0x508
        }

        public static IntPtr kCFAllocatorDefault = IntPtr.Zero;
        private static IntPtr ModuleHandle = IntPtr.Zero;

        static CoreFoundation()
        {
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr __CFStringMakeConstantString(byte[] s);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFAbsoluteTimeGetGregorianDate(ref double at, ref IntPtr tz);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFArrayGetCount(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFArrayGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFArrayGetValues(IntPtr sourceRef, CFRange range, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFBooleanGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDateCreate(IntPtr intptr_2, double double_0);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDataCreate(IntPtr intptr_2, IntPtr intptr_3, int int_1);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFBooleanGetValue(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFDataAppendBytes(IntPtr theData, IntPtr pointer, uint length);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFDataCreateMutable(IntPtr allocator, uint capacity);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDataGetBytePtr(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFDataGetBytes(IntPtr theData, CFRange range, byte[] buffer);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CFDataGetLength(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDataGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double CFDateGetAbsoluteTime(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDateGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern void CFDictionaryAddValue(IntPtr theDict, IntPtr keys, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern IntPtr CFDictionaryCreate(IntPtr allocator, ref IntPtr keys, ref IntPtr values,
            int numValues, IntPtr CFDictionaryKeyCallBacks, IntPtr CFDictionaryValueCallBacks);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, int capacity,
            ref IntPtr CFDictionaryKeyCallBacks, ref IntPtr CFDictionaryValueCallBacks);

        public static IntPtr CFDictionaryFromManagedDictionary(Dictionary<object, object> sourceDict)
        {
            if (sourceDict == null)
            {
                IntPtr ptr = IntPtr.Zero;
                return ptr;
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                IntPtr ptr1 = IntPtr.Zero;
                Dictionary<object, object>.Enumerator enumerator;
                zero = CFDictionaryCreateMutable(kCFAllocatorDefault, sourceDict.Count, ref ptr1, ref ptr1);
                try
                {
                    enumerator = sourceDict.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<object, object> current = enumerator.Current;
                        IntPtr ptr3 = CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(current.Key));
                        IntPtr ptr4 = CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(current.Value));
                        if (ptr3 != IntPtr.Zero && ptr4 != IntPtr.Zero) CFDictionaryAddValue(zero, ptr3, ptr4);
                    }
                }
                finally
                {
                    //enumerator.Dispose();
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return zero;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFWriteStreamCreateWithAllocatedBuffers(IntPtr intptr_2, IntPtr intptr_3);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFWriteStreamOpen(IntPtr intptr_2);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate(IntPtr intptr_2, CFNumber.CFNumberType enum20_0, IntPtr intptr_3);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_1(IntPtr intptr_2, CFNumber.CFNumberType enum20_0, ref short short_0);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_2(IntPtr intptr_2, CFNumber.CFNumberType enum20_0, ref int int_1);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_3(IntPtr intptr_2, CFNumber.CFNumberType enum20_0, ref long long_0);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_4(IntPtr intptr_2, CFNumber.CFNumberType enum20_0, ref float float_0);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_5(IntPtr intptr_2, CFNumber.CFNumberType enum20_0,
            ref double double_0);

        public static IntPtr CFTypeFromManagedType(object inObject)
        {
            if (inObject == null) return IntPtr.Zero;
            IntPtr zero = IntPtr.Zero;
            Type type = inObject.GetType();
            try
            {
                IntPtr ptr2 = IntPtr.Zero;
                if (type == typeof (string)) return StringToCFString(Conversions.ToString(inObject));
                if (type == typeof (IntPtr))
                    return CFNumberCreate(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt32Type,
                        (IntPtr) inObject);
                if (type == typeof (short))
                {
                    short num = Conversions.ToShort(inObject);
                    return CFNumberCreate_1(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt16Type, ref num);
                }
                if (type == typeof (int))
                {
                    int num = Conversions.ToInteger(inObject);
                    return CFNumberCreate_2(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt32Type, ref num);
                }
                if (type == typeof (ushort))
                {
                    int num = Conversions.ToInteger(inObject);
                    return CFNumberCreate_2(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt32Type, ref num);
                }
                if (type == typeof (long))
                {
                    long num = Conversions.ToLong(inObject);
                    return CFNumberCreate_3(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt64Type, ref num);
                }
                if (type == typeof (uint))
                {
                    long num = Conversions.ToLong(inObject);
                    return CFNumberCreate_3(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberSInt64Type, ref num);
                }
                if (type == typeof (float))
                {
                    float num = Conversions.ToSingle(inObject);
                    return CFNumberCreate_4(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberFloat32Type, ref num);
                }
                if (type == typeof (double))
                {
                    double num = Conversions.ToDouble(inObject);
                    return CFNumberCreate_5(kCFAllocatorDefault, CFNumber.CFNumberType.kCFNumberFloat64Type, ref num);
                }
                if (type == typeof (bool)) return CFBoolean.GetCFBoolean(Conversions.ToBoolean(inObject));
                if (type == typeof (DateTime))
                {
                    var time = new DateTime(0x7d1, 1, 1, 0, 0, 0);
                    double num4 = DateAndTime.DateDiff(DateInterval.Second, time, Conversions.ToDate(inObject),
                        FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                    return CFDateCreate(ptr2, num4);
                }
                if (type == typeof (byte[]))
                {
                    var arr = (byte[]) inObject;
                    IntPtr ptr3 = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0);
                    return CFDataCreate(ptr2, ptr3, arr.Length);
                }
                if (type == typeof (object[])) return CFArrayFromManagedArray((object[]) inObject);
                if (type == typeof (ArrayList)) return CFArrayFromManagedArrayList((ArrayList) inObject);
                if (type == typeof (List<object>)) return CFArrayFromManagedList((List<object>) inObject);
                if (type == typeof (Dictionary<object, object>))
                    zero = CFDictionaryFromManagedDictionary((Dictionary<object, object>) inObject);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return zero;
        }

        public static IntPtr CFArrayFromManagedList(IList<object> sourceArray)
        {
            if (sourceArray == null) return IntPtr.Zero;
            IntPtr zero = IntPtr.Zero;
            try
            {
                IEnumerator<object> enumerator;
                zero = CFArrayCreateMutable(kCFAllocatorDefault, sourceArray.Count, IntPtr.Zero);
                try
                {
                    enumerator = sourceArray.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object objectValue = RuntimeHelpers.GetObjectValue(enumerator.Current);
                        CFArrayAppendValue(zero, CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(objectValue)));
                    }
                }
                finally
                {
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return zero;
        }

        public static IntPtr CFArrayFromManagedArrayList(ArrayList sourceArray)
        {
            if (sourceArray == null) return IntPtr.Zero;
            IntPtr zero = IntPtr.Zero;
            try
            {
                IEnumerator enumerator;
                zero = CFArrayCreateMutable(kCFAllocatorDefault, sourceArray.Count, IntPtr.Zero);
                try
                {
                    enumerator = sourceArray.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object objectValue = RuntimeHelpers.GetObjectValue(enumerator.Current);
                        CFArrayAppendValue(zero, CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(objectValue)));
                    }
                }
                finally
                {
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return zero;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFArrayCreateMutable(IntPtr intptr_2, int int_1, IntPtr intptr_3);

        public static IntPtr CFArrayFromManagedArray(object[] sourceArray)
        {
            if (sourceArray == null) return IntPtr.Zero;
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = CFArrayCreateMutable(kCFAllocatorDefault, sourceArray.Length, IntPtr.Zero);
                object[] objArray = sourceArray;
                for (int i = 0; i < objArray.Length; i++)
                {
                    object objectValue = RuntimeHelpers.GetObjectValue(objArray[i]);
                    CFArrayAppendValue(zero, CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(objectValue)));
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            return zero;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFArrayAppendValue(IntPtr intptr_2, IntPtr intptr_3);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFDictionaryGetCount(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFDictionaryGetKeysAndValues(IntPtr sourceRef, IntPtr keys, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDictionaryGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFGetTypeID(IntPtr cf);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFNumberGetType(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFNumberGetTypeID();

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Double(IntPtr number, int theType, ref double valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Int(IntPtr number, int theType, ref int valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Long(IntPtr number, int theType, ref long valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Single(IntPtr number, int theType, ref float valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFPropertyListCreateFromXMLData(IntPtr allocator, IntPtr xmlData,
            CFPropertyListMutabilityOptions mutabilityOption, ref IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFPropertyListCreateXMLData(IntPtr allocator, IntPtr theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFPropertyListIsValid(IntPtr theData, CFPropertyListFormat format);

        private static IntPtr CFRangeMake(int loc, int len)
        {
            IntPtr ptr = Marshal.AllocHGlobal(8);
            Marshal.WriteInt32(ptr, 0, loc);
            Marshal.WriteInt32(ptr, 4, len);
            return ptr;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CFRelease(IntPtr cf);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateFromExternalRepresentation(IntPtr allocator, IntPtr data,
            CFStringEncoding encoding);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithBytes(IntPtr allocator, byte[] data, ulong numBytes,
            CFStringEncoding encoding, bool isExternalRepresentation);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFStringCreateWithCharacters(IntPtr allocator, [MarshalAs(UnmanagedType.LPWStr)] string stingData, int strLength);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithBytesNoCopy(IntPtr allocator, byte[] data, ulong numBytes,
            CFStringEncoding encoding, bool isExternalRepresentation, IntPtr contentsDeallocator);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithCString(IntPtr allocator, string data, CFStringEncoding encoding);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern CFRange CFStringFind(IntPtr theString, IntPtr stringToFind, ushort compareOptions);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFStringGetBytes(IntPtr theString, CFRange range, uint encoding, byte lossByte,
            byte isExternalRepresentation, byte[] buffer, int maxBufLen, ref int usedBufLen);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern char CFStringGetCharacterAtIndex(IntPtr theString, uint idx);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFStringGetLength(IntPtr cf);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFStringGetMaximumSizeForEncoding(int length, uint encoding);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFStringGetTypeID();

        public static string CFStringToString(byte[] value)
        {
            if (value.Length > 9) return Encoding.UTF8.GetString(value, 9, value[9]);
            return "";
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFTimeZoneCopySystem();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFURLCreateDataAndPropertiesFromResource(IntPtr allocator, IntPtr urlRef,
            ref IntPtr resourceData, ref IntPtr properties, IntPtr desiredProperties, ref int errorCode);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFURLCreateWithFileSystemPath(IntPtr allocator, IntPtr stringRef,
            CFURLPathStyle pathStyle, bool isDirectory);

        public static object ManagedPropertyListFromXMLData(byte[] inBytes)
        {
            object objectValue = null;
            if (inBytes != null)
            {
                IntPtr ptr3 = IntPtr.Zero;
                IntPtr ptr5 = IntPtr.Zero;
                int length = inBytes.Length;
                IntPtr theData = CFDataCreateMutable(ptr5, (uint) length);
                IntPtr destination = Marshal.AllocHGlobal(length);
                Marshal.Copy(inBytes, 0, destination, length);
                CFDataAppendBytes(theData, destination, (uint) length);
                IntPtr sourceRef = CFPropertyListCreateFromXMLData(IntPtr.Zero, theData,
                    CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref ptr3);
                if (sourceRef != IntPtr.Zero)
                    objectValue = RuntimeHelpers.GetObjectValue(ManagedTypeFromCFType(ref sourceRef));
                Marshal.FreeHGlobal(destination);
                if (theData != IntPtr.Zero) CFRelease(theData);
                if (sourceRef != IntPtr.Zero) CFRelease(sourceRef);
            }
            return objectValue;
        }

        public static object ManagedPropertyListFromXMLData(string fileOnPC)
        {
            var array = new byte[0];
            using (var stream = new FileStream(fileOnPC, FileMode.Open))
            {
                array = new byte[((int) stream.Length) - 1 + 1];
                stream.Read(array, 0, (int) stream.Length);
            }
            return ManagedPropertyListFromXMLData(array);
        }

        public static object ManagedTypeFromCFType(ref IntPtr sourceRef)
        {
            object obj3 = null;
            if (sourceRef == IntPtr.Zero) return null;
            try
            {
                uint num = CFGetTypeID(sourceRef);
                uint num2 = num;
                if (num2 == CFArrayGetTypeID()) return RuntimeHelpers.GetObjectValue(ReadCFArrayFromIntPtr(sourceRef));
                if (num2 == CFDictionaryGetTypeID())
                    return RuntimeHelpers.GetObjectValue(ReadCFDictionaryFromIntPtr(sourceRef));
                if (num2 == CFDataGetTypeID()) return ReadCFDataFromIntPtr(sourceRef);
                if (num2 == CFBooleanGetTypeID()) return CFBooleanGetValue(sourceRef);
                if (num2 == CFDateGetTypeID()) return ReadCFDateFromIntPtr(sourceRef);
                if (num2 == CFNumberGetTypeID())
                    return RuntimeHelpers.GetObjectValue(ReadCFNumberFromIntPtr(sourceRef));
                obj3 = ReadCFStringFromIntPtr(sourceRef);
                if (num == 7)
                {
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                obj3 = null;
                ProjectData.ClearProjectError();
            }
            return obj3;
        }

        public static string PropertyListToXML(byte[] propertyList)
        {
            try
            {
                int length = propertyList.Length;
                if (propertyList[0] == 0x62 && propertyList[1] == 0x70 && propertyList[2] == 0x6c &&
                    propertyList[3] == 0x69 && propertyList[4] == 0x73 && propertyList[5] == 0x74 &&
                    propertyList[6] == 0x30 && propertyList[7] == 0x30)
                {
                    IntPtr ptr = IntPtr.Zero;
                    IntPtr ptr2 = IntPtr.Zero;
                    ptr2 = CFPropertyListCreateFromXMLData(IntPtr.Zero, ptr2,
                        CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref ptr);
                    if (ptr2 != IntPtr.Zero)
                    {
                        ptr2 = CFPropertyListCreateXMLData(IntPtr.Zero, ptr2);
                        length = CFDataGetLength(ptr2) - 1;
                        propertyList = new byte[length + 1];
                        var range = new CFRange(0, length);
                        CFDataGetBytes(ptr2, range, propertyList);
                    }
                }
                return Encoding.UTF8.GetString(propertyList);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
            return null;
        }

        public static object ReadCFArrayFromIntPtr(IntPtr sourceRef)
        {
            object[] objArray = null;
            if (!(sourceRef != IntPtr.Zero)) return null;
            int len = CFArrayGetCount(sourceRef);
            if (len == 0) return new object[0];
            if (len < 0) return null;
            objArray = new object[len - 1 + 1];
            IntPtr values = Marshal.AllocHGlobal(len*4);
            var range = new CFRange(0, len);
            CFArrayGetValues(sourceRef, range, values);
            int num3 = len - 1;
            for (int i = 0; i <= num3; i++)
            {
                IntPtr intptrvalues = Marshal.ReadIntPtr(values, i*8);
                objArray[i] = RuntimeHelpers.GetObjectValue(ManagedTypeFromCFType(ref intptrvalues));
            }
            Marshal.FreeHGlobal(values);
            return objArray;
        }

        public static byte[] ReadCFDataFromIntPtr(IntPtr sourceRef)
        {
            byte[] buffer2 = null;
            if (!(sourceRef != IntPtr.Zero)) return null;
            int num = CFDataGetLength(sourceRef);
            IntPtr ptr = CFDataGetBytePtr(sourceRef);
            buffer2 = new byte[num - 1 + 1];
            int num3 = num - 1;
            for (int i = 0; i <= num3; i++)
            {
                buffer2[i] = Marshal.ReadByte(ptr, i);
            }
            return buffer2;
        }

        public static DateTime ReadCFDateFromIntPtr(IntPtr sourceRef)
        {
            var time2 = new DateTime();
            if (sourceRef != IntPtr.Zero)
            {
                try
                {
                    double num = CFDateGetAbsoluteTime(sourceRef);
                    time2 = new DateTime(0x7d1, 1, 1, 0, 0, 0).AddSeconds(num);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    time2 = new DateTime();
                    ProjectData.ClearProjectError();
                }
            }
            return time2;
        }

        public static object ReadCFDictionaryFromIntPtr(IntPtr sourceRef)
        {
            Dictionary<object, object> dictionary = null;
            if (sourceRef != IntPtr.Zero)
            {
                dictionary = new Dictionary<object, object>();
                int num = CFDictionaryGetCount(sourceRef);
                if (num == 0) return null;
                if (num < 0 || num <= 0) return dictionary;
                IntPtr keys = Marshal.AllocHGlobal(num*8);
                IntPtr values = Marshal.AllocHGlobal(num*4);
                CFDictionaryGetKeysAndValues(sourceRef, keys, values);
                int num3 = num - 1;
                for (int i = 0; i <= num3; i++)
                {
                    IntPtr intptrkeys = Marshal.ReadIntPtr(keys, i*8);
                    IntPtr intptrvalues = Marshal.ReadIntPtr(values, i*8);
                    object objectValue = RuntimeHelpers.GetObjectValue(ManagedTypeFromCFType(ref intptrkeys));
                    object obj4 = RuntimeHelpers.GetObjectValue(ManagedTypeFromCFType(ref intptrvalues));
                    dictionary.Add(RuntimeHelpers.GetObjectValue(objectValue), RuntimeHelpers.GetObjectValue(obj4));
                }
                Marshal.FreeHGlobal(keys);
                Marshal.FreeHGlobal(values);
            }
            return dictionary;
        }

        public static object ReadCFNumberFromIntPtr(IntPtr sourceRef)
        {
            object obj3 = null;
            if (!(sourceRef != IntPtr.Zero)) return null;
            switch (CFNumberGetType(sourceRef))
            {
                case 1:
                case 2:
                case 3:
                case 7:
                case 8:
                case 9:
                case 10:
                case 14:
                case 15:
                {
                    int valuePtr = 0;
                    if (CFNumberGetValue_Int(sourceRef, 9, ref valuePtr)) obj3 = valuePtr;
                    return obj3;
                }
                case 4:
                case 11:
                {
                    long num2 = 0;
                    if (CFNumberGetValue_Long(sourceRef, 4, ref num2)) obj3 = num2;
                    return obj3;
                }
                case 5:
                case 12:
                case 0x10:
                {
                    float num3 = 0f;
                    if (CFNumberGetValue_Single(sourceRef, 12, ref num3)) obj3 = num3;
                    return obj3;
                }
                case 6:
                case 13:
                {
                    double num4 = 0.0;
                    if (CFNumberGetValue_Double(sourceRef, 13, ref num4)) obj3 = num4;
                    return obj3;
                }
            }
            return obj3;
        }

        public static string ReadCFStringFromIntPtr(IntPtr sourceRef)
        {
            string str2 = null;
            if (sourceRef != IntPtr.Zero)
            {
                int len = CFStringGetLength(sourceRef);
                var buffer = new byte[len*2 - 1 + 1];
                var range = new CFRange(0, len);
                int usedBufLen = 0;
                CFStringGetBytes(sourceRef, range, 0x100, 0x3f, 0, buffer, len*2, ref usedBufLen);
                str2 = Encoding.Unicode.GetString(buffer);
            }
            return str2;
        }

        public static string ReadPlist(string fileOnPC)
        {
            string str3 = "";
            string destFileName = string.Format(@"c:\{0}.log", Guid.NewGuid().ToString("N").ToLower());
            try
            {
                File.Copy(fileOnPC, destFileName, true);
                fileOnPC = destFileName;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
            IntPtr urlRef = CFURLCreateWithFileSystemPath(IntPtr.Zero, StringToCFString(fileOnPC),
                CFURLPathStyle.kCFURLWindowsPathStyle, false);
            if (urlRef != IntPtr.Zero)
            {
                IntPtr ptr5 = IntPtr.Zero;
                int errorCode = 0;
                IntPtr zero = IntPtr.Zero;
                bool flag = CFURLCreateDataAndPropertiesFromResource(IntPtr.Zero, urlRef, ref zero, ref ptr5, ptr5,
                    ref errorCode);
                CFRelease(urlRef);
                if (flag && zero != IntPtr.Zero)
                {
                    IntPtr ptr3 = zero;
                    IntPtr theData = CFPropertyListCreateFromXMLData(IntPtr.Zero, zero,
                        CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref ptr3);
                    CFRelease(zero);
                    if (theData != IntPtr.Zero)
                    {
                        theData = CFPropertyListCreateXMLData(IntPtr.Zero, theData);
                        int len = CFDataGetLength(theData) - 1;
                        var buffer = new byte[len + 1];
                        var range = new CFRange(0, len);
                        CFDataGetBytes(theData, range, buffer);
                        str3 = Encoding.UTF8.GetString(buffer);
                        CFRelease(theData);
                    }
                    else
                        CFRelease(ptr3);
                }
            }
            if (File.Exists(destFileName))
            {
                try
                {
                    File.Delete(destFileName);
                }
                catch (Exception exception3)
                {
                    ProjectData.SetProjectError(exception3);
                    Exception exception2 = exception3;
                    ProjectData.ClearProjectError();
                }
            }
            return str3;
        }

        public static string SearchXmlByKey(string xmlText, string xmlKey)
        {
            string str2 = "";
            int start = 0;
            int num2 = 0;
            start = Strings.InStr(xmlText, xmlKey, CompareMethod.Binary);
            if (start > 0)
            {
                num2 = Strings.InStr(start, xmlText, "<string>", CompareMethod.Binary);
                if (num2 > start)
                {
                    num2 += "<string>".Length;
                    start = Strings.InStr(num2, xmlText, "</string>", CompareMethod.Binary);
                    if (start > num2) str2 = Strings.Mid(xmlText, num2, start - num2);
                }
            }
            return str2.Trim();
        }

        public static IntPtr StringToCFString(string value)
        {
            IntPtr zero = IntPtr.Zero;
            if (value != null && value.Length > 0) zero = __CFStringMakeConstantString(Encoding.UTF8.GetBytes(value));
            return zero;
        }

        public static IntPtr StringToHeap(string srcString, Encoding srcEncoding)
        {
            if (srcEncoding == null) return Marshal.StringToCoTaskMemAnsi(srcString);
            int maxByteCount = srcEncoding.GetMaxByteCount(1);
            char[] chars = srcString.ToCharArray();
            var bytes = new byte[srcEncoding.GetByteCount(chars) + (maxByteCount - 1) + 1];
            if (srcEncoding.GetBytes(chars, 0, chars.Length, bytes, 0) != bytes.Length - maxByteCount)
                throw new NotSupportedException("encoding.GetBytes() doesn't equal encoding.GetByteCount()!");
            IntPtr destination = Marshal.AllocCoTaskMem(bytes.Length);
            if (destination == IntPtr.Zero) throw new OutOfMemoryException();
            bool flag = false;
            try
            {
                Marshal.Copy(bytes, 0, destination, bytes.Length);
                flag = true;
            }
            finally
            {
                if (!flag) Marshal.FreeCoTaskMem(destination);
            }
            return destination;
        }

        private enum CFNumberType
        {
            kCFNumberCFIndexType = 14,
            kCFNumberCGFloatType = 0x10,
            kCFNumberCharType = 7,
            kCFNumberDoubleType = 13,
            kCFNumberFloat32Type = 5,
            kCFNumberFloat64Type = 6,
            kCFNumberFloatType = 12,
            kCFNumberIntType = 9,
            kCFNumberLongLongType = 11,
            kCFNumberLongType = 10,
            kCFNumberMaxType = 0x10,
            kCFNumberNSIntegerType = 15,
            kCFNumberShortType = 8,
            kCFNumberSInt16Type = 2,
            kCFNumberSInt32Type = 3,
            kCFNumberSInt64Type = 4,
            kCFNumberSInt8Type = 1
        }

        private enum CFPropertyListFormat
        {
            kCFPropertyListBinaryFormat_v1_0 = 200,
            kCFPropertyListOpenStepFormat = 1,
            kCFPropertyListXMLFormat_v1_0 = 100
        }

        private enum CFPropertyListMutabilityOptions
        {
            kCFPropertyListImmutable,
            kCFPropertyListMutableContainers,
            kCFPropertyListMutableContainersAndLeaves
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CFRange
        {
            internal long Location;
            internal long Length;
            internal CFRange(int l, int len)
            {
                this.Location = l;
                this.Length = len;
            }
        }

        private enum CFURLPathStyle
        {
            kCFURLPOSIXPathStyle,
            kCFURLHFSPathStyle,
            kCFURLWindowsPathStyle
        }
    }
}