<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="p2pWeb.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Home.aspx">Back</a>
            <asp:TextBox placeholder="Enter User Name" ID="userNameTb" runat="server"></asp:TextBox>
            <input type="password" placeholder="Enter Password" id="passwordTb" runat="server"></input>
            <asp:TextBox placeholder="Enter Email" ID="emailTb" runat="server"></asp:TextBox>

            <asp:Label ID="validationLb" runat="server"> </asp:Label>
            <asp:Button ID="registerBtn" runat="server" Text="Register" OnClick="registerBtn_Click" />
        </div>
    </form>
</body>
</html>
