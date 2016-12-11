<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AlbumEditor.aspx.cs" Inherits="Photos_AlbumEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    编辑相册
    <br />
    相册名字：<asp:TextBox ID="text" runat="server"></asp:TextBox>
    <br />
    相册描述：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    相册状态:<asp:DropDownList ID="state" runat="server" ><asp:ListItem Value="1">所有人可见</asp:ListItem ><asp:ListItem Value="0">仅自己可见</asp:ListItem></asp:DropDownList>
    <br />
    相册封面:<asp:Image ID="image" runat="server" />
    <br />
    <asp:Button ID="editorimage" runat="server" Text="修改相册封面" OnClick="editorimage_Click" />
    <br />
   <asp:Button ID="editor" runat="server" Text="修改" OnClick="editor_Click" />
</asp:Content>

