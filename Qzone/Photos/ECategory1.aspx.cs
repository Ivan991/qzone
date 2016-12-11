using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_ECategory1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"] == Session["id"].ToString() && r.IsMatch(Request.QueryString["id"]))
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

                        string sql1 = "select * from AlbumCategory where OwnId=@id";//找出分类

                        SqlParameter[] sqls1 = { new SqlParameter("@id", Request.QueryString["id"]) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        category.DataSource = dt1;

                        category.DataBind();

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

    protected void btnadd_Click(object sender, EventArgs e)
    {
        string addcategory = newcategory.Text.Trim();//获取新加的分类

        if (addcategory.Length > 0)
        {
            string sql = "select * from AlbumCategory where OwnId=@ownid and Name=@name";

            SqlParameter[] sqls = { new SqlParameter("@ownid", Request.QueryString["id"]), new SqlParameter("@name", addcategory) };

            DataTable dt = sqlh.sqlselect(sql, sqls);

            if (dt.Rows.Count == 0)
            {
                string sql1 = "insert into AlbumCategory(Name,OwnId) values (@name,@ownid)";

                SqlParameter[] sqls1 = { new SqlParameter("@name", addcategory), new SqlParameter("@ownid", Request.QueryString["id"]) };

                int result = sqlh.sqlhelper(sql1, sqls1);

                if (result > 0)

                    Response.Write("<script>alert('添加分类成功');location='/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "'</script>");

                else

                    Response.Write("<script>alert('添加不成功请重试!')</script>");

            }
            else
                Response.Write("<script>alert('该分类已存在!')</script>");

        }
        else

            Response.Write("<script>alert('新添分类不能为空')</script>");

    }

    protected void category_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editor1")
        {
            ((TextBox)e.Item.FindControl("txteditor")).Visible = true;//把编辑的控件显示出来

            ((Button)e.Item.FindControl("btneditor")).Visible = true;
        }

        if (e.CommandName == "btneditor1")
        {
            TextBox txtbox = (TextBox)e.Item.FindControl("txteditor");//把编辑的控件隐藏起来

            txtbox.Visible = false;

            ((Button)e.Item.FindControl("btneditor")).Visible = false;

            string txt = txtbox.Text.Trim();//获取修改的值

            if (txt.Length > 0)
            {
                string sql = "update AlbumCategory set Name=@name where Id=@id";

                SqlParameter[] sqls = { new SqlParameter("@name", txt), new SqlParameter("@id", e.CommandArgument.ToString()) };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)

                    Response.Write("<script>alert('修改成功');location='/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "'</script>");

                else

                    Response.Write("<script>alert('修改失败')</script>");

            }
            else

                Response.Write("<script>alert('输入不能为空')</script>");
        }

        if (e.CommandName == "delete")
        {
            string sql1 = "delete from AlbumCategory where Id=@id";

            SqlParameter[] sqls1 = { new SqlParameter("@id", e.CommandArgument.ToString()) };

            int result1 = sqlh.sqlhelper(sql1, sqls1);

            if (result1 > 0)

                Response.Write("<script>alert('删除成功');location='/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "'</script>");

            else

                Response.Write("<script>alert('删除失败')</script>");
        }
    }
}
