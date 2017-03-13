<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="p2pWeb.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <style type="text/css">
        * {
            font-family: Calibri;
            font-size: 14px;
        }

        .menuWrapper {
            position: fixed;
            top: 0;
            left: 0;
            width:100%;
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
            padding-left:30px;
            padding-top:30px;
        }




        /*********************************************/
        .statisticsWrapper {
            height: 200px;
            width:200px;
            float: left;
            margin-right: 20px;
        }

        .filesWrapper {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menuWrapper">
            <div class="menuItem" onclick="location.href='Registration.aspx';">
                Register
            </div>
            <div class="menuItem" onclick="location.href='ManageUsers.aspx';">
                Manage
            </div>
            <div class="menuItem active">
                Home
            </div>
        </div>
        <div class="pageBody">
            <div class="statisticsWrapper">

                <asp:Label ID="activeUsersLb" runat="server" Text="Active Users: "></asp:Label>
                <asp:Label ID="numberOfActiveUsersLb" runat="server">**</asp:Label>
                <br />

                <asp:Label ID="allUsersLb" runat="server" Text="All Users: "></asp:Label>
                <asp:Label ID="numberOfAllUserLb" runat="server">**</asp:Label>
                <br />

                <asp:Label ID="allFilesLb" runat="server" Text="All Files: "></asp:Label>
                <asp:Label ID="numberOfFilesLb" runat="server">**</asp:Label>
                <br />

            </div>

            <div class="filesWrapper">
                <asp:ListBox ID="filesLb" runat="server" Width="700" Height="350"></asp:ListBox><br />
                <asp:TextBox ID="searchTb" runat="server"></asp:TextBox>
                <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="searchBtn_Click" />
            </div>
        </div>
    </form>
</body>
</html>
