using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace p2pWpf
{
    class TCPHandler
    {



        Socket listener;
        static ManualResetEvent allDone = new ManualResetEvent(false);

        public TCPHandler(int port)
        {
            StartListening(port);
        }

        public void stopListening()
        {
            listener.Close();
        }


        private void StartListening(int port)
        {


            byte[] bytes = new Byte[1024 * 5000];
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, port);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipEnd);
                listener.Listen(100);

                //while (true)
                //{
                //allDone.Reset();
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                //allDone.WaitOne();

                //}
            }
            catch (Exception ex)
            {

            }

        }

        private void AcceptCallback(IAsyncResult ar)
        {

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                string[] str = new string[2];
                str = handler.RemoteEndPoint.ToString().Split(':');
                int bytesRead = handler.EndReceive(ar);
                int fileNameLen = BitConverter.ToInt32(state.buffer, 0);
                string fileName = Encoding.UTF8.GetString(state.buffer, 4, fileNameLen);
                        
            }
            catch (Exception ex)
            {

            }

        }

        public void sendRequest(string ip, int port, string _fileRequest)
        {
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.GetHostEntry(ip);
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // Create a TCP socket.
            Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint.
            client.Connect(ipEndPoint);

            // There is a text file test.txt located in the root directory.
            string fileName = _fileRequest; //my ip, my port + req file

            // Send file fileName to remote device
            Console.WriteLine("Sending {0} to the host.", fileName);
            client.Send(Encoding.ASCII.GetBytes(fileName));

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public void sendFile(string ip, int port, string _fileName)
        {
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.GetHostEntry(ip);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // Create a TCP socket.
            Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint.
            client.Connect(ipEndPoint);

            // There is a text file test.txt located in the root directory.
            string fileName = _fileName;

            // Send file fileName to remote device
            Console.WriteLine("Sending {0} to the host.", fileName);
            client.SendFile(fileName);

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

    }


    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;

        public const int BufferSize = 1024 * 5000;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
    }
}
