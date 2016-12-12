<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  ValidateRequest="false" CodeBehind="Log.aspx.cs" CodeFile="Log.aspx.cs" Inherits="Log" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <br />  
    说说：
    (
    <asp:Label ID="lbcount" runat="server" ></asp:Label>
    )        
    <br />
    <asp:Label ID="lbreportlog" runat="server" Text="发表说说"></asp:Label>
    ：
    <br />
   
    <asp:TextBox ID="reportlog"  runat="server" TextMode="MultiLine" ></asp:TextBox>
    <br />
    <asp:DropDownList ID="state" runat="server" ><asp:ListItem Selected="True" Value="1">所有人可见</asp:ListItem ><asp:ListItem Value="0">仅自己可见</asp:ListItem></asp:DropDownList>
    &nbsp;
    <asp:Button ID="btnreportlog" runat="server" Text="发表" OnClick="btnreportlog_Click" />
    <br />
    <br />
 

    <asp:Repeater ID="log" runat="server" OnItemDataBound="log_ItemDataBound" OnItemCommand="log_ItemCommand">
    
        <ItemTemplate>
           
            <br />
            <asp:ImageButton ID="image" Height="50px" Width="50px" runat="server" ImageUrl='<%# Eval("PoImage") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>' />

            <asp:LinkButton ID="poname" runat="server" Text='<%#Eval("PoName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>'></asp:LinkButton>            

            <%#Eval("Contents") %>
            <br />
            <asp:ImageButton ID="good" runat="server" ImageUrl="~/598772d5d647f5dff78eedf4.jpg" Height="30px" Width="30px" CommandName="good" CommandArgument='<%# Eval("LogId") %>' />
            (<asp:Label ID="lbgood" runat="server" ></asp:Label>)&nbsp;
            <asp:Label ID="lbstate" runat="server" Text='<%# Eval("State") %>' ></asp:Label>&nbsp;
            发表时间: <%#Eval("Dates") %>
            
            评论数量：<%#Eval("Comments") %>

       <asp:LinkButton ID="delete" runat="server" Text="删除" ToolTip="删除这条说说" CommandName="delete" CommandArgument='<%# Eval("LogId") %>'></asp:LinkButton>

            <br />
            评论内容：
            
            
            <asp:Repeater ID="comments" runat="server" OnItemDataBound="comments_ItemDataBound" OnItemCommand="comments_ItemCommand" >

                <ItemTemplate>
                    <br />
                    <asp:LinkButton ID="cname" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                    : 
                    <br />
                    @<asp:LinkButton ID="toname" runat="server" Text='<%# Eval("ParentName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("ParentId") %>'></asp:LinkButton>
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
            <asp:Label ID="commentname" runat="server" Text="评论"></asp:Label>

            <asp:TextBox ID="comment" runat="server" TextMode="SingleLine" ></asp:TextBox>
            
            <asp:Button ID="btncomment" runat="server" Text="提交" CommandName="btncom" CommandArgument='<%#Eval("LogId") %>' />
            <br />
            <asp:LinkButton ID="report" runat="server" Text="转发"   CommandName="report1"></asp:LinkButton>

              <asp:DropDownList ID="reportstate" runat="server" Visible="false" ><asp:ListItem Selected="True" Value="1">所有人可见</asp:ListItem ><asp:ListItem Value="1">仅自己可见</asp:ListItem></asp:DropDownList>
    &nbsp;
          
            <asp:TextBox ID="reporter" runat="server" Visible="false" ></asp:TextBox>
            
            <asp:Button ID="btnreporter" runat="server" Visible="false" Text="转发" CommandName="btnreporter1" CommandArgument='<%# Eval("LogId") %>'  />
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

