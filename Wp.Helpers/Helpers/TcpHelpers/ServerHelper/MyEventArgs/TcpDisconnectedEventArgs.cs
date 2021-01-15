using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs
{
    /// <summary>
    /// 与客户端的连接已断开事件参数
    /// </summary>
    public class TcpDisconnectedEventArgs : MyEventArgsBase
    {
        /// <summary>
        /// 与客户端的连接已断开事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        public TcpDisconnectedEventArgs(TcpClient tcpClient)
        {
            TcpClient = tcpClient ?? throw new ArgumentNullException("tcpClient is null");
        }
    }
}