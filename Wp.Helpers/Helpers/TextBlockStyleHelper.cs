using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Wp.Helpers.Entities.WpfStyle;
using Wp.Helpers.ExtensionMethod;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// WPF TextBlock样式帮助类
    /// </summary>
    public class TextBlockStyleHelper
    {
        /// <summary>
        /// xml文件转style
        /// </summary>
        /// <param name="name">style名称</param>
        /// <param name="styleBase">style基类信息</param>
        /// <param name="isOpenFolder">是否打开文件夹</param>
        public static void GetStyleFromXML(string name, StyleBase styleBase = null, bool isOpenFolder = true)
        {
            var xmlPath = FileHelper.GetFilesFullName(false, filter: "HTML文件(*.html) | *.html", title: "请选择demo_index.html文件").FirstOrDefault();
            if (!string.IsNullOrEmpty(xmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);

                #region

                var style = new StringBuilder();
                style.AppendLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
                style.AppendLine("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
                if (styleBase is null)
                {
                    //
                }
                else
                {
                    style.AppendLine("<ResourceDictionary.MergedDictionaries>");
                    style.AppendLine($"<ResourceDictionary Source=\"{styleBase.BasePath}\"/>");
                    style.AppendLine("</ResourceDictionary.MergedDictionaries>");
                }
                style.AppendLine(JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true).ToALiIconFont().ToStyle(name, styleBase.BaseName));
                style.Append("</ResourceDictionary>");
                var directoryPath = Path.Combine(FileHelper.GetDirectory(), $"{name}TextBlockStyles{DateTime.Now:yyyyMMddHHmmss}");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.WriteAllText(Path.Combine(directoryPath, $"{name}TextBlockStyles.xaml"), style.ToString());
                if (isOpenFolder)
                {
                    System.Diagnostics.Process.Start(directoryPath);
                }
            }

            #endregion
        }
    }
}