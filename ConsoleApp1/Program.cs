using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers;

namespace ConsoleApp1
{
    internal class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
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
                #endregion 文件帮助类测试

                #region math帮助类测试

                //Console.WriteLine(MathHelper.ToChinese(Convert.ToDecimal(123.45)));

                #endregion math帮助类测试

                #region

                //

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
            Console.ReadLine();
        }
    }
}