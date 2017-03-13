<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="p2pWeb.ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Users</title>
    <style type="text/css">
        * {
            font-family: Calibri;
            font-size: 14px;
        }

        .menuWrapper {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            z-index: 999;
            height: 23px;
            display: flex;
        }

        .menuItem {
            cursor: pointer;
            color: blue;
            background-color: white;
            height: 30px;
            width: 33%;
            text-align: center;
            padding-top: 10px;
            border: 1px solid blue;
        }

        .active {
            background-color: blue;
            color: white;
            font-weight: bold;
        }

        .pageBody {
            width: 80%;
            margin: auto;
            height: 500px;
            border: 1px solid lightgray;
            margin-top: 60px;
            padding-left: 30px;
            padding-top: 30px;
        }




        /*********************************************/
        .userUpdateWrapper {
            margin-top:30px;
        }

        .usersListBox{
             margin-top:30px;
        }

        .usersWrapper {
             
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menuWrapper">
            <div class="menuItem" onclick="location.href='Registration.aspx';">
                Register
            </div>
            <div class="menuItem active">
                Manage
            </div>
            <div class="menuItem" onclick="location.href='Home.aspx';">
                Back
            </div>
        </div>
        <div class="pageBody">
            <div class="usersWrapper">
                <asp:Button ID="enableDisableBtn" runat="server" Text="Enable\Disable" Enabled="false" OnClick="enableDisableBtn_Click" />
                <asp:Button ID="deleteBtn" runat="server" Text="Delete" Enabled="false" OnClick="deleteBtn_Click" />
                <asp:Button ID="updateBtn" Visible="false" runat="server" Text="Update" Enabled="false" OnClick="updateBtn_Click" /><br />
                <asp:ListBox ID="usersLb" CssClass="usersListBox" runat="server" Width="700" Height="350" AutoPostBack="true" OnSelectedIndexChanged="usersLb_SelectedIndexChanged"></asp:ListBox><br />
            </div>
            <div class="userUpdateWrapper">
                <asp:TextBox placeholder="Enter Email" ID="emailTb" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="Enter User Name" ID="userNameTb" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="Enter Password" ID="passwordTb" runat="server"></asp:TextBox>
                <asp:Button ID="saveBtn" runat="server" Text="Save" Enabled="false" OnClick="saveBtn_Click" />
            </div>
            <br />
            <asp:Label ID="validationLb" runat="server"> </asp:Label>
        </div>
    </form>
</body>
</html>
