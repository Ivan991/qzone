using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blogs_BlogContents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (!IsPostBack)
        {

            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id1"] != null && r.IsMatch(Request.QueryString["id1"]) && r.IsMatch(Request.QueryString["id"]))//判断地址栏传过来的id是否为数字以及是否为空
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);//空间主人id

                    int id1 = Convert.ToInt32(Request.QueryString["id1"]);//日志id

                    string sql = "select * from Users where Id=@id";  //查找id是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    string sql0 = "select * from Blog where BlogId=@id ";  //找出该blog

                    SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    DataTable dt0 = sqlh.sqlselect(sql0, sqls0);

                    if (dt.Rows.Count > 0 && dt0.Rows.Count > 0)  //空间id与blogid均存在的时候

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

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

                            editor.Visible = false;//编辑日志的空间隐藏

                            delete.Visible = false;//删除日志的空间隐藏

                        }
                        else

                            report.Visible = false;//隐藏转发的按钮

                        string sql1 = "select * from BlogC where BlogId=@id";  //找出该blog

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id1) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        lbblogtitle.Text = dt1.Rows[0][2].ToString();//标题

                        lbblogcontent.Text = dt1.Rows[0][3].ToString();//日志内容

                        contents.DataSource = dt1;

                        contents.DataBind();

                        category.Text = dt1.Rows[0][10].ToString();//分类

                        lbdates.Text = dt1.Rows[0][4].ToString();//时间

                        lbcounts.Text = dt1.Rows[0][6].ToString();//评论数量

                        poname.Text = dt1.Rows[0][5].ToString();//发表人名字

                        poimage.ImageUrl = dt1.Rows[0][8].ToString();//头像
                        
                       
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


    protected void comments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["id"].ToString() != Session["id"].ToString())

                ((LinkButton)e.Item.FindControl("delete1")).Visible = false;  //找到repeater comments 中的所有删除的控件
        }
    }

    protected void comments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete1")  //执行删除评论这条操作
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);       //获取当前页的id

            string sql3 = "delete from ComBlog where CommentId=@commentid";

            string[] str = e.CommandArgument.ToString().Split(',');  //获取穿过来的值

            int commentid = Convert.ToInt32(str[0]);  //被评论的id

            int toid = Convert.ToInt32(str[1]);//被评论的那个日志的id

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {

                string sql2 = "update Blog set Comments=Comments-1 where BlogId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Blogs/Blog.aspx?id=" + id + "'</script>");    //刷新界面
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
                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int parentid = Convert.ToInt32(str1[0]);//被回复人的id

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"]))

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

                int toid1 = Convert.ToInt32(str1[1]);  //被回复的日志的id 

                string contents = text.Text.Trim();//回复的内容

                int userid = Convert.ToInt32(Session["id"].ToString());//回复的id

                string dates = DateTime.Now.ToString();//回复时间

                string username = Session["name"].ToString();//回复人的名字

                string parentname = str1[2];//被回复人的名字

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

                    Response.Write("<script>alert('评论成功！');location='/Blogs/Blog.aspx?id=" + sid + "'</script>");



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

    protected void report_Click(object sender, EventArgs e)//按转发按钮
    {
       
        string sqlc = "select * from BlogCategory where OwnId=@id";//查找分类

        SqlParameter[] sqlsc = { new SqlParameter("@id", Session["id"].ToString()) };

        DataTable dtc = sqlh.sqlselect(sqlc, sqlsc);

        if (dtc.Rows.Count > 0)
        {
            btnreport.Visible = true;

            lbstate.Visible = true;

            state.Visible = true;

            reportcategory.Visible = true;

            for (int i = 0; i < dtc.Rows.Count; i++)
            {
                string idc = dtc.Rows[i][0].ToString();

                string categoryc = dtc.Rows[i][1].ToString();

                reportcategory.Items.Add(new ListItem(categoryc, idc));//循环动态绑定分类

            }
        }
        else
        {
            add.Visible = true;
        }
    }


    protected void btncomment_Click(object sender, EventArgs e)
    {

        int toid = Convert.ToInt32(Request.QueryString["id1"]);//获取blog的id

        string contents = comment.Text.Trim();//获取评论内容

        if (contents.Length > 0)
        {

            int parentid = Convert.ToInt32(Request.QueryString["id"]);

            string sql = "";

            if (parentid != Convert.ToInt32(Session["id"]))

                sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

            else

                sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

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

            int result = sqlh.sqlhelper(sql, sqls);//新增评论

            if (result > 0)
            {
                string sql2 = "update Blog set Comments=Comments+1 where BlogId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('评论成功');location='/Blogs/Blog.aspx?id=" + parentid + "'</script>");
            }

            else

                Response.Write("<script>alert('评论失败');location='/Blogs/Blog.aspx?id=" + parentid + "'</script>");

        }
        else

            Response.Write("<script>alert('评论不能为空')</script>");
    }



    protected void delete_Click(object sender, EventArgs e)//删除日志的点击事件
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);   //获取当前页的id

        string sql2 = "delete from Blog where BlogId=@blogid";

        int blogid = Convert.ToInt32(Request.QueryString["id1"]);

        SqlParameter[] sqls2 = { new SqlParameter("@blogid", blogid) };

        int result = sqlh.sqlhelper(sql2, sqls2);                      //进行删除

        if (result > 0)


            Response.Write("<script>alert('删除成功');location='/Blogs/Blog.aspx?id=" + id + "'</script>");    //刷新界面

        else

            Response.Write("<script>alert('删除失败')</script>");

    }



    protected void btnreport_Click(object sender, EventArgs e)
    {
        reportcategory.Visible = false;

        btnreport.Visible = false;

        lbcategory.Visible = false;

        lbstate.Visible = false;

        state.Visible = false;//把转发的控件都隐藏起来

        string categorys = reportcategory.SelectedValue;//获取选中的分类

        string sqlre = "insert into Blog (PoId,Title,Contents,Dates,PoName,Comments,Category,PoImage,State) values (@PoId,@Title,@Contents,@Dates,@PoName,'0',@Category,@PoImage,@state) ";

        string poid = Session["id"].ToString();  //转发的人的id  

        string poname1 = Session["name"].ToString();  //转发的人的名字

        string dates1 = DateTime.Now.ToString();    //转发时间

        string title = lbblogtitle.Text;//转发标题

        string recontent = lbblogcontent.Text;  //转发blog内容

        string reportname = poname1.ToString();  //被转发人的名字

        string contents1 = "@" + reportname + ":" + recontent + "";

        string state1 = state.SelectedValue;//权限

        SqlParameter[] sqlsre =
        {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@Title",title),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@PoName",poname1),

                    new SqlParameter("@Category",categorys),

                    new SqlParameter("@PoImage",Session["image"].ToString()),

                    new SqlParameter("@state",state1)

       };

        int resultre = sqlh.sqlhelper(sqlre, sqlsre);

        if (resultre > 0)

            Response.Write("<script>alert('转发成功');location='/Blogs/Blog.aspx?id=" + poid + "'</script>");

        else

            Response.Write("<script>alert('转发失败')</script>");
    }

    protected void poname_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void poimage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void editor_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/BlogEditor.aspx?id=" + Request.QueryString["id"] + "&id1=" + Request.QueryString["id1"] + "");
    }

    protected void add_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/ECategory.aspx?id=" + Session["id"] + "");
    }
}