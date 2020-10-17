using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
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
    }
}