using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Helpers.Helpers;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class IniHelper
    {
        #region 声明API函数

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion 声明API函数

        /// <summary>
        /// 异步写ini文件
        /// </summary>
        /// <param name="section">ini文件中区块名</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static async Task WriteAsync(string section, string key, string value, string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    await Task.Run(() => WritePrivateProfileString(section, key, value, path));
                }
                else
                {
                    File.Create(path).Close();
                    await WriteAsync(section, key, value, path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步写ini文件
        /// 键为属性名，值为属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section">ini文件中区块名</param>
        /// <param name="value">写入对象</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static async Task WriteAsyncPropertyNameAsKey<T>(string section, T value, string path) where T : class
        {
            try
            {
                var dic = PropertyHepler.GetPropertyValueKeyIsName(value);
                foreach (var item in dic.Keys)
                {
                    await WriteAsync(section, item, dic[item], path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步写ini文件
        /// 键为属性描述，值为属性值
        /// </summary>
        /// <typeparam name="T">写入对象类型</typeparam>
        /// <param name="section">ini文件中区块名</param>
        /// <param name="value">写入对象</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static async Task WriteAsyncPropertyDescriptionAsKey<T>(string section, T value, string path) where T : class
        {
            try
            {
                var dic = PropertyHepler.GetPropertyDescriptionKeyIsDescription(value);
                foreach (var item in dic.Keys)
                {
                    await WriteAsync(section, item, dic[item], path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步读取配置文件1个键值
        /// </summary>
        /// <param name="section">ini文件中区块名</param>
        /// <param name="key">键名</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static async Task<string> ReadAsync(string section, string key, string path)
        {
            var buffer = new StringBuilder(1024);
            await Task.Run(() => GetPrivateProfileString(section, key, "", buffer, 1024, path));
            return buffer.ToString().Trim();
        }

        /// <summary>
        /// 异步读取ini文件参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="section">ini文件中区块名</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static async Task<T> ReadAsyncT<T>(string section, string path) where T : class, new()
        {
            var res = new T();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                var value = await ReadAsync(section, p.Name, path);
                if (value != null)
                {
                    if (p.PropertyType == typeof(bool))
                    {
                        p.SetValue(res, Convert.ToBoolean(value));
                    }
                    else if (p.PropertyType == typeof(sbyte))
                    {
                        p.SetValue(res, Convert.ToSByte(value));
                    }
                    else if (p.PropertyType == typeof(short))
                    {
                        p.SetValue(res, Convert.ToInt16(value));
                    }
                    else if (p.PropertyType == typeof(int))
                    {
                        p.SetValue(res, Convert.ToInt32(value));
                    }
                    else if (p.PropertyType == typeof(long))
                    {
                        p.SetValue(res, Convert.ToInt32(value));
                    }
                    else if (p.PropertyType == typeof(byte))
                    {
                        p.SetValue(res, Convert.ToByte(value));
                    }
                    else if (p.PropertyType == typeof(ushort))
                    {
                        p.SetValue(res, Convert.ToUInt16(value));
                    }
                    else if (p.PropertyType == typeof(uint))
                    {
                        p.SetValue(res, Convert.ToUInt32(value));
                    }
                    else if (p.PropertyType == typeof(ulong))
                    {
                        p.SetValue(res, Convert.ToUInt64(value));
                    }
                    else if (p.PropertyType == typeof(float))
                    {
                        p.SetValue(res, Convert.ToSingle(value));
                    }
                    else if (p.PropertyType == typeof(double))
                    {
                        p.SetValue(res, Convert.ToDouble(value));
                    }
                    else if (p.PropertyType == typeof(decimal))
                    {
                        p.SetValue(res, Convert.ToDecimal(value));
                    }
                    else if (p.PropertyType == typeof(DateTime))
                    {
                        p.SetValue(res, Convert.ToDateTime(value));
                    }
                    else if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(res, value);
                    }
                }
            }
            return res;
        }
    }
}