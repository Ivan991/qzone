<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

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
    注册界面<br />
        用户名： <asp:TextBox ID="name" runat="server"  TextMode="SingleLine"></asp:TextBox>
        <br />
        密码：<asp:TextBox ID="pwd" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        真实姓名<asp:TextBox ID="usersname" runat="server" TextMode="SingleLine"></asp:TextBox>
        <br />
        性别：<asp:DropDownList ID="sex" runat="server" ><asp:ListItem Selected="True">男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList>
        <br />
        生日:<asp:TextBox ID="birth" runat="server" TextMode="SingleLine"></asp:TextBox>
        <br />
        手机号：<asp:TextBox ID="tel" runat="server" ></asp:TextBox>
        <br />
        血型:<asp:DropDownList ID="blood" runat="server" ><asp:ListItem>A型</asp:ListItem><asp:ListItem>B型</asp:ListItem><asp:ListItem>O型</asp:ListItem></asp:DropDownList>
        <br />
        邮箱:<asp:TextBox ID="email" runat="server" TextMode="SingleLine"></asp:TextBox>
        <br />
        空间名：<asp:TextBox ID="qzonename" runat="server" TextMode="SingleLine"></asp:TextBox>
        <br />
        空间头像：<asp:FileUpload ID="upload" runat="server" />
        <br />

        验证码:
        
       <img src="ValidateCode.aspx" id="img" onclick="f_refreshtype()" />

        <asp:TextBox ID="validate" runat="server" TextMode="SingleLine"></asp:TextBox>

        <asp:Button ID="register" runat="server" Text="注册" OnClick="register_Click" />

        <asp:Button ID="login" runat="server" Text="登录" PostBackUrl="~/MyLogin.aspx" />
        
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
