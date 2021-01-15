using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs
{
    /// <summary>
    /// TCP客户端事件参数基类
    /// </summary>
    public class MyEventArgsBase : EventArgs
    {
        /// <summary>
        /// 服务器IP地址列表
        /// </summary>
        protected IPAddress[] Addresses { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        protected int Port { get; set; }

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var res = new StringBuilder();
            foreach (var item in Addresses)
            {
                res.Append($"{item},");
            }
            res.Remove(res.Length - 1, 1);
            res.Append($"Addresses:{res} Port:{Port}");
            return res.ToString();
        }
    }
}