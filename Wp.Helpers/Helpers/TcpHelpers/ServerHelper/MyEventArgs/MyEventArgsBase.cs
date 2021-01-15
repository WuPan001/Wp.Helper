using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs
{
    public class MyEventArgsBase : EventArgs
    {
        /// <summary>
        /// 客户端
        /// </summary>
        protected TcpClient TcpClient { get; set; }

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