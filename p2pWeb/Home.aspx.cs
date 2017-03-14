using p2p.Entities.File;
using p2pWeb.p2pService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace p2pWeb
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateListBox(getFiles());
            getStatistics();
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
            var result = getFiles().Where(f => f.FileName == searchTb.Text);
            if (result.Count() > 0)
            {
                filesLb.Items.Clear();
                populateListBox(result.ToList());
            }
            else
            {
                validationLb.Text = "No Files Found";
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