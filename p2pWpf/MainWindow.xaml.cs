using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using p2p.Entities;
using p2p.Entities.File;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using p2pWpf.p2pService;
using p2p.Entities.User;

namespace p2pWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string configContent;
        List<FileInfoDTO> userFiles;

        public MainWindow()
        {
            InitializeComponent();
            userFiles = new List<FileInfoDTO>();
            OnLoad();
        }

        /// <summary>
        /// on load check if Myconfix.xml exist and get ditaild from it
        /// </summary>
        private void OnLoad()
        {
            logoutBtn.Visibility = System.Windows.Visibility.Hidden;
            logoutBtn.IsEnabled = false;
            string dir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string xmlPath = (dir + @"\MyConfig.xml");
            configContent = File.ReadAllText(xmlPath);

            //if the config is not empty read data and update all the text boxes.
            if (configContent != "")
            {
                Type _type = typeof(UserLoginDTO);
                UserLoginDTO uld = (UserLoginDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(configContent, _type));
                if(uld == null)
                {
                    return;
                }
                userNameTb.Text = uld.UserName;
                passwordTb.Text = uld.Password;
                ipTb.Text = uld.Ip;
                portTb.Text = uld.Port.ToString();
                inputFilesDirectoryTb.Text = uld.DownloadPath;
                outputFilesDirectoryTb.Text = uld.UploadPath;
            }
        }

        /// <summary>
        /// login to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {   
            // check that all fields are valied       
            if (validateForm())
            {
                XmlDocument xmlDoc = new XmlDocument();
                string dir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string xmlPath = (dir + @"\MyConfig.xml");
                XmlSerializer wr = new XmlSerializer(typeof(UserLoginDTO));
                FileStream fs = File.OpenWrite(xmlPath);

                //read from the config all the files
                var files = Directory.GetFiles(inputFilesDirectoryTb.Text);

                foreach (var fPath in files)
                {
                    FileInfo fInfo = new FileInfo(fPath);
                    FileInfoDTO fid = new FileInfoDTO()
                    {
                        FileName = fInfo.Name.Replace(fInfo.Extension, ""),
                        FileType = fInfo.Extension,
                        FileSize = Math.Round((decimal)(fInfo.Length / 1000000), 2)
                    };

                    userFiles.Add(fid);
                }

                UserLoginDTO user = new UserLoginDTO()
                {
                    UserName = userNameTb.Text,
                    Password = passwordTb.Text,
                    Ip = ipTb.Text,
                    Port = int.Parse(portTb.Text),
                    DownloadPath = inputFilesDirectoryTb.Text,
                    UploadPath = outputFilesDirectoryTb.Text,
                    Files = userFiles
                };
                 // serealize the user object to be able transfer data
                wr.Serialize(fs, user);
                fs.Close();

                //call to server 
                //xmlDoc.InnerXml
                using (Service1Client client = new Service1Client())
                {
                    var result = client.loginUser(p2p.Utils.XmlFormatter.GetXMLFromObject(user));

                    //if the return value is OK open the window to download
                    if (result == "OK")
                    {
                        //hide the login btn and replace by logout btn
                        loginBtn.Visibility = System.Windows.Visibility.Hidden;
                        logoutBtn.Visibility = System.Windows.Visibility.Visible;
                        logoutBtn.IsEnabled = true;

                        DownloadWindow dw = new DownloadWindow()
                        {
                            UserName = userNameTb.Text,
                            Password = passwordTb.Text,
                            Parent = this
                        };
                         //hide main window and show the download window
                        this.Hide();
                        dw.Show();
                    }
                    else
                    {
                        MessageBox.Show(result + ": Login process failed");
                    }
                }
            }
        }

        //Check each textBox is not null of empty
        private bool validateForm()
        {
            if (IsNullOrEmpty("User name", userNameTb.Text))
            {
                return false;
            }
            if (IsNullOrEmpty("Password", passwordTb.Text))
            {
                return false;
            }
            if (IsNullOrEmpty("IP", ipTb.Text))
            {
                return false;
            }
            if (IsNullOrEmpty("Port", portTb.Text))
            {
                return false;
            }
            if (IsNullOrEmpty("Download directory path", inputFilesDirectoryTb.Text))
            {
                return false;
            }
            if (IsNullOrEmpty("Upload directory path", outputFilesDirectoryTb.Text))
            {
                return false;
            }
            return true;
        }

        //If textBox empty notify user
        public static bool IsNullOrEmpty(string property, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                MessageBox.Show(property + " field is requierd");
                return true;
            }
            return false;
        }

        //If user decide to signout change user to be disabled and delete all his files
        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Type _type = typeof(UserLoginDTO);
            UserLoginDTO uld = (UserLoginDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(configContent, _type));

            using (Service1Client client = new Service1Client())
            {
                var result = client.signoutUser(p2p.Utils.XmlFormatter.GetXMLFromObject(uld));
                if (result == "OK")
                {
                    MessageBox.Show("User signout succesfullty");
                }
                else
                {
                    MessageBox.Show(result+ ": User failed to signout");
                }
            }
        }
    }
}
