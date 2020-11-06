using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Entities.ALiIconFont;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// string对象扩展方法
    /// </summary>
    internal static class StringExentionMethod
    {
        /// <summary>
        /// json格式的字符串转为阿里巴巴IconFont
        /// </summary>
        /// <param name="json">json格式的字符串</param>
        /// <returns></returns>
        public static IconFont ToALiIconFont(this string json)
        {
            return JsonConvert.DeserializeObject<IconFont>(json.Replace("@class", "class").Replace("#text", "text"));
        }

        /// <summary>
        /// 非空或null检查
        /// </summary>
        /// <param name="param">参数</param>
        public static void NotNullOrEmpty(this string param)
        {
            param.NotNull();
            if (param is null)
            {
                throw new ArgumentNullException(nameof(param));
            }
        }
    }
}