using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewFriend_AddFriend : System.Web.UI.Page
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

                    int id1 = Convert.ToInt32(Request.QueryString["id1"]);//获取查找的用户的id

                    string sql = "select * from Users where Id=@id";  //查找该用户是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    string sql1 = "select * from View_1 where FriendId=@id1 and OwnId=@ownid";

                    SqlParameter[] sqls1 = { new SqlParameter("@id1", id1),new SqlParameter("@ownid",Session["id"]) };//查找出该用户是否为好友

                    DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                    string sql2 = "select * from Users where Id=@id";

                    SqlParameter[] sqls2 = { new SqlParameter("@id", id1) };

                    DataTable dt2 = sqlh.sqlselect(sql2, sqls2);//查找出这个人的信息

                    if (dt.Rows.Count > 0 )  //如果该用户存在

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

                        linkbutton.Text = dt2.Rows[0][1].ToString();//该用户的姓名

                        linkbutton1.Text = dt2.Rows[0][9].ToString();

                        image.ImageUrl = dt2.Rows[0][11].ToString();//空间头像

                        if (dt1.Rows.Count > 0)
                        {
                            lable.Text = "已关注";

                            btnadd.Visible = false;//把添加好友的按钮隐藏起来
                        }

                        else
                        
                            lable.Text = "未关注";
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
        string sql = "insert into Friends (OwnId,FriendId) values(@ownid,@friendid)";

        SqlParameter[] sqls = { new SqlParameter("@ownid", Session["id"].ToString()), new SqlParameter("@friendid", Request.QueryString["id1"]) };

        int result = sqlh.sqlhelper(sql, sqls);

        if (result > 0)

            Response.Write("<script>alert('添加好友成功');location='/Friend/Friends.aspx?id="+Request.QueryString["id"]+"'</script>");

        else

            Response.Write("<script>alert('添加好友失败')</script>");

    }

    protected void linkbutton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Request.QueryString["id1"] + "");
    }

    protected void linkbutton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Qzone.aspx?id=" + Request.QueryString["id1"] + "");

    }
}