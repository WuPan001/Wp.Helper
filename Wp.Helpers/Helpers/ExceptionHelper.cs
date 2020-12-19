using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 异常帮助类
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// 在控制台打印出异常信息
        /// </summary>
        /// <typeparam name="T">消息头类型，为string或stringbuilder</typeparam>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void PrintException<T>(T msg, Exception ex)
        {
            Console.WriteLine($"{msg}{Environment.NewLine}{GetExceptionDetailByApp(ex)}");
        }

        /// <summary>
        /// 获取异常详情
        /// </summary>
        /// <param name="ex">异常实例</param>
        /// <returns></returns>
        public static string GetExceptionDetailByWeb(Exception ex)
        {
            return $"异常详情:<br>Name:{ex.GetType().FullName}<br>Message:{ex.Message}<br>StackTrace:{ex.StackTrace}<br>TargetSite:{ex.TargetSite}<br>InnerException:{ex.InnerException}<br>Source:{ex.Source}<br>";
        }

        /// <summary>
        /// 获取异常详情
        /// </summary>
        /// <param name="ex">异常实例</param>
        /// <returns></returns>
        public static string GetExceptionDetailByApp(Exception ex)
        {
            return $"异常详情:{Environment.NewLine}Name:{ex.GetType().FullName}{Environment.NewLine}Message:{ex.Message}{Environment.NewLine}StackTrace:{ex.StackTrace}{Environment.NewLine}TargetSite:{ex.TargetSite}{Environment.NewLine}InnerException:{ex.InnerException}{Environment.NewLine}Source:{ex.Source}";
        }
    }
}