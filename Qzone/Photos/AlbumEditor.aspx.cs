using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_AlbumEditor : System.Web.UI.Page
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

                    int id1 = Convert.ToInt32(Request.QueryString["id1"]);//获取相册id

                    string sql = "select * from Users where Id=@id";  //查找该用户是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString()==Session["id"].ToString())  //如果该用户存在并且等于登录id

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

                        string sql1 = "select * from Album where AlbumId=@id";

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id1) }
;
                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        text.Text = dt1.Rows[0][1].ToString();//相册名字

                        TextBox1.Text = dt1.Rows[0][3].ToString();//相册描述

                        image.ImageUrl = dt1.Rows[0][4].ToString();//相册封面

                       
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

    protected void editor_Click(object sender, EventArgs e)
    {

        string name = text.Text.Trim();//相册名字

        string describe = TextBox1.Text.Trim();//相册描述

        string state1 = state.SelectedValue;

        if (name.Length > 0 && describe.Length > 0 && state1.Length > 0)
        {
            string sql = "update Album set AlbumName=@name,Describe=@describe,State=@state where AlbumId=@id";

            SqlParameter[] sqls =
            {
                new SqlParameter("@name",name),

                new SqlParameter("@describe",describe),

                new SqlParameter("@state",state1),

                new SqlParameter("@id",Request.QueryString["id1"])
            };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('修改成功');location='/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "'</script>");

            else

                Response.Write("<script>alert('修改失败请重试')</script>");
        }
        Response.Write("<script>alert('内容不能为空')</script>");

    }

    protected void editorimage_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/EAImage.aspx?id=" + Request.QueryString["id"] + "&id1=" + Request.QueryString["id1"] + "");

    }
}