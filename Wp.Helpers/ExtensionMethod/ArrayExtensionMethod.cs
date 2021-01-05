using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (source.Length < sourceOffset + count)
            {
                if (target.Length < targetOffset + count)
                {
                    var sor = new byte[count];
                    var tar = new byte[count];
                    Buffer.BlockCopy(source, sourceOffset, sor, 0, count);
                    Buffer.BlockCopy(target, targetOffset, tar, 0, count);
                    return Equals(sor, tar);
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
    }
}