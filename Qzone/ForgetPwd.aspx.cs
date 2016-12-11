using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnfpwd_Click(object sender, EventArgs e)
    {
        string name1 = name.Text.Trim();

        string email1 = email.Text.Trim();

        string tel1 = tel.Text.Trim();

        string sql = "select Email,Telephone from Users where Name=@name";

        SqlParameter[] sqls = { new SqlParameter("@name", name1) };

        DataTable dt = sqlh.sqlselect(sql, sqls);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == email1 && dt.Rows[0][1].ToString() == tel1)
            {
                Response.Write("<script>alert('身份认证成功，请修改密码');location='/Editor/EditorPwd.aspx'</script>");

                Session["name"] = name1;
            }

            else

                Response.Write("<script>alert('身份认证错误')</script>");
        }
        else

            Response.Write("<script>alert('该用户不存在')</script>");
    }
}