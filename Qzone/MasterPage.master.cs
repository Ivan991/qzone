using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string sql1 = "select * from ComLog where Information=1 and ParentId=@id";

            SqlParameter[] sqls1 = { new SqlParameter("@id", Session["id"].ToString()) };

            string sql2 = "select * from ComBlog where Information=1 and ParentId=@id";

            SqlParameter[] sqls2 = { new SqlParameter("@id", Session["id"].ToString()) };

            string sql3= "select * from ComPhoto where Information=1 and ParentId=@id";

            SqlParameter[] sqls3 = { new SqlParameter("@id", Session["id"].ToString()) };

            DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

            DataTable dt2 = sqlh.sqlselect(sql2, sqls2);

            DataTable dt3 = sqlh.sqlselect(sql3, sqls3);

            lbinf.Text = (Convert.ToInt32(dt1.Rows.Count) + Convert.ToInt32(dt2.Rows.Count) + Convert.ToInt32(dt3.Rows.Count)).ToString();

        }

    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Session["name"] = null;

        Response.Write("<script>alert('注销成功！');location='/MyLogin.aspx';</script>");

    }

    protected void log_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        Response.Redirect("/Log.aspx?id="+id+"");


    }

    protected void blog_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/Blog.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void photo_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void message_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Message.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void first_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Session["id"].ToString() + "");
    }

    protected void users_Click(object sender, EventArgs e)
    {
        Response.Redirect("/User/Users.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void editorqname_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Editor/EQName.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void friends_Click(object sender, EventArgs e)
    {

        Response.Redirect("/Friend/Friends.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void editorimage_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Editor/EImage.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void qzone_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void trend_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Trend.aspx?id=" + Session["id"] + "");

    }

    protected void information_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Informations/Information.aspx?id=" + Session["id"].ToString() + "");
    }
}
