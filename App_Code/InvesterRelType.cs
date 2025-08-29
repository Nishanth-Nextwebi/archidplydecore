using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvesterRelType
/// </summary>
public class InvesterRelType
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string DisplayOrder { get; set; }
    public string InversterGuid { get; set; }
    public DateTime AddedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string AddedIp { get; set; }
    public string UpdatedIp { get; set; }
    public string AddedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string Status { get; set; }

    public static int InsertInvesterRelType(SqlConnection conAP, InvesterRelType cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into InvesterRelType (DisplayOrder,Title,InversterGuid,Status,AddedIp,AddedOn,AddedBy,UpdatedBy,UpdatedIp,UpdatedOn) values(@DisplayOrder,@Title,@InversterGuid,@Status,@AddedIp,@AddedOn,@AddedBy,@UpdatedBy,@UpdatedIp,@UpdatedOn) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                sqlCommand.Parameters.AddWithValue("@InversterGuid", SqlDbType.NVarChar).Value = cat.InversterGuid;
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                sqlCommand.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertInvesterRelType", ex.Message);
        }

        return result;
    }
    public static int UpdateInvesterRelType(SqlConnection conAP, InvesterRelType cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update InvesterRelType Set DisplayOrder=@DisplayOrder,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp,UpdatedOn=@UpdatedOn, Title=@Title,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                sqlCommand.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                sqlCommand.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value =TimeStamps.UTCTime();
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
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
    public static List<InvesterRelType> GetAllInvesterRelType(SqlConnection conAP)
    {
        List<InvesterRelType> result = new List<InvesterRelType>();
        try
        {
            string cmdText = "Select * from InvesterRelType where Status!='Deleted' order by CAST(DisplayOrder AS INT)";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvesterRelType
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvesterRelType", ex.Message);
        }

        return result;
    }
    public static List<InvesterRelType> GetInvesterRelType(SqlConnection conAP, int id)
    {
        List<InvesterRelType> result = new List<InvesterRelType>();
        try
        {
            string cmdText = "Select * from InvesterRelType where Status=@Status and Id=@Id order by CAST(DisplayOrder AS INT)";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new InvesterRelType
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Title = Convert.ToString(dr["Title"]),
                              DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                              InversterGuid = Convert.ToString(dr["InversterGuid"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelType", ex.Message);
        }

        return result;
    }

    public static InvesterRelType GetInvesterRelTypeByGuid(SqlConnection conAP, string id)
    {
        InvesterRelType pds = null;
        try
        {
            string query = "Select *  from InvesterRelType where Status='Active' and InversterGuid=@id order by CAST(DisplayOrder AS INT)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new InvesterRelType();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.Title = Convert.ToString(dt.Rows[0]["Title"]);
                    pds.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    pds.InversterGuid = Convert.ToString(dt.Rows[0]["InversterGuid"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }


    public static int DeleteInvesterRelType(SqlConnection conAP, InvesterRelType cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update InvesterRelType Set Status=@Status,UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                sqlCommand.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAP.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteInvesterRelType", ex.Message);
        }

        return result;
    }
}