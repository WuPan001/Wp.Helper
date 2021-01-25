using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Wp.Helpers.Entities.ALiIconFont;

namespace Wp.Helpers.ExtensionMethod
{
    /// <summary>
    /// string对象扩展方法
    /// </summary>
    public static class StringExentionMethod
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

        /// <summary>
        /// 判断一个字符串是否可以转为数字
        /// </summary>
        /// <param name="str">字符串实例</param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            return Regex.IsMatch(str, @"^(-?[0-9]*[.][0-9]*)$");
        }

        /// <summary>
        /// json格式字符串转字典扩展方法
        /// </summary>
        /// <param name="json">json格式字符串</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this string json)
        {
            var res = new Dictionary<string, object>();
            try
            {
                JObject jsonObj = JsonConvert.DeserializeObject(json) as JObject;
                foreach (var p in jsonObj.Properties())
                {
                    if (jsonObj[p.Name].GetType() == typeof(JObject))
                    {
                        res.Add(p.Name, jsonObj[p.Name].ToDictionary());
                    }
                    else if (jsonObj[p.Name].GetType() == typeof(JArray))
                    {
                        var jsonArr = JsonConvert.DeserializeObject(jsonObj[p.Name].ToString()) as JArray;
                        var dic = new Dictionary<string, object>();
                        for (int i = 0; i < jsonArr.Count; i++)
                        {
                            dic.Add($"{p.Name}{i}", jsonArr[i].ToDictionary());
                        }
                        res.Add(p.Name, dic);
                    }
                    else if (jsonObj[p.Name].GetType() == typeof(JValue))
                    {
                        res.Add(p.Name, jsonObj[p.Name].Value<string>());
                    }
                }
            }
            catch (Exception)
            {
                res.Add($"NotJson{DateTime.Now:yyyyMMddHHmmssfff}", json);
            }
            return res;
        }
    }
}