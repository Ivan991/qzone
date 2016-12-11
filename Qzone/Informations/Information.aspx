<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="Informations_Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    我的消息
    <br />
    
        <asp:Repeater ID="information" runat="server" OnItemCommand="information_ItemCommand">
            <ItemTemplate>

                <br />
                <asp:LinkButton ID="linkbutton" runat="server" Text='<%#Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                        <asp:LinkButton ID="content" runat="server" Text='<%# Eval("Contents") %>' CommandName="comment" CommandArgument='<%# Eval("ToId")+","+ Eval("Mark") %>'></asp:LinkButton>
                        <br />
                         评论时间:<%# Eval("Dates") %>&nbsp;                        
                    <br />                     

            </ItemTemplate>
        </asp:Repeater>


    <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />

    <asp:Button ID="btnUp" runat="server" Text="上一页" OnClick="btnUp_Click" />

    页次：<asp:Label ID="lbNow" runat="server" Text="1"></asp:Label>

    /<asp:Label ID="lbTotal" runat="server"></asp:Label>

    转<asp:TextBox ID="txtJump" Text="1" runat="server" Width="29px"></asp:TextBox>页  

        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtJump" ErrorMessage="必须为数字" Display="Dynamic" ValidationExpression="^[1-9]d*|0$"></asp:RegularExpressionValidator>

    <asp:Button ID="btnJump" runat="server" Text="Go" OnClick="btnJump_Click" />

    <asp:Button ID="btnDrow" runat="server" Text="下一页" OnClick="btnDrow_Click" />

    <asp:Button ID="btnLast" runat="server" Text="尾页" OnClick="btnLast_Click" />

    <br />

</asp:Content>

