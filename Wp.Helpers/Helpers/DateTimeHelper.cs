using System;

namespace Wp.Helpers.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 返回一个时间戳
        /// </summary>
        /// <returns></returns>
        public static double GetTimeStamp()
        {
            try
            {
                return (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 将时间戳转为对应时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime TimeStampToDateTime(long timeStamp)
        {
            try
            {
                TimeSpan ts = new TimeSpan(timeStamp);
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
                return dt.Add(ts);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}