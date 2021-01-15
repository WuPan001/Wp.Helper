using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs
{
    /// <summary>
    /// TCP服务器端与客户端的连接已建立事件参数
    /// </summary>
    public class TcpConnectedEventArgs : MyEventArgsBase
    {
        /// <summary>
        /// 与客户端的连接已建立事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        public TcpConnectedEventArgs(TcpClient tcpClient)
        {
            TcpClient = tcpClient ?? throw new ArgumentNullException("tcpClient is null");
        }
    }
}