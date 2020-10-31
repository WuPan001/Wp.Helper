using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers;
using Wp.Helpers.Enums;

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
                    #region 文件帮助类测试

                    //foreach (var item in FileHelper.GetFilesFullName(true))
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //foreach (var item in FileHelper.GetFilesFullName())
                    //{
                    //    Console.WriteLine(item);
                    //}
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
                    //FileHelper.RenameFiles(appendString: "TestRename", isTotalRename: true);
                    FileHelper.ClassificationFiles(new string[] { " (" });
                    #endregion 文件帮助类测试

                    #region math帮助类测试

                    //Console.WriteLine(MathHelper.ToChinese(Convert.ToDecimal(123.45)));

                    #endregion math帮助类测试

                    #region

                    //Console.WriteLine(ConverterHelper.Convert(EImgType.JPEG, EImgType.SVG));

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