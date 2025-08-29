using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ResourceRequests
{
  
    #region Contactus region
    public int Id { set; get; }
    public string ResourceName { get; set; }
    public string EmailId { get; set; }
    public string Profession { get; set; }
    public string ContactNo { get; set; }
    public string Name { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string Status { set; get; }
    public static List<ResourceRequests> GetAllResourceRequests(SqlConnection conAP)
    {
        List<ResourceRequests> Blogs = new List<ResourceRequests>();
        try
        {
            string query = "select * from ResourceRequests where Status='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new ResourceRequests()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             Name = Convert.ToString(dr["Name"]),
                             Profession = Convert.ToString(dr["Profession"]),
                             EmailId = Convert.ToString(dr["Email"]),
                             ContactNo = Convert.ToString(dr["Phone"]),
                             ResourceName = Convert.ToString(dr["ResourceName"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllResourceRequests", ex.Message);
        }
        return Blogs;
    }

    public static int InserResourceRequests(SqlConnection conAP, ResourceRequests Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ResourceRequests (Status,Name,Email,Phone,ResourceName,AddedOn,AddedIp,Profession) values(@Status,@Name,@Email,@Phone,@ResourceName,@AddedOn,@AddedIp,@Profession)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = Blog.Name;
                cmd.Parameters.AddWithValue("@Profession", SqlDbType.NVarChar).Value = Blog.Profession;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = Blog.EmailId;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = Blog.ContactNo;
                cmd.Parameters.AddWithValue("@ResourceName", SqlDbType.NVarChar).Value = Blog.ResourceName;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserResourceRequests", ex.Message);
        }
        return result;
    }
    public static int DeleteResourceRequests(SqlConnection _con, ResourceRequests cat)
    {
        int result = 0;
        try
        {
            string query = "Update ResourceRequests Set Status=@Status Where Id=@Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteResourceRequests", ex.Message);
        }
        return result;
    }


    public static string GetResourceNameById(SqlConnection conAP, string id)
    {
        string result = null;
        try
        {
            string cmdText = "select ResourceName from ResourceRequests WHERE Id = @Id";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetResourceNameById", ex.Message);
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
