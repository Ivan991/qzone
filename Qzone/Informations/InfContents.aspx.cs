using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Informations_InfContents : System.Web.UI.Page
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
                       
                        infcontents.DataSource = FillData();

                        infcontents.DataBind();
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


    DataTable FillData()//创建动态的表
    {
        int id2 = Convert.ToInt32(Request.QueryString["id2"].ToString());//标识位

        lbmark.Text = id2.ToString();

        int id1 = Convert.ToInt32(Request.QueryString["id1"]);//动态id


        DataTable dt = new DataTable();//创建一个新表

        dt.TableName = "UnitTable";//设置表的名字
                                   /*规定每列的列名，清楚地告诉dt你需要哪些内容*/
        DataColumn dc1 = new DataColumn("Mark", Type.GetType("System.String"));//因为只需要判断是否显示，所以将列的类型都设为string即可
        DataColumn dc2 = new DataColumn("Id", Type.GetType("System.String"));
        DataColumn dc3 = new DataColumn("PoId", Type.GetType("System.String"));
        DataColumn dc4 = new DataColumn("PoName", Type.GetType("System.String"));
        DataColumn dc5 = new DataColumn("PoImage", Type.GetType("System.String"));
        DataColumn dc6 = new DataColumn("Title", Type.GetType("System.String"));
        DataColumn dc7 = new DataColumn("Message", Type.GetType("System.String"));
        DataColumn dc8 = new DataColumn("Contents", Type.GetType("System.String"));
        DataColumn dc9 = new DataColumn("Comments", Type.GetType("System.String"));
        DataColumn dc10 = new DataColumn("State", Type.GetType("System.String"));
        DataColumn dc11 = new DataColumn("Category", Type.GetType("System.String"));
        DataColumn dc12 = new DataColumn("Dates", Type.GetType("System.DateTime"));

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
        dt.Columns.Add(dc10);
        dt.Columns.Add(dc11);
        dt.Columns.Add(dc12);

        /*到此就建好了一张完美的空表，下面开始填充数据*/

        string sql0 = "";

        if (id2 == 0)
        {
            sql0 = "select * from Log where LogId=@id";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

            DataTable logdt1 = sqlh.sqlselect(sql0, sqls0);//找到那条动态

            for (int i = 0; i < logdt1.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Mark"] = "0";//说说标志位0
                dr["Id"] = logdt1.Rows[i][0].ToString();
                dr["PoId"] = logdt1.Rows[i][1].ToString();
                dr["PoName"] = logdt1.Rows[i][5].ToString();
                dr["PoImage"] = logdt1.Rows[i][6].ToString();
                dr["Title"] = "0";
                dr["Contents"] = logdt1.Rows[i][2].ToString();
                dr["Message"] = '0';
                dr["Comments"] = logdt1.Rows[i][4].ToString();
                dr["State"] = logdt1.Rows[i][7].ToString();
                dr["Category"] = "0";
                dr["Dates"] = (DateTime)logdt1.Rows[i][3];
                dt.Rows.Add(dr);
            }
        }

        if (id2 == 1)
        {
            sql0 = "select * from BlogC where BlogId=@id";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

            DataTable blogdt1 = sqlh.sqlselect(sql0, sqls0);//找到那条动态

            for (int i = 0; i < blogdt1.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Mark"] = "1";//日志标志位1
                dr["Id"] = blogdt1.Rows[i][0].ToString();
                dr["PoId"] = blogdt1.Rows[i][1].ToString();
                dr["PoName"] = blogdt1.Rows[i][5].ToString();
                dr["PoImage"] = blogdt1.Rows[i][8].ToString();
                dr["Title"] = blogdt1.Rows[i][2].ToString();
                dr["Contents"] = blogdt1.Rows[i][3].ToString();
                dr["Message"] = '0';
                dr["Comments"] = blogdt1.Rows[i][6].ToString();
                dr["State"] = blogdt1.Rows[i][8].ToString();
                dr["Category"] = blogdt1.Rows[i][10].ToString();
                dr["Dates"] = (DateTime)blogdt1.Rows[i][4];
                dt.Rows.Add(dr);
            }
        }
        if (id2 == 2)
        {
            sql0 = "select * from PhotoAlbum where PhotoId=@id";

            SqlParameter[] sqls0 = { new SqlParameter("@id", id1) };

            DataTable photodt1 = sqlh.sqlselect(sql0, sqls0);//找到那条动态

            for (int i = 0; i < photodt1.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Mark"] = "2";//照片标志位2
                dr["Id"] = photodt1.Rows[i][0].ToString();
                dr["PoId"] = photodt1.Rows[i][1].ToString();
                dr["PoName"] = photodt1.Rows[i][6].ToString();
                dr["PoImage"] = photodt1.Rows[i][8].ToString();
                dr["Title"] = photodt1.Rows[i][8].ToString(); //相册名字
                dr["Contents"] = photodt1.Rows[i][2].ToString();//照片路径
                dr["Message"] = photodt1.Rows[i][4].ToString();//照片描述
                dr["Comments"] = photodt1.Rows[i][5].ToString();
                dr["State"] = photodt1.Rows[i][9].ToString();
                dr["Category"] = photodt1.Rows[i][11].ToString();
                dr["Dates"] = (DateTime)photodt1.Rows[i][3];

                dt.Rows.Add(dr);
            }
        }

        return dt;

    }

    protected void infcontents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int mark = Convert.ToInt32(lbmark.Text);        //提取标识位

            int id = Convert.ToInt32(Request.QueryString["Id1"]);//动态的id

            string sql = "";

            if (mark == 0)
            {

                sql = "select * from ComLog where ToId=@toid order by Dates";    //在数据库中查找

                SqlParameter[] sqls = { new SqlParameter("@toid", id) };

                DataTable dt = sqlh.sqlselect(sql, sqls);

                Repeater com = (Repeater)e.Item.FindControl("logcomment");  //找到内嵌repeater

                com.DataSource = dt;    //绑定数据

                com.DataBind();

                if (((LinkButton)e.Item.FindControl("LinkButton")).Text == Session["name"].ToString())  //如果发表人的名字为登录用户的名字
                { 
                    ((LinkButton)e.Item.FindControl("LinkButton1")).Visible = false;  //把转发的控件隐藏出来       

                    Label lbstate = (Label)e.Item.FindControl("lbstate1");  //获取权限值

                    if (lbstate.Text == "1")

                        lbstate.Text = "所有人可见";

                    else

                        lbstate.Text = "仅自己可见";
                }
                else

                    ((Label)e.Item.FindControl("lbstate1")).Visible = false;  //找到repeater 中的所有权限的控件           
            }

            if (mark == 1)
            {
                sql = "select * from ComBlog where ToId=@toid order by Dates";    //在数据库中查找

                SqlParameter[] sqls = { new SqlParameter("@toid", id) };

                DataTable dt = sqlh.sqlselect(sql, sqls);

                Repeater com = (Repeater)e.Item.FindControl("blogcomment");  //找到内嵌repeater

                com.DataSource = dt;    //绑定数据

                com.DataBind();

                if (((LinkButton)e.Item.FindControl("LinkButton")).Text == Session["name"].ToString())  //如果发表人的名字为登录用户的名字
                {
                    ((LinkButton)e.Item.FindControl("LinkButton2")).Visible = false;  //把转发的控件隐藏出来          

                    Label lbstate = (Label)e.Item.FindControl("lbstate2");  //获取权限值

                    if (lbstate.Text == "1")

                        lbstate.Text = "所有人可见";

                    else

                        lbstate.Text = "仅自己可见";
                }
                else

                    ((Label)e.Item.FindControl("lbstate2")).Visible = false;  //找到repeater所有权限的控件                

            }

            if (mark == 2)
            {
                sql = "select * from ComPhoto where ToId=@toid order by Dates";    //在数据库中查找

                SqlParameter[] sqls = { new SqlParameter("@toid", id) };

                DataTable dt = sqlh.sqlselect(sql, sqls);

                Repeater com = (Repeater)e.Item.FindControl("photocomment");  //找到内嵌repeater

                com.DataSource = dt;    //绑定数据

                com.DataBind();

                if (((LinkButton)e.Item.FindControl("LinkButton")).Text == Session["name"].ToString())  //如果发表人的名字为登录用户的名字

                    ((LinkButton)e.Item.FindControl("LinkButton3")).Visible = false;  //把转发的控件隐藏出来           
            }

        }
    }

    protected void infcontents_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int mark = Convert.ToInt32(lbmark.Text);        //提取标识位

        int id = Convert.ToInt32(e.CommandArgument);//这条动态的id

        int userid = Convert.ToInt32(Session["id"].ToString());//评论人id

        string dates = DateTime.Now.ToString();//评论时间

        string username = Session["name"].ToString();//评论人名字

        if (e.CommandName == "btn1")//按评论这条说说
        {
            string contents = ((TextBox)e.Item.FindControl("txt1")).Text;  //获取评论值           

            if (contents.Length > 0)
            {
                string sql1 = "select * from Log where LogId=@id";     //找出被回复的名字

                SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                string parentid = dt1.Rows[0][1].ToString();//被回复人的id

                string parentname = dt1.Rows[0][5].ToString();//被回复人名字

                string sql = "";

                if (parentid != Session["id"].ToString())

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

               
                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",id),

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
                    string sql2 = "update Log set Comments=Comments+1 where LogId=@id";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@id", id) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");
                }
                else

                    Response.Write("<script>alert('评论失败')</script>");

            }
            else

                Response.Write("<script>alert('评论内容不能为空')</script>");

        }
        if (e.CommandName == "btn2")//按评论日志
        {

            string contents = ((TextBox)e.Item.FindControl("txt2")).Text;  //获取评论值           

            if (contents.Length > 0)
            {

                string sql1 = "select * from Blog where BlogId=@id";     //找出被回复的名字

                SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                string parentid = dt1.Rows[0][1].ToString();//被回复人的id

                string parentname = dt1.Rows[0][5].ToString();//被回复人名字

                string sql = "";

                if (parentid != Session["id"].ToString())

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",id),

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

                    string sql2 = "update Blog set Comments=Comments+1 where BlogId=@id";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@id", id) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");
                }

                else

                    Response.Write("<script>alert('评论失败')</script>");

            }
            else

                Response.Write("<script>alert('评论内容不能为空')</script>");


        }

        if (e.CommandName == "btn3")//按下评论照片
        {
            string contents = ((TextBox)e.Item.FindControl("txt3")).Text;  //获取评论值           

            if (contents.Length > 0)
            {
                string sql1 = "select * from Photo where PhotoId=@id";     //找出被回复的名字

                SqlParameter[] sqls1 = { new SqlParameter("@id", id) };

                DataTable dt1 = sqlh.sqlselect(sql1, sqls1);

                string parentid = dt1.Rows[0][1].ToString();//被回复人的id

                string parentname = dt1.Rows[0][6].ToString();//被回复人名字

                string sql = "";

                if (parentid != Session["id"].ToString())

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

               
                SqlParameter[] sqls =
                {
                    new SqlParameter("@toid",id),

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

                    string sql2 = "update Photo set Comments=Comments+1 where PhotoId=@id";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@id", id) };

                    int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    Response.Write("<script>alert('评论成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");
                }

                else

                    Response.Write("<script>alert('评论失败')</script>");

            }
            else

                Response.Write("<script>alert('评论内容不能为空')</script>");
        }


        if (e.CommandName == "report1")    //按转发说说显示转发框
        {
            ((TextBox)e.Item.FindControl("TextBox1")).Visible = true;

            ((Button)e.Item.FindControl("Button1")).Visible = true;

            ((DropDownList)e.Item.FindControl("reportstate1")).Visible = true;

        }

        if (e.CommandName == "report2")    //按转发日志显示转发框
        {
            ((DropDownList)e.Item.FindControl("DropDownList2")).Visible = true;//转发分类

            ((Button)e.Item.FindControl("Button2")).Visible = true;

            ((DropDownList)e.Item.FindControl("reportstate2")).Visible = true;

        }

        if (e.CommandName == "report3")    //按转发照片显示转发框
        {
            ((DropDownList)e.Item.FindControl("DropDownList3")).Visible = true;//转发到哪个相册

            ((Label)e.Item.FindControl("Label3")).Visible = true;

            ((TextBox)e.Item.FindControl("TextBox3")).Visible = true;

            ((Button)e.Item.FindControl("Button3")).Visible = true;

        }

        string poid = Session["id"].ToString();  //转发的人的id  

        string poname = Session["name"].ToString();  //转发的人的名字

        string poimage = Session["image"].ToString();//转发人的头像

        string dates1 = DateTime.Now.ToString();    //转发时间

        if (e.CommandName == "btnreporter1")//按转发说说的时候
        {
            TextBox text = (TextBox)e.Item.FindControl("TextBox1");

            Button btn = (Button)e.Item.FindControl("Button1");

            text.Visible = false;

            btn.Visible = false;

            DropDownList restate = (DropDownList)e.Item.FindControl("reportstate1");

            restate.Visible = false;

            string state1 = restate.SelectedValue;

            string sqlre = "insert into Log (PoId,Contents,Dates,Comments,PoName,PoImage,State) values (@PoId,@Contents,@Dates,'0',@PoName,@poimage,@state) ";

            string reportcontent = text.Text.Trim();  //转发说说时自己评论的内容

            string sqlselect = "select * from Log where LogId=@logid";

            SqlParameter[] sqlsselect = { new SqlParameter("@logid", id) };

            DataTable dt1 = sqlh.sqlselect(sqlselect, sqlsselect);

            string logcontent = dt1.Rows[0][2].ToString();   //转发说说的内容

            string reportname = dt1.Rows[0][5].ToString();  //被转发人的名字

            string contents1 = "@" + reportname + ":" + reportcontent + "——" + logcontent + "";

            SqlParameter[] sqlsre =
            {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@PoName",poname),

                    new SqlParameter("@poimage",poimage),

                    new SqlParameter("@state",state1)

                };

            int resultre = sqlh.sqlhelper(sqlre, sqlsre);

            if (resultre > 0)

                Response.Write("<script>alert('转发成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");


            else

                Response.Write("<script>alert('转发失败')</script>");

        }

        if (e.CommandName == "btnreporter2")//转发日志的时候
        {

            DropDownList drop = (DropDownList)e.Item.FindControl("DropDownList2");

            Button btn = (Button)e.Item.FindControl("Button1");

            drop.Visible = false;

            btn.Visible = false;

            DropDownList restate = (DropDownList)e.Item.FindControl("reportstate2");

            restate.Visible = false;

            string state1 = restate.SelectedValue;

            string sqlre = "insert into Blog (PoId,Title,Contents,Dates,Comments,PoName,Category,PoImage,State) values (@PoId,@title,@Contents,@Dates,'0',@PoName,@category,@poimage,@state) ";

            string category1 = drop.SelectedValue;//转发分类的值

            string sqlselect = "select * from Blog where BlogId=@blogid";

            SqlParameter[] sqlsselect = { new SqlParameter("@blogid", id) };

            DataTable dt1 = sqlh.sqlselect(sqlselect, sqlsselect);

            string title = dt1.Rows[0][2].ToString();   //转发日志标题

            string recontent = dt1.Rows[0][3].ToString();//转发日志内容

            string reportname = dt1.Rows[0][5].ToString();  //被转发人的名字

            string contents1 = "@" + reportname + ":" + recontent + "";

            SqlParameter[] sqlsre =
            {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@title",title),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@PoName",poname),

                    new SqlParameter("@poimage",poimage),

                    new SqlParameter("@category",category1),

                    new SqlParameter("@state",state1)

                };

            if (category1.Length > 0)
            {
                int resultre = sqlh.sqlhelper(sqlre, sqlsre);

                if (resultre > 0)

                    Response.Write("<script>alert('转发成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");

                else

                    Response.Write("<script>alert('转发失败')</script>");
            }
            else

                Response.Write("<script>alert('请填写转发日志的分类')</script>");
        }

        if (e.CommandName == "btnreporter3")
        {
            DropDownList drop = (DropDownList)e.Item.FindControl("DropDownList3");

            Label label = (Label)e.Item.FindControl("Label3");

            TextBox text = (TextBox)e.Item.FindControl("TextBox3");

            Button btn = (Button)e.Item.FindControl("Button3");

            string sqlre = "insert into Photo (PoId,Contents,Dates,Message,Comments,PoName,AlbumId,PoImage) values (@PoId,@Contents,@Dates,@message,'0',@PoName,@albumid,@poimage) ";

            string albumid = drop.SelectedValue;//转发到哪个相册 不能为空

            string poimage1 = Session["image"].ToString();

            string message = text.Text.Trim();//照片描述 可以为空

            string sqlselect = "select * from Photo where PhotoId=@id";

            SqlParameter[] sqlsselect = { new SqlParameter("@id", id) };

            DataTable dt1 = sqlh.sqlselect(sqlselect, sqlsselect);

            string contents1 = dt1.Rows[0][2].ToString();//转发图片路径

            SqlParameter[] sqlsre =
            {
                    new SqlParameter("@PoId",poid),

                    new SqlParameter("@Contents",contents1),

                    new SqlParameter("@Dates",dates1),

                    new SqlParameter("@message",message),

                    new SqlParameter("@PoName",poname),

                    new SqlParameter("@albumid",albumid),

                    new SqlParameter("@poimage",poimage1)

                };
            if (albumid.Length > 0)
            {
                int resultre = sqlh.sqlhelper(sqlre, sqlsre);

                if (resultre > 0)

                    Response.Write("<script>alert('转发成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");

                else

                    Response.Write("<script>alert('转发失败')</script>");
            }
            else

                Response.Write("<script>alert('请填写转发到哪个相册')</script>");
        }



    }


    protected void logcomment_ItemDataBound(object sender, RepeaterItemEventArgs e)//绑定说说评论这个repeater数据时
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (((LinkButton)e.Item.FindControl("cname1")).Text == Session["name"].ToString())//如果回复人的名字为登录用户的名字

                ((LinkButton)e.Item.FindControl("delete1")).Visible = true;  //把删除评论的控件显示出来

        }
    }

    protected void logcomment_ItemCommand(object source, RepeaterCommandEventArgs e)//当按下说说这个repeater的按钮的时候
    {

        if (e.CommandName == "delete1")  //执行删除评论这条操作
        {
            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);//这条评论的id

            int toid = Convert.ToInt32(str[1]);//这条评论对应的说说的id

            string sql3 = "delete from ComLog where CommentId=@commentid";

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {
                string sql2 = "update Log set Comments=Comments-1 where LogId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");    //刷新界面
            }
            else

                Response.Write("<script>alert('删除失败')</script>");

        }


        if (e.CommandName == "commentor1")     //点击回复的内容弹出评论框
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");

            Button btn = (Button)e.Item.FindControl("btncomment1");

            Label lb = (Label)e.Item.FindControl("lb1");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }

        if (e.CommandName == "btncom1")    //回复这条评论
        {
            TextBox text = (TextBox)e.Item.FindControl("comment1");//评论完毕

            Button btn = (Button)e.Item.FindControl("btncomment1");

            Label lb = (Label)e.Item.FindControl("lb1");

            lb.Visible = false;

            text.Visible = false;

            btn.Visible = false;

            string contents = text.Text.Trim();

            if (contents.Length > 0)
            {
                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid1 = Convert.ToInt32(str1[1]);//那条说说id

                int parentid = Convert.ToInt32(str1[0]);//被回复人id

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"].ToString()))

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComLog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

            
                int userid = Convert.ToInt32(Session["id"].ToString());//回复者id

                string dates = DateTime.Now.ToString();//时间

                string username = Session["name"].ToString();//回复者名字

                string parentname = str1[2];//被回复人名字

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
                    string sql2 = "update Log set Comments=Comments+1 where LogId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid1) };

                    int result2 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    int sid = Convert.ToInt32(Request.QueryString["id"]);

                    Response.Write("<script>alert('评论成功！');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");

                }

                else

                    Response.Write("<script>alert('评论失败！')</script>");

            }

            else

                Response.Write("<script>alert('评论失败！')</script>");
        }
    }



    protected void blogcomment_ItemDataBound(object sender, RepeaterItemEventArgs e)//绑定日志评论的数据时
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (((LinkButton)e.Item.FindControl("cname2")).Text == Session["name"].ToString())//如果回复人的名字为登录用户的名字

                ((LinkButton)e.Item.FindControl("delete2")).Visible = true;  //把删除评论的控件显示出来

        }
    }

    protected void blogcomment_ItemCommand(object source, RepeaterCommandEventArgs e)//当按下日志评论区的按钮
    {
        if (e.CommandName == "delete2")  //执行删除评论这条操作
        {
            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);//这条评论的id

            int toid = Convert.ToInt32(str[1]);//这条评论对应的日志的id

            string sql3 = "delete from ComBlog where CommentId=@commentid";

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {
                string sql2 = "update Blog set Comments=Comments-1 where BlogId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");    //刷新界面
            }
            else

                Response.Write("<script>alert('删除失败')</script>");

        }


        if (e.CommandName == "commentor2")     //点击回复的内容弹出评论框
        {
            TextBox text = (TextBox)e.Item.FindControl("comment2");

            Button btn = (Button)e.Item.FindControl("btncomment2");

            Label lb = (Label)e.Item.FindControl("lb2");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }

        if (e.CommandName == "btncom2")    //回复这条评论
        {
            TextBox text = (TextBox)e.Item.FindControl("comment2");//评论完毕

            Button btn = (Button)e.Item.FindControl("btncomment2");

            Label lb = (Label)e.Item.FindControl("lb2");

            lb.Visible = false;

            text.Visible = false;

            btn.Visible = false;

            string contents = text.Text.Trim();

            if (contents.Length > 0)
            {

                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid1 = Convert.ToInt32(str1[1]);//那条说说id

                int parentid = Convert.ToInt32(str1[0]);//被回复人id

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"].ToString()))

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComBlog (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";

                int userid = Convert.ToInt32(Session["id"].ToString());//回复者id

                string dates = DateTime.Now.ToString();//时间

                string username = Session["name"].ToString();//回复者名字

                string parentname = str1[2];//被回复人名字

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
                    string sql2 = "update Blog set Comments=Comments+1 where BlogId=@toid";  //更新评论数目

                    SqlParameter[] sqls2 = { new SqlParameter("@toid", toid1) };

                    int result2 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                    int sid = Convert.ToInt32(Request.QueryString["id"]);

                    Response.Write("<script>alert('评论成功！');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");

                }

                else

                    Response.Write("<script>alert('评论失败！')</script>");

            }

            else

                Response.Write("<script>alert('评论失败！')</script>");
        }
    }



    protected void photocomment_ItemDataBound(object sender, RepeaterItemEventArgs e)//当绑定photo的评论的repeater的时候
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (((LinkButton)e.Item.FindControl("cname3")).Text == Session["name"].ToString())//如果回复人的名字为登录用户的名字

                ((LinkButton)e.Item.FindControl("delete3")).Visible = true;  //把删除评论的控件显示出来

        }
    }

    protected void photocomment_ItemCommand(object source, RepeaterCommandEventArgs e)//按下photo评论区的按钮时
    {
        if (e.CommandName == "delete3")  //执行删除评论这条操作
        {
            string[] str = e.CommandArgument.ToString().Split(',');

            int commentid = Convert.ToInt32(str[0]);//这条评论的id

            int toid = Convert.ToInt32(str[1]);//这条评论对应的照片的id

            string sql3 = "delete from ComPhoto where CommentId=@commentid";

            SqlParameter[] sqls3 = { new SqlParameter("@commentid", commentid) };

            int result = sqlh.sqlhelper(sql3, sqls3);                      //进行删除

            if (result > 0)
            {
                string sql2 = "update Photo set Comments=Comments-1 where PhotoId=@toid";  //更新评论数目

                SqlParameter[] sqls2 = { new SqlParameter("@toid", toid) };

                int result1 = sqlh.sqlhelper(sql2, sqls2);  //如果数据更新失败

                Response.Write("<script>alert('删除成功');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");    //刷新界面
            }
            else

                Response.Write("<script>alert('删除失败')</script>");

        }


        if (e.CommandName == "commentor3")     //点击回复的内容弹出评论框
        {
            TextBox text = (TextBox)e.Item.FindControl("comment3");

            Button btn = (Button)e.Item.FindControl("btncomment3");

            Label lb = (Label)e.Item.FindControl("lb3");

            lb.Visible = true;

            text.Visible = true;

            btn.Visible = true;
        }

        if (e.CommandName == "btncom3")    //回复这条评论
        {
            TextBox text = (TextBox)e.Item.FindControl("comment3");//评论完毕

            Button btn = (Button)e.Item.FindControl("btncomment3");

            Label lb = (Label)e.Item.FindControl("lb3");

            lb.Visible = false;

            text.Visible = false;

            btn.Visible = false;

            string contents = text.Text.Trim();

            if (contents.Length > 0)
            {
                string[] str1 = e.CommandArgument.ToString().Split(',');    //获取传过来的值

                int toid1 = Convert.ToInt32(str1[1]);//那条说说id

                int parentid = Convert.ToInt32(str1[0]);//被回复人id

                string sql = "";

                if (parentid != Convert.ToInt32(Session["id"].ToString()))

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,1)";

                else

                    sql = "insert into ComPhoto (ToId,ParentId,UserId,Contents,Dates,UserName,ParentName,Information) values (@toid,@parentid,@userid,@contents,@dates,@username,@parentname,0)";
      
                int userid = Convert.ToInt32(Session["id"].ToString());//回复者id

                string dates = DateTime.Now.ToString();//时间

                string username = Session["name"].ToString();//回复者名字

                string parentname = str1[2];//被回复人名字

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

                    Response.Write("<script>alert('评论成功！');location='/Trend.aspx?id=" + Session["id"].ToString() + "'</script>");

                }
                else

                    Response.Write("<script>alert('评论失败！')</script>");

            }
            else

                Response.Write("<script>alert('评论失败！')</script>");
        }
    }


}