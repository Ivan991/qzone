<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EPassword.aspx.cs" Inherits="User_EPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    修改密码
    <br />
    您的旧密码为:<asp:TextBox ID="oldpwd" runat="server"  TextMode="Password"></asp:TextBox>
    <br />
    您的新密码为：<asp:TextBox ID="newpwd" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    请再次输入您的新密码：<asp:TextBox ID="newpwd1" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <asp:Button ID="btneditor" Text="修改密码" runat="server" OnClick="btneditor_Click" />
    <asp:Button ID="btnback" Text="返回" runat="server" OnClick="btnback_Click" />
</asp:Content>

