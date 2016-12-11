using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_PhotoEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if ( r.IsMatch(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Session["id"]);

                    string sql = "select * from Users where Id=@id";

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    int photoid = Convert.ToInt32(Request.QueryString["id"]);

                    string sql1 = "select * from Photo where PhotoId=@id";

                    SqlParameter[] sqls1 = { new SqlParameter("@id", photoid) };

                    DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                    if (dt1.Rows.Count > 0)//照片id是否找得到

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

                        describe.Text = dt1.Rows[0][4].ToString();//把之前的照片描述显示出来

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

    protected void btneditor_Click(object sender, EventArgs e)
    {
        string textdescribe = describe.Text;

        string sql = "update Photo set Message=@contents where PhotoId=@id";

        SqlParameter[] sqls = { new SqlParameter("@contents", textdescribe), new SqlParameter("@id", Request.QueryString["id"]) };

        int result = sqlh.sqlhelper(sql, sqls);

        string sql1 = "select * from Photo where PhotoId=@id";

        SqlParameter[] sqls1= { new SqlParameter("@id", Request.QueryString["id"]) };

        DataTable dt = sqlh.sqlselect(sql1, sqls1);

        int id1 = Convert.ToInt32(dt.Rows[0][7].ToString());

        int id = Convert.ToInt32(dt.Rows[0][1].ToString());

        if (result > 0)
        {

            Response.Write("<script>alert('修改成功');location='/Photos/Images.aspx?id=" + id + "&id1=" + id1 + "'</script>");
        }

        else

            Response.Write("<script>alert('修改失败')</script>");
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        string sql1 = "select * from Photo where PhotoId=@id";

        SqlParameter[] sqls1 = { new SqlParameter("@id", Request.QueryString["id"]) };

        DataTable dt = sqlh.sqlselect(sql1, sqls1);

        int id1 = Convert.ToInt32(dt.Rows[0][7].ToString());

        int id = Convert.ToInt32(dt.Rows[0][1].ToString());

        Response.Redirect("/Photos/Images.aspx?id=" + id + "&id1="+id1+"");
    }
}