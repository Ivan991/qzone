<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPwd.aspx.cs" Inherits="FindPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    重设密码

        用户名：<asp:TextBox ID="name" runat="server" TextMode="SingleLine"></asp:TextBox>
        邮箱:<asp:TextBox ID="email" runat="server" TextMode="SingleLine"></asp:TextBox>
        手机号:<asp:TextBox ID="tel" runat="server" TextMode="SingleLine"></asp:TextBox>
        <asp:Button ID="btnfpwd" runat="server" Text="重设密码" OnClick="btnfpwd_Click" />
        <asp:Button ID="back" runat="server" Text="返回" PostBackUrl="~/MyLogin.aspx" />
    </div>
    </form>
</body>
</html>
