using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Qzone_Photo : System.Web.UI.Page
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
                    int id = Convert.ToInt32(Request.QueryString["id"]);

                    string sql = "select * from Users where Id=@id";  //查找id是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    if (dt.Rows.Count > 0)
                    {

                        if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id
                        {
                            LinkButton editorimage = (LinkButton)((MasterPage)Master).FindControl("editorimage");

                            LinkButton editorqname = (LinkButton)((MasterPage)Master).FindControl("editorqname");

                            editorimage.Visible = false;//修改空间头像的控件隐藏出来

                            editorqname.Visible = false;//修改空间名的控件隐藏出来

                            lbnewalbum.Visible = false;//显示新建相册和编辑和删除的控件

                            string sql0 = "select * from AlbumC where OwnId=@id and State=1 order by Dates desc";

                            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                            PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

                            photo.DataSource = bind;  //把分页对象绑定到repeater

                            photo.DataBind();

                            string sql1 = "select * from AlbumC where OwnId=@id and State=1 order by Dates desc";

                            SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                            DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                            lbacount.Text = dt1.Rows.Count.ToString();

                            lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                            Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

                            Label username = (Label)((MasterPage)Master).FindControl("username");

                            Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                            Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                            qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                            username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                            welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                            userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

                            ecategory.Visible = false;
                        }
                        else
                        {
                            string sql0 = "select * from AlbumC where OwnId=@id order by Dates desc";

                            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                            PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

                            photo.DataSource = bind;  //把分页对象绑定到repeater

                            photo.DataBind();

                            string sql1 = "select * from AlbumC where OwnId=@id order by Dates desc";

                            SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                            DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                            lbacount.Text = dt1.Rows.Count.ToString();

                            lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                            Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

                            Label username = (Label)((MasterPage)Master).FindControl("username");

                            Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                            Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                            qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                            username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                            welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                            userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像                           
                        }
                        string sqlc = "select * from AlbumCategory where OwnId=@id";//查找分类

                        SqlParameter[] sqlsc = { new SqlParameter("@id", id) };

                        DataTable dtc = sqlh.sqlselect(sqlc, sqlsc);

                        for (int i = 0; i < dtc.Rows.Count; i++)
                        {
                            string idc = dtc.Rows[i][0].ToString();

                            string categoryc = dtc.Rows[i][1].ToString();

                            category.Items.Add(new ListItem(categoryc, idc));//循环动态绑定分类

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

    PagedDataSource categ(int pages)//返回分页函数
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        string categoryitem = category.SelectedValue;

        if (categoryitem == "所有相册")
        {
            string sql0 = "";

            if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id

                sql0 = "select * from AlbumC where OwnId=@id State=1 order by Dates desc";

            else

                sql0 = "select * from AlbumC where OwnId=@id order by Dates desc";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            return bind;

        }
        else
        {
            string sql0 = "";

            if (Session["id"].ToString() != Request.QueryString["id"].ToString())//如果登录id不等于空间用户id

                sql0 = "select * from AlbumC where OwnId=@id and Category=@cate and State=1 order by Dates desc";
            else

                sql0 = "select * from AlbumC where OwnId=@id and Category=@cate  order by Dates desc";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id), new SqlParameter("@cate", categoryitem) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            return bind;

        }

    }


    protected void btnFirst_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        PagedDataSource bind = categ(1);

        photo.DataSource = bind;  //把分页对象绑定到repeater

        photo.DataBind();

        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

        lbNow.Text = "1";     //当前页设为1

    }

    protected void btnUp_Click(object sender, EventArgs e)//上一页
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            PagedDataSource bind = categ(pages);

            photo.DataSource = bind;  //把分页对象绑定到repeater

            photo.DataBind();

            lbTotal.Text = bind.PageCount.ToString();  //显示总页数
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

                PagedDataSource bind = categ(pages);

                photo.DataSource = bind;  //把分页对象绑定到repeater

                photo.DataBind();

                lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                lbNow.Text = Convert.ToString(pages);             //设置当前页数

            }

            else

                Response.Write("<script>alert('输入页数不在范围内')</script>");

        }

        else

            Response.Write("<script>alert('输入页数不能为空')</script>");
    }

    protected void btnDrow_Click(object sender, EventArgs e)//下一页
    {
        if (Convert.ToInt32(lbNow.Text) < Convert.ToInt32(lbTotal.Text))                                             //如果当前页数不是尾页
        {

            int id = Convert.ToInt32(Request.QueryString["id"]);

            int pages = Convert.ToInt32(lbNow.Text) + 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数+1

            PagedDataSource bind = categ(pages);

            photo.DataSource = bind;  //把分页对象绑定到repeater

            photo.DataBind();

            lbTotal.Text = bind.PageCount.ToString();  //显示总页数

        }



    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        PagedDataSource bind = categ(pages);

        photo.DataSource = bind;  //把分页对象绑定到repeater

        photo.DataBind();

        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

        lbNow.Text = pages.ToString();     //当前页设为最后一页

    }



    protected void photo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Request.QueryString["id"].ToString() != Session["id"].ToString())
        {

            ((LinkButton)e.Item.FindControl("albumeditor")).Visible = false;  //找到repeater photo 中的所有删除和编辑的控件

            ((LinkButton)e.Item.FindControl("albumdelete")).Visible = false;

            ((Label)e.Item.FindControl("astate")).Visible = false;

        }
        else
        {
            Label state = (Label)e.Item.FindControl("astate");

            if (state.Text == "1")

                state.Text = "所有人可见";

            else

                state.Text = "仅自己可见";

        }

    }

    protected void photo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        if (e.CommandName == "delete")  //删除这个相册
        {
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());  //获取该相册的id

            string sql = "delete from Album where AlbumId=@id";

            SqlParameter[] sqls = { new SqlParameter("@id", albumid) };

            int result = sqlh.sqlhelper(sql, sqls);   //执行删除操作

            string sql1 = "delete from Photo where AlbumId=@id";//删除相册里的所有照片

            SqlParameter[] sqls1 = { new SqlParameter("@id", albumid) };

            int result1 = sqlh.sqlhelper(sql1, sqls1);

            if (result > 0 && result1>0)

                Response.Write("<script>alert('删除相册成功');location='/Photos/Photo.aspx?id=" + id + "'</script>");

            else

                Response.Write("<script>alert('删除相册失败')</script>");
        }

        if (e.CommandName == "image")
        {
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());

            Response.Redirect("Images.aspx?id=" + id + "&id1=" + albumid + "");
        }

    }

    protected void lbnewalbum_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/NewAlbum.aspx?id=" + Request.QueryString["id"] + "");
    }

    protected void ecategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/ECategory1.aspx?id=" + Request.QueryString["id"].ToString() + "");
    }
}