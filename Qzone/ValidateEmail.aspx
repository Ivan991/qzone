<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidateEmail.aspx.cs" Inherits="ValidateEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    请验证邮箱

        验证邮件已发送，该验证码将在五分钟内失效

        请输入邮箱中的验证码：

        <asp:TextBox ID="email" runat="server" TextMode="SingleLine"></asp:TextBox>


        <asp:Button ID="btnsubmit" runat="server" Text="提交" OnClick="btnsubmit_Click1" />
    </div>
    </form>
</body>
</html>
