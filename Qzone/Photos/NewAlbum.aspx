<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewAlbum.aspx.cs" Inherits="Photos_NewAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    相册名字：<asp:TextBox ID="albumname" runat="server"></asp:TextBox>
    <br />
    相册封面:<asp:FileUpload  ID="upload" runat="server" />
    <br />
    相册描述:<asp:TextBox ID="describe" TextMode="MultiLine" runat="server" ></asp:TextBox>
    <br />
    相册权限:<asp:DropDownList ID="state" runat="server" >
        <asp:ListItem Selected="True" Value="1">所有好友可见</asp:ListItem >
        <asp:ListItem Value="0">仅自己可见</asp:ListItem></asp:DropDownList>
    <br />
    <asp:LinkButton ID="newc" runat="server" Visible="false" Text="现在没有分类请新建分类" OnClick="newc_Click"></asp:LinkButton>
     <asp:DropDownList ID="category" runat="server" ></asp:DropDownList>
    <asp:Button ID="btnupload" runat="server" Text="新建相册" OnClick="btnupload_Click"/>
    &nbsp;
    <asp:Button ID="btnback" runat="server" Text="返回"  OnClick="btnback_Click" />
</asp:Content>

