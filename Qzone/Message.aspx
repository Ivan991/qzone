<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Qzone_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <br />
    留言板
    <br />
    <asp:Label ID="lbreportmessage" runat="server" Text="发布留言"></asp:Label>
    ：
    <br />
    <asp:TextBox ID="reportmessage" runat="server"  TextMode="MultiLine" ></asp:TextBox>
    <br />
    <asp:Button ID="btnreportmessage" runat="server" Text="发表"  OnClick="btnreportmessage_Click" />
    <br />
    <br />
    
    留言：
    (
    <asp:Label ID="lbcount" runat="server" ></asp:Label>
    )        
    <br />


    <asp:Repeater ID="message" runat="server" OnItemDataBound="message_ItemDataBound" OnItemCommand="message_ItemCommand">
    
        <ItemTemplate>

            <br />
            <asp:ImageButton ID="image" Height="50px" Width="50px" runat="server" ImageUrl='<%# Eval("PoImage") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>' />
            <asp:LinkButton ID="poname" runat="server" Text='<%#Eval("PoName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>'></asp:LinkButton>

            

            <%#Eval("Contents") %>

            <br />
            发表时间: <%#Eval("Dates") %>
            
            评论数量：<%#Eval("Comments") %>

       <asp:LinkButton ID="delete" runat="server" Text="删除" ToolTip="删除这条留言" CommandName="delete" CommandArgument='<%# Eval("MessageId") %>'></asp:LinkButton>

            <br />
            评论内容：
           

            <asp:Repeater ID="comments" runat="server" OnItemDataBound="comments_ItemDataBound" OnItemCommand="comments_ItemCommand" >

                <ItemTemplate>
                    <br />
                    <asp:LinkButton ID="cname" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                    : 
                    <br />
                    @<asp:LinkButton ID="toname" runat="server" Text='<%# Eval("ParentName") %>'  PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                    :
                    &nbsp;
                    <asp:LinkButton ID="content" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor" ></asp:LinkButton>  
                    <br />
                    回复时间:<%#Eval("Dates") %>

                    <asp:LinkButton ID="delete1" runat="server" Text="删除" ToolTip="删除这条评论" CommandName="delete1" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>
                    
                    <br />
                    
                    <asp:Label ID="lb" runat="server" Text="评论" Visible="false"></asp:Label>

                    <asp:TextBox ID="comment1" runat="server" TextMode="SingleLine" Visible="false" ></asp:TextBox>

                    <asp:Button ID="btncomment1" runat="server" Text="评论" Visible="false" CommandName="btncom1" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />
               
                     </ItemTemplate>

            </asp:Repeater>




            <br />

          评论:<asp:TextBox ID="comment" runat="server" TextMode="SingleLine" ></asp:TextBox>
            
            <asp:Button ID="btncomment" runat="server" Text="提交" CommandName="btncom" CommandArgument='<%#Eval("MessageId") %>' />

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

