using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Enums;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// Bool对象扩展方法
    /// </summary>
    public static class BooleanExtensionMethod
    {
        /// <summary>
        /// Bool数组转Byte数组
        /// 示例：bool[] test=new bool[15];test[0]=true;test[3]=true;test[4]=true;test[14]=true; 选择 endian = EEndian.BigEndian，则转换后的结果为byte[] res=new byte[2]{25,64}
        /// </summary>
        /// <param name="data">Bool数组</param>
        /// <param name="endian">大端小端</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this bool[] data, EEndian endian = EEndian.BigEndian)
        {
            var temp = new bool[8];
            var count = data.Length / 8 + 1;
            var buffer = new bool[count * 8];
            var res = new byte[count];
            Buffer.BlockCopy(data, 0, buffer, 0, data.Length);
            for (int i = 0; i < buffer.Length; i += 8)
            {
                Buffer.BlockCopy(buffer, i, temp, 0, temp.Length);
                temp = endian == EEndian.BigEndian ? temp.Reverse().ToArray() : temp;
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[j])
                    {
                        res[i / 8] += (byte)Math.Pow(2, 7 - j);
                    }
                }
            }
            return res;
        }
    }
}