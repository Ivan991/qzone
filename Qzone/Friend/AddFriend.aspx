<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddFriend.aspx.cs" Inherits="NewFriend_AddFriend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     用户名：<asp:LinkButton ID="linkbutton" runat="server" OnClick="linkbutton_Click"></asp:LinkButton>
    <br />
    空间名;<asp:LinkButton ID="linkbutton1" runat="server"  OnClick="linkbutton1_Click"></asp:LinkButton>
    <br />
    空间头像：<asp:Image ID="image" runat="server" />
    <br />
    状态:<asp:Label ID="lable" runat="server"></asp:Label>

    <asp:Button ID="btnadd" runat="server" Text="添加好友" OnClick="btnadd_Click" />

</asp:Content>

