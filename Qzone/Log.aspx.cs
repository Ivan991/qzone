using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Log : System.Web.UI.Page
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

                        string sql1 = "";

                        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id
                        {
                            LinkButton editorimage = (LinkButton)((MasterPage)Master).FindControl("editorimage");

                            LinkButton editorqname = (LinkButton)((MasterPage)Master).FindControl("editorqname");

                            editorimage.Visible = false;//修改空间头像的控件隐藏出来

                            editorqname.Visible = false;//修改空间名的控件隐藏出来

                            lbreportlog.Visible = false;//隐藏发表说说的控件

                            reportlog.Visible = false;

                            btnreportlog.Visible = false;

                            state.Visible = false;

                            sql1 = "select * from Log where PoId=@id and State=1";  //查找出说说数量并显示出来
                        }
                        else

                            sql1 = "select * from Log where PoId=@id";  //查找出说说数量并显示出来              

                        datatorepeater(1);//绑定repeater         

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

    void datatorepeater(int currentge)//分页接口
    {
        string sql0 = "";

        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id

            sql0 = "select * from Log where PoId=@id and State=1 order by Dates desc";  //查找该用户的说说

        else

            sql0 = "select * from Log where PoId=@id order by Dates desc";  //查找该用户的说说

        SqlParameter[] sqls0 = { new SqlParameter("@id", Request.QueryString["id"]) };

        PagedDataSource bind = sqlh.DataBindToRepeater(currentge, sql0, sqls0);

        log.DataSource = bind;  //把分页对象绑定到repeater

        log.DataBind();//把绑定的数据显示出来

        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

    }


    protected void log_ItemDataBound(object sender, RepeaterItemEventArgs e)    //嵌套绑定repeater
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;    //查找外层repeater

            int id = Convert.ToInt32(rowv["LogId"]);        //提取外层repeater中的数据

            string sql = "select * from ComLog where ToId=@toid order by Dates";    //在数据库中查找

            SqlParameter[] sqls = { new SqlParameter("@toid", id) };

            DataTable dt = sqlh.sqlselect(sql, sqls);

            Repeater com = (Repeater)e.Item.FindControl("comments");  //找到内嵌repeater

            com.DataSource = dt;    //绑定数据

            com.DataBind();

            string sql1 = "select * from GoodLog where LogId=@id";

            SqlParameter[] sqls1 = { new SqlParameter("@id", id) };//找到有多少个赞

            DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

            ((Label)e.Item.FindControl("lbgood")).Text = dt1.Rows.Count.ToString();  //设置点赞的数量

            if (Request.QueryString["id"].ToString() == Session["id"].ToString())
            {

                ((LinkButton)e.Item.FindControl("report")).Visible = false;  //找到repeater log 中的所有转发的控件

                 Label lbstate= (Label)e.Item.FindControl("lbstate");  //获取权限值

                if (lbstate.Text == "1")

                    lbstate.Text = "所有人可见";

                else

                    lbstate.Text = "仅自己可见";                 

            }
            else
            {

                ((LinkButton)e.Item.FindControl("delete")).Visible = false;  //找到repeater log 中的所有删除的控件 

                ((Label)e.Item.FindControl("lbstate")).Visible = false;  //找到repeater log 中的所有权限的控件                

            }

        }
    }


    protected void log_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "good")//按点赞的按钮
        {
            string sql1 = "select * from GoodLog where LogId=@id and OwnId=@id1";

            SqlParameter[] sqls1 = { new SqlParameter("@id", e.CommandArgument.ToString()), new SqlParameter("@id1", Session["id"].ToString()) };//找到用户有没有点过赞

            DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

            if (dt1.Rows.Count == 0)
            {
                string sql = "insert into GoodLog(LogId,OwnId) values (@id,@id1)";

                SqlParameter[] sqls = { new SqlParameter("@id", e.CommandArgument), new SqlParameter("@id1", Session["id"].ToString()) };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)

                    Response.Write("<script>alert('点赞成功');location='/Log.aspx?id=" + Request.QueryString["id"] + "'</script>");

                else

                    Response.Write("<script>alert('点赞失败请重试')</script>");

            }

            else
            {
                string sql = "delete from GoodLog where LogId=@id and OwnId=@id1";

                SqlParameter[] sqls = { new SqlParameter("@id", e.CommandArgument.ToString()), new SqlParameter("@id1", Session["id"].ToString()) };//删除点赞记录

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)

                    Response.Write("<script>alert('已取消点赞');location='/Log.aspx?id=" + Request.QueryString["id"] + "'</script>");

                else

                    Response.Write("<script>alert('取消点赞请重试')</script>");
            }
        }

        if (e.CommandName == "report1")    //按转发显示转发框
        {
            TextBox text1 = (TextBox)e.Item.FindControl("reporter");

            Button btn1 = (Button)e.Item.FindControl("btnreporter");

            DropDownList restate = (DropDownList)e.Item.FindControl("reportstate");

            restate.Visible = true;

            text1.Visible = true;

            btn1.Visible = true;

           
        }

        if (e.CommandName == "btnreporter1")
        {
            TextBox text2 = (TextBox)e.Item.FindControl("reporter");

            Button btn2 = (Button)e.Item.FindControl("btnreporter");

            DropDownList restate = (DropDownList)e.Item.FindControl("reportstate");

            restate.Visible = false;

            text2.Visible = false;

            btn2.Visible = false;

            string sqlre = "insert into Log (PoId,Contents,Dates,Comments,PoName,PoImage,State) values (@PoId,@Contents,@Dates,'0',@PoName,@poimage,@state) ";

            string poid = Session["id"].ToString();  //转发的人的id  

            string poname = Session["name"].ToString();  //转发的人的名字

            string dates1 = DateTime.Now.ToString();    //转发时间

            string reportcontent = text2.Text.Trim();  //转发说说时自己评论的内容

            string logid = e.CommandArgument.ToString();  //这条说说的id

            string poimage = Session["image"].ToString();

            string state1 = restate.SelectedValue.ToString();

            string sqlselect = "select * from Log where LogId=@logid";

            SqlParameter[] sqlsselect = { new SqlParameter("@logid", logid) };

            DataTable dt1 = sqlh.sqlselect(sqlselect, sqlsselect);

            string logcontent = dt1.Rows[0][2].ToString();   //转发说说的内容

            string reportname = dt1.Rows[0][5].ToString();  //被转发人的名字

            string contents1 = "@" + reportname + ":" + reportcontent + "——" + logcontent + "";

            SqlParameter[] sqlsre =
            {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@PoName",poname),

                    new SqlParameter("@poimage",poimage),

                    new SqlParameter("@state",state1)

                };

            int resultre = sqlh.sqlhelper(sqlre, sqlsre);

            if (resultre > 0)

                Response.Write("<script>alert('转发成功');location='/Log.aspx?id=" + poid + "'</script>");

            else

                Response.Write("<script>alert('转发失败')</script>");

        }

        if (e.CommandName == "btncom")     //评论说说
        {
            TextBox box = (TextBox)e.Item.FindControl("comment");

            int toid = Convert.ToInt32(e.CommandArgument.ToString());

            if (box.Text.Trim().Length > 0)
            {
                string contents = box.Text.Trim();

                string sql = "";

                int parentid = Convert.ToInt32(Request.QueryString["id"]);

                if (parentid != Convert.ToInt32(Session["id"]))

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";


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
                    string sql2 = "update Log set Comments=Comments+1 where LogId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='Log.aspx?id=" + parentid + "'</script>");
                }

                else

                    Response.Write("<script>alert('评论失败');location='Log.aspx?id=" + parentid + "'</script>");

            }
            else

                Response.Write("<script>alert('评论不能为空')</script>");
        }


        if (e.CommandName == "delete")   //删除说说
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);   //获取当前页的id

            string sql2 = "delete from Log where LogId=@logid";

            int logid = Convert.ToInt32(e.CommandArgument.ToString());

            SqlParameter[] sqls2 = { new SqlParameter("@logid", logid) };

            int result = sqlh.sqlhelper(sql2, sqls2);                      //进行删除

            if (result > 0)


                Response.Write("<script>alert('删除成功');location='Log.aspx?id=" + id + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('删除失败')</script>");

        }

    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {

        lbNow.Text = "1";     //当前页设为1

        datatorepeater(1);
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            datatorepeater(pages);
        }

    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师

                datatorepeater(pages);

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

            datatorepeater(pages);
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int pages = Convert.ToInt32(lbTotal.Text);

        lbNow.Text = pages.ToString();     //当前页设为最后一页

        datatorepeater(pages);
    }



    protected void comments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete1")  //执行删除评论这条操作
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);       //获取当前页的id

            string sql3 = "delete from ComLog where CommentId=@commentid";

            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);

            int toid = Convert.ToInt32(str[1]);

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {
                string sql2 = "update Log set Comments=Comments-1 where LogId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Log.aspx?id=" + id + "'</script>");    //刷新界面
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

            string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

            int toid1 = Convert.ToInt32(str1[1]);

            string contents = text.Text.Trim();

            int parentid = Convert.ToInt32(str1[0]);

            if (text.Text.Trim().Length > 0)
            {

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"]))

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";


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
                    string sql2 = "update Log set Comments=Comments+1 where LogId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid1) };

                    int result2 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    int sid = Convert.ToInt32(Request.QueryString["id"]);

                    Response.Write("<script>alert('评论成功！');location='Log.aspx?id=" + sid + "'</script>");
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


    protected void btnreportlog_Click(object sender, EventArgs e)  //发表说说
    {
        string text = reportlog.Text.Trim();

        if (text.Length > 0)
        {
            string dates = DateTime.Now.ToString();

            string poid = Session["id"].ToString();

            string poname = Session["name"].ToString();

            string state1 = state.SelectedValue;

            string sql = "insert into Log (PoId,Contents,Dates,Comments,PoName,PoImage,State) values (@poid,@text,@dates,'0',@poname,@poimage,@state) ";

            SqlParameter[] sqls =
           {

                new SqlParameter("@text", text),

                new SqlParameter("@poid",poid),

                new SqlParameter("@dates",dates),

                new SqlParameter("@poname",poname),

                new SqlParameter("@poimage",Session["image"].ToString()),

                new SqlParameter("@state",state1),


            };
            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('发表说说成功');location='Log.aspx?id=" + poid + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('发表失败')</script>");
        }

        else

            Response.Write("<script>alert('发表说说内容不能为空')</script>");
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