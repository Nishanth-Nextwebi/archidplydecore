using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ContactUs
{
    #region Contactus region
    public int Id { set; get; }

    public string Message { get; set; }
    public string EmailId { get; set; }
    public string ContactNo { get; set; }
    public string UserName { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string Status { set; get; }
    public static List<ContactUs> GetAllContactUs(SqlConnection conAP)
    {
        List<ContactUs> Blogs = new List<ContactUs>();
        try
        {
            string query = "select * from ContactUs where Status='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new ContactUs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             UserName = Convert.ToString(dr["UserName"]),
                             EmailId = Convert.ToString(dr["Email"]),
                             ContactNo = Convert.ToString(dr["Phone"]),
                             Message = Convert.ToString(dr["Message"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllContactUs", ex.Message);
        }
        return Blogs;
    }

    public static int InserContactUs(SqlConnection conAP, ContactUs Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ContactUs (Status,UserName,Email,Phone,Message,AddedOn,AddedIp) values(@Status,@UserName,@Email,@Phone,@Message,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = Blog.UserName;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.EmailId;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.ContactNo;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = Blog.Message;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserContactUs", ex.Message);
        }
        return result;
    }
    public static int DeleteContactUs(SqlConnection _con, ContactUs cat)
    {
        int result = 0;
        try
        {
            string query = "Update ContactUs Set Status=@Status Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, _con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                _con.Open();
                result = cmd.ExecuteNonQuery();
                _con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteContactUs", ex.Message);
        }
        return result;
    }


    public static string GetMessageById(SqlConnection conAP, string id)
    {
        string result = null;
        try
        {
            string cmdText = "select Message from ContactUs WHERE Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                conAP.Open();
                object obj = sqlCommand.ExecuteScalar();
                if (obj != DBNull.Value)
                {
                    result = obj.ToString();
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMessageById", ex.Message);
        }
        finally
        {
            if (conAP.State == ConnectionState.Open)
            {
                conAP.Close();
            }
        }

        return result;
    }

    #endregion
}
