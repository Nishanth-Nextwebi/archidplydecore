using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Brochures
/// </summary>
public class Brochures
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string PDF { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }


    public static int InsertBrochures(SqlConnection conAP, Brochures cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into Brochures (Title,PDF,Image,Status,AddedIp,AddedOn,AddedBy) values(@Title,@PDF,@Image,@Status,@AddedIp,@AddedOn,@AddedBy) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                sqlCommand.Parameters.AddWithValue("@PDF", SqlDbType.NVarChar).Value = cat.PDF;
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
    public static int UpdateBrochures(SqlConnection conAP, Brochures cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update Brochures Set Title=@Title,PDF=@PDF,Image=@Image,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                sqlCommand.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = cat.Image;
                sqlCommand.Parameters.AddWithValue("@PDF", SqlDbType.NVarChar).Value = cat.PDF;
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
    public static List<Brochures> GetAllBrochures(SqlConnection conAP)
    {
        List<Brochures> result = new List<Brochures>();
        try
        {
            string cmdText = "Select * from Brochures where Status='Active'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new Brochures
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              PDF = Convert.ToString(dr["PDF"]),
                              Image = Convert.ToString(dr["Image"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBrochures", ex.Message);
        }

        return result;
    }
    public static List<Brochures> GetBrochures(SqlConnection conAP, int id)
    {
        List<Brochures> result = new List<Brochures>();
        try
        {
            string cmdText = "Select * from Brochures where Status=@Status and Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new Brochures
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              PDF = Convert.ToString(dr["PDF"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBrochures", ex.Message);
        }

        return result;
    }


    public static int DeleteBrochures(SqlConnection conAP, Brochures cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update Brochures Set Status=@Status,AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBrochures", ex.Message);
        }

        return result;
    }
}