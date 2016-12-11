<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" Validaterequest="false" AutoEventWireup="true" CodeFile="BlogEditor.aspx.cs" Inherits="Blogs_BlogEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
    <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>

    <style type="text/css">
        div{
            width:100%;
        }
        #editor {
            height: 66px;
            width: 173px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    修改日志
    <asp:TextBox ID="title" runat="server" ></asp:TextBox>
    <br />
    <br />
    <textarea id="editor" runat="server"  style="width:1024px;height:500px;"></textarea>
    <script type="text/javascript">
        var ue = UE.getEditor('<%=editor.ClientID %>');
    </script>
    <br />
    <br />
    分类:
    <asp:DropDownList ID="category" runat="server"  ></asp:DropDownList>
    <br />
    权限
    <asp:DropDownList ID="state" runat="server"  >
        <asp:ListItem Value="1">所有人可见</asp:ListItem>
        <asp:ListItem Value="0">仅自己可见</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Button ID="btneditor" runat="server" Text="修改"  OnClick="btneditor_Click" />
    <asp:Button ID="btncancel" runat="server" Text="取消" OnClick="btncancel_Click" />
</asp:Content>

