using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 计算任意2个小数之间的随机数
        /// </summary>
        /// <param name="first">第一个小数</param>
        /// <param name="second">第二个小数</param>
        /// <returns></returns>
        public static double NextDouble(double first, double second)
        {
            if (first == second)
            {
                return first;
            }
            else
            {
                var firstCount = MathHelper.GetPointCount(first);
                var secondCount = MathHelper.GetPointCount(second);
                var max = Math.Max(firstCount, secondCount);
                var firstInt = (int)(first * Math.Pow(10, max));
                var secondInt = (int)(second * Math.Pow(10, max));
                var random = new Random();
                return firstInt > secondInt ? random.Next(secondInt, firstInt) / Math.Pow(10, max) : random.Next(firstInt, secondInt) / Math.Pow(10, max);
            }
        }
    }
}