using System.Linq;
using System.Text;
using Wp.Helpers.Entities.ALiIconFont;

namespace Wp.Helpers.ExtensionMethod
{
    internal static class ALiIconFontExtensionMethod
    {
        public static string ToStyle(this IconFont iconFont, string name, string baseStyleName)
        {
            var style = new StringBuilder();
            style.AppendLine($"<Style x:Key=\"{name}IconFontBaseStyle\"");
            style.AppendLine("TargetType=\"TextBlock\"");
            if (!string.IsNullOrEmpty(baseStyleName))
            {
                style.Append("BasedOn=\"{StaticResource ");
                style.Append(baseStyleName);
                style.AppendLine("}\"");
            }
            style.Remove(style.Length - 2, 2);
            style.AppendLine(">");
            style.AppendLine("<Setter Property=\"FontFamily\"");
            style.AppendLine("Value=\"/Resources/#iconfont\"/>");
            style.AppendLine("</Style>");
            foreach (var li in iconFont.Li.OrderBy(li => li.Div.FirstOrDefault().Text))
            {
                style.AppendLine($"<Style x:Key=\"{li.Div.FirstOrDefault().Text}TextBlockStyle\"");
                style.AppendLine("TargetType=\"TextBlock\"");
                style.Append("BasedOn=\"{StaticResource ");
                style.Append($"{name}IconFontBaseStyle");
                style.AppendLine("}\">");
                style.AppendLine("<Setter Property=\"Text\"");
                style.AppendLine($"Value=\"{ li.Div.LastOrDefault().Text}\"/>");
                style.AppendLine("</Style>");
            }
            return style.ToString();
        }
    }
}