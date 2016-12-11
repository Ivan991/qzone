<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Friends.aspx.cs" Inherits="Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    关注好友
    <br />
    数量(<asp:Label ID="lbcount" runat="server"></asp:Label>)
    <asp:Repeater ID="friends" runat="server" OnItemDataBound="friends_ItemDataBound" OnItemCommand="friends_ItemCommand">

        <ItemTemplate>
            <br />
            用户名： <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Name") %>' CommandName="report" CommandArgument='<%# Eval("FriendId") %>'></asp:LinkButton>
            <br />
            空间名：<asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("QzoneName") %>' CommandName="report1" CommandArgument='<%# Eval("FriendId") %>'></asp:LinkButton>
            <br />
             空间头像:<asp:ImageButton ID="image" runat="server" ImageUrl='<%# Eval("Image") %>' Height="300px" Width="300px"  CommandName="report2" CommandArgument='<%# Eval("FriendId") %>'/>
            <br />
            <asp:LinkButton ID="editor" runat="server" Text="取消关注" CommandName="cancel" CommandArgument='<%# Eval("FriendId") %>'></asp:LinkButton>
            <br />
        </ItemTemplate>


    </asp:Repeater>
    <br />
    <asp:Button ID="newfriend" runat="server" Text="添加好友" OnClick="newfriend_Click" />
    
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

