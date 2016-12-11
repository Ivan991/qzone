using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Session["id"] = null;

            Session["name"] = null;

            Session["image"] = null;

            string sql0 = "select * from Users where State=0";

            SqlParameter[] sqls0 = { };

            DataTable dt = sqlh.sqlselect(sql0, sqls0);

            if (dt.Rows.Count > 0)
            {

                string sql = "delete from Users where State=0";

                SqlParameter[] sqls = { };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)

                    Response.Write("<script>alert('数据更新成功')</script>");

                else

                    Response.Write("<script>alert('数据更新失败');location='/MyLogin.aspx'</script>");

            }

        }
    }

    

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        string name1 = name.Text.Trim();

        string pwd1 = pwd.Text.Trim();

        string code1 = code.Text.Trim();

        if (code1 == Session["vcode"].ToString())
        {
            string pwd2 = sqlh.MD5(pwd1);

            string sqlselect = "select * from Users where Name=@name";

            SqlParameter[] paras = { new SqlParameter("@name", name1) };

            DataTable dt = sqlh.sqlselect(sqlselect, paras);

            if (dt.Rows.Count > 0)//当用户存在的时候
            {

                if (dt.Rows[0][2].ToString() == pwd2)
                {

                    Session["name"] = name1;

                    Session["id"] = dt.Rows[0][0].ToString();

                    Session["image"] = dt.Rows[0][11].ToString();

                    Response.Write("<script>alert('登录成功'</script>");

                    Response.Redirect("Qzone.aspx?id=" + Session["id"].ToString() + "");
                }

                else

                    Response.Write("<script>alert('密码输入错误')</script>");
            }
            else

            Response.Write("<script>alert('该用户不存在')</script>");


        }

        else

            Response.Write("<script>alert('验证码输入错误')</script>");
    }

  
}