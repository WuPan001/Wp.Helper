using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wp.Helpers.Helpers.Helpers
{
    /// <summary>
    /// 属性帮助类
    /// </summary>
    public class PropertyHepler
    {
        /// <summary>
        /// 获取属性描述
        /// </summary>
        /// <typeparam name="T">类类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static string GetPropertyDescription<T>(string propertyName) where T : class
        {
            object[] objs = typeof(T).GetProperty(propertyName).GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (objs.Length > 0)
            {
                return ((DescriptionAttribute)objs[0]).Description;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取类属性描述和属性值
        /// 属性描述为key
        /// </summary>
        /// <typeparam name="T">类类型</typeparam>
        /// <param name="obj">类实例</param>
        /// <param name="ExceptType">不需返回的属性类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyDescriptionKeyIsDescription<T>(T obj, List<Type> ExceptType = null) where T : class
        {
            try
            {
                var res = new Dictionary<string, string>();

                if (ExceptType != null)
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties())
                    {
                        if (ExceptType.Contains(p.PropertyType))
                        {
                            //
                        }
                        else
                        {
                            res.Add(((DescriptionAttribute)p.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()).Description, p.GetValue(obj).ToString());
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties())
                    {
                        res.Add(((DescriptionAttribute)p.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()).Description, p.GetValue(obj).ToString());
                    }
                }

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取类属性描述和属性值
        /// 属性名为key
        /// </summary>
        /// <typeparam name="T">类类型</typeparam>
        /// <param name="obj">类实例</param>
        /// <param name="ExceptType">不需返回的属性类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyValueKeyIsName<T>(T obj, List<Type> ExceptType = null) where T : class
        {
            try
            {
                var res = new Dictionary<string, string>();
                if (ExceptType != null)
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties())
                    {
                        if (ExceptType.Contains(p.PropertyType))
                        {
                            //
                        }
                        else
                        {
                            res.Add(p.Name, p.GetValue(obj).ToString());
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties())
                    {
                        res.Add(p.Name, p.GetValue(obj).ToString());
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}