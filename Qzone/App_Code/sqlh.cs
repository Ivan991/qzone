using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// sql 的摘要说明
/// </summary>
public class sqlh
{
    static string str = @"server=LAPTOP-HULUCDN3;Integrated Security=SSPI;database=Qzone;";//创建连接字符串

    public sqlh()
    {

        //
        // TODO: 在此处添加构造函数逻辑
        //
    }  

    public enum FileExtension   //检查上传文件的头文件的函数
    {
        GIF = 7173,
        JPG = 255216,
        BMP = 6677,
        PNG = 255217
        //PNG = 13780,
        //DOC = 208207,
        //DOCX = 8075,
        //XLSX = 8075,
        //JS = 239187,
        //XLS = 208207,
        //SWF = 6787,
        //MID = 7784,
        //RAR = 8297,
        //ZIP = 8075,
        //XML = 6063,
        //TXT = 7067,
        //MP3 = 7368,
        //WMA = 4838,
        // 239187 aspx
        // 117115 cs
        // 119105 js
        // 210187 txt
        //255254 sql 		
        // 7790 exe dll,
        // 8297 rar
        // 6063 xml
        // 6033 html
    }

    static public class FileValidation  //判断上传的文件是否为图片类型
    {
        public static bool IsAllowedExtension(FileUpload fu, FileExtension[] fileEx)
        {
            int fileLen = fu.PostedFile.ContentLength;  //获取上传文件的大小

            byte[] imgArray = new byte[fileLen];

            fu.PostedFile.InputStream.Read(imgArray, 0, fileLen); //获取文件并准备读取文件

            MemoryStream ms = new MemoryStream(imgArray);

            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);  //把文件转化成二进制

            string fileclass = "";

            byte buffer;

            try
            {
                buffer = br.ReadByte();

                fileclass = buffer.ToString();

                buffer = br.ReadByte();

                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            //注意将文件流指针还原
            //fu.PostedFile.InputStream.Postion = 0;
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe)
                    return true;
            }
            return false;
        }
    }

    static  public string MD5(string pwd)              //密码加密
    {

        MD5 md5 = new MD5CryptoServiceProvider();

        byte[] data = System.Text.Encoding.Default.GetBytes(pwd);

        byte[] md5data = md5.ComputeHash(data);

        md5.Clear();

        string Str = "";

        for(int i=0;i<md5data.Length-1;i++)
        {

            Str += md5data[i].ToString("x").PadLeft(2, '0');
        }

        return Str;

    }

   static public string change(string x)
    {
        x = x.Replace("&lt;", "<");
        x = x.Replace("&gt;", ">");
        x = x.Replace("&quot;", "\"");

        return x;
    }


    static public void email(string toemail,string totitle, string tobody)    //发送邮件
    {
        MailMessage mailObj = new MailMessage();

        mailObj.From = new MailAddress("ivanleaves@sina.cn"); //发送人邮箱地址

        MailAddress messageto = new MailAddress(toemail);

        mailObj.To.Add(messageto);   //收件人邮箱地址

        mailObj.Subject = totitle;    //主题

        mailObj.IsBodyHtml = true;//确认是否是html格式

        mailObj.Priority = MailPriority.High;//确认优先级

        mailObj.Body =tobody;    //正文

        SmtpClient smtp = new SmtpClient("smtp.sina.cn",25);

            //指定smtp服务器名称

        smtp.UseDefaultCredentials = true;

        smtp.Credentials = new System.Net.NetworkCredential("ivanleaves@sina.cn", "IvanLeaves^&");  //发送人的登录名和密码

        smtp.Send(mailObj);
    }

    static public PagedDataSource DataBindToRepeater(int currentpage, string sql,SqlParameter[] sqls)   //分页函数
    {
        DataTable dt = sqlh.sqlselect(sql, sqls);

        PagedDataSource bind = new PagedDataSource(); //创建分页对象

        bind.AllowPaging = true;  //允许分页

        bind.PageSize = 5;  //设置每页显示的数

        bind.DataSource = dt.DefaultView;

        bind.CurrentPageIndex = currentpage - 1;

        return bind;    //返回这个分页对象
    }


    static public int sqlhelper(string sql,SqlParameter[] a)//参数化查询
    {
        SqlConnection conn = new SqlConnection(str);    //连接数据库

        conn.Open();

        SqlCommand comm = new SqlCommand();

        comm.Connection = conn;     //为每一条数据添加一个参数

        comm.CommandText = sql;

        comm.Parameters.AddRange(a);     //使sql语句参数化        

        int count= comm.ExecuteNonQuery();  //返回受影响的行数

        conn.Close();

        return count;
    }


    static public DataTable sqlselect (string sql,SqlParameter[] a)   //sql select
    {
        SqlConnection conn = new SqlConnection(str);

        conn.Open();

        SqlCommand comm = new SqlCommand();

        comm.Connection = conn;

        comm.CommandText = sql;

        comm.Parameters.AddRange(a);

        DataTable dt = new DataTable();

        SqlDataAdapter da = new SqlDataAdapter(comm);              

        da.Fill(dt);

        conn.Close();

        return dt;

    }


}