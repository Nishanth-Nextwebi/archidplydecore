using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvesterRelations
/// </summary>
public class InvesterRelations
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string DisplayOrder { get; set; }
    public string InversterGuid { get; set; }
    public string PDF { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }

    public static int InsertInvesterRelations(SqlConnection conAP, InvesterRelations cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into InvesterRelations (DisplayOrder,InversterGuid,Title,PDF,Status,AddedIp,AddedOn,AddedBy) values(@DisplayOrder,@InversterGuid,@Title,@PDF,@Status,@AddedIp,@AddedOn,@AddedBy) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                sqlCommand.Parameters.AddWithValue("@InversterGuid", SqlDbType.NVarChar).Value = cat.InversterGuid;
                sqlCommand.Parameters.AddWithValue("@PDF", SqlDbType.NVarChar).Value = cat.PDF;
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertInvesterRelations", ex.Message);
        }

        return result;
    }
    public static int UpdateInvesterRelations(SqlConnection conAP, InvesterRelations cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update InvesterRelations Set DisplayOrder=@DisplayOrder,Title=@Title,PDF=@PDF,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
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
    public static List<InvesterRelations> GetAllInvesterRelations(SqlConnection conAP)
    {
        List<InvesterRelations> result = new List<InvesterRelations>();
        try
        {
            string cmdText = "Select * from InvesterRelations where Status!='Deleted' order by CAST(DisplayOrder AS INT)";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvesterRelations
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                              PDF = Convert.ToString(dr["PDF"]),
                              InversterGuid = Convert.ToString(dr["InversterGuid"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvesterRelations", ex.Message);
        }

        return result;
    }
    public static List<InvesterRelations> GetInvesterRelations(SqlConnection conAP, string id)
    {
        List<InvesterRelations> result = new List<InvesterRelations>();
        try
        {
            string cmdText = "Select * from InvesterRelations where Status=@Status and InversterGuid=@Id order by CAST(DisplayOrder AS INT) desc";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvesterRelations
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                              InversterGuid = Convert.ToString(dr["InversterGuid"]),
                              PDF = Convert.ToString(dr["PDF"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelations", ex.Message);
        }

        return result;
    }

    public static List<InvesterRelations> GetInvesterRelationsById(SqlConnection conAP, string id)
    {
        List<InvesterRelations> result = new List<InvesterRelations>();
        try
        {
            string cmdText = "Select * from InvesterRelations where Status=@Status and Id=@Id order by CAST(DisplayOrder AS INT)";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvesterRelations
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              InversterGuid = Convert.ToString(dr["InversterGuid"]),
                              DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                              PDF = Convert.ToString(dr["PDF"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelations", ex.Message);
        }

        return result;
    }
    public static int DeleteInvesterRelations(SqlConnection conAP, InvesterRelations cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update InvesterRelations Set Status=@Status,AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteInvesterRelations", ex.Message);
        }

        return result;
    }
}