using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// object扩展方法
    /// </summary>
    public static class ObjectExtensionMethod
    {
        /// <summary>
        /// 非null检查
        /// </summary>
        /// <param name="param">参数</param>
        public static void NotNull(this object param)
        {
            if (param is null)
            {
                throw new ArgumentNullException(nameof(param));
            }
        }
    }
}