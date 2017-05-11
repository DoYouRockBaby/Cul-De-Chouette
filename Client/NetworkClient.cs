using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public delegate void OnMessageHandler(string message);

    public class NetworkClient
    {
        private System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        private NetworkStream serverStream = default(NetworkStream);
        public OnMessageHandler OnMessage;

        public void Start(string address, int port)
        {
            clientSocket.Connect(address, port);
            serverStream = clientSocket.GetStream();

            Thread ctThread = new Thread(Listen);
            ctThread.Start();
        }

        public void Send(string message)
        {
            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(message);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void Listen()
        {
            while (clientSocket.Connected)
            {
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = clientSocket.ReceiveBufferSize;
                int length = serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.UTF8.GetString(inStream);
                returndata = returndata.Substring(0, length);
                OnMessage(returndata);
            }
        }
    }
}