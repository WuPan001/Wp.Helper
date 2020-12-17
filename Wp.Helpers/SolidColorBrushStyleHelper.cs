using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
    /// <summary>
    /// SolidColorBrushStyle样式文件生成帮助类
    /// </summary>
    public class SolidColorBrushStyleHelper
    {
        /// <summary>
        /// 保存SolidColorBrushStyle样式文件
        /// </summary>
        public static void SaveSolidColorBrushStyle()
        {
            var res = new StringBuilder();
            var filePath = FileHelper.GetFilesFullName(false).FirstOrDefault();
            var dic = new Dictionary<string, string>();
            var content = new StringBuilder();
            try
            {
                if (File.Exists(filePath))
                {
                    var text = File.ReadAllText(filePath);
                    var temp = text.Split((new string[] { "<Color x:Key=\"", "\">#" }), StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (i % 2 != 0)
                        {
                            dic.Add(temp[i].Replace("ColorStyle", "SolidColorBrushStyle"), temp[i]);
                        }
                    }
                    content.AppendLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
                    content.AppendLine(" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
                    foreach (var item in dic.Keys)
                    {
                        content.Append("< SolidColorBrush x:Key =\"");
                        content.Append(item);
                        content.AppendLine("\"");
                        content.Append("Color=\"{StaticResource ");
                        content.Append(dic[item]);
                        content.AppendLine("}\"/>");
                    }
                    content.Append("</ResourceDictionary>");

                    var saveDirectory = Path.Combine(FileHelper.GetDirectory(), $"{DateTime.Now:yyyyMMddHHmmss}");
                    var savePath = Path.Combine(saveDirectory, "SolidColorBrushStyles.xaml");
                    Directory.CreateDirectory(saveDirectory);
                    File.WriteAllText(savePath, content.ToString());
                    System.Diagnostics.Process.Start(saveDirectory);
                }
                else
                {
                    res = null;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"FilePath:{filePath}");
            }
        }
    }
}