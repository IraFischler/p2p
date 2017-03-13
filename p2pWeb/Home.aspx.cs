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
            //getFiles();
            //getStatistics();
        }


        private void getFiles()
        {
            using (Service1Client client = new Service1Client())
            {
                var result = client.getFilesList();

                if (result.SearchResult == "OK" && result.Files.Count() > 0)
                {
                    foreach (var item in result.Files)
                    {
                        filesLb.Items.Add(item.ToString());
                    }
                }
            }
        }

        public void searchBtn_Click(object sender, EventArgs e)
        {
            //??????????????????what shoul i do with this button?????
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