using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs
{
    /// <summary>
    /// TCP客户端连接异常事件参数
    /// </summary>
    public class TcpCnnExceptionEventArgs : MyEventArgsBase
    {
        /// <summary>
        /// 与服务器的连接发生异常事件参数
        /// </summary>
        /// <param name="ipAddresses">服务器IP地址列表</param>
        /// <param name="port">服务器端口</param>
        /// <param name="ex">内部异常</param>
        public TcpCnnExceptionEventArgs(IPAddress[] ipAddresses, int port, Exception ex)
        {
            if (ipAddresses is null)
                throw new ArgumentNullException("ipAddresses is null");
            Addresses = ipAddresses;
            Port = port;
            Ex = ex;
        }

        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception Ex { get; private set; }

        /// <summary>
        /// ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()} Exception:{Ex}";
        }
    }
}