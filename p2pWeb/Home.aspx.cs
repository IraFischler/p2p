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
            getFiles();
            getStatistics();


        }

        private void getFiles()
        {

            loadFilesData();
        }

        private void loadFilesData()
        {
            //as same as in the download window
        }

        private void getStatistics()
        {


        }


        
    }
}