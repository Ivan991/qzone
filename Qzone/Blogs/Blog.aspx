<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Qzone_Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script id="editor" type="text/plain" style="width:1024px;height:500px;"></script>
    <br />
    日志：
    (
    <asp:Label ID="lbcount" runat="server" ></asp:Label>
    )        
    <br />
    <asp:Button ID="reportblog" runat="server" Text="发表日志" OnClick="reportblog_Click" />

    <br />
    <br />

    <asp:DropDownList ID="category" runat="server" OnSelectedIndexChanged="btnFirst_Click" AutoPostBack="true">
    <asp:ListItem Selected="True">所有日志</asp:ListItem>
    </asp:DropDownList>
   &nbsp;
    <asp:LinkButton ID="ecategory" runat="server" OnClick="ecategory_Click" Text="修改分类"></asp:LinkButton>
   <br />
    <asp:Repeater ID="blog" runat="server"  OnItemDataBound="blog_ItemDataBound" OnItemCommand="blog_ItemCommand">
    
        <ItemTemplate>
            <br />
            <br />
            <asp:ImageButton ID="LinkButton1" Height="50px" Width="50px" runat="server" ImageUrl='<%#Eval("PoImage") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>'></asp:ImageButton>
            
            <asp:LinkButton ID="poname" runat="server" Text='<%#Eval("PoName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>'></asp:LinkButton>
            :<br />
            <br />
            <asp:LinkButton ID="titles" runat="server" Text='<%# Eval("Title") %>' PostBackUrl='<%#"/Blogs/BlogContents.aspx?id="+Eval("PoId")+"&id1="+ Eval("BlogId") %>'></asp:LinkButton>
            <br />
            分类:<%# Eval("Name") %>&nbsp;
            <asp:Label ID="lbstate" runat="server" Text='<%# Eval("State") %>' ></asp:Label>&nbsp;
            发表时间: <%#Eval("Dates") %>&nbsp;   
            评论数量：<%#Eval("Comments") %><br />
            <asp:LinkButton ID="editor" runat="server" Text="编辑" PostBackUrl='<%#"/Blogs/BlogEditor.aspx?id="+Eval("PoId")+"&id1="+ Eval("BlogId") %>'></asp:LinkButton>

            <asp:LinkButton ID="delete" runat="server" Text="删除" ToolTip="删除这条日志"  CommandName="delete" CommandArgument='<%# Eval("BlogId") %>' ></asp:LinkButton>
            <br />
            <br />
        </ItemTemplate>

    </asp:Repeater>



        <asp:Button ID="btnFirst" runat="server" Text="首页"  OnClick="btnFirst_Click" />

        <asp:Button ID="btnUp" runat="server" Text="上一页"  OnClick="btnUp_Click" />

        页次：<asp:Label ID="lbNow" runat="server" Text="1"></asp:Label>

        /<asp:Label ID="lbTotal" runat="server" ></asp:Label>

        转<asp:TextBox ID="txtJump" Text="1" runat="server" Width="29px"></asp:TextBox>页  

        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtJump" ErrorMessage="必须为数字" Display="Dynamic" ValidationExpression="^[1-9]d*|0$"></asp:RegularExpressionValidator>

        <asp:Button ID="btnJump" runat="server"  Text="Go" OnClick="btnJump_Click"/>

        <asp:Button ID="btnDrow" runat="server" Text="下一页"  OnClick="btnDrow_Click"/>
        
        <asp:Button ID="btnLast" runat="server" Text="尾页"   OnClick="btnLast_Click"/>
        
        <br />

</asp:Content>

