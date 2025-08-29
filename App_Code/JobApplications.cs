using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JobApplications
/// </summary>
public class JobApplications
{
    public JobApplications()
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
    public string InterestedField { get; set; }
    public string UserGuid { get; set; }
    public DateTime AddedOn { get; set; }
    public string CoverLetter { get; set; }
    public string Exp { get; set; }
    public string City { get; set; }
    #endregion
    #region Contactus region

    public static List<JobApplications> GetAllJobApplications(SqlConnection conAP)
    {
        List<JobApplications> Blogs = new List<JobApplications>();
        try
        {
            string query = "select * from JobApplications where Status ='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new JobApplications()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             Email = Convert.ToString(dr["Email"]),
                             Name = Convert.ToString(dr["Name"]),
                             Phone = Convert.ToString(dr["Phone"]),
                             City = Convert.ToString(dr["City"]),
                             Exp = Convert.ToString(dr["Exp"]),
                             CoverLetter = Convert.ToString(dr["CoverLetter"]),
                             InterestedField = Convert.ToString(dr["InterestedField"]),
                             UserGuid = Convert.ToString(dr["UserGuid"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllJobApplications", ex.Message);
        }
        return Blogs;
    }

    public static List<JobApplications> GetAllJobApplicationsdCheck(SqlConnection conAP, string Email)
    {
        List<JobApplications> Blogs = new List<JobApplications>();
        try
        {
            string query = "select * from JobApplications where Email=@Email";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Email;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new JobApplications()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             Email = Convert.ToString(dr["Email"]),
                             Name = Convert.ToString(dr["Name"]),
                             City = Convert.ToString(dr["City"]),
                             Exp = Convert.ToString(dr["Exp"]),
                             UserGuid = Convert.ToString(dr["UserGuid"]),
                             CoverLetter = Convert.ToString(dr["CoverLetter"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllJobApplications", ex.Message);
        }
        return Blogs;
    }

    public static int InserJobApplications(SqlConnection conAP, JobApplications Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into JobApplications (Name,UserGuid,CoverLetter,Phone,InterestedField,Email,AddedOn,Status,Exp,City) values(@Name,@UserGuid,@CoverLetter,@Phone,@InterestedField,@Email,@AddedOn,@Status,@Exp,@City)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = Blog.Name;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = Blog.City;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = Blog.UserGuid;
                cmd.Parameters.AddWithValue("@CoverLetter", SqlDbType.NVarChar).Value = Blog.CoverLetter;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.Phone;
                cmd.Parameters.AddWithValue("@InterestedField", SqlDbType.NVarChar).Value = Blog.InterestedField;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.Email;
                cmd.Parameters.AddWithValue("@Exp", SqlDbType.NVarChar).Value = Blog.Exp;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserJobApplications", ex.Message);
        }
        return result;
    }

    #endregion
    public static int DeleteJobApplications(SqlConnection _con, JobApplications cat)
    {
        int result = 0;
        try
        {
            string query = "Update JobApplications Set Status=@Status Where Id=@Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteJobApplications", ex.Message);
        }
        return result;
    }
}