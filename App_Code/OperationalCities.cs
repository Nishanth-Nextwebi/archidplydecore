using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OperationalCities
/// </summary>
public class OperationalCities
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public DateTime AddedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string AddedIp { get; set; }
    public string UpdatedIp { get; set; }
    public string AddedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string Status { get; set; }

    public static int InsertOperationalCities(SqlConnection conAP, OperationalCities cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into OperationalCities (CityName,Status,AddedIp,AddedOn,AddedBy,UpdatedBy,UpdatedIp,UpdatedOn) values(@CityName,@Status,@AddedIp,@AddedOn,@AddedBy,@UpdatedBy,@UpdatedIp,@UpdatedOn) ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@CityName", SqlDbType.NVarChar).Value = cat.CityName;
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertOperationalCities", ex.Message);
        }

        return result;
    }
    public static int UpdateOperationalCities(SqlConnection conAP, OperationalCities cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update OperationalCities Set UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp,UpdatedOn=@UpdatedOn, CityName=@CityName,Status=@Status where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@CityName", SqlDbType.NVarChar).Value = cat.CityName;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                sqlCommand.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
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
    public static List<OperationalCities> GetAllOperationalCities(SqlConnection conAP)
    {
        List<OperationalCities> result = new List<OperationalCities>();
        try
        {
            string cmdText = "Select * from OperationalCities where Status!='Deleted' order by id desc";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new OperationalCities
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              CityName = Convert.ToString(dr["CityName"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllOperationalCities", ex.Message);
        }

        return result;
    }
    public static List<OperationalCities> GetOperationalCities(SqlConnection conAP, int id)
    {
        List<OperationalCities> result = new List<OperationalCities>();
        try
        {
            string cmdText = "Select * from OperationalCities where Status=@Status and Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new OperationalCities
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              CityName = Convert.ToString(dr["CityName"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOperationalCities", ex.Message);
        }

        return result;
    }
    public static List<OperationalCities> GetOperationalCitiesByOpCity(SqlConnection conAP, string cat)
    {
        List<OperationalCities> result = new List<OperationalCities>();
        try
        {
            string cmdText = "Select * from OperationalCities where Status!='Deleted' and CityName=@CityName";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@CityName", SqlDbType.NVarChar).Value = cat;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new OperationalCities
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              CityName = Convert.ToString(dr["CityName"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOperationalCities", ex.Message);
        }

        return result;
    }
    public static OperationalCities GetOperationalCitiesByGuid(SqlConnection conAP, string id)
    {
        OperationalCities pds = null;
        try
        {
            string query = "Select *  from OperationalCities where Status='Active' and Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new OperationalCities();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.CityName = Convert.ToString(dt.Rows[0]["CityName"]);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }

    public static List<OperationalCities> GetOperationalCitiesByCity(SqlConnection conAP)
    {
        List<OperationalCities> categories = new List<OperationalCities>();
        try
        {
            string query = "Select*,(Select Top 1 UserName From CreateUser Where UserGuid=OperationalCities.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=OperationalCities.UpdatedBy) UpdatedBy1  from OperationalCities where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new OperationalCities()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  CityName = Convert.ToString(dr["CityName"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIP"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllOperationalCities", ex.Message);
        }
        return categories;
    }
    public static int DeleteOperationalCities(SqlConnection conAP, OperationalCities cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update OperationalCities Set Status=@Status,UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteOperationalCities", ex.Message);
        }

        return result;
    }
}