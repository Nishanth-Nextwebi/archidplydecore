using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class CouponCode
{
    public int Id { set; get; }
    public int NoOfUsage { set; get; }
    public string CType { get; set; }
    public string CValue { get; set; }
    public string CCode { get; set; }
    public string CustomerId { get; set; }
    public string CDesc { get; set; }
    public Decimal minCartVal { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public DateTime ExpiryDate { get; set; }
    public string Status { set; get; }
    #region CouponCode Mehtods
    public static List<CouponCode> GetAllCoupon(SqlConnection conAP)
    {
        List<CouponCode> coupons = new List<CouponCode>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=CouponCode.AddedBy) as adby from CouponCode where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                coupons = (from DataRow dr in dt.Rows
                          select new CouponCode()
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              NoOfUsage = Convert.ToInt32(dr["NoOfUsage"]),
                              CCode = Convert.ToString(dr["CCode"]),
                              ExpiryDate = Convert.ToDateTime(Convert.ToString(dr["ExpiryDate"])),
                              CType =  Convert.ToString(dr["CType"]),
                              CustomerId =  Convert.ToString(dr["CustomerId"]),
                              CValue = Convert.ToString(dr["CValue"]),
                              CDesc = Convert.ToString(dr["CDesc"]),
                              minCartVal =Convert.ToDecimal(Convert.ToString(dr["minCartVal"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              AddedBy = Convert.ToString(dr["adby"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
        return coupons;
    }
    public static List<CouponCode> GetCouponByCID(SqlConnection conAP, string Id)
    {
        List<CouponCode> coupons = new List<CouponCode>();
        try
        {
            string query = "Select * from CouponCode where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Id;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                coupons = (from DataRow dr in dt.Rows
                           select new CouponCode()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               NoOfUsage = Convert.ToInt32(Convert.ToString(dr["NoOfUsage"])),
                               CCode = Convert.ToString(dr["CCode"]),
                               ExpiryDate = Convert.ToDateTime(Convert.ToString(dr["ExpiryDate"])),
                               CType = Convert.ToString(dr["CType"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               CValue = Convert.ToString(dr["CValue"]),
                               CDesc = Convert.ToString(dr["CDesc"]),
                               minCartVal = Convert.ToDecimal(Convert.ToString(dr["minCartVal"])),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
        return coupons;
    }

    public static List<CouponCode> GetAllAvailableCoupons(SqlConnection conAP, string custid, string price)
    {
        List<CouponCode> coupons = new List<CouponCode>();
        try
        {
            string query = @"
            SELECT * FROM CouponCode 
            WHERE 
                (CustomerId = @CustomerId or CustomerId='All')
                AND minCartVal <= @Price
                AND Status = 'Active'
                AND ExpiryDate >= GETDATE()";

            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = custid;
                cmd.Parameters.AddWithValue("@Price", SqlDbType.NVarChar).Value = Convert.ToDecimal(price);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                coupons = (from DataRow dr in dt.Rows
                           select new CouponCode()
                           {
                               Id = Convert.ToInt32(dr["Id"]),
                               NoOfUsage = Convert.ToInt32(dr["NoOfUsage"]),
                               CCode = Convert.ToString(dr["CCode"]),
                               ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]),
                               CType = Convert.ToString(dr["CType"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               CValue = Convert.ToString(dr["CValue"]),
                               CDesc = Convert.ToString(dr["CDesc"]),
                               minCartVal = Convert.ToDecimal(dr["minCartVal"]),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddedBy = Convert.ToString(dr["AddedBy"]),
                               AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
        return coupons;
    }


    //public static List<CouponCode> GetAllAvailableCoupons(SqlConnection conAP,string custid,string price)
    //{
    //    List<CouponCode> coupons = new List<CouponCode>();
    //    try
    //    {
    //        string query = "Select * from CouponCode where CustomerId=@CustomerId or CustomerId='General' or CustomerId='' and  minCartVal >= " + price + " and Status='Active'";
    //        using (SqlCommand cmd = new SqlCommand(query, conAP))
    //        {
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = custid;
    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);
    //            coupons = (from DataRow dr in dt.Rows
    //                       select new CouponCode()
    //                       {
    //                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
    //                           NoOfUsage = Convert.ToInt32(dr["NoOfUsage"]),
    //                           CCode = Convert.ToString(dr["CCode"]),
    //                           ExpiryDate = Convert.ToDateTime(Convert.ToString(dr["ExpiryDate"])),
    //                           CType = Convert.ToString(dr["CType"]),
    //                           CustomerId = Convert.ToString(dr["CustomerId"]),
    //                           CValue = Convert.ToString(dr["CValue"]),
    //                           CDesc = Convert.ToString(dr["CDesc"]),
    //                           minCartVal = Convert.ToDecimal(Convert.ToString(dr["minCartVal"])),
    //                           AddedIp = Convert.ToString(dr["AddedIp"]),
    //                           AddedBy = Convert.ToString(dr["adby"]),
    //                           AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
    //                           Status = Convert.ToString(dr["Status"])
    //                       }).ToList();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
    //    }
    //    return coupons;
    //}
    public static List<CouponCode> GetCouponByCode(SqlConnection conAP,string name)
    {
        List<CouponCode> coupons = new List<CouponCode>();
        try
        {
            string query = "Select * from CouponCode where Status='Active' and CCode=@CCode";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@CCode", SqlDbType.NVarChar).Value = name;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                coupons = (from DataRow dr in dt.Rows
                           select new CouponCode()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               NoOfUsage = Convert.ToInt32(Convert.ToString(dr["NoOfUsage"])),
                               CCode = Convert.ToString(dr["CCode"]),
                               ExpiryDate = Convert.ToDateTime(Convert.ToString(dr["ExpiryDate"])),
                               CType = Convert.ToString(dr["CType"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               CValue = Convert.ToString(dr["CValue"]),
                               CDesc = Convert.ToString(dr["CDesc"]),
                               minCartVal = Convert.ToDecimal(Convert.ToString(dr["minCartVal"])),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
        return coupons;
    }
    public static List<CouponCode> GetCouponById(SqlConnection conAP)
    {
        List<CouponCode> coupons = new List<CouponCode>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=CouponCode.AddedBy) adby from CouponCode where Status='Active' ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                coupons = (from DataRow dr in dt.Rows
                           select new CouponCode()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               NoOfUsage = Convert.ToInt32(Convert.ToString(dr["NoOfUsage"])),
                               CCode = Convert.ToString(dr["CCode"]),
                               ExpiryDate = Convert.ToDateTime(Convert.ToString(dr["ExpiryDate"])),
                               CType = Convert.ToString(dr["CType"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               CValue = Convert.ToString(dr["CValue"]),
                               CDesc = Convert.ToString(dr["CDesc"]),
                               minCartVal = Convert.ToDecimal(Convert.ToString(dr["minCartVal"])),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddedBy = Convert.ToString(dr["adby"]),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
        return coupons;
    }
    public static int InsertCouponCode(SqlConnection conAP, CouponCode coupon)
    {
        int result = 0;

        try
        {
            string query = "Insert Into CouponCode (minCartVal,CDesc,CType,CValue,ExpiryDate,CCode,Status,AddedOn,AddedIp,AddedBy,UpdatedOn,UpdatedIp,UpdatedBy,NoOfUsage,CustomerId) values (@minCartVal,@CDesc,@CType,@CValue, @ExpiryDate,@CCode,@Status,@AddedOn,@AddedIp,@AddedBy,@UpdatedOn,@UpdatedIp,@UpdatedBy,@NoOfUsage,@CustomerId)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = coupon.CustomerId;
                cmd.Parameters.AddWithValue("@NoOfUsage", SqlDbType.NVarChar).Value = coupon.NoOfUsage;
                cmd.Parameters.AddWithValue("@CType", SqlDbType.NVarChar).Value = coupon.CType;
                cmd.Parameters.AddWithValue("@CValue", SqlDbType.NVarChar).Value = coupon.CValue; 
                cmd.Parameters.AddWithValue("@CDesc", SqlDbType.NVarChar).Value = coupon.CDesc;
                cmd.Parameters.AddWithValue("@minCartVal", SqlDbType.Decimal).Value = coupon.minCartVal;
                cmd.Parameters.AddWithValue("@ExpiryDate", SqlDbType.NVarChar).Value = coupon.ExpiryDate;
                cmd.Parameters.AddWithValue("@CCode", SqlDbType.NVarChar).Value = coupon.CCode; 
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = coupon.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = coupon.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertCouponCode", ex.Message);
        }
        return result;
    }
    public static int UpdateCouponCode(SqlConnection conAP, CouponCode coupon)
    {
        int result = 0;
        try
        {
            string query = "Update CouponCode Set minCartVal=@minCartVal,CDesc=@CDesc,CType=@CType,CValue=@CValue,NoOfUsage=@NoOfUsage,CustomerId=@CustomerId,ExpiryDate=@ExpiryDate,CCode=@CCode,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@CType", SqlDbType.NVarChar).Value = coupon.CType;
                cmd.Parameters.AddWithValue("@NoOfUsage", SqlDbType.NVarChar).Value = coupon.NoOfUsage;
                cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = coupon.CustomerId;
                cmd.Parameters.AddWithValue("@CValue", SqlDbType.NVarChar).Value = coupon.CValue; 
                cmd.Parameters.AddWithValue("@CDesc", SqlDbType.NVarChar).Value = coupon.CDesc;
                cmd.Parameters.AddWithValue("@minCartVal", SqlDbType.Decimal).Value = coupon.minCartVal;
                cmd.Parameters.AddWithValue("@ExpiryDate", SqlDbType.NVarChar).Value = coupon.ExpiryDate;
                cmd.Parameters.AddWithValue("@CCode", SqlDbType.NVarChar).Value = coupon.CCode; 
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = coupon.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = coupon.Id;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCouponCode", ex.Message);
        }
        return result;
    }
    public static int DeleteCouponCode(SqlConnection conAP, CouponCode coupon)
    {
        int result = 0;
        try
        {
            string query = "Update CouponCode Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = coupon.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value ="Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = coupon.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value =CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteCouponCode", ex.Message);
        }
        return result;
    }
    #endregion
}