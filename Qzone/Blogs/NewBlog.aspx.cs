using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blogs_NewBlog : System.Web.UI.Page
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
                    
                        string sqlc = "select * from BlogCategory where OwnId=@id";//查找分类

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

    protected void btnreportblog_Click(object sender, EventArgs e)//发表日志
    {

        string text = editor.InnerHtml;//获取编辑器中的内容

        string category1 = category.SelectedValue;//选择分类的值

        string title = reporttitle.Text.Trim();//标题

        string state1 = state.SelectedValue;//权限

        if (text.Length > 0 && title.Length > 0 && category1.Length>0 && state1.Length>0)
        {
            string text1 = sqlh.change(text);

            string dates = DateTime.Now.ToString();

            string poid = Session["id"].ToString();//发表者id

            string poname = Session["name"].ToString();//发表者名字

            string sql = "insert into Blog (PoId,Title,Contents,Dates,Comments,PoName,Category,PoImage,State) values (@poid,@title,@text,@dates,'0',@poname,@category,@poimage,@state) ";

            SqlParameter[] sqls =
           {

                    new SqlParameter("@text", text1),

                    new SqlParameter("@title",title),

                    new SqlParameter("@poid",poid),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@poname",poname),

                    new SqlParameter("@category",category1),

                    new SqlParameter("@poimage",Session["image"].ToString()),

                    new SqlParameter("@state",state1)

                };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('发表日志成功');location='/Blogs/Blog.aspx?id=" + poid + "'</script>");    //刷新界面

            else

                Response.Write("<script>alert('发表失败')</script>");
        }

        else

            Response.Write("<script>alert('发表日志内容不能为空')</script>");

    }
}