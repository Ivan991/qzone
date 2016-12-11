using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blogs_BlogEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Regex r = new Regex("^[1-9]d*|0$");

            if (Session["id"] != null)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id1"]!=null&& Request.QueryString["id"] == Session["id"].ToString()&& r.IsMatch(Request.QueryString["id1"]) && r.IsMatch(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);//空间主人id

                    int id1 = Convert.ToInt32(Request.QueryString["id1"]);//日志id

                    string sql = "select * from Users where Id=@id";  //查找id是否存在

                    SqlParameter[] sqls = { new SqlParameter("@id", id) };

                    string sql0 = "select * from Blog where BlogId=@id ";  //找出该blog

                    SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

                    DataTable dt = sqlh.sqlselect(sql, sqls);

                    DataTable dt0 = sqlh.sqlselect(sql0, sqls0);

                    if (dt.Rows.Count > 0 && dt0.Rows.Count > 0)  //空间id与blogid均存在的时候

                    {
                        Label qname = (Label)((MasterPage)Master).FindControl("qname");//找到母版页中的控件

                        Label username = (Label)((MasterPage)Master).FindControl("username");

                        Label welcome = (Label)((MasterPage)Master).FindControl("welcome");

                        Image userimage = (Image)((MasterPage)Master).FindControl("userimage");

                        qname.Text = dt.Rows[0][9].ToString();  //qq空间名

                        username.Text = dt.Rows[0][1].ToString();//访问的空间用户的名字

                        welcome.Text = Session["name"].ToString();//登录的空间用户的名字

                        userimage.ImageUrl = dt.Rows[0][11].ToString();//赋值给头像
               
                        string sql1 = "select * from Blog where BlogId=@id";  //找出该blog

                        SqlParameter[] sqls1 = { new SqlParameter("@id", id1) };

                        DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                        title.Text = dt1.Rows[0][2].ToString();//标题

                        editor.InnerHtml= dt1.Rows[0][3].ToString();//内容

                        string sqlc = "select * from BlogCategory where OwnId=@id";//查找分类

                        SqlParameter[] sqlsc = { new SqlParameter("@id", id) };

                        DataTable dtc = sqlh.sqlselect(sqlc, sqlsc);

                        for (int i = 0; i < dtc.Rows.Count; i++)
                        {
                            string idc = dtc.Rows[i][0].ToString();

                            string categoryc = dtc.Rows[i][1].ToString();

                            category.Items.Add(new ListItem(categoryc, idc));//循环动态绑定分类

                        }
                        category.SelectedIndex = category.Items.IndexOf(category.Items.FindByValue(dt1.Rows[0][7].ToString()));//设置默认选中值
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
        string text = editor.InnerHtml;//获取编辑器中的内容

        string category1 = category.SelectedValue;//选择分类的值

        string title1 = title.Text.Trim();//标题

        string state1 = state.SelectedValue;//权限

        if (text.Length > 0 && title1.Length > 0 && category1.Length > 0)
        {
            string dates = DateTime.Now.ToString();

            string text1 = sqlh.change(text);

            string sql = "update  Blog set Title=@title,Contents=@contents,Dates=@dates,Category=@category,State=@state where BlogId=@id";

            SqlParameter[] sqls =
           {

                    new SqlParameter("@contents", text1),

                    new SqlParameter("@title",title1),

                    new SqlParameter("@dates",dates),

                    new SqlParameter("@state",state1),

                    new SqlParameter("@category",category1),

                    new SqlParameter("@id",Request.QueryString["id1"])

           };

            int result = sqlh.sqlhelper(sql, sqls);

            if (result > 0)

                Response.Write("<script>alert('编辑日志成功');location='/Blogs/BlogContents.aspx?id=" + Request["id"] + "&id1="+Request.QueryString["id1"]+"'</script>");    //刷新界面

            else

                Response.Write("<script>alert('编辑失败')</script>");
        }

        else

            Response.Write("<script>alert('编辑内容不能为空')</script>");
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Blogs/BlogContents.aspx?id=" + Request.QueryString["id"] + "&id1=" + Request.QueryString["id1"] + "");
    }
}