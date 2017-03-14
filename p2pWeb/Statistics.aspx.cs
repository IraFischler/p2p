using p2p.Entities.File;
using p2pWeb.p2pService;
using p2p.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace p2pWeb
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateListBox(getFiles());
        }

        private List<FileInfoDTO> getFiles()
        {
            using (Service1Client client = new Service1Client())
            {
                var result = client.getFilesList();

                if (result.SearchResult == "OK" && result.Files.Count() > 0)
                {
                    foreach (var item in result.Files)
                    {
                        return result.Files.ToList();
                    }
                }
                else
                {
                    validationLb.Text = "No Files Found";
                }
            }
            return new List<FileInfoDTO>();
        }

        private void populateListBox(List<FileInfoDTO> files)
        {
            foreach (var item in files)
            {
                filesLb.Items.Add(item.ToString());
            }
        }

        private object getSelectedFile()
        {
            if (filesLb.SelectedValue == null)
            {
                return null;
            }
            else
            {
                var res = getFiles().Find(u => { return u.ToString() == filesLb.SelectedValue; });
                return res;
            }
        }

        public void searchBtn_Click(object sender, EventArgs e)
        {
            FileInfoDTO f = new FileInfoDTO()
            {
                FileName = searchBtn.Text,
            };
            using (Service1Client client = new Service1Client())
            {
                var result = client.searchFiles(p2p.Utils.XmlFormatter.GetXMLFromObject(f));

                if (result.SearchResult == "OK" && result.Files.Count() > 0)
                {
                    filesLb.Items.Clear();
                    foreach (var item in result.Files)
                    {
                        populateListBox(result.Files.ToList());
                    }
                }
                else
                {
                    validationLb.Text = "No Files Found";
                }
            }
        }
        public void getStatistics()
        {
            using (Service1Client client = new Service1Client())
            {
                var res = client.getStatistics();

                numberOfActiveUsersLb.Text = res.NumOfActiveUsers.ToString();
                numberOfAllUserLb.Text = res.NumOfUsers.ToString();
                numberOfFilesLb.Text = res.NumOfFiles.ToString();
            }

        }
    }
}