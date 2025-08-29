using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class PinCode
{
    public int Id { set; get; }
    public string Pincode { get; set; }
    public string ShippingPrice { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    public string City { get; set; }
    #region Admin Pincode region
    public static List<PinCode> GetAllPincode(SqlConnection conAP)
    {
        List<PinCode> categories = new List<PinCode>();
        try
        {
            string query = "Select Top 100 *,(Select Top 1 UserName From CreateUser Where UserGuid=pd.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid = pd.UpdatedBy) UpdatedBy1 from Pincode pd where pd.Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PinCode()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pincode = Convert.ToString(dr["Pincode"]),
                                  City = Convert.ToString(dr["PCity"]),
                                  ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPincode", ex.Message);
        }
        return categories;
    }

    public static List<PinCode> GetPincodeById(SqlConnection conAP, int Id)
    {
        List<PinCode> categories = new List<PinCode>();
        try
        {
            string query = "Select Top 100 *,(Select Top 1 UserName From CreateUser Where UserGuid=pd.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid = pd.UpdatedBy) UpdatedBy1 from Pincode pd where pd.Status = 'Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PinCode()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pincode = Convert.ToString(dr["Pincode"]),
                                  City = Convert.ToString(dr["PCity"]),
                                  ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPincode", ex.Message);
        }
        return categories;
    }
    public static List<PinCode> GetPincodeByPinCode(SqlConnection conAP, string pincode)
    {
        List<PinCode> categories = new List<PinCode>();
        try
        {
            string query = "Select Top 100 *,(Select Top 1 UserName From CreateUser Where UserGuid=pd.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid = pd.UpdatedBy) UpdatedBy1 from Pincode pd where pd.Status = 'Active' and Pincode=@Pincode";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = pincode;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PinCode()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pincode = Convert.ToString(dr["Pincode"]),
                                  City = Convert.ToString(dr["PCity"]),
                                  ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPincode", ex.Message);
        }
        return categories;
    }
    public static List<PinCode> GetAllPincodeByCode(SqlConnection conAP, string pincode)
    {
        List<PinCode> categories = new List<PinCode>();
        try
        {
            string query = "Select Top 100 *,(Select Top 1 UserName From CreateUser Where UserGuid=pd.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid = pd.UpdatedBy) UpdatedBy1 from Pincode pd where pd.Status = 'Active' and (pd.Pincode like '%'+@Pincode+'%' or pd.PCity like '%'+@Pincode+'%')";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = pincode;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PinCode()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pincode = Convert.ToString(dr["Pincode"]),
                                  City = Convert.ToString(dr["PCity"]),
                                  ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPincode", ex.Message);
        }
        return categories;
    }
    public static int InsertPincode(SqlConnection conAP, PinCode cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Pincode (Pincode,ShippingPrice,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp,PCity) values(@Pincode,@ShippingPrice,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp,@PCity)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = cat.Pincode;
                cmd.Parameters.AddWithValue("@PCity", SqlDbType.NVarChar).Value = cat.City;
                cmd.Parameters.AddWithValue("@ShippingPrice", SqlDbType.NVarChar).Value = cat.ShippingPrice;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value ="Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertPincode", ex.Message);
        }
        return result;
    }
    public static int UpdatePincode(SqlConnection conAP, PinCode cat)
    {
        int result = 0;
        try
        {
            string query = "Update Pincode Set Pincode=@Pincode,ShippingPrice=@ShippingPrice,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy,PCity=@PCity Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = cat.Pincode;
                cmd.Parameters.AddWithValue("@PCity", SqlDbType.NVarChar).Value = cat.City;
                cmd.Parameters.AddWithValue("@ShippingPrice", SqlDbType.NVarChar).Value = cat.ShippingPrice;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdatePincode", ex.Message);
        }
        return result;
    }
    public static int DeletePincode(SqlConnection conAP, PinCode cat)
    {
        int result = 0;
        try
        {
            string query = "Update Pincode Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value =CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeletePincode", ex.Message);
        }
        return result;
    }
    #endregion
}