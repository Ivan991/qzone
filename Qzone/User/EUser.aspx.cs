using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EUser : System.Web.UI.Page
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

                        textbox.Text = dt.Rows[0][1].ToString();

                        textbox1.Text = dt.Rows[0][3].ToString();

                        textbox2.Text = dt.Rows[0][4].ToString();

                        textbox3.Text = dt.Rows[0][5].ToString();

                        textbox4.Text = dt.Rows[0][6].ToString();

                        textbox5.Text = dt.Rows[0][7].ToString();

                        textbox6.Text = dt.Rows[0][8].ToString();
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
        string text = textbox.Text.Trim();

        string text1 = textbox1.Text.Trim();

        string text2 = textbox2.SelectedValue;

        string text3 = textbox3.Text.Trim();

        string text4 = textbox4.Text.Trim();

        string text5 = textbox5.Text.Trim();

        string text6 = textbox6.Text.Trim();

        if(text.Length>0&& text1.Length > 0 && text2.Length > 0 && text3.Length>0&&text4.Length > 0 && text5.Length > 0 && text6.Length > 0 )
        {
            string sql = "update Users set Name=@name,UsersName=@username,Sex=@sex,Email=@email,Telephone=@telephone,Birthday=@birthday,Blood=@blood where id=@id";

            SqlParameter[] sqls =
            {
                new SqlParameter("@name",text),

                new SqlParameter("@username",text1),

                new SqlParameter("@sex",text2),

                new SqlParameter("@email",text3),

                new SqlParameter("@telephone",text4),

                new SqlParameter("@birthday",text5),

                new SqlParameter("@blood",text6),
                
                new SqlParameter("@id",Request.QueryString["id"])
            };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('修改成功');location='/User/Users.aspx?id=" + Session["id"] + "'</script>");

            else

                Response.Write("<script>alert('修改失败')</script>");

        }

        else
            Response.Write("<script>alert('输入不能为空')</script>");

    }
}