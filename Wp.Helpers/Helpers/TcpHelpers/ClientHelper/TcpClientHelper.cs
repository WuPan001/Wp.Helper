using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs;

namespace Wp.Helpers.Helpers.TcpHelpers.ClientHelper
{
    /// <summary>
    /// TCP客户端帮助类
    /// </summary>
    public class TcpClientHelper
    {
        #region 事件

        /// <summary>
        /// 接收报文事件
        /// </summary>
        public event EventHandler<TcpMsgReceivedEventArgs<string>> OnMsgReceived;

        /// <summary>
        /// 与服务器的连接已建立事件
        /// </summary>
        public event EventHandler<TcpConnectedEventArgs> OnConnected;

        /// <summary>
        /// 与服务器的连接已断开事件
        /// </summary>
        public event EventHandler<TcpDisconnectedEventArgs> OnDisconnected;

        /// <summary>
        /// 与服务器的连接发生异常事件
        /// </summary>
        public event EventHandler<TcpCnnExceptionEventArgs> OnCnnExceptionOccurred;

        #endregion 事件

        #region 属性、字段

        private TcpClient tcpClient;
        private bool disposed = false;

        /// <summary>
        /// 标识是否建立连接
        /// </summary>
        public bool Connected { get { return tcpClient.Client.Connected; } }

        /// <summary>
        /// 远端服务器的IP地址列表
        /// </summary>
        public IPAddress[] Addresses { get; private set; }

        /// <summary>
        /// 远端服务器的端口
        /// </summary>
        public int Port { get; private set; }

        private int _retries = 0;

        /// <summary>
        /// 连接重试次数
        /// </summary>
        public int Retries { get { return _retries; } set { _retries = value; } }

        private int _retryInterval;

        /// <summary>
        /// 连接重试间隔
        /// </summary>
        public int RetryInterval
        {
            get { return _retryInterval; }
            set { _retryInterval = value; }
        }

        /// <summary>
        /// 远端服务器终结点
        /// </summary>
        public IPEndPoint RemoteIPEndPoint
        {
            get { return new IPEndPoint(Addresses[0], Port); }
        }

        /// <summary>
        /// 本地客户端终结点
        /// </summary>
        protected IPEndPoint LocalIPEndPoint { get; private set; }

        /// <summary>
        /// 通信所使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion 属性、字段

        #region 方法

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteEP">远端服务器终结点</param>
        public TcpClientHelper(IPEndPoint remoteEP) : this(new[] { remoteEP.Address }, remoteEP.Port)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddress">远端服务器IP地址</param>
        /// <param name="remotePort">远端服务器端口</param>
        public TcpClientHelper(IPAddress remoteIPAddress, int remotePort) : this(new[] { remoteIPAddress }, remotePort)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddresses">远端服务器IP地址列表</param>
        /// <param name="remotePort">远端服务器端口</param>
        public TcpClientHelper(IPAddress[] remoteIPAddresses, int remotePort) : this(remoteIPAddresses, remotePort, null)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddresses">远端服务器IP地址列表</param>
        /// <param name="remotePort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        public TcpClientHelper(IPAddress[] remoteIPAddresses, int remotePort, IPEndPoint localEP)
        {
            Addresses = remoteIPAddresses;
            Port = remotePort;
            LocalIPEndPoint = localEP;
            Encoding = Encoding.UTF8;//.Default;
            if (LocalIPEndPoint != null)
            {
                tcpClient = new TcpClient(LocalIPEndPoint);
            }
            else
            {
                tcpClient = new TcpClient();
            }

            Retries = 3;
            RetryInterval = 5;
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <returns>异步TCP客户端</returns>
        public TcpClientHelper Connect()
        {
            if (!Connected)
            {
                tcpClient.BeginConnect(Addresses, Port, HandleTcpServerConnected, tcpClient);
            }

            return this;
        }

        /// <summary>
        /// 关闭与服务器的连接
        /// </summary>
        /// <returns>异步TCP客户端</returns>
        public TcpClientHelper Close()
        {
            if (Connected)
            {
                _retries = 0;
                tcpClient.Close();
                RaiseServerDisconnected(Addresses, Port);
            }
            return this;
        }

        private void HandleTcpServerConnected(IAsyncResult ar)
        {
            try
            {
                tcpClient.EndConnect(ar);
                RaiseServerConnected(Addresses, Port);
                _retries = 0;
            }
            catch (Exception ex)
            {
                //ExceptionHandler.Handle(ex);
                //if (retries > 0)
                //{
                //    Logger.Debug(string.Format(CultureInfo.InvariantCulture,
                //      "Connect to server with retry {0} failed.", retries));
                //}

                _retries++;
                if (_retries > Retries)
                {
                    // we have failed to connect to all the IP Addresses,
                    // connection has failed overall.
                    RaiseServerExceptionOccurred(Addresses, Port, ex);
                    return;
                }
                else
                {
                    //Logger.Debug(string.Format(CultureInfo.InvariantCulture,
                    //  "Waiting {0} seconds before retrying to connect to server.",
                    //  RetryInterval));
                    Thread.Sleep(TimeSpan.FromSeconds(RetryInterval));
                    Connect();
                    return;
                }
            }

            // we are connected successfully and start asyn read operation.
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            tcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, HandleMsgReceived, buffer);
        }

