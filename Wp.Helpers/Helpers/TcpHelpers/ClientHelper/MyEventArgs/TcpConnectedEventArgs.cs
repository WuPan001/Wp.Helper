using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs
{
    /// <summary>
    /// TCP客户端与服务器的连接已建立事件参数
    /// </summary>
    public class TcpConnectedEventArgs : MyEventArgsBase
    {
        /// <summary>
        /// 与服务器的连接已建立事件参数
        /// </summary>
        /// <param name="ipAddresses">服务器IP地址列表</param>
        /// <param name="port">服务器端口</param>
        public TcpConnectedEventArgs(IPAddress[] ipAddresses, int port)
        {
            if (ipAddresses is null)
                throw new ArgumentNullException("ipAddresses is null");
            Addresses = ipAddresses;
            Port = port;
        }
    }
}