using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvestorsList
/// </summary>
public class InvestorsList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Logo { get; set; }
    public string Link { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string Status { get; set; }

    public static int InsertInvestorList(SqlConnection conAP, InvestorsList pro)
    {
        int result = 0;
        try
        {
            string query = "Insert Into InvestorsList (Title,Link,Logo,AddedBy,AddedOn,AddedIp,Status) values(@Title,@Link,@Logo,@AddedBy,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = pro.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = pro.Link;
                cmd.Parameters.AddWithValue("@Logo", SqlDbType.NVarChar).Value = pro.Logo;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = pro.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertInvestorImage", ex.Message);
        }
        return result;
    }

    public static int UpdateInvestorList(SqlConnection conAP, InvestorsList investor)
    {
        int result = 0;
        try
        {
            string query = "Update InvestorsList Set Title=@Title,Link=@Link,AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = investor.Id;
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = investor.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = investor.Link;
                cmd.Parameters.AddWithValue("@Logo", SqlDbType.NVarChar).Value = investor.Logo;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = investor.AddedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateInvestor", ex.Message);
        }
        return result;
    }
    public static List<InvestorsList> GetInvestorList(SqlConnection conAP, int id)
    {
        List<InvestorsList> result = new List<InvestorsList>();
        try
        {
            string cmdText = "Select * from InvestorsList where Status=@Status and Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvestorsList
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Logo = Convert.ToString(dr["Logo"]),
                              Title = Convert.ToString(dr["Title"]),
                              Link = Convert.ToString(dr["Link"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvestorList", ex.Message);
        }

        return result;
    }
    public static List<InvestorsList> GetAllInvestorList(SqlConnection conAP)
    {
        List<InvestorsList> result = new List<InvestorsList>();
        try
        {
            string cmdText = "Select * from InvestorsList where Status='Active'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvestorsList
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Logo = Convert.ToString(dr["Logo"]),
                              Title = Convert.ToString(dr["Title"]),
                              Link = Convert.ToString(dr["Link"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvestorList", ex.Message);
        }

        return result;
    }
    public static int DeleteInvestorsList(SqlConnection conAP, InvestorsList cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update InvestorsList Set Status=@Status,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteInvestorsList", ex.Message);
        }

        return result;
    }
    public InvestorsList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}