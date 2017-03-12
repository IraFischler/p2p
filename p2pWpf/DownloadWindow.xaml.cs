using Microsoft.Win32;
using p2p.Entities.File;
using p2p.Utils;
using p2pWpf.p2pService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
        const int PORT = 8005;
        TcpListener listener;
        TcpClient client;
        NetworkStream ns;

        public DownloadWindow()
        {
            InitializeComponent();
            listener = TcpListener.Create(PORT);

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

                //DownloadFile df = new DownloadFile()
                //{
                //    Ip = res.Ip,
                //    Port = res.Port,
                //    //Request = request
                //};
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

        private async  void fileTransferring(string fileName, string ip, int port)
        {
            // Listen           
            listener.Start();
            client = await listener.AcceptTcpClientAsync();
            ns = client.GetStream();

            // Get file info
            long fileLength;
            string fileName;
            {
                byte[] fileNameBytes;
                byte[] fileNameLengthBytes = new byte[4]; //int32
                byte[] fileLengthBytes = new byte[8]; //int64

                await ns.ReadAsync(fileLengthBytes, 0, 8); // int64
                await ns.ReadAsync(fileNameLengthBytes, 0, 4); // int32
                fileNameBytes = new byte[BitConverter.ToInt32(fileNameLengthBytes, 0)];
                await ns.ReadAsync(fileNameBytes, 0, fileNameBytes.Length);

                fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
                fileName = ASCIIEncoding.ASCII.GetString(fileNameBytes);
            }

            // Get permission
            if (MessageBox.Show(string.Format("Requesting permission to receive file:\r\n\r\n{0}\r\n{1} bytes long", fileName, fileLength), "", MessageBoxButton.YesNo).Equals(!DialogResult.Equals("OK")))
            {
                return;
            }

            // Set save location
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CreatePrompt = false;
            sfd.OverwritePrompt = true;
            sfd.FileName = fileName;
            if (sfd.ShowDialog() != DialogResult.Equals("OK"))
            {
                ns.WriteByte(0); // Permission denied
                return;
            }
            ns.WriteByte(1); // Permission grantedd
            FileStream fileStream = File.Open(sfd.FileName, FileMode.Create);



            int read;
            int totalRead = 0;
            byte[] buffer = new byte[32 * 1024]; // 32k chunks
            while ((read = await ns.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                totalRead += read;

            }

            fileStream.Dispose();
            client.Close();
            MessageBox.Show("File successfully received");

        }
    }
}
