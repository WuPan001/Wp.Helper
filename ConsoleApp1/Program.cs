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
                foreach (var item in FileHelper.GetFilesFullName(true))
                {
                    Console.WriteLine(item);
                }
                foreach (var item in FileHelper.GetFilesFullName())
                {
                    Console.WriteLine(item);
                }
                foreach (var item in FileHelper.GetDirectoriesFullName())
                {
                    Console.WriteLine(item);
                }
                foreach (var item in FileHelper.GetFileSystemInfo())
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
            Console.ReadLine();
        }
    }
}