<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Trend.aspx.cs" Inherits="Trend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-3.1.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    好友动态
    <br />
    <ul>
        <asp:Repeater ID="trend" runat="server" OnItemDataBound="trend_ItemDataBound" OnItemCommand="trend_ItemCommand">
            <ItemTemplate>
                <br />
                <asp:ImageButton ID="image" Height="30px" Width="30px" runat="server" ImageUrl='<%# Eval("PoImage") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>' />

                <asp:LinkButton ID="linkbutton" runat="server" Text='<%#Eval("PoName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("PoId") %>'></asp:LinkButton>

                <li class="list">
                    <%--这是一个标记位，表明这条状态是个啥--%>

                    <input type="hidden" class="mark" value="<%#Eval("mark")%>" />

                    <%--说说的div显示说说的基本内容--%>
                    <div class="talk" style="display: none">
                        <br />
                        <asp:Label ID="logcontent" runat="server" Text='<%# Eval("Contents") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lbstate1" runat="server" Text='<%# Eval("State") %>' ></asp:Label>&nbsp;
                         发表时间:<%# Eval("Dates") %>&nbsp;                        
                         评论数量:<%# Eval("Comments") %>                                    
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="转发" CommandName="report1" CommandArgument='<%# Eval("Mark")+","+ Eval("Id")%>'></asp:LinkButton>

                    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                        <%--转发权限--%>
                    <asp:DropDownList ID="reportstate1" runat="server" Visible="false" ><asp:ListItem Selected="True" Value="1">所有人可见</asp:ListItem ><asp:ListItem Value="0">仅自己可见</asp:ListItem></asp:DropDownList>
                        &nbsp;

                    <asp:Button ID="Button1" runat="server" Visible="false" Text="转发" CommandName="btnreporter1" CommandArgument='<%# Eval("Mark")+","+ Eval("Id") %>' />
                    <br />
                        <asp:Repeater ID="logcomment" runat="server" OnItemDataBound="logcomment_ItemDataBound" OnItemCommand="logcomment_ItemCommand">
                            <HeaderTemplate>评论区</HeaderTemplate>
                            <ItemTemplate>
                                <br />
                                <asp:LinkButton ID="cname1" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                : 
                                <br />
                                @<asp:LinkButton ID="toname1" runat="server" Text='<%# Eval("ParentName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                :
                                &nbsp;
                                <asp:LinkButton ID="content1" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor1" ></asp:LinkButton>
                                <br />
                                回复时间:<%#Eval("Dates") %>
                                <br />
                                <asp:LinkButton ID="delete1" runat="server" Text="删除" ToolTip="删除这条评论" Visible="false" CommandName="delete1" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>
                                <br />

                                <asp:Label ID="lb1" runat="server" Text="评论" Visible="false"></asp:Label>

                                <asp:TextBox ID="comment1" runat="server" TextMode="SingleLine" Visible="false"></asp:TextBox>

                                <asp:Button ID="btncomment1" runat="server" Text="评论" Visible="false" CommandName="btncom1" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <%--日志的div显示日志的基本内容--%>
                    <%--style="display:none" ----- 块隐藏     style="display:block" ------ 块显示--%>
                    <div class="blog" style="display: none">
                        <asp:LinkButton ID="title" runat="server" Text='<%#Eval("Title") %>' PostBackUrl='<%#"/Blogs/BlogContents.aspx?id="+Eval("PoId")+"&id1="+ Eval("Id") %>'></asp:LinkButton>
                        <br />
                        所属分类：<%# Eval("Category") %>
                        <br />
                         <asp:Label ID="lbstate2" runat="server" Text='<%# Eval("State") %>' ></asp:Label>&nbsp;
                        发表时间:<%# Eval("Dates") %>&nbsp;                        
                        评论数量:<%# Eval("Comments") %>                                    
                    <br />
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="转发" CommandName="report2" CommandArgument='<%# Eval("Mark")+","+ Eval("Id")%>'></asp:LinkButton>
                    <%--选择到转发的分类--%>
                    <asp:DropDownList ID="DropDownList2" Visible="false" runat="server"></asp:DropDownList>

                            <%--转发权限--%>
                    <asp:DropDownList ID="reportstate2" runat="server" Visible="false" ><asp:ListItem Selected="True" Value="1">所有人可见</asp:ListItem ><asp:ListItem Value="0">仅自己可见</asp:ListItem></asp:DropDownList>
             

                    <asp:Button ID="Button2" runat="server" Visible="false" Text="转发" CommandName="btnreporter2" CommandArgument='<%# Eval("Mark")+","+ Eval("Id") %>' />
                    <br />
                    <br />
                        <asp:Repeater ID="blogcomment" runat="server"  OnItemDataBound="blogcomment_ItemDataBound" OnItemCommand="blogcomment_ItemCommand">
                            <HeaderTemplate>评论区</HeaderTemplate>
                            <ItemTemplate>
                                <br />
                                <asp:LinkButton ID="cname2" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                : 
                                <br />
                                @<asp:LinkButton ID="toname2" runat="server" Text='<%# Eval("ParentName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                :
                                &nbsp;
                                <asp:LinkButton ID="content2" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor2" ></asp:LinkButton>
                                <br />
                                回复时间:<%#Eval("Dates") %>
                                <br />
                                <asp:LinkButton ID="delete2" runat="server" Text="删除" ToolTip="删除这条评论" Visible="false" CommandName="delete2" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>

                                <br />

                                <asp:Label ID="lb2" runat="server" Text="评论" Visible="false"></asp:Label>

                                <asp:TextBox ID="comment2" runat="server" TextMode="SingleLine" Visible="false"></asp:TextBox>

                                <asp:Button ID="btncomment2" runat="server" Text="评论" Visible="false" CommandName="btncom2" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <%--照片的div显示照片的基本内容--%>
                    <div class="photo" style="display: none">
                        <img src='<%#Eval("Contents")%>' height="100px" width="100px" />
                        <br />
                        所属相册：<%# Eval("Title") %>

                        所属分类：<%# Eval("Category") %>
                        <br />
                        发表时间:<%# Eval("Dates") %>&nbsp;                        
                        评论数量:<%# Eval("Comments") %>                                    
                    <br />
                    <asp:LinkButton ID="LinkButton3" runat="server" Text="转发" CommandName="report3" CommandArgument='<%# Eval("Mark")+","+ Eval("Id")%>'></asp:LinkButton>
                    <%--选择转发到的相册--%>
                    <asp:DropDownList ID="DropDownList3" Visible="false" runat="server"></asp:DropDownList>

                    <asp:Label ID="Label3" runat="server" Text="照片描述" Visible="false"></asp:Label>
                         
                    <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>

                    <asp:Button ID="Button3" runat="server" Visible="false" Text="转发" CommandName="btnreporter3" CommandArgument='<%# Eval("Mark")+","+ Eval("Id") %>' />
                    <br />
                    <br />
                        <asp:Repeater ID="photocomment" runat="server"  OnItemDataBound="photocomment_ItemDataBound" OnItemCommand="photocomment_ItemCommand">
                            <HeaderTemplate>评论区</HeaderTemplate>
                            <ItemTemplate>
                                <br />
                                <asp:LinkButton ID="cname3" runat="server" Text='<%# Eval("UserName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                : 
                                <br />
                                @<asp:LinkButton ID="toname3" runat="server" Text='<%# Eval("ParentName") %>' PostBackUrl='<%#"/Qzone.aspx?id="+Eval("UserId") %>'></asp:LinkButton>
                                :
                                &nbsp;
                                <asp:LinkButton ID="content3" runat="server" Text='<%# Eval("Contents")%>' CommandName="commentor3" ></asp:LinkButton>
                                <br />
                                回复时间:<%#Eval("Dates") %>
                                <br />
                                <asp:LinkButton ID="delete3" runat="server" Text="删除" ToolTip="删除这条评论" Visible="false" CommandName="delete3" CommandArgument='<%# Eval("CommentId")+","+ Eval("ToId") %>'></asp:LinkButton>
                                <br />
                                <asp:Label ID="lb3" runat="server" Text="评论" Visible="false"></asp:Label>

                                <asp:TextBox ID="comment3" runat="server" TextMode="SingleLine" Visible="false"></asp:TextBox>

                                <asp:Button ID="btncomment3" runat="server" Text="评论" Visible="false" CommandName="btncom3" CommandArgument='<%# Eval("UserId")+","+Eval("ToId")+","+ Eval("UserName") %>' />

                            </ItemTemplate>

                        </asp:Repeater>
                    </div>              

                    <br />
                    评论：
                    <asp:TextBox ID="comment" runat="server" TextMode="SingleLine"></asp:TextBox>
                    <asp:Button ID="btncomment" runat="server" Text="提交" CommandName="btncom" CommandArgument='<%# Eval("Mark")+","+ Eval("Id") %>' />
                    <br />
                </li>

            </ItemTemplate>
        </asp:Repeater>
    </ul>



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



    <script>
        var mark = $(".mark");//所有的标记位-------*代码意义：所有class=mark的标签
        var blog = $(".blog");//所有的日志div
        var photo = $(".photo");//所有的照片div
        var talk = $(".talk");//所有的说说div
        for (var i = 0; i < mark.length; i++)
        {//遍历对象数组mark
            if (mark.eq(i).val() == '1')
            {      //判断标志位---------*代码意义：mark.eq(i)----mark数组中第i个元素，即第i个class=mark的标签。
                //mark.eq(i).val()----元素的值，对应该标签就是value。
                blog.eq(i).attr('style', 'display:block');//如果标志位表明是日志，就把日志的块改为显示
                //blog.eq(i).attr('propertyName','value')  将元素blog.eq(i)的propertyName属性的值改为value
            }
            if (mark.eq(i).val() == '2') {
                photo.eq(i).attr('style', 'display:block');//如果标志位表明是照片，就把照片的块改为显示
            }
            if (mark.eq(i).val() == '0') {
                talk.eq(i).attr('style', 'display:block');//如果标志位表明是说说，就把说说的块改为显示
            }
        }
    </script>
</asp:Content>

