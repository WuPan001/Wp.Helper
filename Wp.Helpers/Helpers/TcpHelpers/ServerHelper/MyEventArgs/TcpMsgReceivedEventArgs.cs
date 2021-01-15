﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs
{
    /// <summary>
    /// TCP服务器端接收到报文事件
    /// </summary>
    public class TcpMsgReceivedEventArgs : MyEventArgsBase
    {
        /// <summary>
        /// 接收到数据报文事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="msg">报文</param>
        public TcpMsgReceivedEventArgs(TcpClient tcpClient, byte[] msg)
        {
            TcpClient = tcpClient;
            Msg = msg;
        }

        /// <summary>
        /// 报文
        /// </summary>
        public byte[] Msg { get; private set; }

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()} Msg:{Msg}";
        }
    }
}