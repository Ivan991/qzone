<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" Validaterequest="false" AutoEventWireup="true" CodeFile="NewBlog.aspx.cs" Inherits="Blogs_NewBlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    
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
    日志
    <br />
    <asp:Label ID="lbreportblog" runat="server"  Text="发表日志"></asp:Label>
    &nbsp;<br />
    日志标题：
    <asp:TextBox ID="reporttitle" runat="server"  TextMode="SingleLine"></asp:TextBox>
    &nbsp;
    分类:<asp:DropDownList ID="category" runat="server"  ></asp:DropDownList>
    &nbsp;
    权限:<asp:DropDownList ID="state" runat="server"  >
        <asp:ListItem Selected="True"  Value="1">所有人可见</asp:ListItem>
        <asp:ListItem Value="0">仅自己可见</asp:ListItem>
    </asp:DropDownList>
     <br />
    <br />

   <textarea id="editor" runat="server" type="text/plain"  style="width:1024px;height:500px;" ></textarea>
   <script type="text/javascript">
    var ue = UE.getEditor('<%=editor.ClientID %>');</script>

     <br />

    <asp:Button ID="btnreportblog" runat="server" Text="发表" OnClick="btnreportblog_Click" />
    <br />
    <br />

</asp:Content>

