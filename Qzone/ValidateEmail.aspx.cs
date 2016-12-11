using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ValidateEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {
      
    }

    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        string name = Session["name"].ToString();

        string code = email.Text.Trim();

        if (code == Request.Cookies["code1"].Value)
        {
            string sqlc = "update Users set State='1' where Name=@name";

            SqlParameter[] sqls = { new SqlParameter("@name", name) };

            int result = sqlh.sqlhelper(sqlc, sqls);

            if (result > 0)

                Response.Write("<script>alert('注册确认成功！请登录');location='/MyLogin.aspx'</script>");

            else

                Response.Write("<script>alert('请重试！')</script>");
        }
        else

            Response.Write("<script>alert('验证码错误！')</script>");
    }
}