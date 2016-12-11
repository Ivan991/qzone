<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EQName.aspx.cs" Inherits="EQName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    修改空间名
    <br />
    新的空间名为:<asp:TextBox ID="newqname" runat="server" TextMode="SingleLine"></asp:TextBox>
    <br />
    <asp:Button ID="btn" runat="server" Text="修改" OnClick="btn_Click" />
   
</asp:Content>

