using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ShiptypeDetails

{


    #region Properties
    public int Id { get; set; }
    public string ShipType { get; set; }
    public string PincodeLocation { get; set; }
    public string Amount { get; set; }
    public DateTime AddedOn { get; set; }
    public string UpdatedIp { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string Status { get; set; }
    #endregion

     

    #region Admin Ship type details region

    public static List<ShiptypeDetails> GetAllShiptypeDetails(SqlConnection conAP)
    {
        List<ShiptypeDetails> categories = new List<ShiptypeDetails>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=AddShipType.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=AddShipType.UpdatedBy) UpdatedBy1 from AddShipType where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ShiptypeDetails()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ShipType = Convert.ToString(dr["ShipType"]),
                                  PincodeLocation = Convert.ToString(dr["PincodeLocation"]),
                                  Amount = Convert.ToString(dr["Amount"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllShiptypeDetails", ex.Message);
        }
        return categories;
    }


    public static List<ShiptypeDetails> GetShiptypeDetailsbyUrl(SqlConnection conAP, string catg)
    {
        List<ShiptypeDetails> categories = new List<ShiptypeDetails>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from AddShipType Where Status='Active' and (Id=@Id or ShipType=@ShipType or  PincodeLocation=@PincodeLocation)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@ShipType", SqlDbType.NVarChar).Value = catg;
                cmd.Parameters.AddWithValue("@PincodeLocation", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ShiptypeDetails cat = new ShiptypeDetails();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.ShipType = Convert.ToString(dt.Rows[0]["ShipType"]);
                    cat.PincodeLocation = Convert.ToString(dt.Rows[0]["PincodeLocation"]);
                    cat.Amount = Convert.ToString(dt.Rows[0]["Amount"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIP"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.AddedBy = Convert.ToString(dt.Rows[0]["AddedBy"]);
                    cat.UpdatedIp = Convert.ToString(dt.Rows[0]["UpdatedIp"]);
                    cat.UpdatedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["UpdatedOn"]));
                    cat.UpdatedBy = Convert.ToString(dt.Rows[0]["UpdatedBy"]);
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetShiptypeDetailsbyUrl", ex.Message);
        }
        return categories;
    }
    public static int InsertShiptypeDetails(SqlConnection conAP, ShiptypeDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into AddShipType (ShipType,Amount,PincodeLocation,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp) values(@ShipType,@Amount,@PincodeLocation,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ShipType", SqlDbType.NVarChar).Value = cat.ShipType;
                cmd.Parameters.AddWithValue("@Amount", SqlDbType.NVarChar).Value = cat.Amount;
                cmd.Parameters.AddWithValue("@PincodeLocation", SqlDbType.NVarChar).Value = cat.PincodeLocation;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertShiptypeDetails", ex.Message);
        }
        return result;
    }
    public static int UpdateShiptypeDetails(SqlConnection conAP, ShiptypeDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update AddShipType Set   ShipType=@ShipType,Amount=@Amount,PincodeLocation=@PincodeLocation,Status=@Status,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ShipType", SqlDbType.NVarChar).Value = cat.ShipType;
                cmd.Parameters.AddWithValue("@Amount", SqlDbType.NVarChar).Value = cat.Amount;
                cmd.Parameters.AddWithValue("@PincodeLocation", SqlDbType.NVarChar).Value = cat.PincodeLocation;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateShiptypeDetails", ex.Message);
        }
        return result;
    }
    public static int DeleteShiptypeDetails(SqlConnection conAP, ShiptypeDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update AddShipType Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value =CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteShiptypeDetails", ex.Message);
        }
        return result;
    }

    #endregion


    #region get pincode  delivary methods

    public static List<ShiptypeDetails> GetShiptypeDetailsbypinocde(SqlConnection conAP, string pincode)
    {
        List<ShiptypeDetails> categories = new List<ShiptypeDetails>();
        try
        {

            string query = "Select * from AddShipType Where Status='Active' and PincodeLocation=@PincodeLocation";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
              
                cmd.Parameters.AddWithValue("@PincodeLocation", SqlDbType.NVarChar).Value = pincode;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ShiptypeDetails cat = new ShiptypeDetails();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.ShipType = Convert.ToString(dt.Rows[0]["ShipType"]);
                    cat.PincodeLocation = Convert.ToString(dt.Rows[0]["PincodeLocation"]);
                    cat.Amount = Convert.ToString(dt.Rows[0]["Amount"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIP"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.AddedBy = Convert.ToString(dt.Rows[0]["AddedBy"]);
                    cat.UpdatedIp = Convert.ToString(dt.Rows[0]["UpdatedIp"]);
                    cat.UpdatedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["UpdatedOn"]));
                    cat.UpdatedBy = Convert.ToString(dt.Rows[0]["UpdatedBy"]);
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetShiptypeDetailsbypinocde", ex.Message);
        }
        return categories;
    }


    public static List<ShiptypeDetails> GetShippingCharge(SqlConnection conAP, string pincode, string city, string state)
    {
        List<ShiptypeDetails> shipCharges = new List<ShiptypeDetails>();
        try
        {

            string query = "CheckPincode";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable(); 
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = pincode;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = city;
                cmd.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = state;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                shipCharges = (from DataRow dr in dt.Rows
                              select new ShiptypeDetails()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ShipType = Convert.ToString(dr["ShipType"]),
                                  PincodeLocation = Convert.ToString(dr["PincodeLocation"]),
                                  Amount = Convert.ToString(dr["Amount"]), 
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetShiptypeDetailsbypinocde", ex.Message);
        }
        return shipCharges;
    }


    #endregion
}


public class Extrashipcost 
{


    #region Properties
    public int Id { get; set; }
    public string ExtraCost { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }

    public string Status { get; set; }
    #endregion


    #region methods
    public static List<Extrashipcost> GetAllExtrashipcost(SqlConnection conAP)
    {
        List<Extrashipcost> scrl = new List<Extrashipcost>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=AddExtraCost.AddedBy) AddedBy1 from AddExtraCost Order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                scrl = (from DataRow dr in dt.Rows
                        select new Extrashipcost()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            ExtraCost = Convert.ToString(dr["ExtraCost"]),
                            AddedBy = Convert.ToString(dr["AddedBy1"]),
                            AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                            AddedIp = Convert.ToString(dr["AddedIp"]),
                        }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllExtrashipcost", ex.Message);
        }
        return scrl;
    }
    public static int UpdateExtracost(SqlConnection conAP, Extrashipcost cat)
    {
        int result = 0;
        try
        {
            string query = "Update AddExtraCost Set ExtraCost=@ExtraCost,AddedIp=@AddedIp,AddedOn=@AddedOn,AddedBy=@AddedBy Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ExtraCost", SqlDbType.NVarChar).Value = cat.ExtraCost;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateExtracost", ex.Message);
        }
        return result;
    }
    public static int InsertExtracost(SqlConnection conAP, Extrashipcost cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into AddExtraCost (ExtraCost,AddedBy,AddedOn,AddedIp) values (@ExtraCost,@AddedBy,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ExtraCost", SqlDbType.NVarChar).Value = cat.ExtraCost;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertExtracost", ex.Message);
        }
        return result;
    }
    #endregion
}
public class AppliedPincode
{
    public string ActSubTotal { get; set; }
    public string SubTotal { get; set; }
    public string ShippingAmount { get; set; } 
    public string Discount { get; set; }
    public string FinalAmount { get; set; }  
    public string Status { get; set; }
    public string Pincode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
