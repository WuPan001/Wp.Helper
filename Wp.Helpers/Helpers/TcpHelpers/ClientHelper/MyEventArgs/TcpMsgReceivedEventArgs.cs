﻿using System;
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
        /// <param name="encoding">报文编码方式</param>
        public TcpMsgReceivedEventArgs(TcpClient tcpClient, T msg, Encoding encoding)
        {
            TcpClient = tcpClient;
            Msg = msg;
            Encoding = encoding;
        }

        /// <summary>
        /// 接收到报文事件参数
        /// </summary>
        /// <param name="msg">报文</param>
        /// <param name="encoding">报文编码方式</param>
        public TcpMsgReceivedEventArgs(T msg, Encoding encoding) : this(null, msg, encoding)
        {
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get; private set; }

        /// <summary>
        /// 报文
        /// </summary>
        public T Msg { get; private set; }

        /// <summary>
        /// 报文编码方式
        /// </summary>
        public Encoding Encoding { get; private set; }
    }
}