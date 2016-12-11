<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ECategory.aspx.cs" Inherits="Blogs_ECategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:Repeater ID="category" runat="server" OnItemCommand="category_ItemCommand">
        <ItemTemplate>
          <br />
              <%# Eval("Name") %>&nbsp;
              <asp:Button ID="editor" runat="server" Text="修改" CommandName="editor1" /> &nbsp;
              <asp:TextBox ID="txteditor" runat="server" Visible="false" ></asp:TextBox>&nbsp;
              <asp:Button ID="btneditor" runat="server" Text="修改" Visible="false" CommandName="btneditor1" CommandArgument='<%# Eval("Id") %>' />&nbsp;
              <asp:Button ID="btndelete" Text="删除" OnClientClick="return confirm('如果删除该分类，该分类下的所有日志都将被删除，请问还继续删除么？')" runat="server"  CommandName="delete" CommandArgument='<%# Eval("Id") %>' />&nbsp;
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <br />
   
    添加新分类
    <br />
    <asp:TextBox ID="newcategory" runat="server" ></asp:TextBox>
    &nbsp;
    <asp:Button ID="btnadd" Text="添加" runat="server" OnClick="btnadd_Click" />
</asp:Content>

