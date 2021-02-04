using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TemplateHelpers.VsCodeHelpers
{
    /// <summary>
    /// 用户模板生成帮助类
    /// </summary>
    public class UserTemplateHelper
    {
        /// <summary>
        /// 生成vscode 用户模板
        /// </summary>
        /// <param name="templateName">模板名</param>
        /// <param name="prefix">快捷键</param>
        /// <param name="body">模板内容</param>
        /// <param name="description">模板描述</param>
        /// <returns></returns>
        public static string GetUserTemplate(string templateName, string prefix, List<string> body, string description)
        {
            try
            {
                var res = new StringBuilder();
                res.Append($"\"{templateName}\":");
                res.AppendLine(" {");
                res.AppendLine($"\"prefix\": \"{prefix}\",");
                res.AppendLine("\"body\": [");
                foreach (var item in body)
                {
                    res.AppendLine($"\"{item}\",");
                }
                res.AppendLine("],");
                res.AppendLine($"\"description\": \"{description}\"");
                res.AppendLine(" }");
                return res.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}