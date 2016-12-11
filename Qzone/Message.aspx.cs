using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Qzone_Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (!IsPostBack)
        {
            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && r.IsMatch(Request.QueryString["id"]))//判断地址栏传过来的id是否为数字以及是否为空
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);   //获取地址栏中的用户的id

                    string sql = "select * from Users where Id=@id";  //查找该用户是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    if (dt.Rows.Count > 0)  //如果该用户存在

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

                        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id等于空间用户id
                        {
                            LinkButton editorimage = (LinkButton)((MasterPage)Master).FindControl("editorimage");

                            LinkButton editorqname = (LinkButton)((MasterPage)Master).FindControl("editorqname");                         

                            editorimage.Visible = false;//修改空间头像的控件隐藏出来

                            editorqname.Visible = false;//修改空间名的控件隐藏出来

                        }

                        string sql0 = "select * from Message where ParentId=@id order by Dates desc";  //查找该用户的留言

                        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

                        message.DataSource = bind;  //把分页对象绑定到repeater

                        message.DataBind();//把绑定的数据显示出来

                        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                        string sql1 = "select * from Message where ParentId=@id";  //查找出留言数量并显示出来

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        lbcount.Text = dt1.Rows.Count.ToString();
                    }
                    else

                        Response.Redirect("/Qzone.aspx?id=" + Session["id"].ToString() + "");
                }
                else

                    Response.Redirect("/Qzone.aspx?id=" + Session["id"].ToString() + "");
            }
            else

                Response.Redirect("/MyLogin.aspx");
        }
    }

    protected void message_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;    //查找外层repeater

            int id = Convert.ToInt32(rowv["MessageId"]);        //提取外层repeater中的数据

            string sql = "select * from ComMessage where ToId=@toid order by Dates";    //在数据库中查找

            SqlParameter[] sqls = { new SqlParameter("@toid", id) };

            DataTable dt = sqlh.sqlselect(sql, sqls);

            Repeater com = (Repeater)e.Item.FindControl("comments");  //找到内嵌repeater

            com.DataSource = dt;    //绑定数据

            com.DataBind();

            if (Request.QueryString["id"].ToString() != Session["id"].ToString())//如果空间主人id不等于登录id

                ((LinkButton)e.Item.FindControl("delete")).Visible = false;  //找到repeater message 中的所有删除的控件

        }
    }


    protected void message_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
      
        if (e.CommandName == "btncom")     //评论留言
        {

            int toid = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox box = (TextBox)e.Item.FindControl("comment");

            if (box.Text.Trim().Length > 0)
            {
                string contents = box.Text.Trim();

                string sql = "insert into ComMessage (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname)";

                int parentid = Convert.ToInt32(Request.QueryString["id"]);

                int userid = Convert.ToInt32(Session["id"].ToString());

                string dates = DateTime.Now.ToString();

                string username = Session["name"].ToString();

                string sql1 = "select Name from Users where Id=@parentid";     //找出被回复的名字

                SqlParameter[] sqls1 = { new SqlParameter("@parentid", parentid) };

                DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                string parentname = dt1.Rows[0][0].ToString();

                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",toid),

                    new SqlParameter("@parentid",parentid),

                    new SqlParameter("@userid",userid),

                    new SqlParameter("@contents",contents),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@username",username),

                    new SqlParameter("@parentname",parentname)

                };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)
                {
                    string sql2 = "update Message set Comments=Comments+1 where MessageId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='/Message.aspx?id=" + parentid + "'</script>");
                }

                else

                    Response.Write("<script>alert('评论失败');location=‘/Message.aspx?id=" + parentid + "'</script>");

            }
            else

                Response.Write("<script>alert('评论不能为空')</script>");

        }


        if (e.CommandName == "delete")   //删除留言
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);   //获取当前页的id

            string sql2 = "delete from Message where MessageId=@messageid";

            int messageid = Convert.ToInt32(e.CommandArgument.ToString());

            SqlParameter[] sqls2 = { new SqlParameter("@messageid", messageid) };

            int result = sqlh.sqlhelper(sql2, sqls2);                      //进行删除

            if (result > 0)


                Response.Write("<script>alert('删除成功');location='/Message.aspx?id=" + id + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('删除失败')</script>");

        }

    }

    protected void comments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete1")  //执行删除评论这条操作
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);       //获取当前页的id

            string sql3 = "delete from ComMessage where CommentId=@commentid";

            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);

            int toid = Convert.ToInt32(str[1]);

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {

                string sql2 = "update Message set Comments=Comments-1 where MessageId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Message.aspx?id=" + id + "'</script>");    //刷新界面
            }
            else

                Response.Write("<script>alert('删除失败')</script>");

        }
        if (e.CommandName == "commentor")     //点击回复的内容弹出评论框
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");

            Button btn = (Button)e.Item.FindControl("btncomment1");

            Label lb = (Label)e.Item.FindControl("lb");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }

        if (e.CommandName == "btncom1")    //回复这条评论
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");

            if (text.Text.Trim().Length > 0)
            {
                string sql = "insert into ComMessage (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname)";

                //string[] estr = e.CommandArgument.ToString().Split(',');
                //int Parent_Id = Convert.ToInt32(estr[0]);
                //int TypeId = Convert.ToInt32(estr[1]);

                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid1 = Convert.ToInt32(str1[1]);

                string contents = text.Text.Trim();

                int parentid = Convert.ToInt32(str1[0]);

                int userid = Convert.ToInt32(Session["id"].ToString());

                string dates = DateTime.Now.ToString();

                string username = Session["name"].ToString();

                string parentname = str1[2];

                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",toid1),

                    new SqlParameter("@parentid",parentid),

                    new SqlParameter("@userid",userid),

                    new SqlParameter("@contents",contents),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@username",username),

                    new SqlParameter("@parentname",parentname)
                };

                int result1 = sqlh.sqlhelper(sql, sqls);     //对数据库进行操作

                if (result1 > 0)

                {
                    int sid = Convert.ToInt32(Request.QueryString["id"]);

                    Response.Write("<script>alert('评论成功！');location='/Message.aspx?id=" + sid + "'</script>");


                }

                else

                    Response.Write("<script>alert('评论失败！')</script>");

            }

            else

                Response.Write("<script>alert('评论失败！')</script>");

            Button btn = (Button)e.Item.FindControl("btncomment1");   //评论完毕

            Label lb = (Label)e.Item.FindControl("lb");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }
    }

    protected void btnFirst_Click(object sender, EventArgs e)//首页
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        string sql0 = "select * from Message where ParentId=@id order by Dates desc";  //查找该用户的留言

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

        lbNow.Text = "1";     //当前页设为1

        message.DataSource = bind;

        message.DataBind();
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            string sql0 = "select * from Message where ParentId=@id order by Dates desc";  //查找该用户的留言

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            message.DataSource = bind;

            message.DataBind();
        }

    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                string sql0 = "select * from Message where ParentId=@id order by Dates desc";

                SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

                lbNow.Text = Convert.ToString(pages);             //设置当前页数

                message.DataSource = bind;

                message.DataBind();
            }

            else

                Response.Write("<script>alert('输入页数不在范围内')</script>");

        }

        else

            Response.Write("<script>alert('输入页数不能为空')</script>");
    }

    protected void btnDrow_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) < Convert.ToInt32(lbTotal.Text))                                             //如果当前页数不是尾页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) + 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数+1

            string sql0 = "select * from Message where ParentId=@id order by Dates desc";  //查找该用户的留言

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            message.DataSource = bind;

            message.DataBind();
        }



    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        string sql0 = "select * from Message where ParentId=@id order by Dates desc";  //查找该用户的留言

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

        lbNow.Text = pages.ToString();     //当前页设为最后一页

        message.DataSource = bind;

        message.DataBind();    //显示最后一页
    }


    protected void btnreportmessage_Click(object sender, EventArgs e) //发布留言
    {
        string text = reportmessage.Text.Trim();

        if (text.Length > 0)
        {
            string dates = DateTime.Now.ToString();

            string poid = Session["id"].ToString();//发布者的id

            string parentid = Request.QueryString["id"].ToString();//该空间主人名字


            string sql0 = "select * from Users where Id=@id";

            SqlParameter[] sqls0 = { new SqlParameter("@id", parentid) };

            DataTable dt0 = sqlh.sqlselect(sql0, sqls0);//查找出该用户的姓名

            string poname = Session["name"].ToString();

            string parentname = dt0.Rows[0][1].ToString();//该空间主人姓名

            string sql = "insert into Message (PoId,Contents,Dates,Comments,PoName,ParentId,ParentName,PoImage) values (@poid,@text,@dates,'0',@poname,@parentid,@parentname,@poimage) ";

            SqlParameter[] sqls =
           {

                new SqlParameter("@text", text),

                new SqlParameter("@poid",poid),

                new SqlParameter("@dates",dates),

                new SqlParameter("@poname",poname),

               new SqlParameter("@parentid",parentid),

               new SqlParameter("@parentname",parentname),

               new SqlParameter("@poimage",Session["image"])

            };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('发布留言成功');location='/Message.aspx?id=" + parentid + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('发表失败')</script>");
        }

        else

            Response.Write("<script>alert('发表留言内容不能为空')</script>");

    }



    protected void comments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["id"].ToString() != Session["id"].ToString())

                ((LinkButton)e.Item.FindControl("delete1")).Visible = false;  //找到repeater comments 中的所有删除的控件
        }
    }
}