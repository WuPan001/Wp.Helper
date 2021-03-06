﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs
{
    /// <summary>
    /// TCP服务器端事件基类
    /// </summary>
    public class MyEventArgsBase : EventArgs
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get; set; }

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"<ClientRemoteEndPoint:{TcpClient.Client.RemoteEndPoint}>";
        }
    }
}