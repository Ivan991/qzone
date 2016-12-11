using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && r.IsMatch(Request.QueryString["id"]))//判断地址栏传过来的id是否为数字以及是否为空
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

                        string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

                        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

                        friends.DataSource = bind;  //把分页对象绑定到repeater

                        friends.DataBind();//把绑定的数据显示出来

                        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                        string sql1 = "select * from Friends where OwnId=@id";  //查找出好友数量并显示出来

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        lbcount.Text = dt1.Rows.Count.ToString();

                        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id
                        {
                            LinkButton editorimage = (LinkButton)((MasterPage)Master).FindControl("editorimage");

                            LinkButton editorqname = (LinkButton)((MasterPage)Master).FindControl("editorqname");

                           
                            editorimage.Visible = false;//修改空间头像的控件显示出来

                            editorqname.Visible = false;//修改空间名的控件显示出来

                            newfriend.Visible = false;//添加好友的控件隐藏起来
                        }
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

    protected void friends_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="report"||e.CommandName=="report1"||e.CommandName=="report2")
        {
            Response.Redirect("/Qzone.aspx?id=" + e.CommandArgument.ToString() + "");//跳转到好友空间
        }

        if(e.CommandName=="cancel")
        {
            string sql = "delete from Friends where FriendId=@id";//从列表中删除

            SqlParameter[] sqls = { new SqlParameter("@id", e.CommandArgument.ToString()) };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('取消关注成功');location='/Friend/Friends.aspx?id=" + Request.QueryString["id"] + "'</script>");

            else

                Response.Write("<script>alert('操作失败请重试')</script>");
        }
    }

    protected void friends_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Request.QueryString["id"].ToString() != Session["id"].ToString())
        {

                LinkButton editor = (LinkButton)e.Item.FindControl("editor");  //找到repeater friends 中的所有取消关注的控件

                editor.Visible = false;
        }
    }


    protected void btnFirst_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

        lbNow.Text = "1";     //当前页设为1

        friends.DataSource = bind;

        friends.DataBind();
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            friends.DataSource = bind;

            friends.DataBind();
        }

    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

                SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

                lbNow.Text = Convert.ToString(pages);             //设置当前页数

                friends.DataSource = bind;

                friends.DataBind();
            }

            else

                Response.Write("<script>alert('输入页数不在范围内')</script>");

        }

        else

            Response.Write("<script>alert('输入页数不能为空')</script>");
    }

    protected void btnDrow_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) < Convert.ToInt32(lbTotal.Text))                                             //如果当前页数不是尾页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) + 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数+1

            string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            friends.DataSource = bind;

            friends.DataBind();
        }



    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        string sql0 = "select * from View_1 where OwnId=@id";  //查找该用户的好友

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

        lbNow.Text = pages.ToString();     //当前页设为最后一页

        friends.DataSource = bind;

        friends.DataBind();    //显示最后一页
    }

    protected void newfriend_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Friend/FindFriend.aspx?id="+Request.QueryString["id"]+"");
    }
}