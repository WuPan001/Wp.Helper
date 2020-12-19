using System.Collections.Generic;
using System.Linq;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 查询帮助类
    /// </summary>
    public static class QueryHelper
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页内容量</param>
        /// <returns></returns>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}