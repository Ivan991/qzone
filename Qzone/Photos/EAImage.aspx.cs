using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos_EAImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id1"] != null && Session["id"].ToString()==Request.QueryString["id"] && r.IsMatch(Request.QueryString["id1"]) && r.IsMatch(Request.QueryString["id"]))//判断地址栏传过来的id是否为数字以及是否为空
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);   //获取地址栏中的用户的id

                    string sql = "select * from Users where Id=@id";  //查找该用户是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    string sql1 = "select * from Album where AlbumId=@id";

                    SqlParameter[] sqls1 = { new SqlParameter("@id", Request.QueryString["id1"]) };

                    DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                    if (dt.Rows.Count > 0  &&  dt1.Rows.Count>0 && Session["id"].ToString() == Request.QueryString["id"].ToString())  //如果该用户存在该相册存在

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像

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

    protected void btnupload_Click(object sender, EventArgs e)
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
            int id = Convert.ToInt32(Request.QueryString["id1"].ToString());

            string fileExt = System.IO.Path.GetExtension(upload.FileName).ToLower();

            filename = "/PhotoFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;  //设置文件名

            upload.SaveAs(Server.MapPath(filename));//存储文件

            string sql = "update Album set AlbumImage=@image where AlbumId=@id";

            SqlParameter[] sqls = { new SqlParameter("@image", filename), new SqlParameter("@id", id) };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('修改相册封面成功');location='/Photos/Album.aspx?id=" + Request.QueryString["id"] + "'</script>");

            else

                Response.Write("<script>alert('修改相册封面失败')</script>");
        }

        else

            Response.Write("<script>alert('图片类型不符！')</script>");
    }
}
