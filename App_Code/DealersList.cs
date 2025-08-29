using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DealersList
/// </summary>
public class DealersList
{
    public int Id { get; set; }
    public string City { get; set; }
    public string CityName { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string EmailId { get; set; }
    public string Logo { get; set; }
    public string Link { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string Status { get; set; }

    public static int InsertDealerList(SqlConnection conAP, DealersList pro)
    {
        int result = 0;
        try
        {
            string query = "Insert Into DealersList (Name,EmailId,Address,Phone,City,Link,Logo,AddedBy,AddedOn,AddedIp,Status) values(@Name,@EmailId,@Address,@Phone,@City,@Link,@Logo,@AddedBy,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = pro.City;
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = pro.Name;
                cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = pro.Address;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = pro.EmailId;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = pro.Phone;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertDealerImage", ex.Message);
        }
        return result;
    }

    public static List<DealersList> GetDealerListByCity(SqlConnection conAP, string city)
    {
        List<DealersList> result = new List<DealersList>();
        try
        {
            string cmdText = "Select *,(SELECT TOP 1 CityName FROM City WHERE id = City) AS CityName from DealersList where Status=@Status and City=@City or @City=''";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@City", SqlDbType.Int).Value = city;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new DealersList
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Logo = Convert.ToString(dr["Logo"]),
                              City = Convert.ToString(dr["City"]),
                              CityName = Convert.ToString(dr["CityName"]),
                              Link = Convert.ToString(dr["Link"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              Name = Convert.ToString(dr["Name"]),
                              EmailId = Convert.ToString(dr["EmailId"]),
                              Phone = Convert.ToString(dr["Phone"]),
                              Address = Convert.ToString(dr["Address"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDealerList", ex.Message);
        }

        return result;
    }
    public static int UpdateDealerList(SqlConnection conAP, DealersList dealer)
    {
        int result = 0;
        try
        {
            string query = "Update DealersList Set Name=@Name,EmailId=@EmailId,Address=@Address,Phone=@Phone, City=@City,Link=@Link,AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = dealer.Id;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = dealer.City;
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = dealer.Name;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = dealer.EmailId;
                cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = dealer.Address;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = dealer.Phone;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = dealer.Link;
                cmd.Parameters.AddWithValue("@Logo", SqlDbType.NVarChar).Value = dealer.Logo;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = dealer.AddedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateDealer", ex.Message);
        }
        return result;
    }
    public static List<DealersList> GetDealerList(SqlConnection conAP, int id)
    {
        List<DealersList> result = new List<DealersList>();
        try
        {
            string cmdText = "Select * from DealersList where Status=@Status and Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new DealersList
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Logo = Convert.ToString(dr["Logo"]),
                              City = Convert.ToString(dr["City"]),
                              Link = Convert.ToString(dr["Link"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              Name = Convert.ToString(dr["Name"]),
                              EmailId = Convert.ToString(dr["EmailId"]),
                              Phone = Convert.ToString(dr["Phone"]),
                              Address = Convert.ToString(dr["Address"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDealerList", ex.Message);
        }

        return result;
    }
    public static List<DealersList> GetAllDealerList(SqlConnection conAP)
    {
        List<DealersList> result = new List<DealersList>();
        try
        {
            string cmdText = "Select *,(SELECT TOP 1 CityName FROM City WHERE id = City) AS CityName from DealersList where Status!='Deleted'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conAP))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new DealersList
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              Logo = Convert.ToString(dr["Logo"]),
                              City = Convert.ToString(dr["City"]),
                              CityName = Convert.ToString(dr["CityName"]),
                              Link = Convert.ToString(dr["Link"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              Name = Convert.ToString(dr["Name"]),
                              EmailId = Convert.ToString(dr["EmailId"]),
                              Phone = Convert.ToString(dr["Phone"]),
                              Address = Convert.ToString(dr["Address"])
                          }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDealerList", ex.Message);
        }

        return result;
    }
    public static int DeleteDealersList(SqlConnection conAP, DealersList cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update DealersList Set Status=@Status,AddedBy=@AddedBy Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteDealersList", ex.Message);
        }

        return result;
    }
    public DealersList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}