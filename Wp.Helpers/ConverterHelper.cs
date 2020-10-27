using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
    /// <summary>
    /// 转换器帮助类
    /// </summary>
    public class ConverterHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Convert<T>(string value)
        {
            return $"/Resources/{EnumHelper.GetEnumNameByDescription(value, typeof(T))}.png";
        }

        /// <summary>
        /// 将枚举值转换为图片
        /// 注意：图片格式必须为png
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNormal<T>(string value)
        {
            return string.Empty;
        }
    }
}