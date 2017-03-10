using p2p.Entities.User;
using p2pWeb.p2pService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace p2pWeb
{
    public partial class ManageUsers : System.Web.UI.Page
    {

        UserInfoDTO u = new UserInfoDTO()
        {
            UserName = "orin",
            Password = "111",
            Email = "orin@gmail.com"
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            getUsers();
        }

        private void getUsers()
        {
            using (Service1Client client = new Service1Client())
            {
                var result = client.getUsersList();

                if (result.SearchResult == "OK" && result.Users.Count() > 0)
                {
                    foreach (var item in result.Users)
                    {
                        usersLb.Items.Add(item.ToString());
                    }
                }
                else
                {
                    validationLb.Text = "There are no users";
                }
            }
        }

       
        private bool validateUser(UserInfoDTO u)
        {
            
            if (userNameTb.Text == "" || passwordTb.Text == "" || emailTb.Text == "")
            {
                validationLb.Text = "Fill  all fields";
                return false;
            }
            if(userNameTb.Text == u.UserName && passwordTb.Text == u.Password && emailTb.Text == u.Email)
            {
                return false;
            }
            else
            {
                validationLb.Text = "";
                return true;
            }
        }

        private UserInfoDTO getSelectedUser()
        {
            if (usersLb.SelectedValue == null)
            {
                return null;
            }
            else
            {
                return u; //(UserInfoDTO)usersLv.SelectedValue;
            }
        }

        protected void usersLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = getSelectedUser();
            if (user != null)
            {
                if (user.Enabled)
                {
                    enableDisableBtn.Text = "Disable";
                }
                else
                {
                    enableDisableBtn.Text = "Enable";
                }

                deleteBtn.Enabled = true;
                enableDisableBtn.Enabled = true;
                updateBtn.Enabled = true;

                userNameTb.Text = user.UserName;
                passwordTb.Text = user.Password;
                emailTb.Text = user.Email;
            }
            else
            {
                deleteBtn.Enabled = false;
                enableDisableBtn.Enabled = false;
                updateBtn.Enabled = false;

                userNameTb.Text = "";
                passwordTb.Text = "";
                emailTb.Text = "";
                enableDisableBtn.Text = "Enable/Disable";
            }
        }

        protected void enableDisableBtn_Click(object sender, EventArgs e)
        {
            var user = getSelectedUser();
            user.Enabled = !user.Enabled;
            using(p2pService.Service1Client client  = new p2pService.Service1Client())
            {
                var result = client.enableDisableUser(user);
                if(result == "OK")
                {
                    validationLb.Text = "User has been succesfully " + enableDisableBtn.Text ;
                }                
            }

            usersLb.Items.Clear();
            getUsers();
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            var user = getSelectedUser();
            using (Service1Client client = new Service1Client())
            {
                var result = client.deleteUser(user);

                if (result == "OK")
                {
                    validationLb.Text = "User has been succesfully removed ";
                    usersLb.Items.Clear();
                    getUsers();
                }
                else
                {
                    validationLb.Text = "There are no users";
                }
            }
            //delete user: service+dal+remove row rfom list view + remove user files if exist           
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            //copy user object ditailes into text boxes to be able to update. 
            var user = getSelectedUser();
            userNameTb.Text = user.UserName;
            passwordTb.Text = user.Password;
            emailTb.Text = user.Email;
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            var user = getSelectedUser();
            if (validateUser(user))
            {
                using (Service1Client client = new Service1Client())
                {
                    var result = client.updateUser(user);

                    if (result== "OK" )
                    {
                        validationLb.Text = "User has been succesfully updated ";
                        usersLb.Items.Clear();
                        getUsers();
                    }
                   
                }
                //delete user: service+dal+remove row rfom list view + remove user files if exist
            }
        }
    }
}