using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Enums;

namespace Wp.Helpers
{
    /// <summary>
    /// 转换器帮助类
    /// </summary>
    public class ConverterHelper
    {
        /// <summary>
        /// 将枚举描述转换为对应的资源图片路径
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举描述</param>
        /// <param name="imgType">图片文件类型</param>
        /// <returns></returns>
        public static string Convert<T>(string value, EImgType imgType = EImgType.PNG)
        {
            return $"/Resources/{EnumHelper.GetEnumNameByDescription(value, typeof(T))}{EnumHelper.GetEnumDescription(imgType)}";
        }

        /// <summary>
        /// 将枚举值转换为对应的资源图片路径
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="imgType">图片文件类型</param>
        /// <returns></returns>
        public static string Convert<T>(T value, EImgType imgType = EImgType.PNG)
        {
            return $"/Resources/{value}{EnumHelper.GetEnumDescription(imgType)}";
        }

        /// <summary>
        /// 将枚举描述转换为对应的默认资源图片路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">枚举描述</param>
        /// <param name="suffix">图片文件名后缀</param>
        /// <param name="imgType">图片文件类型</param>
        /// <returns></returns>
        public static string Convert<T>(string value, string suffix = "Normal", EImgType imgType = EImgType.PNG)
        {
            return $"/Resources/{EnumHelper.GetEnumNameByDescription(value, typeof(T))}{suffix}{EnumHelper.GetEnumDescription(imgType)}";
        }

        /// <summary>
        /// 将枚举值转换为对应的默认资源图片路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="suffix">图片文件名后缀</param>
        /// <param name="imgType">图片文件类型</param>
        /// <returns></returns>
        public static string Convert<T>(T value, string suffix = "Normal", EImgType imgType = EImgType.PNG)
        {
            return $"/Resources/{value}{suffix}{EnumHelper.GetEnumDescription(imgType)}";
        }
    }
}