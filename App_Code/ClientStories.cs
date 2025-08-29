using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClientStories
/// </summary>
public class ClientStories
{
   public int Id { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public string Image { get; set; }
    public string Details { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }

    public static int InsertClientStories(SqlConnection conAP, ClientStories cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into ClientStories (Name,Designation,Details,Image,Status,AddedIp,AddedOn,AddedBy) values(@Name,@Designation,@Details,@Image,@Status,@AddedIp,@AddedOn,@AddedBy) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = cat.Name;
                sqlCommand.Parameters.AddWithValue("@Details", SqlDbType.NVarChar).Value = cat.Details;
                sqlCommand.Parameters.AddWithValue("@Designation", SqlDbType.NVarChar).Value = cat.Designation;
                sqlCommand.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = cat.Image;
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertNoticeBoard", ex.Message);
        }

        return result;
    }
    public static int UpdateClientStories(SqlConnection conAP, ClientStories cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update ClientStories Set Designation=@Designation,Name=@Name,Details=@Details,Image=@Image,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = cat.Name;
                sqlCommand.Parameters.AddWithValue("@Designation", SqlDbType.NVarChar).Value = cat.Designation;
                sqlCommand.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = cat.Image;
                sqlCommand.Parameters.AddWithValue("@Details", SqlDbType.NVarChar).Value = cat.Details;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateNoticeBoard", ex.Message);
        }

        return result;
    }
    public static List<ClientStories> GetAllClientStories(SqlConnection conAP)
    {
        List<ClientStories> result = new List<ClientStories>();
        try
        {
            string cmdText = "Select * from ClientStories where Status!='Deleted'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new ClientStories
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Name = Convert.ToString(dr["Name"]),
                              Details = Convert.ToString(dr["Details"]),
                              Image = Convert.ToString(dr["Image"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Designation = Convert.ToString(dr["Designation"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllClientStories", ex.Message);
        }

        return result;
    }
    public static List<ClientStories> GetClientStories(SqlConnection conAP, int id)
    {
        List<ClientStories> result = new List<ClientStories>();
        try
        {
            string cmdText = "Select * from ClientStories where Status=@Status and Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new ClientStories
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Name = Convert.ToString(dr["Name"]),
                              Designation = Convert.ToString(dr["Designation"]),
                              Details = Convert.ToString(dr["Details"]),
                              Image = Convert.ToString(dr["Image"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetClientStories", ex.Message);
        }

        return result;
    }


    public static int DeleteClientStories(SqlConnection conAP, ClientStories cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update ClientStories Set Status=@Status,AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteClientStories", ex.Message);
        }

        return result;
    }
}