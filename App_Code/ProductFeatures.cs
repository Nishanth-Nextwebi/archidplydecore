using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ProductFeatures
{
    public int Id { set; get; }
    public string Title { set; get; }
    public string Image { set; get; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin ProductFeatures region

    public static List<ProductFeatures> GetAllProductFeaturesByTitle(SqlConnection conAP,string Title)
    {
        List<ProductFeatures> categories = new List<ProductFeatures>();
        try
        {
            string query = "select *,(select UserName from CreateUser where UserGuid=ProductFeatures.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=ProductFeatures.UpdatedBy) UpdatedBy1 from ProductFeatures where Status='Active' and Title=@Title";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = Title;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductFeatures()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  Image = Convert.ToString(dr["Image"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductFeatures", ex.Message);
        }
        return categories;
    }
    public static List<ProductFeatures> GetAllProductFeatures(SqlConnection conAP)
    {
        List<ProductFeatures> categories = new List<ProductFeatures>();
        try
        {
            string query = "select *,(select UserName from CreateUser where UserGuid=ProductFeatures.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=ProductFeatures.UpdatedBy) UpdatedBy1 from ProductFeatures where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductFeatures()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  Image = Convert.ToString(dr["Image"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductFeatures", ex.Message);
        }
        return categories;
    }
    public static List<ProductFeatures> GetProductFeatures(SqlConnection conAP, string Features)
    {
        List<ProductFeatures> categories = new List<ProductFeatures>();
        try
        {
            string query = "select * from Productfeatures where Id in (SELECT CAST(value AS INT) FROM STRING_SPLIT(@Features, '|'))";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Features", SqlDbType.NVarChar).Value = Features;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductFeatures()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  Image = Convert.ToString(dr["Image"]),
                                  AddedBy = Convert.ToString(dr["AddedBy"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy = Convert.ToString(dr["UpdatedBy"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAmenity", ex.Message);
        }
        return categories;
    }
    public static int InsertProductFeatures(SqlConnection conAP, ProductFeatures cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductFeatures (Title,Image,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp) values (@Title,@Image,@AddedOn, @AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = cat.Image;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductFeatures", ex.Message);
        }
        return result;
    }
    public static int UpdateProductFeatures(SqlConnection conAP, ProductFeatures cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductFeatures Set Title=@Title,Image=@Image,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = cat.Image;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductFeatures", ex.Message);
        }
        return result;
    }
    public static int DeleteProductFeatures(SqlConnection conAP, ProductFeatures cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductFeatures Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductFeatures", ex.Message);
        }
        return result;
    }
    #endregion
}