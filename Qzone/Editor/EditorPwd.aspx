<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditorPwd.aspx.cs" Inherits="EditorPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    用户<asp:Label ID="name" runat="server" ></asp:Label>

        请输入您的新密码:<asp:TextBox ID="newpwd" runat="server" TextMode="Password"></asp:TextBox>
        请再次输入您的新密码<asp:TextBox ID="newpwd1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:CompareValidator ID="compare" runat="server" ControlToCompare="newpwd" ControlToValidate="newpwd1" ErrorMessage="两次输入的密码必须一致" Display="Dynamic"></asp:CompareValidator>
        <asp:Button ID="btn" runat="server" Text="修改" OnClick="btn_Click" />
        <asp:Button ID="back" runat="server" Text="登录页" OnClick="back_Click" />S
     </div>
    </form>
</body>
</html>
