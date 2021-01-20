using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wp.Helpers.Helpers
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
        /// <param name="bindingFlags">属性类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyDescriptionKeyIsDescription<T>(
            T obj,
            List<Type> ExceptType = null,
            BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance) where T : class
        {
            try
            {
                var res = new Dictionary<string, string>();
                PropertyValue<T> propertyValue = new PropertyValue<T>(obj);
                if (ExceptType != null)
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties(bindingFlags))
                    {
                        if (ExceptType.Contains(p.PropertyType))
                        {
                            //
                        }
                        else
                        {
                            var v = propertyValue.Get(p.Name);
                            res.Add(((DescriptionAttribute)p.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()).Description, v is null ? string.Empty : v.ToString());
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties(bindingFlags))
                    {
                        var v = propertyValue.Get(p.Name);
                        res.Add(((DescriptionAttribute)p.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()).Description, v is null ? string.Empty : v.ToString());
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
        /// <param name="bindingFlags">属性类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyValueKeyIsName<T>(
            T obj,
            List<Type> ExceptType = null,
            BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance) where T : class
        {
            try
            {
                PropertyValue<T> propertyValue = new PropertyValue<T>(obj);
                var res = new Dictionary<string, string>();
                if (ExceptType != null)
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties(bindingFlags))
                    {
                        if (ExceptType.Contains(p.PropertyType))
                        {
                            //
                        }
                        else
                        {
                            var v = propertyValue.Get(p.Name);
                            res.Add(p.Name, v is null ? string.Empty : v.ToString());
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo p in typeof(T).GetProperties(bindingFlags))
                    {
                        var v = propertyValue.Get(p.Name);
                        res.Add(p.Name, v is null ? string.Empty : v.ToString());
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class PropertyValue<T>
        {
            private static ConcurrentDictionary<string, MemberGetDelegate> _memberGetDelegate = new ConcurrentDictionary<string, MemberGetDelegate>();

            private delegate object MemberGetDelegate(T obj);

            public PropertyValue(T obj)
            {
                Target = obj;
            }

            public T Target { get; private set; }

            public object Get(string name)
            {
                MemberGetDelegate memberGet = _memberGetDelegate.GetOrAdd(name, BuildDelegate);
                return memberGet(Target);
            }

            private MemberGetDelegate BuildDelegate(string name)
            {
                Type type = typeof(T);
                PropertyInfo property = type.GetProperty(name);
                return (MemberGetDelegate)Delegate.CreateDelegate(typeof(MemberGetDelegate), property.GetGetMethod());
            }
        }
    }
}