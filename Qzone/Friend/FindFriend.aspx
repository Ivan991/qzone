<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FindFriend.aspx.cs" Inherits="NewFriend_FindFriend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     查找好友

    用户名:<asp:TextBox ID="name" runat="server" ></asp:TextBox>
    <br />
    <asp:Button ID="find" runat="server" Text="查找好友" OnClick="find_Click" />
</asp:Content>

