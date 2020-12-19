using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers;
using Wp.Helpers.Entities.ALiIconFont;
using Wp.Helpers.Entities.WpfStyle;
using Wp.Helpers.Enums;
using Wp.Helpers.ExtensionMethod;
using Wp.Helpers.Helpers;
using Wp.Helpers.Redis;

namespace ConsoleApp1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var cmd = string.Empty;
            var cmdCache = string.Empty;
            while (true)
            {
                try
                {
                    Console.WriteLine("输入\"e\"退出程序");
                    Console.WriteLine("输入\"r\"重复上次操作");
                    Console.WriteLine("输入\"t\"进入测试程序");
                    Console.WriteLine("输入\"生成2个小数之间的随机数\"，以生成2个小数之间的随机数");
                    Console.WriteLine("输入\"保存几何样式\" ，以根据SVG文件生成并保存几何样式");
                    Console.WriteLine("输入\"保存IconFont转TextBlock样式\" ，以根据阿里IconFont生成IconFontStyle样式文件");
                    Console.WriteLine("输入\"数字转中文大写\" ，以将数字转为中文大写");
                    Console.WriteLine("输入\"保存svg转TextBlock样式\" ，以根据svg文件生成TextBlockStyle样式文件");
                    cmd = Console.ReadLine();
                    Do(cmd, ref cmdCache);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); ;
                }
                Console.WriteLine("Done");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void Do(string cmd, ref string cmdCache)
        {
            switch (cmd)
            {
                case "保存svg转TextBlock样式":
                    SvgHelper.SaveGeometryStyle(FileHelper.GetFilesName());
                    break;

                case "e":
                    return;

                case "r":
                    Do(cmdCache, ref cmdCache);
                    break;

                case "t":

                    #region 文件帮助类测试

                    //foreach (var item in FileHelper.GetFilesFullName(true))
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //break;

                    //foreach (var item in FileHelper.GetDirectoriesFullName())
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //foreach (var item in FileHelper.GetFileSystemInfo())
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //var dic = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
                    //foreach (var item in dic.Keys)
                    //{
                    //    Console.WriteLine(item);
                    //    Console.WriteLine(dic[item]);
                    //    Console.WriteLine(File.ReadAllText(dic[item]));
                    //}
                    //SvgHelper.SaveGeometryStyle(dic);
                    //FileHelper.RenameFiles(new string[] { " (" });
                    //FileHelper.ClassificationFiles(new string[] { " (" });

                    #endregion 文件帮助类测试

                    #region RedisHelper测试

                    //var redis = new RedisHelper();
                    //redis.PushListRight("Test1", "2344");
                    //redis.PushListRight("Test1", "2344");
                    //redis.PushListRight("Test1", "2344");
                    //Console.WriteLine(redis.GetString("Test"));

                    #endregion RedisHelper测试

                    #region Enum帮助类测试

                    //var test = EnumHelper.GetEnumItems<EImgType>();

                    #endregion Enum帮助类测试

                    //var dic1 = EnumHelper.GetEnumValueKeyIsDescription<EImgType>();

                    #region 属性帮助类测试

                    //var styleBase = new StyleBase() { BaseName = "Name", BasePath = "Path" };
                    //var propertyDic = PropertyHepler.GetPropertyValueKeyIsName(styleBase);
                    //foreach (var item in propertyDic.Keys)
                    //{
                    //    Console.WriteLine($"{item}    {propertyDic[item]}");
                    //}

                    #endregion 属性帮助类测试

                    #region SolidColorBrushStyleHelper测试

                    //SolidColorBrushStyleHelper.SaveSolidColorBrushStyle();

                    #endregion SolidColorBrushStyleHelper测试

                    #region ini文件帮助类测试

                    //var styleBase = new StyleBase() { BaseName = "Name", BasePath = "Path", TestBool = true };
                    //var path = FileHelper.GetDirectory();
                    //IniHelper.WriteAsyncPropertyNameAsKey("TestSection", styleBase, Path.Combine(path, "TestIni.ini")).Wait();
                    //var temp = IniHelper.ReadAsync("TestSection", "TestBool", Path.Combine(FileHelper.GetDirectory(), "TestIni.ini")).Result;
                    //var temp1 = Convert.ToBoolean(temp);
                    //Console.WriteLine(temp1);
                    //Console.WriteLine(DateTime.Now);
                    var test = IniHelper.ReadAsyncT<StyleBase>("TestSection", Path.Combine(FileHelper.GetDirectory(), "TestIni.ini")).Result;
                    Console.WriteLine(DateTime.Now);

                    #endregion ini文件帮助类测试

                    break;

                case "保存IconFont转TextBlock样式":
                    cmdCache = cmd;
                    Console.WriteLine("请输入样式名：");
                    var name = Console.ReadLine().Trim();
                    Console.WriteLine("请输入样式BaseName，默认为\"PrimaryIconFontTextBlockStyle\"，可回车使用默认BaseName");
                    var baseName = Console.ReadLine().Trim();
                    Console.WriteLine("请输入样式BasePath，默认为\"/Styles/TextBlockStyles.xaml\"，可回车使用默认BasePath");
                    var basePath = Console.ReadLine().Trim();
                    TextBlockStyleHelper.GetStyleFromXML(name, new StyleBase()
                    {
                        BaseName = string.IsNullOrEmpty(baseName) ? "PrimaryIconFontTextBlockStyle" : baseName,
                        BasePath = string.IsNullOrEmpty(basePath) ? "/Styles/TextBlockStyles.xaml" : basePath
                    });
                    break;

                case "数字转中文大写":
                    cmdCache = cmd;
                    Console.WriteLine("请输入数字：");
                    Console.WriteLine(MathHelper.ToChinese(Convert.ToDecimal(Console.ReadLine())));
                    break;

                case "保存几何样式":
                    cmdCache = cmd;
                    var dic = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
                    SvgHelper.SaveGeometryStyle(dic);
                    break;

                case "生成2个小数之间的随机数":
                    cmdCache = cmd;
                    Console.WriteLine("请输入第一个小数值：");
                    var first = Convert.ToDouble(Console.ReadLine().Trim());
                    Console.WriteLine("请输入第二个小数值：");
                    var second = Convert.ToDouble(Console.ReadLine().Trim());
                    Console.WriteLine(RandomHelper.NextDouble(first, second));
                    break;

                default:
                    break;
            }

            #region 文件帮助类测试

            //foreach (var item in FileHelper.GetFilesFullName(true))
            //{
            //    Console.WriteLine(item);
            //}
            //break;

            //foreach (var item in FileHelper.GetDirectoriesFullName())
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in FileHelper.GetFileSystemInfo())
            //{
            //    Console.WriteLine(item);
            //}
            //var dic = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
            //foreach (var item in dic.Keys)
            //{
            //    Console.WriteLine(item);
            //    Console.WriteLine(dic[item]);
            //    Console.WriteLine(File.ReadAllText(dic[item]));
            //}
            //SvgHelper.SaveGeometryStyle(dic);
            //FileHelper.RenameFiles(new string[] { " (" });
            //FileHelper.ClassificationFiles(new string[] { " (" });

            #endregion 文件帮助类测试

            #region RedisHelper测试

            //var redis = new RedisHelper();
            //redis.PushListRight("Test1", "2344");
            //redis.PushListRight("Test1", "2344");
            //redis.PushListRight("Test1", "2344");
            //Console.WriteLine(redis.GetString("Test"));

            #endregion RedisHelper测试

            #region Enum帮助类测试

            //var test = EnumHelper.GetEnumItems<EImgType>();

            #endregion Enum帮助类测试
        }
    }
}