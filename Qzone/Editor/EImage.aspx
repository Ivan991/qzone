<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EImage.aspx.cs" Inherits="EImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    修改空间头像
    <br />
    <asp:Image ID="image" runat="server" />
    <br />
    <asp:FileUpload ID="upload" runat="server" />
    <br />
    <asp:Button ID="btnupload" runat="server" Text="修改" OnClick="btnupload_Click" />

</asp:Content>

