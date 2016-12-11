using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Qzone_Blog : System.Web.UI.Page
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

                        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id等于空间用户id
                        {
                            LinkButton editorimage = (LinkButton)((MasterPage)Master).FindControl("editorimage");

                            LinkButton editorqname = (LinkButton)((MasterPage)Master).FindControl("editorqname");

                            editorimage.Visible = false;//修改空间头像的控件隐藏出来

                            editorqname.Visible = false;//修改空间名的控件隐藏出来

                            reportblog.Visible = false;//隐藏发表日志

                            ecategory.Visible = false;//隐藏修改分类

                        }
                        string sqlc = "select * from BlogCategory where OwnId=@id";//查找分类

                        SqlParameter[] sqlsc = { new SqlParameter("@id", id) };

                        DataTable dtc = sqlh.sqlselect(sqlc, sqlsc);

                        for (int i = 0; i < dtc.Rows.Count; i++)
                        {
                            string idc = dtc.Rows[i][0].ToString();

                            string categoryc = dtc.Rows[i][1].ToString();

                            category.Items.Add(new ListItem(categoryc, idc));//循环动态绑定分类

                        }

                        PagedDataSource bind = categ(1);

                        blog.DataSource = bind;  //把分页对象绑定到repeater

                        blog.DataBind();//把绑定的数据显示出来

                        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                        string sql1 = "select * from Blog where PoId=@id";  //查找出日志数量并显示出来

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        lbcount.Text = dt1.Rows.Count.ToString();
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


    protected void blog_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["id"].ToString() != Session["id"].ToString())
            {
                ((LinkButton)e.Item.FindControl("delete")).Visible = false;  //找到repeater blog 中的所有删除的控件

                ((LinkButton)e.Item.FindControl("editor")).Visible = false;  //找到repeater blog 中的所有编辑的控件

                ((Label)e.Item.FindControl("lbstate")).Visible = false;  //找到repeater log 中的所有权限的控件                

            }

            else
            {
                Label lbstate = (Label)e.Item.FindControl("lbstate");  //获取权限值

                if (lbstate.Text == "1")

                    lbstate.Text = "所有人可见";

                else

                    lbstate.Text = "仅自己可见";
            }
        }
    }

    protected void blog_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        if (e.CommandName == "delete")   //删除日志
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);   //获取当前页的id

            string sql2 = "delete from Blog where BlogId=@blogid";

            int blogid = Convert.ToInt32(e.CommandArgument.ToString());

            SqlParameter[] sqls2 = { new SqlParameter("@blogid", blogid) };

            int result = sqlh.sqlhelper(sql2, sqls2);                      //进行删除

            if (result > 0)

                Response.Write("<script>alert('删除成功');location='/Blogs/Blog.aspx?id=" + id + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('删除失败')</script>");

        }
    }

    PagedDataSource categ(int pages)//返回分页函数
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        string categoryitem = category.SelectedValue;


        if (categoryitem == "所有日志")
        {
            string sql0 = "select * from BlogC where PoId=@id order by Dates desc";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            return bind;

        }
        else
        {
            string sql0 = "select * from BlogC where PoId=@id and Category=@cate order by Dates desc";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id), new SqlParameter("@cate", categoryitem) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            return bind;

        }

    }


    protected void btnFirst_Click(object sender, EventArgs e)
    {

        PagedDataSource bind = categ(1);

        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

        lbNow.Text = "1";     //当前页设为1

        blog.DataSource = bind;

        blog.DataBind();
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            PagedDataSource bind = categ(pages);

            lbTotal.Text = bind.PageCount.ToString();  //显示总页数

            blog.DataSource = bind;

            blog.DataBind();
        }

    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师
            {
                PagedDataSource bind = categ(pages);


                lbNow.Text = Convert.ToString(pages);             //设置当前页数

                blog.DataSource = bind;

                lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                blog.DataBind();
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

            PagedDataSource bind = categ(pages);

            blog.DataSource = bind;

            lbTotal.Text = bind.PageCount.ToString();  //显示总页数

            blog.DataBind();
        }



    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        PagedDataSource bind = categ(pages);

        lbNow.Text = pages.ToString();     //当前页设为最后一页

        blog.DataSource = bind;

        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

        blog.DataBind();    //显示最后一页
    }


    protected void reportblog_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/NewBlog.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void ecategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/ECategory.aspx?id="+Request.QueryString["id"].ToString()+"");
    }
}