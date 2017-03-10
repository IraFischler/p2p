<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="p2pWeb.ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="Home.aspx">Back</a>
        <asp:Button ID="enableDisableBtn" runat="server" Text="Enable\Disable" Enabled ="false" OnClick="enableDisableBtn_Click" />
        <asp:Button ID="deleteBtn" runat="server" Text="Delete" Enabled ="false" OnClick="deleteBtn_Click"/>
        <asp:Button ID="updateBtn" runat="server" Text="Update" Enabled ="false" OnClick="updateBtn_Click"/><br />
        
        <asp:ListBox ID="usersLb" runat="server" OnSelectedIndexChanged="usersLb_SelectedIndexChanged"></asp:ListBox><br />
       
        <asp:TextBox  placeholder="Enter Email" ID ="emailTb" runat ="server"></asp:TextBox>
        <asp:TextBox  placeholder="Enter User Name" ID ="userNameTb" runat ="server"></asp:TextBox>
        <asp:TextBox  placeholder="Enter Password" ID ="passwordTb" runat ="server"></asp:TextBox>
        <asp:Button ID="saveBtn" runat="server" Text="Save" Enabled ="false" OnClick="saveBtn_Click"/>
        <asp:Label ID="validationLb" runat="server"> </asp:Label>
    </div>
    </form>
</body>
</html>
