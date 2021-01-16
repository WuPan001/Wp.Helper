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
    public class TcpClientHelper : IDisposable
    {
        #region 事件

        /// <summary>
        /// 接收到报文事件
        /// </summary>
        public event EventHandler<TcpMsgReceivedEventArgs<byte[]>> ReceiveMsg;

        /// <summary>
        /// 与服务器的连接已建立事件
        /// </summary>
        public event EventHandler<TcpConnectedEventArgs> ConnectServer;

        /// <summary>
        /// 与服务器的连接已断开事件
        /// </summary>
        public event EventHandler<TcpDisconnectedEventArgs> DisconnectServer;

        /// <summary>
        /// 与服务器的连接发生异常事件
        /// </summary>
        public event EventHandler<TcpCnnExceptionEventArgs> CnnExceptionOccurred;

        #endregion 事件

        #region 属性、字段

        private TcpClient tcpClient;
        private bool _disposed = false;

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
        public IPEndPoint ServerIPEndPoint
        {
            get { return new IPEndPoint(Addresses[0], Port); }
        }

        /// <summary>
        /// 本地客户端终结点
        /// </summary>
        protected IPEndPoint LocalIPEndPoint { get; private set; }

        private Encoding _encoding;

        /// <summary>
        /// 通信所使用的编码
        /// </summary>
        public Encoding Encoding { get { return _encoding; } set { _encoding = value; } }

        #endregion 属性、字段

        #region 方法

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="serverEP">远端服务器终结点</param>
        /// <param name="localEP">本地客户端终结点</param>
        /// <param name="retries">重试次数</param>
        /// <param name="retryInterval">重试间隔，单位为秒</param>
        /// <param name="encoding">报文编码方式</param>
        public TcpClientHelper(IPEndPoint serverEP, IPEndPoint localEP = null, byte retries = 3, int retryInterval = 2, Encoding encoding = null)
            : this(new[] { serverEP.Address }, serverEP.Port, localEP, retries, retryInterval, encoding)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="serverIPAddress">远端服务器IP地址</param>
        /// <param name="serverPort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        /// <param name="retries">重试次数</param>
        /// <param name="retryInterval">重试间隔，单位为秒</param>
        /// <param name="encoding">报文编码方式</param>
        public TcpClientHelper(IPAddress serverIPAddress, int serverPort, IPEndPoint localEP = null, byte retries = 3, int retryInterval = 2, Encoding encoding = null)
            : this(new[] { serverIPAddress }, serverPort, localEP, retries, retryInterval, encoding)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="serverIPAddresses">远端服务器IP地址列表</param>
        /// <param name="serverPort">远端服务器端口</param>
        /// <param name="retries">重试次数</param>
        /// <param name="retryInterval">重试间隔，单位为秒</param>
        /// <param name="encoding">报文编码方式</param>
        public TcpClientHelper(IPAddress[] serverIPAddresses, int serverPort, byte retries = 3, int retryInterval = 2, Encoding encoding = null)
            : this(serverIPAddresses, serverPort, null, retries, retryInterval, encoding)
        {
        }

        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="serverIPAddresses">远端服务器IP地址列表</param>
        /// <param name="serverPort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        /// <param name="retries">重试次数</param>
        /// <param name="retryInterval">重试间隔，单位为秒</param>
        /// <param name="encoding">报文编码方式</param>
        public TcpClientHelper(IPAddress[] serverIPAddresses, int serverPort, IPEndPoint localEP, byte retries = 3, int retryInterval = 2, Encoding encoding = null)
        {
            Addresses = serverIPAddresses;
            Port = serverPort;
            LocalIPEndPoint = localEP;
            Encoding = encoding is null ? Encoding.UTF8 : encoding;
            if (LocalIPEndPoint != null)
            {
                tcpClient = new TcpClient(LocalIPEndPoint);
            }
            else
            {
                tcpClient = new TcpClient();
            }

            Retries = retries;
            RetryInterval = retryInterval;
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
                DisconnectServer?.BeginInvoke(this, new TcpDisconnectedEventArgs(Addresses, Port), null, null);
            }
            return this;
        }

        private void HandleTcpServerConnected(IAsyncResult ar)
        {
            try
            {
                tcpClient.EndConnect(ar);
                ConnectServer?.BeginInvoke(this, new TcpConnectedEventArgs(Addresses, Port), null, null);
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
                    CnnExceptionOccurred?.BeginInvoke(this, new TcpCnnExceptionEventArgs(Addresses, Port, ex), null, null);
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
            if (receivedBytes != null)
            {
                ReceiveMsg?.BeginInvoke(this, new TcpMsgReceivedEventArgs<byte[]>(tcpClient, receivedBytes, _encoding), null, null);
            }
            stream.BeginRead(buffer, 0, buffer.Length, HandleMsgReceived, buffer); // then start reading from the network again
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

        /// <summary>
        /// Dispose方法
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //标记gc不在调用析构函数
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TcpClientHelper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose方法
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed) return; //如果已经被回收，就中断执行
            if (disposing)
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                }
                tcpClient.Dispose();
            }
            //TODO:释放非托管资源
            _disposed = true;
        }

        #endregion 方法
    }
}