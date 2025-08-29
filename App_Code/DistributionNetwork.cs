using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DistributionNetwork
/// </summary>
public class DistributionNetwork
{
    public DistributionNetwork()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public string City { get; set; }
    public DateTime AddedOn { get; set; }
    public string State { get; set; }
    #endregion
    #region Contactus region

    public static List<DistributionNetwork> GetAllDistributionNetwork(SqlConnection conAP)
    {
        List<DistributionNetwork> Blogs = new List<DistributionNetwork>();
        try
        {
            string query = "select * from DistributionNetwork where Status ='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new DistributionNetwork()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             Email = Convert.ToString(dr["Email"]),
                             Name = Convert.ToString(dr["Name"]),
                             Phone = Convert.ToString(dr["Phone"]),
                             City = Convert.ToString(dr["City"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDistributionNetwork", ex.Message);
        }
        return Blogs;
    }

    public static List<DistributionNetwork> GetAllDistributionNetworkdCheck(SqlConnection conAP, string Email)
    {
        List<DistributionNetwork> Blogs = new List<DistributionNetwork>();
        try
        {
            string query = "select * from DistributionNetwork where Email=@Email";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Email;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new DistributionNetwork()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             Email = Convert.ToString(dr["Email"]),
                             Name = Convert.ToString(dr["Name"]),
                             City = Convert.ToString(dr["City"]),
                             State = Convert.ToString(dr["State"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDistributionNetwork", ex.Message);
        }
        return Blogs;
    }

    public static int InserDistributionNetwork(SqlConnection conAP, DistributionNetwork Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into DistributionNetwork (Name,City,State,Phone,Message,Email,AddedOn,Status) values(@Name,@City,@State,@Phone,@Message,@Email,@AddedOn,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = Blog.Name;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = Blog.City;
                cmd.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = Blog.State;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.Phone;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = Blog.Message;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.Email;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserDistributionNetwork", ex.Message);
        }
        return result;
    }

    #endregion
    public static int DeleteDistributionNetwork(SqlConnection _con, DistributionNetwork cat)
    {
        int result = 0;
        try
        {
            string query = "Update DistributionNetwork Set Status=@Status Where Id=@Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteDistributionNetwork", ex.Message);
        }
        return result;
    }
}