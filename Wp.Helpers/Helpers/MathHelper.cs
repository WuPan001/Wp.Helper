using System.Text.RegularExpressions;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// math帮助类
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// 将金额转成中文大写
        /// </summary>
        /// <returns></returns>
        public static string ToChinese(decimal amount)
        {
            var s = amount.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
        }

        /// <summary>
        /// 计算小数位数
        /// </summary>
        /// <param name="num">值</param>
        /// <returns></returns>
        public static int GetPointCount(decimal num)
        {
            var str = num.ToString();
            return str.IndexOf(".") > 0 ? str.Length - 1 - str.IndexOf('.') : 0;
        }

        /// <summary>
        /// 计算小数位数
        /// </summary>
        /// <param name="num">值</param>
        /// <returns></returns>
        public static int GetPointCount(double num)
        {
            return GetPointCount((decimal)num);
        }

        /// <summary>
        /// 计算小数位数
        /// </summary>
        /// <param name="num">值</param>
        /// <returns></returns>
        public static int GetPointCount(float num)
        {
            return GetPointCount((decimal)num);
        }
    }
}