using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;


namespace p2pWpf
{
    /// <summary>
    /// transffer files between users
    /// </summary>
            
    class TCPHandler
    {
        Socket listener;
        static ManualResetEvent allDone = new ManualResetEvent(false);
        int PORT = 8005;
        string reqFileName;
        Stopwatch stopwatch = new Stopwatch();
        
        public TimeSpan total { get; set; }

        /// <summary>
        /// the constractor activate the listener 
        /// </summary>
        /// <param name="port"></param>
        public TCPHandler(int port)
        {
            StartListening(port);
        }
        public TCPHandler()
        {
            
        }

        /// <summary>
        /// stops the listener
        /// </summary>
        public void stopListening()
        {
            listener.Close();
        }

        /// <summary>
        /// start listening by placing a socket in a listening state
        /// </summary>
        /// <param name="port">default port 8005</param>
        private void StartListening(int port)
        {
            byte[] bytes = new Byte[1024 * 5000];
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, port);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipEnd);
                listener.Listen(100);
                //Beging a asynchronous operation to accept an incomming connection attempt
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            }
            catch (Exception ex){}
        }

        /// <summary>
        /// accepts the connection and create new asynchronous callback
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
        }

        /// <summary>
        /// sending the file 
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                string[] str = new string[2];
                str = handler.RemoteEndPoint.ToString().Split(':');
                int bytesRead = handler.EndReceive(ar);
                string content = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);

                string[] req = content.Split(':');

                if (req != null && req.Length == 2 && req[0] == "sendFile")
                {
                    sendFile(str[0], PORT, req[1]);
                }
                else
                {
                    System.IO.File.WriteAllText(@"C:\a\" + reqFileName, content);

                    //stop timer
                    stopwatch.Stop();                   
                }
            }
            catch (Exception ex){}
        }

        /// <summary>
        /// sends requested for the file
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="_fileRequest">name of the file</param>
        public void sendRequest(string ip, int port, string _fileRequest)
        {
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.GetHostEntry(ip); 
            IPAddress ipAddr;
            if (ipHost.AddressList.Length == 2)
            {
                ipAddr = ipHost.AddressList[1];
            }
            else {
                ipAddr = ipHost.AddressList[0];
            }

            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, PORT);

            // Create a TCP socket.
            Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint.
            client.Connect(ipEndPoint);

            // There is a text file test.txt located in the root directory.
            string fileName = _fileRequest; //my ip, my port + req file

            //start timer
            stopwatch.Start();

            // Send file fileName to remote device
            Console.WriteLine("Sending {0} to the host.", fileName);
            reqFileName = fileName;
            client.Send(Encoding.ASCII.GetBytes("sendFile:" + fileName));

            total = stopwatch.Elapsed;
                     
            // Release the socket and close the client
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public void sendFile(string ip, int port, string _fileName)
        {
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.GetHostEntry(ip);
            IPAddress ipAddr;
            if (ipHost.AddressList.Length == 2)
            {
                ipAddr = ipHost.AddressList[1];
            }
            else
            {
                ipAddr = ipHost.AddressList[0];
            }
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
            client.SendFile(@"c:\a\" + fileName);

            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }      
    }

    /// <summary>
    /// craete a buffer to be able to transfer the file
    /// </summary>
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;

        public const int BufferSize = 1024 * 5000;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
    }
}
