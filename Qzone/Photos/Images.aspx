<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Images.aspx.cs" Inherits="Photos_Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
    <asp:Label ID="albumname" runat="server" ></asp:Label>:
    <br />
    <asp:Label ID="albumdescribe" runat="server" ></asp:Label>
    <br />
    <asp:Image ID="albumimage"  Height="200px" Width="227px"  runat="server" />
    <br />
    相册照片数量：
    &nbsp;
    <asp:Label ID="lbcount" runat="server" ></asp:Label>
    <br />
    <asp:Label ID="uploadname" runat="server" Text="上传照片："></asp:Label>
<%--    <asp:Image ID="uploadimage"  Height="227px" Width="227px"  runat="server" />--%>
    <asp:FileUpload  ID="upload"  runat="server" />
    <br />
    <asp:Label ID="uploadname1" runat="server" Text="图片描述："></asp:Label>
    <asp:TextBox ID="describe"  runat="server" ></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnupload" runat="server"  Text="上传" OnClick="btnupload_Click" />

    <asp:Repeater ID="photos" runat="server" OnItemDataBound="photos_ItemDataBound" OnItemCommand="photos_ItemCommand">

        <ItemTemplate>
            <br />
            <asp:Image ID="image"  Height="300px" Width="300px"  runat="server" ImageUrl='<%# Eval("Contents") %>' />
            <br />
            <asp:Label ID="lbimage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
            <br />
             <asp:LinkButton ID="editor" runat="server" Text="编辑照片描述"  PostBackUrl='<%#"/Photos/PhotoEditor.aspx?id="+Eval("PhotoId") %>'></asp:LinkButton>
            <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%# Eval("PhotoId") %>'></asp:LinkButton>
            &nbsp;
            评论数量：<%#Eval("Comments") %>
            <br />
            评论内容:
            <br />
            <asp:Repeater ID="comphotos" runat="server" OnItemDataBound="comphotos_ItemDataBound" OnItemCommand="comphotos_ItemCommand">

                <ItemTemplate>
                    <br />
                    <asp:LinkButton ID="comname" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"~/Qzone.aspx?ID="+Eval("UserId") %>'></asp:LinkButton>
                    :<br />
                    @<asp:LinkButton ID="toname" runat="server" Text='<%# Eval("ParentName") %>' PostBackUrl='<%#"~/Qzone.aspx?ID="+Eval("ParentId") %>'></asp:LinkButton>
                    :&nbsp;
                    <asp:LinkButton ID="content" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor" CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>  
                    <br />
                    回复时间:<%#Eval("Dates") %>
                    &nbsp;
                    <asp:LinkButton ID="delete1" runat="server" Text="删除" ToolTip="删除这条评论" CommandName="delete1" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>
                    <br />                    
                    <asp:Label ID="lb" runat="server" Text="评论" Visible="false"></asp:Label>

                    <asp:TextBox ID="comment1" runat="server" TextMode="SingleLine" Visible="false" ></asp:TextBox>

                    <asp:Button ID="btncomment1" runat="server" Text="评论" Visible="false" CommandName="btncom1" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />
               
                    <br />
                </ItemTemplate>

            </asp:Repeater>

            <br />
            评论:<asp:TextBox ID="comment" runat="server" TextMode="SingleLine" ></asp:TextBox>
            
            <asp:Button ID="btncomment" runat="server" Text="提交" CommandName="btncom" CommandArgument='<%#Eval("PhotoId")+","+ Eval("PoId") %>' />

            <br />
            <asp:LinkButton ID="report" runat="server" Text="转发"   CommandName="report1"></asp:LinkButton>
            <%--转发到哪个相册--%>
            <asp:LinkButton ID="add" runat="server" Visible="false" Text="当前没有可用相册请添加相册" OnClick="add_Click"></asp:LinkButton>
            <asp:DropDownList ID="album" Visible="false" runat="server" ></asp:DropDownList>

            <asp:Label ID="lbreport" runat="server" Text="照片描述" Visible="false"></asp:Label>

            <asp:TextBox ID="reporter" runat="server" Visible="false" ></asp:TextBox>
            
            <asp:Button ID="btnreporter" runat="server" Visible="false" Text="转发" CommandName="btnreporter1" CommandArgument='<%# Eval("Contents") %>'  />
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
        
       <asp:Button ID="btnback" runat="server" Text="返回"   OnClick="btnback_Click"/>

        <br />

</asp:Content>

