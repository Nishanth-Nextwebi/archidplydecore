using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Consult
{
    #region Consult region
    public int Id { set; get; }

    public string Message { get; set; }
    public string EmailId { get; set; }
    public string ContactNo { get; set; }
    public string City { get; set; }
    public string UserName { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string Status { set; get; }
    public static List<Consult> GetAllConsult(SqlConnection conAP)
    {
        List<Consult> Blogs = new List<Consult>();
        try
        {
            string query = "select * from Consult order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new Consult()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             UserName = Convert.ToString(dr["UserName"]),
                             EmailId = Convert.ToString(dr["Email"]),
                             ContactNo = Convert.ToString(dr["Phone"]),
                             Message = Convert.ToString(dr["Message"]),
                             City = Convert.ToString(dr["City"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllConsult", ex.Message);
        }
        return Blogs;
    }

    public static int InsertConsult(SqlConnection conAP, Consult Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Consult (UserName,Email,Phone,Message,City,AddedOn,AddedIp) values(@UserName,@Email,@Phone,@Message,@City,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = Blog.UserName;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.EmailId;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.ContactNo;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = Blog.Message;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = Blog.City;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertConsult", ex.Message);
        }
        return result;
    }

    #endregion
}
