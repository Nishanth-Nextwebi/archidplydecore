using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

/// <summary>
/// Summary description for ShippingCharges
/// </summary>
public class ShippingCharges
{
    public int Id { get; set; }
    public Decimal ShippingCharge { get; set; }
    public Decimal MinCartPrice { get; set; }
    public string Status { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string UpdatedIp { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; }

    public static int InserShippingCharges(SqlConnection conAP, ShippingCharges charges)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ShippingCharges (ShippingCharge,MinCartPrice,AddedBy,UpdatedIp,UpdatedOn,UpdatedBy,AddedOn,AddedIp,Status) values(@ShippingCharge,@MinCartPrice,@AddedBy,@UpdatedIp,@UpdatedOn,@UpdatedBy,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ShippingCharge", SqlDbType.NVarChar).Value = charges.ShippingCharge;
                cmd.Parameters.AddWithValue("@MinCartPrice", SqlDbType.NVarChar).Value = charges.MinCartPrice;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = charges.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = charges.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InserShippingCharges", ex.Message);
        }
        return result;
    }
    public static int UpdateShippingCharges(SqlConnection conAP, ShippingCharges charges)
    {
        int result = 0;
        try
        {
            string query = "Update ShippingCharges Set MinCartPrice=@MinCartPrice,ShippingCharge=@ShippingCharge,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = charges.Id;
                cmd.Parameters.AddWithValue("@ShippingCharge", SqlDbType.NVarChar).Value = charges.ShippingCharge;
                cmd.Parameters.AddWithValue("@MinCartPrice", SqlDbType.NVarChar).Value = charges.MinCartPrice;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = charges.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateShippingCharges", ex.Message);
        }
        return result;
    }
    public static int DeleteShippingCharges(SqlConnection conAP, ShippingCharges charges)
    {
        int result = 0;
        try
        {
            string query = "Update ShippingCharges Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = charges.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = charges.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteShippingCharges", ex.Message);
        }
        return result;
    }
    public static List<ShippingCharges> CheckDetailsExist(SqlConnection conAP,decimal price, decimal cart)
    {
        List<ShippingCharges> charges = new List<ShippingCharges>();
        try
        {
            string query = "Select * from ShippingCharges where Status='Active' and MinCartPrice=@MinCartPrice and ShippingCharge=@ShippingCharge";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@MinCartPrice", SqlDbType.NVarChar).Value = cart;
                cmd.Parameters.AddWithValue("@ShippingCharge", SqlDbType.NVarChar).Value = price;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                charges = (from DataRow dr in dt.Rows
                           select new ShippingCharges()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               MinCartPrice = Convert.ToInt32(Convert.ToString(dr["MinCartPrice"])),
                               ShippingCharge = Convert.ToInt32(Convert.ToString(dr["ShippingCharge"])),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckDetailsExist", ex.Message);
        }
        return charges;
    }
    public static List<ShippingCharges> GetAllcharges(SqlConnection conAP)
    {
        List<ShippingCharges> charges = new List<ShippingCharges>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=ShippingCharges.AddedBy) as adby from ShippingCharges where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                charges = (from DataRow dr in dt.Rows
                           select new ShippingCharges()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               MinCartPrice = Convert.ToDecimal(Convert.ToString(dr["MinCartPrice"])),
                               ShippingCharge = Convert.ToDecimal(Convert.ToString(dr["ShippingCharge"])),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddedBy = Convert.ToString(dr["adby"]),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllcharges", ex.Message);
        }
        return charges;
    }
    public static List<ShippingCharges> GetchargesByID(SqlConnection conAP, int Id)
    {
        List<ShippingCharges> charges = new List<ShippingCharges>();
        try
        {
            string query = "Select * from ShippingCharges where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Id;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                charges = (from DataRow dr in dt.Rows
                           select new ShippingCharges()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               ShippingCharge = Convert.ToDecimal(Convert.ToString(dr["ShippingCharge"])),
                               MinCartPrice = Convert.ToDecimal(Convert.ToString(dr["MinCartPrice"])),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetchargesByID", ex.Message);
        }
        return charges;
    }
    public ShippingCharges()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}