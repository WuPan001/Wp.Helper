﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper
{
    /// <summary>
    /// TCP服务器端帮助类
    /// </summary>
    public class TcpServerHelper
    {
        #region 事件

        /// <summary>
        /// 接收到报文事件
        /// </summary>
        public event EventHandler<TcpMsgReceivedEventArgs<byte[]>> ReceiveMsg;

        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<TcpConnectedEventArgs> ClientConnected;

        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<TcpDisconnectedEventArgs> ClientDisconnected;

        #endregion 事件

        #region 属性、字段

        private TcpListener listener;
        private List<TcpClientState> clients;
        private bool disposed = false;

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }

        private Encoding _encoding;

        /// <summary>
        /// 通信使用的编码
        /// </summary>
        public Encoding Encoding { get { return _encoding; } set { _encoding = value; } }

        #endregion 属性、字段

        #region 方法

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public TcpServerHelper(int listenPort) : this(IPAddress.Any, listenPort)
        {
        }

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public TcpServerHelper(IPEndPoint localEP) : this(localEP.Address, localEP.Port)
        {
        }

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        public TcpServerHelper(IPAddress localIPAddress, int listenPort)
        {
            Address = localIPAddress;
            Port = listenPort;
            Encoding = Encoding.UTF8;//.Default;
            clients = new List<TcpClientState>();
            listener = new TcpListener(Address, Port);
            listener.AllowNatTraversal(true);
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns>异步TCP服务器</returns>
        public TcpServerHelper Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                listener.Start();
                listener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted), listener);
            }
            return this;
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="backlog">
        /// 服务器所允许的挂起连接序列的最大长度
        /// </param>
        /// <returns>异步TCP服务器</returns>
        public TcpServerHelper Start(int backlog)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                listener.Start(backlog);
                listener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted), listener);
            }
            return this;
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        /// <returns>异步TCP服务器</returns>
        public TcpServerHelper Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                listener.Stop();
                lock (clients)
                {
                    foreach (var item in clients)
                    {
                        item.TcpClient.Client.Disconnect(false);
                    }
                    clients.Clear();
                }
            }
            return this;
        }

        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            if (IsRunning)
            {
                TcpListener tcpListener = (TcpListener)ar.AsyncState;
                TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);
                byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                TcpClientState internalClient = new TcpClientState(tcpClient, buffer);
                lock (clients)
                {
                    clients.Add(internalClient);
                    ClientConnected?.Invoke(this, new TcpConnectedEventArgs(tcpClient));
                }
                NetworkStream networkStream = internalClient.NetworkStream;
                networkStream.BeginRead(internalClient.Buffer, 0, internalClient.Buffer.Length, HandleDatagramReceived, internalClient);
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted), ar.AsyncState);
            }
        }

        private void HandleDatagramReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
                TcpClientState internalClient = (TcpClientState)ar.AsyncState;
                NetworkStream networkStream = internalClient.NetworkStream;
                int numberOfReadBytes = 0;
                try
                {
                    numberOfReadBytes = networkStream.EndRead(ar);
                }
                catch
                {
                    numberOfReadBytes = 0;
                }

                if (numberOfReadBytes == 0)
                {
                    // connection has been closed
                    lock (clients)
                    {
                        clients.Remove(internalClient);
                        ClientDisconnected?.BeginInvoke(this, new TcpDisconnectedEventArgs(internalClient.TcpClient), null, null);
                        return;
                    }
                }

                // received byte and trigger event notification
                byte[] receivedBytes = new byte[numberOfReadBytes];
                Buffer.BlockCopy(internalClient.Buffer, 0, receivedBytes, 0, numberOfReadBytes);
                ReceiveMsg?.BeginInvoke(this, new TcpMsgReceivedEventArgs<byte[]>(internalClient.TcpClient, receivedBytes, _encoding), null, null);
                networkStream.BeginRead(internalClient.Buffer, 0, internalClient.Buffer.Length, HandleDatagramReceived, internalClient);// continue listening for tcp datagram packets
            }
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="msg">报文</param>
        public void Send(TcpClient tcpClient, byte[] msg)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP server has not been started.");

            if (tcpClient == null)
                throw new ArgumentNullException("tcpClient is null");

            if (msg == null)
                throw new ArgumentNullException("msg is null");

            tcpClient.GetStream().BeginWrite(msg, 0, msg.Length, (IAsyncResult ar) => ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar), tcpClient);
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="msg">报文</param>
        public void Send(TcpClient tcpClient, string msg)
        {
            Send(tcpClient, Encoding.GetBytes(msg));
        }

        /// <summary>
        /// 发送报文至所有客户端
        /// </summary>
        /// <param name="msg"></param>
        public void SendAll(string msg)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP server has not been started.");

            for (int i = 0; i < clients.Count; i++)
            {
                Send(clients[i].TcpClient, msg);
            }
        }

        /// <summary>
        /// Dispose方法
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TcpServerHelper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Stop();

                    if (listener != null)
                    {
                        listener = null;
                    }
                }

                disposed = true;
            }
        }

        #endregion 方法
    }
}