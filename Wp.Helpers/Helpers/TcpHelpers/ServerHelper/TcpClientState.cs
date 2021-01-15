using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers.TcpHelpers.ServerHelper
{
    internal class TcpClientState
    {
        public TcpClientState(TcpClient tcpClient, byte[] buffer)
        {
            TcpClient = tcpClient ?? throw new ArgumentNullException("tcpClient is null");
            Buffer = buffer ?? throw new ArgumentNullException("buffer is null");
        }

        public TcpClient TcpClient { get; private set; }

        public byte[] Buffer { get; private set; }

        public NetworkStream NetworkStream
        {
            get { return TcpClient.GetStream(); }
        }
    }
}