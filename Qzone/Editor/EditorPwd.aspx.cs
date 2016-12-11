using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditorPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (Session["name"] == null)

            Response.Redirect("/MyLogin.aspx");

        else

            name.Text = Session["name"].ToString();


    }

    protected void btn_Click(object sender, EventArgs e)
    {

        string npwd = newpwd.Text.Trim();

        string name1 = Session["name"].ToString();

        if(npwd.Length>0)
        {
            string sql = "update Users set Password =@npwd where Name=@name";

            string npwd1 = sqlh.MD5(npwd);

            SqlParameter[] sqls =
           { new SqlParameter("@npwd", npwd1),
             new SqlParameter("@name",name1)
            };

            int result = sqlh.sqlhelper(sql, sqls);

            if ( result > 0)
            {
                Response.Write("<script>alert('密码修改成功请重新登录');location='/MyLogin.aspx'</script>");

                Session["name"] = null;

            }
            else

                Response.Write("<script>alert('修改失败请重试')</script>");

        }
        else

            Response.Write("<script>alert('输入不能为空')</script>");

    }

    protected void back_Click(object sender, EventArgs e)
    {
        Session["name"] = null;

        Response.Redirect("/MyLogin.aspx");
    }
}