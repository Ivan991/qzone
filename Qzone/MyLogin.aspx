<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyLogin.aspx.cs" Inherits="MyLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<script src="styles/jquery-1.4.1.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Qzone登录界面<br />
        <br />
        <br />
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 用户名：<asp:TextBox ID="name" runat="server" TextMode="SingleLine"></asp:TextBox>

        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        密码:&nbsp;&nbsp; <asp:TextBox ID="pwd" runat="server" TextMode="Password"></asp:TextBox>

        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        验证码：<img src="ValidateCode.aspx" id="img" onclick="f_refreshtype()" width="50px"/>

        <asp:TextBox ID="code" runat="server" TextMode="SingleLine"></asp:TextBox>

        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <asp:Button ID="btnlogin" runat="server" Text="登录" OnClick="btnlogin_Click" />

        &nbsp;

        <asp:Button ID="btnregister" runat="server" Text="注册" PostBackUrl="~/Register.aspx"/>

        &nbsp;&nbsp;

        <asp:Button ID="btnfpwd" runat="server" Text="忘记密码" PostBackUrl="~/ForgetPwd.aspx" />

    </div>
    </form>
</body>

            <script>
            function f_refreshtype()
            {
             var Image1 = document.getElementById("img");
             if (Image1 != null)
             {
                 Image1.src = Image1.src + "?";
             }
         }
        </script>

</html>
