using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Enums;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// 数组扩展方法
    /// </summary>
    public static class ArrayExtensionMethod
    {
        /// <summary>
        /// 确定指定对象是否相等
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="sourceOffset">源偏移量</param>
        /// <param name="target">目标数组</param>
        /// <param name="targetOffset">目标偏移量</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public static bool Equals(this Array source, int sourceOffset, Array target, int targetOffset, int count)
        {
            if (source.Length >= sourceOffset + count)
            {
                if (target.Length >= targetOffset + count)
                {
                    var res = true;
                    var sor = new byte[count];
                    var tar = new byte[count];
                    Buffer.BlockCopy(source, sourceOffset, sor, 0, count);
                    Buffer.BlockCopy(target, targetOffset, tar, 0, count);
                    for (int i = 0; i < count; i++)
                    {
                        res = sor[i] == tar[i] && res;
                    }
                    return res;
                }
                else
                {
                    throw new Exception("目标数组长度超出！");
                }
            }
            else
            {
                throw new Exception("源数组长度超出！");
            }
        }

        /// <summary>
        /// 格式化输出Byte数组
        /// </summary>
        /// <param name="data">数组</param>
        /// <param name="scale">输出进制</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string ToFormatString(this byte[] data, EScale scale = EScale.HexWithTokenToUp, string spliter = " ")
        {
            var res = new StringBuilder();
            foreach (var item in data)
            {
                switch (scale)
                {
                    case EScale.Decimal:
                        res.Append($"{item}{spliter}");
                        break;

                    case EScale.Hex:
                        res.Append($"{item:x2}{spliter}");
                        break;

                    case EScale.HexWithToken:
                        res.Append($"0x{item:x2}{spliter}");
                        break;

                    case EScale.HexToUp:
                        res.Append($"{item:X2}{spliter}");
                        break;

                    case EScale.HexWithTokenToUp:
                        res.Append($"0x{item:X2}{spliter}");
                        break;

                    default:
                        break;
                }
            }
            return res.Remove(res.Length - 1, 1).ToString();
        }
    }
}