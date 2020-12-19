using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// EnumerableObject扩展方法
    /// </summary>
    public static class EnumerableObjectExtensionMethod
    {
        /// <summary>
        /// 非空或null检查
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="param">参数</param>
        public static void NotNullOrEmpty<T>(this IEnumerable<T> param)
        {
            param.NotNull();
            if (param.Count() == 0)
            {
                throw new ArgumentException("The collection can not be empty.", nameof(param));
            }
        }

        /// <summary>
        /// 转成ObserableCollection对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">原对象</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObserableCollection<T>(IEnumerable<T> obj)
        {
            try
            {
                var res = new ObservableCollection<T>();
                foreach (var item in obj)
                {
                    res.Add(item);
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