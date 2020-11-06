using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
    }
}