<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendBadge.aspx.cs" Inherits="WebPushNotifications.SendBadge" %>

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
            Number:<br />
            <asp:TextBox ID="TextBoxNumber" runat="server" Width="300px"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="ButtonSendBadge" runat="server" OnClick="ButtonSendBadge_Click"
                Text="Send Badge Notification" />
            <br />
            <br />
            Response:<br />
            <asp:TextBox ID="TextBoxResponse" runat="server" Height="78px" Width="199px" Wrap="true"></asp:TextBox>
        </div>
    </form>
</body>
</html>
