<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendRaw.aspx.cs" Inherits="WebPushNotifications.SendRaw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            Uri:<br />
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
            Raw notification:<br />
            <asp:TextBox ID="TextBoxRawNotification" runat="server" Width="300px"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="ButtonSendRaw" runat="server" OnClick="ButtonSendRaw_Click"
                Text="Send Raw Notification" />
            <br />
            <br />
            Response:<br />
            <asp:TextBox ID="TextBoxResponse" runat="server" Height="78px" Width="199px" Wrap="true"></asp:TextBox>
        </div>
    </form>
</body>
</html>
