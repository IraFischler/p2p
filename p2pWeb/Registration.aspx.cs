using p2p.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace p2pWeb
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool validateUser()
        {
            if(userNameTb.Text == "" || passwordTb.Value == "" || emailTb.Text == "")
            {
                validationLb.Text = "Fill  all fields";
                return false;
            }
            else
            {
                validationLb.Text = "";
                return true;
            }
        }

        private void registerUser()
        {
            if (validateUser())
            {
                using(p2pService.Service1Client client = new p2pService.Service1Client())
                {
                    UserRegisterDTO urd = new UserRegisterDTO()
                    {
                        UserName = userNameTb.Text,
                        Password = passwordTb.Value,
                        Email = emailTb.Text
                    };
                    var result = client.registerUser(urd);
                    if(result == "OK")
                    {
                        validationLb.Text = "User was created succesfully";
                        userNameTb.Text = "";
                        passwordTb.Value = "";
                        emailTb.Text = "";
                    }
                    else
                    {
                        validationLb.Text = result;
                    }
                }
            }
        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            registerUser();
        }
    }
}