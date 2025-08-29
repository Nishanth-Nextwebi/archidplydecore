using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Vendor
{
    #region Vendor region
    public int Id { set; get; }

    public string Message { get; set; }
    public string EmailId { get; set; }
    public string ContactNo { get; set; }
    public string Company { get; set; }
    public string UserName { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string Status { set; get; }
    public static List<Vendor> GetAllVendor(SqlConnection conAP)
    {
        List<Vendor> Blogs = new List<Vendor>();
        try
        {
            string query = "select * from Vendor order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new Vendor()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             UserName = Convert.ToString(dr["UserName"]),
                             EmailId = Convert.ToString(dr["Email"]),
                             ContactNo = Convert.ToString(dr["Phone"]),
                             Message = Convert.ToString(dr["Message"]),
                             Company = Convert.ToString(dr["Company"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllVendor", ex.Message);
        }
        return Blogs;
    }

    public static int InsertVendor(SqlConnection conAP, Vendor Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Vendor (UserName,Email,Phone,Message,Company,AddedOn,AddedIp) values(@UserName,@Email,@Phone,@Message,@Company,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = Blog.UserName;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.EmailId;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.ContactNo;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = Blog.Message;
                cmd.Parameters.AddWithValue("@Company", SqlDbType.NVarChar).Value = Blog.Company;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertVendor", ex.Message);
        }
        return result;
    }

    #endregion
}
