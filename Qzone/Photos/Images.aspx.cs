using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_Images : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (!IsPostBack)
        {

            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id1"] != null && r.IsMatch(Request.QueryString["id1"]) && r.IsMatch(Request.QueryString["id"]))//判断地址栏传过来的id是否为数字以及是否为空
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);//空间主人id

                    int id1 = Convert.ToInt32(Request.QueryString["id1"]);//相册id

                    string sql = "select * from Users where Id=@id";  //查找id是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    string sql0 = "select * from Album where AlbumId=@id ";  //找出该相册

                    SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    DataTable dt0 = sqlh.sqlselect(sql0, sqls0);

                    if (dt.Rows.Count > 0 && dt0.Rows.Count > 0)  //空间id与相册id均存在的时候

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

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

                            upload.Visible = false;//隐藏上传图片的控件

                            uploadname.Visible = false;//隐藏标签

                            uploadname1.Visible = false;

                            describe.Visible = false;//隐藏新建相片描述的空间

                            //uploadimage.Visible = false;

                            btnupload.Visible = false;
                        }

                        string sql1 = "select * from Photo where AlbumId=@id order by Dates desc";  //按时间顺序找出该相册的照片

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id1) };

                        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql1, sqls1);

                        photos.DataSource = bind;  //把分页对象绑定到repeater

                        photos.DataBind();//把repeater显示出来

                        string sql2 = "select * from Photo where AlbumId=@id order by Dates desc";  //按时间顺序找出该相册的照片

                        SqlParameter[] sqls2 = { new SqlParameter("@id", id1) };

                        DataTable dt2 = sqlh.sqlselect(sql2, sqls2);

                        lbTotal.Text = bind.PageCount.ToString();  //显示总页数

                        lbcount.Text = dt2.Rows.Count.ToString();//照片总数

                        albumname.Text = dt0.Rows[0][1].ToString();//相册名

                        albumimage.ImageUrl = dt0.Rows[0][4].ToString();//相册封面

                        albumdescribe.Text = dt0.Rows[0][3].ToString();//相册描述

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

    protected void photos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView rowv = (DataRowView)e.Item.DataItem;    //查找外层repeater

            int id = Convert.ToInt32(rowv["PhotoId"]);        //提取外层repeater中的数据

            string sql = "select * from ComPhoto where ToId=@toid order by Dates ";    //在数据库中查找

            SqlParameter[] sqls = { new SqlParameter("@toid", id) };

            DataTable dt = sqlh.sqlselect(sql, sqls);

            Repeater com = (Repeater)e.Item.FindControl("comphotos");  //找到内嵌repeater

            com.DataSource = dt;    //绑定数据

            com.DataBind();

            if (Request.QueryString["id"].ToString() != Session["id"].ToString())
            {

                ((LinkButton)e.Item.FindControl("editor")).Visible = false;  //找到repeater photos 中的所有删除和编辑的控件

                ((LinkButton)e.Item.FindControl("delete")).Visible = false;

            }
            else

                ((LinkButton)e.Item.FindControl("report")).Visible = false;//把转发的控件隐藏起来

        }
    }

    protected void photos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "btncom")     //评论
        {

            TextBox box = (TextBox)e.Item.FindControl("comment");


            if (box.Text.Trim().Length > 0)
            {
                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid = Convert.ToInt32(str1[0]);   //照片id

                string contents = box.Text.Trim();

                int parentid = Convert.ToInt32(Request.QueryString["id"]);//被回复人的id

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"]))

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

                int userid = Convert.ToInt32(Session["id"].ToString());//回复者的id

                string dates = DateTime.Now.ToString();//时间

                string username = Session["name"].ToString();//回复人的名字

                string sql1 = "select Name from Users where Id=@parentid";     //找出被回复的名字

                SqlParameter[] sqls1 = { new SqlParameter("@parentid", parentid) };

                DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                string parentname = dt1.Rows[0][0].ToString();

                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",toid),

                    new SqlParameter("@parentid",parentid),

                    new SqlParameter("@userid",userid),

                    new SqlParameter("@contents",contents),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@username",username),

                    new SqlParameter("@parentname",parentname)

                };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)
                {
                    string sql2 = "update Photo set Comments=Comments+1 where PhotoId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='/Photos/Image.aspx?id=" + parentid + "&id1=" + Request.QueryString["id1"] + "'</script>");
                }

                else

                    Response.Write("<script>alert('评论失败')</script>");

            }
            else

                Response.Write("<script>alert('评论不能为空')</script>");

        }
        if (e.CommandName == "report1")    //按转发显示转发框
        {
          

            string sql = "select * from Album where OwnId=@id";

            SqlParameter[] sqls = { new SqlParameter("@id",Session["id"]) };

            DataTable dt = sqlh.sqlselect(sql, sqls);

            if(dt.Rows.Count>0)
            {
                ((TextBox)e.Item.FindControl("reporter")).Visible = true;

                ((Button)e.Item.FindControl("btnreporter")).Visible = true;

                ((Label)e.Item.FindControl("lbreport")).Visible = true;

                ((DropDownList)e.Item.FindControl("album")).Visible = true;
            }

            else

                ((LinkButton)e.Item.FindControl("add")).Visible = true;

        }

        if (e.CommandName == "btnreporter1")//按转发
        {
            TextBox txt = (TextBox)e.Item.FindControl("reporter");

            ((Button)e.Item.FindControl("btnreporter")).Visible = false;

            ((Label)e.Item.FindControl("lbreport")).Visible = false;

            DropDownList drop = (DropDownList)e.Item.FindControl("album");

            txt.Visible = false;

            drop.Visible = false;

            string sqlre = "insert into Photo (PoId,Contents,Dates,Message,Comments,PoName,AlbumId,PoImage) values (@PoId,@Contents,@Dates,@message,'0',@PoName,@albumid,@poimage) ";

            string poid = Session["id"].ToString();  //转发的人的id  

            string poname = Session["name"].ToString();  //转发的人的名字

            string dates1 = DateTime.Now.ToString();    //转发时间

            string poimage = Session["image"].ToString();

            string message = txt.Text.Trim();  //转发照片描述

            string albumid = drop.SelectedValue;//转发到哪个相册

            string contents1 = e.CommandArgument.ToString();//转发图片路径

            SqlParameter[] sqlsre =
            {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@message",message),

                    new SqlParameter("@PoName",poname),

                    new SqlParameter("@albumid",albumid),

                    new SqlParameter("@poimage",poimage)

                };

            int resultre = sqlh.sqlhelper(sqlre, sqlsre);

            if (resultre > 0)

                Response.Write("<script>alert('转发成功');location='/Photos/Photo.aspx?id=" + poid + "'</script>");

            else

                Response.Write("<script>alert('转发失败')</script>");

        }

        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql0 = "delete from Photo where PhotoId=@id";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            int result0 = sqlh.sqlhelper(sql0, sqls0);

            if (result0 > 0)

                Response.Write("<script>alert('删除成功');location='/Photos/Images.aspx?id=" + Request.QueryString["id"] + "&id1=" + Request.QueryString["id1"] + "'</script>");

            else

                Response.Write("<script>alert('删除失败')</script>");
        }
    }

    protected void btnupload_Click(object sender, EventArgs e)    //上传文件
    {


        string filename = "";

        Boolean fileOK = false;

        if (upload.HasFile)//判断是否已选取文件
        {
            String fileExtension = System.IO.Path.GetExtension(upload.FileName).ToLower(); //取得文件的扩展名

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };//限定上传文件类型为这几种类型

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])   //判断文件的类型是否为图片类型一个个匹对
                {
                    fileOK = true;
                }
            }
        }
        sqlh.FileExtension[] fe = { sqlh.FileExtension.BMP, sqlh.FileExtension.GIF, sqlh.FileExtension.JPG, sqlh.FileExtension.PNG };

        if (fileOK && sqlh.FileValidation.IsAllowedExtension(upload, fe))  //判断文件类型是否为bmp/gif/jpg/png以及检测头文件
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());

            int id1 = Convert.ToInt32(Request.QueryString["id1"].ToString());

            string fileExt = System.IO.Path.GetExtension(upload.FileName).ToLower();

            filename = "/PhotoFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;  //设置文件名

            upload.SaveAs(Server.MapPath(filename));//存储文件

            string sql = "insert into Photo (PoId,Contents,Dates,Message,Comments,PoName,AlbumId)values(@poid,@contents,@dates,@message,'0',@poname,@albumid)";

            int poid = Convert.ToInt32(Session["id"].ToString());

            string dates = DateTime.Now.ToString();

            string message = describe.Text.Trim();

            string poname = Session["name"].ToString();


            SqlParameter[] sqls =
            {
                new SqlParameter("@poid",poid),

                new SqlParameter("@contents",filename),

                new SqlParameter("@dates",dates),

                new SqlParameter("@message",message),

                new SqlParameter("@poname",poname),

                new SqlParameter("@albumid",id1)
            };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('上传图片成功');location='/Photos/Images.aspx?id=" + id + "&id1=" + id1 + "'</script>");

            else

                Response.Write("<script>alert('上传图片失败，图片类型不相符')</script>");

        }
        else
        {
            Response.Write("<script>alert('上传图片失败，图片类型不相符')</script>");
        }
    }




    protected void btnFirst_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id1"]);

        string sql0 = "select * from Photo where AlbumId=@id order by Dates ";  //按时间顺序找出该相册的照片

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(1, sql0, sqls0);

        photos.DataSource = bind;  //把分页对象绑定到repeater

        photos.DataBind();

        lbNow.Text = "1";     //当前页设为1

    }

    protected void btnUp_Click(object sender, EventArgs e)//上一页
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int id = Convert.ToInt32(Request.QueryString["id1"]);

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            string sql0 = "select * from Photo where AlbumId=@id order by Dates ";  //按时间顺序找出该相册的照片

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            photos.DataSource = bind;  //把分页对象绑定到repeater

            photos.DataBind();
        }

    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师
            {
                int id = Convert.ToInt32(Request.QueryString["id1"]);

                string sql0 = "select * from Photo where AlbumId=@id order by Dates ";  //按时间顺序找出该相册的照片

                SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

                PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

                lbNow.Text = Convert.ToString(pages);             //设置当前页数

                photos.DataSource = bind;  //把分页对象绑定到repeater

                photos.DataBind();
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

            int id = Convert.ToInt32(Request.QueryString["id1"]);

            int pages = Convert.ToInt32(lbNow.Text) + 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数+1

            string sql0 = "select * from Photo where AlbumId=@id order by Dates ";  //按时间顺序找出该相册的照片

            SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

            PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

            photos.DataSource = bind;  //把分页对象绑定到repeater

            photos.DataBind();
        }



    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id1"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        string sql0 = "select * from Photo where AlbumId=@id order by Dates ";  //按时间顺序找出该相册的照片

        SqlParameter[] sqls0 = { new SqlParameter("@id", id) };

        PagedDataSource bind = sqlh.DataBindToRepeater(pages, sql0, sqls0);

        photos.DataSource = bind;  //把分页对象绑定到repeater

        photos.DataBind();

        lbNow.Text = pages.ToString();     //当前页设为最后一页

    }



    protected void comphotos_ItemCommand(object source, RepeaterCommandEventArgs e)  //内嵌的repeater中的事件
    {
        if (e.CommandName == "delete1")  //执行删除评论这条操作
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);       //获取当前页的id

            string sql3 = "delete from ComPhoto where CommentId=@commentid";

            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);

            int toid = Convert.ToInt32(str[1]);

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {

                string sql2 = "update Photo set Comments=Comments-1 where PhotoId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Photos/Images.aspx?id=" + id + "&id1=" + Request.QueryString["id1"] + "'</script>");    //刷新界面
            }
            else

                Response.Write("<script>alert('删除失败')</script>");

        }
        if (e.CommandName == "commentor")     //点击回复的内容弹出评论框
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");

            Button btn = (Button)e.Item.FindControl("btncomment1");

            Label lb = (Label)e.Item.FindControl("lb");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }
        if (e.CommandName == "btncom1")    //回复这条评论
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");

            if (text.Text.Trim().Length > 0)
            {
                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid1 = Convert.ToInt32(str1[1]);

                string contents = text.Text.Trim();

                int parentid = Convert.ToInt32(str1[0]);

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"]))

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";


                int userid = Convert.ToInt32(Session["id"].ToString());

                string dates = DateTime.Now.ToString();

                string username = Session["name"].ToString();

                string parentname = str1[2];

                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",toid1),

                    new SqlParameter("@parentid",parentid),

                    new SqlParameter("@userid",userid),

                    new SqlParameter("@contents",contents),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@username",username),

                    new SqlParameter("@parentname",parentname)
                };

                int result1 = sqlh.sqlhelper(sql, sqls);     //对数据库进行操作

                if (result1 > 0)

                {
                    string sql2 = "update Photo set Comments=Comments+1 where PhotoId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid1) };

                    int result2 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    int sid = Convert.ToInt32(Request.QueryString["id"]);

                    Response.Write("<script>alert('评论成功！');location='/Photos/Images.aspx?id=" + sid + "&id1=" + Request.QueryString["id1"] + "'</script>");

                }

                else

                    Response.Write("<script>alert('评论失败！')</script>");

            }

            else

                Response.Write("<script>alert('评论失败！')</script>");

            Button btn = (Button)e.Item.FindControl("btncomment1");   //评论完毕

            Label lb = (Label)e.Item.FindControl("lb");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }

    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "");
    }



    protected void comphotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["id"].ToString() != Session["id"].ToString())

                ((LinkButton)e.Item.FindControl("delete1")).Visible = false;  //找到repeater comments 中的所有删除的控件
        }
    }

    protected void add_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/NewAlbum.aspx?id=" + Session["id"] + "");
    }
}