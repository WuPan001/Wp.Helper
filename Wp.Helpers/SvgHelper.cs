﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
    /// <summary>
    /// Svg格式图片帮助类
    /// </summary>
    public class SvgHelper
    {
        /// <summary>
        /// 获取图片图案几何路径
        /// </summary>
        /// <param name="filePath">图片文件路径</param>
        /// <param name="splits">拆分字符串集合，默认为null后，集合为 new string[] { "path d=\"", "\" fill=\"", "\" p-id=\"" }</param>
        /// <returns></returns>
        public static string GetGeometryPath(string filePath, string[] splits = null)
        {
            var res = new StringBuilder();
            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath);
                var temp = text.Split(splits == null ? new string[] { "path d=\"", "\" fill=\"", "\" p-id=\"" } : splits, 1024, StringSplitOptions.RemoveEmptyEntries);
                for (byte i = 0; i < temp.Length; i++)
                {
                    if (temp[i][0] == 'M' && temp[i][temp[i].Length - 1] == 'z')
                    {
                        res.Append($"{temp[i]} ");
                    }
                }
                res.Remove(res.Length - 1, 1);
            }
            else
            {
                res = null;
            }
            return res.ToString();
        }

        /// <summary>
        /// 获取图片图案几何路径样式
        /// </summary>
        /// <param name="fileInfo">图片文件路径</param>
        /// <param name="brush">图案其他状态时的颜色，{StaticResource MainSolidColorBurshStyle} or #e0620d</param>
        /// <param name="normalBrush">图案默认颜色，{StaticResource TertiaryTextSolidColorBurshStyle} or #707070</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetGeometryStyle(Dictionary<string, string> fileInfo,
            string brush = "{StaticResource MainSolidColorBurshStyle}", string normalBrush = "{StaticResource TertiaryTextSolidColorBurshStyle}")
        {
            var res = new Dictionary<string, string>();
            var content = new StringBuilder();
            foreach (var item in fileInfo.Keys)
            {
                content.Clear();
                content.AppendLine($"<Geometry x:Key=\"{item}Geometry\">{GetGeometryPath(fileInfo[item])}</Geometry>");
                content.AppendLine($"<DrawingImage x:Key=\"{item}\">");
                content.AppendLine("<DrawingImage.Drawing>");
                content.AppendLine("<DrawingGroup>");
                content.Append("<GeometryDrawing Brush=\"");
                content.Append($"{brush}");
                content.Append("\" Geometry=\"{StaticResource ");
                content.Append($"{item}Geometry");
                content.AppendLine("}\"/>");
                content.AppendLine("	</DrawingGroup>");
                content.AppendLine("</DrawingImage.Drawing>");
                content.AppendLine("</DrawingImage>");
                content.AppendLine($"<DrawingImage x:Key=\"{item}Normal\">");
                content.AppendLine("<DrawingImage.Drawing>");
                content.AppendLine("<DrawingGroup>");
                content.Append("<GeometryDrawing Brush=\"");
                content.Append($"{normalBrush}");
                content.Append("\" Geometry=\"{StaticResource ");
                content.Append($"{item}Geometry");
                content.AppendLine("}\"/>");
                content.AppendLine("	</DrawingGroup>");
                content.AppendLine("</DrawingImage.Drawing>");
                content.Append("</DrawingImage>");
                res.Add(item, content.ToString());
            }
            return res;
        }

        /// <summary>
        /// 保存图片几何图案路径样式到文件
        /// </summary>
        /// <param name="fileInfo">图片文件路径</param>
        public static void SaveGeometryStyle(Dictionary<string, string> fileInfo)
        {
            var saveDirectory = $"{FileHelper.GetDirectory()}//{DateTime.Now:yyyyMMddHHmmss}";
            var savePath = $"{saveDirectory}//GeometryStyles.xaml";
            Directory.CreateDirectory(saveDirectory);
            File.Create(savePath).Close();
            var styles = GetGeometryStyle(fileInfo);
            var sb = new StringBuilder();
            sb.AppendLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            sb.AppendLine(" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
            foreach (var item in styles.Keys)
            {
                sb.AppendLine(styles[item]);
            }
            sb.Append("</ResourceDictionary>");
            File.WriteAllText(savePath, sb.ToString());
        }
    }
}