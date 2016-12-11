<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EUser.aspx.cs" Inherits="EUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    个人档
    <br />

            用户名：<asp:TextBox ID="textbox"  runat="server"></asp:TextBox>
            <br />
            真实姓名：<asp:TextBox ID="textbox1"  runat="server"></asp:TextBox>
            <br />
            性别：<asp:DropDownList ID="textbox2" runat="server" ><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList> 
            <br />
            邮箱：<asp:TextBox ID="textbox3"  runat="server"></asp:TextBox>
            <br />
            电话：<asp:TextBox ID="textbox4"  runat="server"></asp:TextBox>
            <br />
            生日:<asp:TextBox ID="textbox5"  runat="server"></asp:TextBox>
            <br />
            血型：<asp:TextBox ID="textbox6"  runat="server"></asp:TextBox>
            <br />
    <asp:Button ID="btneditor" runat="server" Text="修改"  OnClick="btneditor_Click"/>

</asp:Content>

