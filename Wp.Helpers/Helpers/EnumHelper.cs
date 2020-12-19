using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举值数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetEnumCount<T>() where T : class
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        }

        /// <summary>
        /// 获取所有枚举元素枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>
        public static ObservableCollection<T> GetEnumItems<T>()
        {
            var res = new ObservableCollection<T>();
            var arr = Enum.GetValues(typeof(T));
            foreach (var item in arr)
            {
                res.Add((T)item);
            }
            return res;
        }

        /// <summary>
        /// 获取所有枚举项描述
        /// 可用做填充Combobox
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>}
        public static ObservableCollection<string> GetAllDescriptions<T>()
        {
            var collection = new ObservableCollection<string>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    collection.Add(attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description.Split('|')[0]);
                }
            }
            return collection;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            if (field == null)
            {
                return value;
            }
            else
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length == 0)
                {
                    return value;
                }
                else
                {
                    return ((DescriptionAttribute)objs[0]).Description;
                }
            }
        }

        /// <summary>
        /// 根据枚举描述获取枚举名
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns></returns>
        public static string GetEnumNameByDescription<T>(string description)
        {
            return GetEnumNameByDescription(description, typeof(T));
        }

        /// <summary>
        /// 根据枚举描述获取枚举名
        /// </summary>
        /// <param name="description">枚举描述</param>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumNameByDescription(string description, Type type)
        {
            var str = string.Empty;
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    str = description == ((DescriptionAttribute)attr[0]).Description ? field.Name : str;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取枚举描述和枚举值名
        /// 返回值为字典类型，key为枚举名，value为枚举描述
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescriptionKeyIsName<T>()
        {
            var dic = new Dictionary<string, string>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(field.Name, attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取枚举值和枚举描述
        /// 返回值为字典类型，key为枚举描述，value为枚举值
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>

        public static Dictionary<string, T> GetEnumValueKeyIsDescription<T>()
        {
            var dic = new Dictionary<string, T>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description, (T)field.GetValue(fields));
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取枚举描述和枚举值
        /// 返回值为字典类型，key为枚举值，value为枚举描述
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns></returns>
        public static Dictionary<byte, string> GetEnumDescriptionKeyIsValue<T>()
        {
            Dictionary<byte, string> dic = new Dictionary<byte, string>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(Convert.ToByte(field.GetValue(fields)), attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description);
                }
            }
            return dic;
        }
    }
}