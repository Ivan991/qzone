<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BlogContents.aspx.cs" Inherits="Blogs_BlogContents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
     <asp:ImageButton ID="poimage" runat="server" Height="50px" Width="50px"  OnClick="poimage_Click"></asp:ImageButton>
    <asp:LinkButton ID="poname" runat="server"  OnClick="poname_Click"></asp:LinkButton>
    :<br />
    <br />
    <asp:label id="lbblogtitle" runat="server"></asp:label>
    <br />
    <asp:Label ID="lbblogcontent" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:Repeater ID="contents" runat="server">
        <ItemTemplate>        
            <%# Eval("Contents")%>
        </ItemTemplate>
    </asp:Repeater>
    
    <br />
    <br />
    所属分类：<asp:label ID="category" runat="server"></asp:label>
    &nbsp;
    发表时间：<asp:label id="lbdates" runat="server" ></asp:label>
    &nbsp;
    评论数量：<asp:label id="lbcounts" runat="server" ></asp:label>
     &nbsp;&nbsp;<br />
&nbsp;<asp:Button ID="editor" runat="server" Text="编辑" ToolTip="编辑这篇日志"  OnClick="editor_Click"></asp:Button>

     &nbsp;

     <asp:Button ID="delete" runat="server" Text="删除" ToolTip="删除这条日志"  OnClick="delete_Click"  ></asp:Button>
  

            <br />

            <br />

            <br />

            <asp:LinkButton ID="report" runat="server" Text="转发"  OnClick="report_Click"></asp:LinkButton>
            <asp:Label ID="lbcategory" runat="server" Text="转发分类" Visible="false"></asp:Label>
            <asp:DropDownList ID="reportcategory" runat="server" Visible="false" ></asp:DropDownList>
    <asp:LinkButton ID="add" runat="server"  Text="日志未分类，请添加分类" OnClick="add_Click" Visible="false"></asp:LinkButton>
    &nbsp;<asp:Label ID="lbstate" runat="server" Visible="false" Text="转发权限"></asp:Label>
        <asp:DropDownList ID="state" Visible="false" runat="server"  >
        <asp:ListItem Selected="True"  Value="1">所有人可见</asp:ListItem>
        <asp:ListItem Value="0">仅自己可见</asp:ListItem>
    </asp:DropDownList>
     <br />
    <br />
    <asp:Button ID="btnreport" runat="server" Text="确定转发" Visible="false" OnClick="btnreport_Click" />
            <br />
            <br />

            评论:<asp:TextBox ID="comment" runat="server" TextMode="SingleLine" ></asp:TextBox>
            
            <asp:Button ID="btncomment" runat="server" OnClick="btncomment_Click" Text="提交" />

            <br />

     <asp:Repeater ID="comments" runat="server" OnItemDataBound="comments_ItemDataBound"  OnItemCommand="comments_ItemCommand" >

                <ItemTemplate>
                    <br />
                    <asp:LinkButton ID="cname" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"~/Qzone.aspx?ID="+Eval("UserId") %>'></asp:LinkButton>
                    : 
                    <br />
                    @<asp:LinkButton ID="toname" runat="server" Text='<%# Eval("ParentName") %>'   PostBackUrl='<%#"~/Qzone.aspx?ID="+Eval("UserId") %>'></asp:LinkButton>
                    :
                    &nbsp;
                    <asp:LinkButton ID="content" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor" ></asp:LinkButton>  
                    
                    回复时间:<%#Eval("Dates") %><asp:LinkButton ID="delete1" runat="server" Text="删除" ToolTip="删除这条评论" CommandName="delete1" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>
                    
                    <br />
                    
                    <asp:Label ID="lb" runat="server" Text="评论" Visible="false"></asp:Label>

                    <asp:TextBox ID="comment1" runat="server" TextMode="SingleLine" Visible="false" ></asp:TextBox>

                    <asp:Button ID="btncomment1" runat="server" Text="评论" Visible="false" CommandName="btncom1" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />
               
                     </ItemTemplate>

            </asp:Repeater>

</asp:Content>

