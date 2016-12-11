<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="Qzone_Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    
    相册(<asp:Label ID="lbacount" runat="server"></asp:Label> )
    <br />
    <asp:Button ID="lbnewalbum" runat="server" Text="新建相册"  OnClick="lbnewalbum_Click" />
    <br />
    <asp:DropDownList ID="category" runat="server" OnSelectedIndexChanged="btnFirst_Click" AutoPostBack="true">
    <asp:ListItem Selected="True">所有相册</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:LinkButton ID="ecategory" runat="server" OnClick="ecategory_Click" Text="编辑分类"></asp:LinkButton>
    <br />
    <asp:Repeater  ID="photo" runat="server" OnItemDataBound="photo_ItemDataBound" OnItemCommand="photo_ItemCommand">

    <ItemTemplate>
    <br />
    相册名字:<asp:Label ID="albumname" runat="server" Text='<%# Eval("AlbumName") %>'></asp:Label>
    <br />
    相册描述:<asp:Label ID="albumdescribe" runat="server" Text='<%# Eval("Describe") %>'></asp:Label>
     <br />    
    分类:<%# Eval("Name") %>&nbsp;   
    <asp:Label ID="astate" runat="server" Text='<%# Eval("State") %>' ></asp:Label>
    <asp:LinkButton ID="albumeditor" runat="server" Text="编辑" PostBackUrl='<%#"AlbumEditor.aspx?id="+Eval("OwnId")+"&id1="+ Eval("AlbumId") %>'></asp:LinkButton>
    <br />
    <asp:LinkButton ID="albumdelete"  OnClientClick="return confirm('如果删除该相册，该相册下的所有照片将被删除，请问还继续删除么？')" runat="server" Text="删除" ToolTip="删除这个相册" CommandName="delete" CommandArgument='<%# Eval("AlbumId") %>'></asp:LinkButton>
    <br />
    <asp:ImageButton ID="albumimage" runat="server" Height="227px" Width="227px"  ImageUrl='<%# Eval("AlbumImage") %>' CommandName="image" CommandArgument='<%# Eval("AlbumId") %>' />
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

