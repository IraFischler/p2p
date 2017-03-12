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
        TCPHandler tcpH;
        int PORT = 8005;
      

        public DownloadWindow()
        {
            InitializeComponent();
            tcpH = new TCPHandler(PORT);
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
                tcpH.sendRequest(res.Ip, res.Port, request.FileName+request.FileType);
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
                tcpH.stopListening();
            }
            catch (Exception)
            {
            }

            Parent.Show();
        }

    }
}