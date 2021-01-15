using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs
{
    /// <summary>
    /// TCP客户端接收到来自服务器端的报文事件参数
    /// </summary>
    public class TcpMsgReceivedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 接收到报文事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="msg">报文</param>
        public TcpMsgReceivedEventArgs(TcpClient tcpClient, T msg)
        {
            TcpClient = tcpClient;
            Msg = msg;
        }

        /// <summary>
        /// 接收到报文事件参数
        /// </summary>
        /// <param name="msg">报文</param>
        public TcpMsgReceivedEventArgs(T msg)
        {
            TcpClient = null;
            Msg = msg;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get; private set; }

        /// <summary>
        /// 报文
        /// </summary>
        public T Msg { get; private set; }
    }
}