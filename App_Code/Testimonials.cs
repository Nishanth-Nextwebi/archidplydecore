using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Testimonials
/// </summary>
public class Testimonials
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string Details { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }

    public static int InsertTestimonials(SqlConnection conAP, Testimonials cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into Testimonials (UserName,Details,Image,Status,AddedIp,AddedOn,AddedBy) values(@UserName,@Details,@Image,@Status,@AddedIp,@AddedOn,@AddedBy) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = cat.UserName;
                sqlCommand.Parameters.AddWithValue("@Details", SqlDbType.NVarChar).Value = cat.Details;
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
    public static int UpdateTestimonials(SqlConnection conAP, Testimonials cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update Testimonials Set UserName=@UserName,Details=@Details,Image=@Image,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = cat.UserName;
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
    public static List<Testimonials> GetAllTestimonials(SqlConnection conAP)
    {
        List<Testimonials> result = new List<Testimonials>();
        try
        {
            string cmdText = "Select * from Testimonials where Status='Active'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new Testimonials
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              UserName = Convert.ToString(dr["UserName"]),
                              Details = Convert.ToString(dr["Details"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllTestimonials", ex.Message);
        }

        return result;
    }
    public static List<Testimonials> GetTestimonials(SqlConnection conAP, int id)
    {
        List<Testimonials> result = new List<Testimonials>();
        try
        {
            string cmdText = "Select * from Testimonials where Status=@Status and Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new Testimonials
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              UserName = Convert.ToString(dr["UserName"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTestimonials", ex.Message);
        }

        return result;
    }


    public static int DeleteTestimonials(SqlConnection conAP, Testimonials cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update Testimonials Set Status=@Status,AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteTestimonials", ex.Message);
        }

        return result;
    }
}