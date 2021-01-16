using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wp.Helpers.Helpers.TcpHelpers.ClientHelper;
using Wp.Helpers.Helpers.TcpHelpers.ServerHelper;

namespace TcpHelperTest
{
    public partial class Form1 : Form
    {
        private TcpClientHelper _client;
        private TcpServerHelper _server;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_client = new ClientHelper(new IPEndPoint(IPAddress.Parse(ServerIP.Text.Trim()), Convert.ToInt32(ServerPort.Text.Trim())));
            _client = new TcpClientHelper(
                new IPAddress[] { IPAddress.Parse(ServerIP.Text.Trim()) },
                Convert.ToInt32(ServerPort.Text.Trim()),
                new IPEndPoint(IPAddress.Parse(ClientIP.Text.Trim()),
                Convert.ToInt32(ClientPort.Text.Trim())));
            _client.ConnectServer += _client_ConnectServer;
            _client.DisconnectServer += _client_DisconnectServer;
            _client.CnnExceptionOccurred += _client_CnnExceptionOccurred;
            _client.ReceiveMsg += _client_ReceiveMsg;
            _client.Connect();
        }

        private void _client_ReceiveMsg(object sender, Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs.TcpMsgReceivedEventArgs<byte[]> e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Logger.Invoke(new MethodInvoker(delegate
                    {
                        Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>接收到服务器<{e.TcpClient.Client.RemoteEndPoint}> {e.Encoding.GetString(e.Msg)}\r\n");
                        MsgFromServer.Invoke(new MethodInvoker(delegate { MsgFromServer.Text = e.Encoding.GetString(e.Msg); }));
                    }));
                }
                else
                {
                    Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>接收到服务器<{e.TcpClient.Client.RemoteEndPoint}> {e.Encoding.GetString(e.Msg)}\r\n");
                    MsgFromServer.Text = e.Encoding.GetString(e.Msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _client_CnnExceptionOccurred(object sender, Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs.TcpCnnExceptionEventArgs e)
        {
            if (InvokeRequired)
            {
                Logger.Invoke(new MethodInvoker(delegate
                {
                    Logger.AppendText($"{DateTime.Now}|连接抛出异常：{e}\r\n");
                }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|连接抛出异常：{e}\r\n");
            }
        }

        private void _client_DisconnectServer(object sender, Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs.TcpDisconnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Logger.Invoke(new MethodInvoker(delegate
                {
                    Logger.AppendText($"{DateTime.Now}|成功断开连接{e}\r\n");
                }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|成功断开连接{e}\r\n");
            }
        }

        private void _client_ConnectServer(object sender, Wp.Helpers.Helpers.TcpHelpers.ClientHelper.MyEventArgs.TcpConnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Logger.Invoke(new MethodInvoker(delegate
                {
                    Logger.AppendText($"{DateTime.Now}|成功连接到{e}\r\n");
                }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|成功连接到{e}\r\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client.Connected)
                {
                    _client.Send(MsgToServer.Text.Trim());
                }
                else
                {
                    MessageBox.Show("未连接到服务器！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _server = new TcpServerHelper(Convert.ToInt32(ServerPort.Text.Trim()));
            _server.ClientConnected += _server_ClientConnected;
            _server.ClientDisconnected += _server_ClientDisconnected;
            _server.ReceiveMsg += _server_ReceiveMsg;
            _server.Start();
            if (InvokeRequired)
            {
                Logger.Invoke(new MethodInvoker(delegate { Logger.AppendText($"{DateTime.Now}|TCP服务器开启监听成功，Port:{_server.Port}\r\n"); }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|TCP服务器开启监听成功，Port:{_server.Port}\r\n");
            }
        }

        private void _server_ReceiveMsg(object sender, Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs.TcpMsgReceivedEventArgs<byte[]> e)
        {
            if (InvokeRequired)
            {
                MsgFromClient.Invoke(new MethodInvoker(delegate { MsgFromClient.Text = _server.Encoding.GetString(e.Msg); }));
                Logger.Invoke(
                    new MethodInvoker(
                    delegate
                    {
                        Logger.AppendText($"{DateTime.Now}|服务器<{e.TcpClient.Client.RemoteEndPoint}>收到客户端<{e.TcpClient.Client.LocalEndPoint}> {e.Encoding.GetString(e.Msg)}\r\n");
                    }));
            }
            else
            {
                MsgFromClient.Text = _server.Encoding.GetString(e.Msg);
                Logger.AppendText($"{DateTime.Now}|服务器<{e.TcpClient.Client.RemoteEndPoint}>收到客户端<{e.TcpClient.Client.LocalEndPoint}> {e.Encoding.GetString(e.Msg)}\r\n");
            }
        }

        private void _server_ClientDisconnected(object sender, Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs.TcpDisconnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Logger.Invoke(
                    new MethodInvoker(
                        delegate
                        {
                            Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>断开与服务器<{e.TcpClient.Client.RemoteEndPoint}>连接\r\n");
                        }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>断开与服务器<{e.TcpClient.Client.RemoteEndPoint}>连接\r\n");
            }
        }

        private void _server_ClientConnected(object sender, Wp.Helpers.Helpers.TcpHelpers.ServerHelper.MyEventArgs.TcpConnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Logger.Invoke(new MethodInvoker(delegate { Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>建立连接\r\n"); }));
            }
            else
            {
                Logger.AppendText($"{DateTime.Now}|客户端<{e.TcpClient.Client.LocalEndPoint}>建立连接\r\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _server.SendAll(MsgToClient.Text.Trim());
        }
    }
}