using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Informations_Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

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

                        datatorepeater(1);

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


    DataTable FillDataCom()//创建评论的表
    {
        int id = Convert.ToInt32(Session["id"]);

        string sql1 = "select * from ComLog where Information=1 and ParentId=@ownid order by Dates desc";

        SqlParameter[] sqls1 = { new SqlParameter("@ownid", id) };

        string sql2 = "select * from ComBlog where Information=1 and ParentId=@ownid order by Dates desc";

        SqlParameter[] sqls2 = { new SqlParameter("@ownid", id) };

        string sql3 = "select * from ComPhoto where Information=1 and ParentId=@ownid order by Dates desc";

        SqlParameter[] sqls3 = { new SqlParameter("@ownid", id) };

        DataTable logdt = sqlh.sqlselect(sql1, sqls1);//查找出所有的log评论

        DataTable blogdt = sqlh.sqlselect(sql2, sqls2);//查找出所有的blog评论

        DataTable photodt = sqlh.sqlselect(sql3, sqls3);//查找出所有的photo评论

        DataTable dt = new DataTable();//创建一个新表

        dt.TableName = "UnitTable";//设置表的名字
                                   /*规定每列的列名，清楚地告诉dt你需要哪些内容*/
        DataColumn dc1 = new DataColumn("Mark", Type.GetType("System.String"));//因为只需要判断是否显示，所以将列的类型都设为string即可
        DataColumn dc2 = new DataColumn("CommentId", Type.GetType("System.String"));
        DataColumn dc3 = new DataColumn("ToId", Type.GetType("System.String"));
        DataColumn dc4 = new DataColumn("ParentId", Type.GetType("System.String"));
        DataColumn dc5 = new DataColumn("UserId", Type.GetType("System.String"));
        DataColumn dc6 = new DataColumn("Contents", Type.GetType("System.String"));
        DataColumn dc7 = new DataColumn("Dates", Type.GetType("System.DateTime"));
        DataColumn dc8 = new DataColumn("UserName", Type.GetType("System.String"));
        DataColumn dc9 = new DataColumn("ParentName", Type.GetType("System.String"));


        /*将规定好的列填加进表中*/
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);
        dt.Columns.Add(dc4);
        dt.Columns.Add(dc5);
        dt.Columns.Add(dc6);
        dt.Columns.Add(dc7);
        dt.Columns.Add(dc8);
        dt.Columns.Add(dc9);


        /*到此就建好了一张完美的空表，下面开始填充数据*/
        /*把log中表的数据填进dt*/
        for (int i = 0; i < logdt.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Mark"] = "0";//说说标志位0
            dr["CommentId"] = logdt.Rows[i][0].ToString();
            dr["ToId"] = logdt.Rows[i][1].ToString();
            dr["ParentId"] = logdt.Rows[i][2].ToString();
            dr["UserId"] = logdt.Rows[i][3].ToString();
            dr["Contents"] = logdt.Rows[i][4].ToString();
            dr["Dates"] = (DateTime)logdt.Rows[i][5];
            dr["UserName"] = logdt.Rows[i][6].ToString();
            dr["ParentName"] = logdt.Rows[i][7].ToString();
            dt.Rows.Add(dr);
        }
        /*将blog表中的数据填进dt*/
        for (int i = 0; i < blogdt.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Mark"] = "1";//日志标志位1
            dr["CommentId"] = blogdt.Rows[i][0].ToString();
            dr["ToId"] = blogdt.Rows[i][1].ToString();
            dr["ParentId"] = blogdt.Rows[i][2].ToString();
            dr["UserId"] = blogdt.Rows[i][3].ToString();
            dr["Contents"] = blogdt.Rows[i][4].ToString();
            dr["Dates"] = (DateTime)blogdt.Rows[i][5];
            dr["UserName"] = blogdt.Rows[i][6].ToString();
            dr["ParentName"] = blogdt.Rows[i][7].ToString();
            dt.Rows.Add(dr);
        }
        /*将photo表中的数据填进dt*/
        for (int i = 0; i < photodt.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Mark"] = "2";//照片标志位2
            dr["CommentId"] = photodt.Rows[i][0].ToString();
            dr["ToId"] = photodt.Rows[i][1].ToString();
            dr["ParentId"] = photodt.Rows[i][2].ToString();
            dr["UserId"] = photodt.Rows[i][3].ToString();
            dr["Contents"] = photodt.Rows[i][4].ToString();
            dr["Dates"] = (DateTime)photodt.Rows[i][5];
            dr["UserName"] = photodt.Rows[i][6].ToString();
            dr["ParentName"] = photodt.Rows[i][7].ToString();
            dt.Rows.Add(dr);
        }
        /*将talk表中的数据填进dt*/

        dt.DefaultView.Sort = "Dates DESC";//给dt排个序

        return dt;

    }

    void datatorepeater(int currentpage)
    {
        int id = Convert.ToInt32(Session["id"].ToString());
        
        DataTable dt = FillDataCom();//评论的多表

        PagedDataSource bind = new PagedDataSource(); //创建分页对象

        bind.AllowPaging = true;  //允许分页

        bind.PageSize = 5;  //设置每页显示的数

        bind.DataSource = dt.DefaultView;

        bind.CurrentPageIndex = currentpage - 1;

        if (bind.Count == 0)

            lbTotal.Text = "1";
        else

        lbTotal.Text = bind.Count.ToString();//设置总页数

        information.DataSource = bind;//把合并的表绑定到repeater

        information.DataBind();

    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {

        lbNow.Text = "1";     //当前页设为1

        datatorepeater(1);
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) > 1)                                             //如果当前页数不是首页
        {

            int pages = Convert.ToInt32(lbNow.Text) - 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数-1

            datatorepeater(pages);

        }
    }

    protected void btnJump_Click(object sender, EventArgs e)   //跳页
    {
        if (txtJump.Text.Trim().Length > 0)
        {
            int pages = Convert.ToInt32(txtJump.Text);

            if (pages > 0 && pages <= Convert.ToInt32(lbTotal.Text))     //当输入的页数不超过限制师
            {
                datatorepeater(pages);

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
            int pages = Convert.ToInt32(lbNow.Text) + 1;

            lbNow.Text = Convert.ToString(pages);             //把当前页数+1

            datatorepeater(pages);
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)//尾页跳转
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        int pages = Convert.ToInt32(lbTotal.Text);

        datatorepeater(pages);

        lbNow.Text = pages.ToString();     //当前页设为最后一页

    }



    protected void information_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="comment")
        {
            string[] str = e.CommandArgument.ToString().Split(',');

            int mark = Convert.ToInt32(str[1]);

            int id = Convert.ToInt32(str[0]);//这条评论所对应动态的id

            string sql = "";

            if (mark == 0)

                sql = "update ComLog set Information=0 where ToId=@id";

            if(mark==1)

                sql = "update ComBlog set Information=0 where ToId=@id";

            if(mark==2)

                sql = "update ComPhoto set Information=0 where ToId=@id";

            SqlParameter[] sqls = { new SqlParameter("@id", id) };

            int result = sqlh.sqlhelper(sql, sqls);

            if(result>0)

                     Response.Redirect("/Informations/InfContents.aspx?id="+Session["id"] +"&id1="+id+"&id2="+mark+"");
        }
    }

}