        private void HandleMsgReceived(IAsyncResult ar)
        {
            NetworkStream stream = tcpClient.GetStream();
            int numberOfReadBytes = 0;
            try
            {
                numberOfReadBytes = stream.EndRead(ar);
            }
            catch
            {
                numberOfReadBytes = 0;
            }

            if (numberOfReadBytes == 0)
            {
                // connection has been closed
                Close();
                return;
            }
            // received byte and trigger event notification
            byte[] buffer = (byte[])ar.AsyncState;
            byte[] receivedBytes = new byte[numberOfReadBytes];
            Buffer.BlockCopy(buffer, 0, receivedBytes, 0, numberOfReadBytes);
            RaiseMsgReceived(tcpClient, receivedBytes);
            // then start reading from the network again
            stream.BeginRead(buffer, 0, buffer.Length, HandleMsgReceived, buffer);
        }

        private void RaiseMsgReceived(TcpClient sender, byte[] datagram)
        {
            if (OnMsgReceived != null)
            {
                string tmpString = Encoding.UTF8.GetString(datagram);
                if (tmpString.Contains("receipt"))
                {
                    OnMsgReceived(this, new TcpMsgReceivedEventArgs<string>(sender, tmpString));
                }
                else
                {
                    OnMsgReceived(this, new TcpMsgReceivedEventArgs<string>(sender, Encoding.GetString(datagram, 0, datagram.Length)));
                }
            }
        }

        private void RaiseServerConnected(IPAddress[] ipAddresses, int port)
        {
            OnConnected?.Invoke(this, new TcpConnectedEventArgs(ipAddresses, port));
        }

        private void RaiseServerDisconnected(IPAddress[] ipAddresses, int port)
        {
            OnDisconnected?.Invoke(this, new TcpDisconnectedEventArgs(ipAddresses, port));
        }

        private void RaiseServerExceptionOccurred(IPAddress[] ipAddresses, int port, Exception ex)
        {
            OnCnnExceptionOccurred?.Invoke(this, new TcpCnnExceptionEventArgs(ipAddresses, port, ex));
        }

        /// <summary>
        /// 发送报文
        /// </summary>
        /// <param name="msg">报文</param>
        public void Send(string msg)
        {
            Send(Encoding.GetBytes(msg));
        }

        /// <summary>
        /// 发送报文
        /// </summary>
        /// <param name="msg">报文</param>
        public void Send(byte[] msg)
        {
            tcpClient.GetStream().BeginWrite(msg, 0, msg.Length, (IAsyncResult ar) => ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar), tcpClient);
        }

        #endregion 方法
    }
}