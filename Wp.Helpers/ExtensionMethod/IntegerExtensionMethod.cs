using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Enums;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// 整形对象扩展方法
    /// </summary>
    public static class IntegerExtensionMethod
    {
        /// <summary>
        /// Byte转Boolean数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="endian">大端小端</param>
        /// <returns></returns>
        public static bool[] ToBooleanArray(this byte data, EEndian endian = EEndian.LittleEndian)
        {
            var res = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                res[i] = (data & (byte)(Math.Pow(2, i) * 0x01)) == Math.Pow(2, i) * 0x01;
            }
            return endian switch
            {
                EEndian.BigEndian => res.Reverse().ToArray(),
                EEndian.LittleEndian => res,
                _ => res,
            };
        }

        /// <summary>
        /// Ushort转Boolean数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="endian">大端小端</param>
        /// <returns></returns>
        public static bool[] ToBooleanArray(this ushort data, EEndian endian = EEndian.LittleEndian)
        {
            var res = new bool[16];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (data & (ushort)(Math.Pow(2, i) * 0x01)) == Math.Pow(2, i) * 0x01;
            }
            return endian switch
            {
                EEndian.BigEndian => res.Reverse().ToArray(),
                EEndian.LittleEndian => res,
                _ => res,
            };
        }

        /// <summary>
        /// Uint转Boolean数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="endian">大端小端</param>
        /// <returns></returns>
        public static bool[] ToBooleanArray(this uint data, EEndian endian = EEndian.LittleEndian)
        {
            var res = new bool[32];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (data & (uint)(Math.Pow(2, i) * 0x01)) == Math.Pow(2, i) * 0x01;
            }
            return endian switch
            {
                EEndian.BigEndian => res.Reverse().ToArray(),
                EEndian.LittleEndian => res,
                _ => res,
            };
        }

        /// <summary>
        /// Ulong转Boolean数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="endian">大端小端</param>
        /// <returns></returns>
        public static bool[] ToBooleanArray(this ulong data, EEndian endian = EEndian.LittleEndian)
        {
            var res = new bool[64];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (data & (ulong)(Math.Pow(2, i) * 0x01)) == Math.Pow(2, i) * 0x01;
            }
            return endian switch
            {
                EEndian.BigEndian => res.Reverse().ToArray(),
                EEndian.LittleEndian => res,
                _ => res,
            };
        }
    }
}