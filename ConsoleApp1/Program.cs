﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers;
using Wp.Helpers.Entities.WpfStyle;
using Wp.Helpers.Enums;
using Wp.Helpers.Redis;

namespace ConsoleApp1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("输入“e”退出程序");

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

                    #region SVG帮助类
                    //var dic = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
                    //SvgHelper.SaveGeometryStyle(dic);

                    #endregion SVG帮助类

                    #region math帮助类测试

                    //Console.WriteLine(MathHelper.ToChinese(Convert.ToDecimal(123.45)));

                    #endregion math帮助类测试

                    #region

                    //Console.WriteLine(ConverterHelper.Convert(EImgType.JPEG, EImgType.SVG));

                    #endregion

                    #region RedisHelper测试
                    //var redis = new RedisHelper();
                    //redis.PushListRight("Test1", "2344");
                    //redis.PushListRight("Test1", "2344");
                    //redis.PushListRight("Test1", "2344");
                    //Console.WriteLine(redis.GetString("Test"));
                    #endregion

                    #region TextBlockStyleHelper测试

                    //Console.WriteLine("请输入样式名：");
                    //var name = Console.ReadLine();
                    //if (name == "e")
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    //    TextBlockStyleHelper.GetStyleFromXML(name, new StyleBase() { BaseName = "PrimaryIconFontTextBlockStyle", BasePath = "/Styles/TextBlockStyles.xaml" });
                    //}

                    #endregion

                    #region Enum帮助类测试
                    //var test = EnumHelper.GetEnumItems<EImgType>();
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); ;
                }
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}