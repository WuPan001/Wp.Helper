using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 字节数组帮助类
    /// </summary>
    public class ByteArrayHelper
    {
        /// <summary>
        /// 大端小端枚举
        /// </summary>
        public enum EEndType
        {
            /// <summary>
            /// 大端：是指数据的低位保存在内存的高地址中，而数据的高位，保存在内存的低地址中
            /// </summary>
            BigEnd,

            /// <summary>
            /// 小端：是指数据的低位保存在内存的低地址中，而数据的高位，保存在内存的高地址中
            /// </summary>
            LittleEnd
        }

        /// <summary>
        /// 用于字节数组转ushort
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns></returns>
        public static ushort ByteArrayToUshort(byte[] data)
        {
            var result = 0d;
            for (var i = 0; i < data.Length; i++)
            {
                result += Math.Pow(16, data.Length - 1 - i) * data[i];
            }
            return Convert.ToUInt16(result);
        }

        /// <summary>
        /// 用于字节数组转ushort
        /// 数组长度默认为2
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static ushort ByteArrayToUshort(byte[] data, byte index = 0, byte len = 2)
        {
            ushort result = 0;
            for (var i = 0; i < len; i++)
            {
                result += Convert.ToUInt16(data[index + i] * Math.Pow(256, len - 1 - i));
            }
            return result;
        }

        /// <summary>
        /// ushort转字节数组
        /// </summary>
        /// <param name="data">ushort数据</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte[] UshortToByteArray(ushort data, EEndType type = EEndType.BigEnd)
        {
            var result = new byte[2];
            switch (type)
            {
                case EEndType.BigEnd:
                    result[0] = Convert.ToByte(data >> 8);
                    result[1] = Convert.ToByte(data & 0xFF);
                    break;

                case EEndType.LittleEnd:
                    result[0] = Convert.ToByte(data & 0xFF);
                    result[1] = Convert.ToByte(data >> 8);
                    break;

                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 本地MAC转Byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] LocalMacToByteArray()
        {
            var MAC = string.Empty;
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    MAC = mo["MacAddress"].ToString();
                }
            }
            var temp = MAC.Split(':');
            var result = new List<byte>();
            foreach (var item in temp)
            {
                result.Add(Convert.ToByte(item, 16));
            }
            return result.ToArray();
        }
    }
}