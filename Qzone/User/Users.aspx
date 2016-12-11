<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Qzone_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    个人档
    <br />

            用户名：<asp:Label ID="label" runat="server"></asp:Label>
            <br />
            真实姓名：<asp:Label ID="label1" runat="server"></asp:Label>
            <br />
            性别：<asp:Label ID="label2" runat="server"></asp:Label>
            <br />
            邮箱：<asp:Label ID="label3" runat="server"></asp:Label>
            <br />
            电话：<asp:Label ID="label4" runat="server"></asp:Label>
            <br />
            生日:<asp:Label ID="label5" runat="server"></asp:Label>
            <br />
            血型：<asp:Label ID="label6" runat="server"></asp:Label>
            <br />
    <asp:Button ID="btneditor" Text="修改资料" runat="server"  OnClick="btneditor_Click" />
    &nbsp;
    <asp:Button ID="btnepwd" Text="修改密码" runat="server"  PostBackUrl="~/User/EPassword.aspx" />

</asp:Content>

