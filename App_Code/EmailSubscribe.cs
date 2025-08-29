using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmailSubscribe
/// </summary>
public class EmailSubscribe
{
    public EmailSubscribe()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties
    public int Id { get; set; }
    public string EmailId { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    #endregion
    #region Contactus region

    public static List<EmailSubscribe> GetAllEmailSubscribe(SqlConnection conAP)
    {
        List<EmailSubscribe> Blogs = new List<EmailSubscribe>();
        try
        {
            string query = "select * from EmailSubscribe where Status ='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new EmailSubscribe()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             EmailId = Convert.ToString(dr["EmailId"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIp"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllEmailSubscribe", ex.Message);
        }
        return Blogs;
    }

    public static List<EmailSubscribe> GetAllEmailSubscribedCheck(SqlConnection conAP ,string EmailId)
    {
        List<EmailSubscribe> Blogs = new List<EmailSubscribe>();
        try
        {
            string query = "select * from EmailSubscribe where EmailId=@EmailId";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = EmailId;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new EmailSubscribe()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             EmailId = Convert.ToString(dr["EmailId"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIp"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllEmailSubscribe", ex.Message);
        }
        return Blogs;
    }

    public static int InserEmailSubscribe(SqlConnection conAP, EmailSubscribe Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into EmailSubscribe (EmailId,AddedOn,AddedIp,Status) values(@em,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@em", SqlDbType.NVarChar).Value = Blog.EmailId;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserEmailSubscribe", ex.Message);
        }
        return result;
    }

    #endregion
    public static int DeleteEmailSubscribe(SqlConnection _con, EmailSubscribe cat)
    {
        int result = 0;
        try
        {
            string query = "Update EmailSubscribe Set Status=@Status Where Id=@Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEmailSubscribe", ex.Message);
        }
        return result;
    }
}