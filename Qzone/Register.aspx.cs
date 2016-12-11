using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    protected void register_Click(object sender, EventArgs e)
    {
        string name1 = name.Text.Trim();

        string uname = usersname.Text.Trim();

        string pwd1 = pwd.Text.Trim();

        string sex1 = sex.SelectedValue;

        string tel1 = tel.Text.Trim();

        string birth1 = birth.Text.Trim();

        string blood1 = blood.Text.Trim();

        string qzone = qzonename.Text.Trim();

        string email1 = email.Text.Trim();

        string validatecode = validate.Text.Trim();

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

        if (name1.Length > 0 && uname.Length > 0 && pwd1.Length > 0 && sex1.Length > 0 && tel1.Length > 0 && birth1.Length > 0 && blood1.Length > 0 && email1.Length > 0)
        {

            if (validatecode == Session["vcode"].ToString())                        //验证码是否正确
            {
                string sqlname = "select * from Users where Name=@name";         //查找用户名是否存在

                SqlParameter[] pname = { new SqlParameter("@name", name1) };

                DataTable dt = sqlh.sqlselect(sqlname, pname);

                string sqltel = "select * from Users where Telephone=@tel";         //查找手机号是否存在

                SqlParameter[] ptel = { new SqlParameter("@tel", tel1) };

                DataTable dt2 = sqlh.sqlselect(sqltel, ptel);

                string sqlemail = "select * from Users where Email=@email";         //查找邮箱是否存在

                SqlParameter[] pemail = { new SqlParameter("@email", email1) };

                DataTable dt1 = sqlh.sqlselect(sqlemail, pemail);

                if (dt.Rows.Count == 0)     //如果用户名和邮箱和手机号都不存在
                {

                    if (dt1.Rows.Count == 0)
                    {
                        if (dt2.Rows.Count == 0)
                        {
                            if (fileOK && sqlh.FileValidation.IsAllowedExtension(upload, fe))  //判断文件类型是否为bmp/gif/jpg/png以及检测头文件
                            {
                                string fileExt = System.IO.Path.GetExtension(upload.FileName).ToLower();

                                filename = "/PhotoFile/" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;  //设置文件名

                                upload.SaveAs(Server.MapPath(filename));//存储文件

                                string pwd2 = sqlh.MD5(pwd1);//密码加密

                                string sql = "insert into Users(Name,Password,UsersName,Sex,Telephone,Birthday,Blood,Email,QzoneName,State,Image) values(@name2,@pwd2,@uname2,@sex2,@tel2,@birth2,@blood2,@email2,@qzone,'0',@image)";

                                SqlParameter[] sqls =
                                {

                            new SqlParameter("@name2",name1),

                            new SqlParameter("@pwd2",pwd2),

                            new SqlParameter("@uname2",uname),

                            new SqlParameter("@sex2",sex1),

                            new SqlParameter("@tel2",tel1),

                            new SqlParameter("@birth2",birth1),

                            new SqlParameter("@blood2",blood1),

                            new SqlParameter("@email2",email1),

                            new SqlParameter("@qzone",qzone),

                            new SqlParameter("@image",filename)

                        };                       //参数化处理

                                int result = sqlh.sqlhelper(sql, sqls);

                                if (result > 0)
                                {
                                    Session["name"] = name1;

                                    Session["email"] = email1;

                                    Response.Write("<script>alert('注册成功！');location='ValidateEmail.aspx'</script>");

                                    Random ran = new Random();

                                    int n = ran.Next(1000, 9999);   //生成1000-9999的随机数

                                    Response.Cookies["code1"].Value = n.ToString();   //设置邮箱中验证码的cookies

                                    Response.Cookies["code1"].Expires = DateTime.Now.AddMinutes(5);

                                    string totitle = "亲爱的，这里有封信件~";

                                    string tobody = "亲爱的qq空间用户'" + name1 + "'，欢迎注册，您的注册确认验证码为'" + n + "'，请尽快确认噢，验证码在五分钟后失效";

                                    sqlh.email(email1, totitle, tobody);    //执行发送邮件

                                }
                                else

                                    Response.Write("<script>alert('注册失败请重试')</script>");
                            }
                            else

                                Response.Write("<script>alert('图片类型不符！')</script>");
                        }

                        else

                            Response.Write("<script>alert('手机号已存在！')</script>");
                    }

                    else

                        Response.Write("<script>alert('邮箱已存在！')</script>");
                }
                else

                    Response.Write("<script>alert('用户名已存在！')</script>");

            }
            else

                Response.Write("<script>alert('验证码输入错误！')</script>");

        }
        else

            Response.Write("<script>alert('注册信息不能为空！')</script>");

    }




}