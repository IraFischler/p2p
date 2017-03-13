<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="p2pWeb.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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
            width: 60%;
            margin: auto;
            height: 80px;
            border: 1px solid lightgray;
            margin-top: 120px;
            padding-left: 30px;
            padding-top: 30px;
        }




        /*********************************************/

        .userRegisterWrapper {
            margin-top: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menuWrapper">
            <div class="menuItem active">
                Register
            </div>
            <div class="menuItem" onclick="location.href='ManageUsers.aspx';">
                Manage
            </div>
            <div class="menuItem " onclick="location.href='Home.aspx';">
                Back
            </div>
        </div>
        <div class="pageBody">
            <asp:TextBox placeholder="Enter User Name" ID="userNameTb" runat="server"></asp:TextBox>
            <input type="password" placeholder="Enter Password" id="passwordTb" runat="server"></input>
            <asp:TextBox placeholder="Enter Email" ID="emailTb" runat="server"></asp:TextBox>

            <asp:Button ID="registerBtn" runat="server" Text="Register" OnClick="registerBtn_Click" />
            <br />
            <asp:Label ID="validationLb" runat="server"> </asp:Label>
        </div>
    </form>
</body>
</html>
