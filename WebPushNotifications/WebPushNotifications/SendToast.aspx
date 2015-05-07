<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendToast.aspx.cs" Inherits="WebPushNotifications.SendToast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
		Uri:</div>
    <asp:TextBox ID="TextBoxUri" runat="server" Width="666px"></asp:TextBox>
    <br />
    <br />
        Package SID:<br />
    <asp:TextBox ID="TextBoxPackageSID" runat="server" Width="666px"></asp:TextBox>
    <br />
    <br />
    Client secret:<br />
    <asp:TextBox ID="TextBoxClientSecret" runat="server" Width="666px"></asp:TextBox>
    <br />
    <br />
	Title:<br />
    <asp:TextBox ID="TextBoxTitle" runat="server" Width="300px"></asp:TextBox>
    <br />
    <br />
    Subtitle:<br />
    <asp:TextBox ID="TextBoxSubTitle" runat="server" Width="300px"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Button ID="ButtonSendToast" runat="server" OnClick="ButtonSendToast_Click" 
        Text="Send Toast Notification" />
    <br />
    <br />
    Response:<br />
    <asp:TextBox ID="TextBoxResponse" runat="server" Height="78px" Width="199px" Wrap="true"></asp:TextBox>
    </form>
</body>
</html>
