using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; } = "OK";

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="success">结果</param>
        /// <param name="message">消息内容</param>
        public Msg(bool success = true, string message = null)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="success">结果</param>
        /// <param name="message">消息内容</param>
        public Msg(bool success = true, StringBuilder message = null) : this(success, message?.ToString())
        {
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public Msg()
        {
        }

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Success:{Success},Msg:{Message}";
        }
    }
}