using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_EPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);   //获用户的id

                string sql = "select * from Users where Id=@id";  //查找该用户

                SqlParameter[] sqls = { new SqlParameter("@id", id) };

                DataTable dt = sqlh.sqlselect(sql, sqls);

                Label qname = (Label)((MasterPage)Master).FindControl("qname");

                Label username = (Label)((MasterPage)Master).FindControl("username");

                Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

            }
            else

                Response.Redirect("/MyLogin.aspx");
        }


    }


    protected void btneditor_Click(object sender, EventArgs e)
    {
        string oldpwd0 = oldpwd.Text.Trim();

        string newpwd0 = newpwd.Text.Trim();

        string newpwd01 = newpwd1.Text.Trim();

        if (oldpwd0.Length > 0 && newpwd0.Length > 0 && newpwd01.Length > 0)
        {

            if (newpwd0 == newpwd01)
            {
                string pwd = sqlh.MD5(oldpwd0);

                string sql = "select * from Users where Id=@id ";

                SqlParameter[] sqls = { new SqlParameter("@id", Session["id"].ToString()) };

                DataTable dt = sqlh.sqlselect(sql, sqls);

                if (dt.Rows[0][2].ToString() == pwd)
                {

                    string sql1 = "update Users set Password=@password where Id=@id";

                    SqlParameter[] sqls1 = { new SqlParameter("@password", pwd), new SqlParameter("@id", Session["id"].ToString()) };

                    int result = sqlh.sqlhelper(sql1, sqls1);

                    if (result > 0)
                    {
                        Session["id"] = null;

                        Session["name"] = null;

                        Session["image"] = null;

                        Response.Write("<script>alert('修改密码成功请重新登录');location='/MyLogin.aspx'</script>");
                    }
                    else

                        Response.Write("<script>alert('修改密码失败请重试')</script>");

                }
                else

                    Response.Write("<script>alert('输入的旧密码错误')</script>");
            }
            else

                Response.Write("<script>alert('两次输入的新密码必须一致')</script>");

        }
        else

            Response.Write("<script>alert('输入不能为空')</script>");
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("/User/Users.aspx?id=" + Session["id"].ToString() + "");
    }
}