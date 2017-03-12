using Microsoft.Win32;
using p2p.Entities.File;
using p2p.Utils;
using p2pWpf.p2pService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace p2pWpf
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MainWindow Parent { get; set; }
        int PORT = 8005;
        int IP = 8005;
        TcpListener listener;
        TcpClient client;
        NetworkStream ns;
        Socket clientsock;

        public DownloadWindow()
        {
            InitializeComponent();
            listener = TcpListener.Create(PORT);
            Connect();
            listenclient();

        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select file");
                return;
            }

            var fr = (FileInfoDTO)resultsListBox.SelectedItem;
            FileRequestDTO request = new FileRequestDTO()
            {
                FileName = fr.FileName,
                FileType = fr.FileType,
                //FileSize = fr.FileSize,
                UserName = UserName,
                Password = Password,
                //Id = f.Id
            };

            using (Service1Client client = new Service1Client())
            {
                var res = client.downloadRequest(request);
                downloadFromServer(clientsock, request.FileName, 255);
                //out

            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            FileSearchDTO file = new FileSearchDTO()
            {
                UserName = UserName,
                Password = Password,
                SearchText = fileNameTb.Text
            };

            using (Service1Client client = new Service1Client())
            {
                var result = client.searchFiles(p2p.Utils.XmlFormatter.GetXMLFromObject(file));

                if (result.SearchResult == "OK" && result.Files.Count() > 0)
                {
                    resultsListBox.Items.Clear();
                    foreach (var item in result.Files)
                    {
                        resultsListBox.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("File Not Found");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                client.Close();
                listener.Stop();
            }
            catch (Exception)
            {
            }

            Parent.Show();
        }

        private void Connect()
        {
            try
            {
                Socket serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serversocket.Blocking = true;

                IPHostEntry IPHost = Dns.Resolve(IP.ToString()); //Dns.Resolve(textBox1.Text);
                string[] aliases = IPHost.Aliases;
                IPAddress[] addr = IPHost.AddressList;

                IPEndPoint ipepServer = new IPEndPoint(addr[0], PORT);
                serversocket.Connect(ipepServer);
                Socket clientsock = serversocket;

                Thread MainThread = new Thread(new ThreadStart(listenclient));
                MainThread.Start();
                // MessageBox.Show("Connected successfully", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
            catch (Exception eee)
            {
                // MessageBox.Show("Socket Connect Error.\n\n" + eee.Message + "\nPossible Cause: Server Already running. Check the tasklist for running processes", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void listenclient()
        {
            Socket sock = clientsock;
            string cmd = " eliran";
            byte[] sender = System.Text.Encoding.ASCII.GetBytes("CLIENT " + cmd);
            sock.Send(sender, sender.Length, 0);

            while (sock != null)
            {
                //cmd = "";
                byte[] recs = new byte[32767];
                int rcount = sock.Receive(recs, recs.Length, 0);
                string clientmessage = System.Text.Encoding.ASCII.GetString(recs);
                clientmessage = clientmessage.Substring(0, rcount);

                string smk = clientmessage;


                var cmdList = clientmessage.Split(' ');
                string execmd = cmdList[0];

                sender = null;
                sender = new Byte[32767];

                string parm1 = "";


                if (execmd == "CommitRequest")
                {
                    for (int i = 3; i < cmdList.Length - 1; i++)
                    {
                        if (i % 2 == 1)
                        {
                            // sendComment("downloadFile " + cmdList[i]); // after receiving this, server will upload the file requested
                            downloadFromServer(sock, cmdList[i], long.Parse(cmdList[i + 1]));
                        }

                    }
                    continue;
                }
            }
        }


        private void downloadFromServer(Socket s, string fileN, long fileS)
        {
            Socket sock = s;
            string rootDir;
            rootDir = @"D:/a";//@"D:\Client Data" + "\\" + userID + "\\" + mName; //@"D:\Client Data" + "\\" + userID + "\\" + mName;
            Directory.CreateDirectory(rootDir);
            System.IO.FileStream fout = new System.IO.FileStream(rootDir + "\\" + fileN, FileMode.Create, FileAccess.Write);
            NetworkStream nfs = new NetworkStream(sock);
            long size = fileS;
            long rby = 0;
            try
            {
                while (rby < size)
                {
                    byte[] buffer = new byte[1024];
                    int i = nfs.Read(buffer, 0, buffer.Length);
                    fout.Write(buffer, 0, (int)i);
                    rby = rby + i;
                }
                fout.Close();
            }
            catch (Exception ed)
            {
                Console.WriteLine("A Exception occured in file transfer" + ed.ToString());
                MessageBox.Show(ed.Message);
            }
        }

    }
}