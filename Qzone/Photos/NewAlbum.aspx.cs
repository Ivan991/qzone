using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_NewAlbum : System.Web.UI.Page
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

                        string sqlc = "select * from AlbumCategory where OwnId=@id";//查找分类

                        SqlParameter[] sqlsc = { new SqlParameter("@id", id) };

                        DataTable dtc = sqlh.sqlselect(sqlc, sqlsc);

                        if (dtc.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtc.Rows.Count; i++)
                            {
                                string idc = dtc.Rows[i][0].ToString();

                                string categoryc = dtc.Rows[i][1].ToString();

                                category.Items.Add(new ListItem(categoryc, idc));//循环动态绑定分类

                            }
                        }
                        else
                        {
                            newc.Visible = true;

                            category.Visible = false;
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


    protected void btnupload_Click(object sender, EventArgs e)    //上传文件
    {
        string albumname1 = albumname.Text.Trim();

        string describe1 = describe.Text.Trim();

        string category1 = category.SelectedValue;//选择分类

        if (albumname1.Length > 0 && describe1.Length > 0 && category1.Length > 0)
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

                string fileExt = System.IO.Path.GetExtension(upload.FileName).ToLower();

                filename = "/PhotoFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;  //设置文件名

                upload.SaveAs(Server.MapPath(filename));//存储文件

                string sql = "insert into Album (AlbumName,OwnId,Describe,AlbumImage,State,Dates,Category)values(@albumname,@ownid,@describe,@albumimage,@state,@dates,@category)";

                string state1 = state.SelectedValue;

                string dates = DateTime.Now.ToString();

                SqlParameter[] sqls =
                {
                new SqlParameter("@albumname",albumname1),

                new SqlParameter("@albumimage",filename),

                new SqlParameter("@dates",dates),

                new SqlParameter("@ownid",id),

                new SqlParameter("@describe",describe1),

                new SqlParameter("@state",state1),

                new SqlParameter("@category",category1)
                };

                int result = sqlh.sqlhelper(sql, sqls);

                if (result > 0)


                    Response.Write("<script>alert('新建相册成功');location='/Photos/Photo.aspx?id=" + id + "'</script>");

                else

                    Response.Write("<script>alert('上传图片失败，图片类型不相符')</script>");


            }
            else
            {
                Response.Write("<script>alert('上传图片失败，图片类型不相符')</script>");
            }
        }

        else

            Response.Write("<script>alert('相册名字和描述和分类不能为空')</script>");

    }



    protected void btnback_Click(object sender, EventArgs e)
    {

        Response.Redirect("/Photos/Photo.aspx?id=" + Request.QueryString["id"] + "");

    }

    protected void newc_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Photos/ECategory1.aspx?id=" + Session["id"].ToString() + "");
    }
}