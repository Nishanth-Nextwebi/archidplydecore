using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for City
/// </summary>
public class City
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

    public static int InsertCity(SqlConnection conAP, City cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into City (CityName,Status,AddedIp,AddedOn,AddedBy,UpdatedBy,UpdatedIp,UpdatedOn) values(@CityName,@Status,@AddedIp,@AddedOn,@AddedBy,@UpdatedBy,@UpdatedIp,@UpdatedOn) ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertCity", ex.Message);
        }

        return result;
    }
    public static int UpdateCity(SqlConnection conAP, City cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update City Set UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp,UpdatedOn=@UpdatedOn, CityName=@CityName,Status=@Status where Id=@Id";
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
    public static List<City> GetAllCity(SqlConnection conAP)
    {
        List<City> result = new List<City>();
        try
        {
            string cmdText = "Select * from City where Status!='Deleted' order by id desc";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new City
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCity", ex.Message);
        }

        return result;
    }
    public static List<City> GetCity(SqlConnection conAP, int id)
    {
        List<City> result = new List<City>();
        try
        {
            string cmdText = "Select * from City where Status=@Status and Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new City
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCity", ex.Message);
        }

        return result;
    }
    public static List<City> GetCityByCity(SqlConnection conAP, string cat)
    {
        List<City> result = new List<City>();
        try
        {
            string cmdText = "Select * from City where Status!='Deleted' and CityName=@CityName";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@CityName", SqlDbType.NVarChar).Value = cat;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new City
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCity", ex.Message);
        }

        return result;
    }
    public static City GetCityByGuid(SqlConnection conAP, string id)
    {
        City pds = null;
        try
        {
            string query = "Select *  from City where Status='Active' and Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new City();
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


    public static int DeleteCity(SqlConnection conAP, City cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update City Set Status=@Status,UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteCity", ex.Message);
        }

        return result;
    }
}