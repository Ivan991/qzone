<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoEditor.aspx.cs" Inherits="Photos_PhotoEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    编辑照片描述
    <br />
    <asp:TextBox ID="describe" runat="server" ></asp:TextBox>
    <br />
    <asp:Button ID="btneditor" runat="server" Text="修改" OnClick="btneditor_Click" />
    &nbsp;
    <asp:Button ID="btnback" runat="server" Text="返回"  OnClick="btnback_Click" />
</asp:Content>

