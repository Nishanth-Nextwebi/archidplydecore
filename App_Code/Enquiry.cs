using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Enquiry
{
    #region Enquiry region
    public int Id { set; get; }

    public string Message { get; set; }
    public string EmailId { get; set; }
    public string ContactNo { get; set; }
    public string UserName { get; set; }
    public string City { get; set; }
    public string Products { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string Status { set; get; }
    public string Size { set; get; }
    public string Thickness { set; get; }
    public string ProductType { set; get; }
    public static List<Enquiry> GetAllEnquiry(SqlConnection conAP)
    {
        List<Enquiry> enquirys = new List<Enquiry>();
        try
        {
            string query = "select * from Enquiry where status='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                enquirys = (from DataRow dr in dt.Rows
                         select new Enquiry()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             UserName = Convert.ToString(dr["UserName"]),
                             ProductType = Convert.ToString(dr["ProductType"]),
                             EmailId = Convert.ToString(dr["Email"]),
                             Thickness = Convert.ToString(dr["Thickness"]),
                             Size   = Convert.ToString(dr["Size"]),
                             City = Convert.ToString(dr["City"]),
                             ContactNo = Convert.ToString(dr["Phone"]),
                             Message = Convert.ToString(dr["Message"]),
                             Products = Convert.ToString(dr["Products"]),
                             Status = Convert.ToString(dr["Status"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllEnquiry", ex.Message);
        }
        return enquirys;
    }
    public static int DeleteEnquiry(SqlConnection _con, Enquiry cat)
    {
        int result = 0;
        try
        {
            string query = "Update Enquiry Set Status=@Status Where Id=@Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnquiry", ex.Message);
        }
        return result;
    }

    public static int InserEnquiry(SqlConnection conAP, Enquiry enquiry)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Enquiry (Size,Thickness,ProductType,Products,UserName,Email,City,Phone,Message,AddedOn,AddedIp,Status) values(@Size,@Thickness,@ProductType,@Products,@UserName,@Email,@City,@Phone,@Message,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = enquiry.UserName;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = enquiry.City;
                cmd.Parameters.AddWithValue("@Products", SqlDbType.NVarChar).Value = enquiry.Products;
                cmd.Parameters.AddWithValue("@ProductType", SqlDbType.NVarChar).Value = enquiry.ProductType;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = enquiry.EmailId;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@Thickness", SqlDbType.NVarChar).Value = enquiry.Thickness;
                cmd.Parameters.AddWithValue("@Size", SqlDbType.NVarChar).Value = enquiry.Size;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = enquiry.ContactNo;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = enquiry.Message;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserEnquiry", ex.Message);
        }
        return result;
    }

    #endregion
}
