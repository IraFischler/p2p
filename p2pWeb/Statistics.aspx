<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="p2pWeb.Statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="Home.aspx">Back</a>
        <asp:Label ID="activeUsersLb" runat="server" Text="Active Users "></asp:Label>
        <asp:Label ID="numberOfActiveUsersLb" runat="server"></asp:Label>
        <asp:Label ID="allUsersLb" runat="server" Text="All Users "></asp:Label>
        <asp:Label ID="numberOfAllUserLb" runat="server"></asp:Label>
        <asp:Label ID="allFilesLb" runat="server" Text="All Files "></asp:Label>
        <asp:Label ID="numberOfFilesLb" runat="server"></asp:Label>
        
        <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="searchBtn_Click"/>
        <asp:ListBox ID="filesLb" runat="server" OnSelectedIndexChanged="usersLb_SelectedIndexChanged"></asp:ListBox><br />
    </div>
    </form>
</body>
</html>
