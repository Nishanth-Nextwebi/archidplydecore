using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class ProductDescImages
{
    public int Id { set; get; }
    public string ProductGuid { get; set; }
    public string ImageUrl { get; set; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime AddedOn { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin ProductDescImages region
    public static List<ProductDescImages> GetAllProductDescImages(SqlConnection conAP)
    {
        List<ProductDescImages> allImage = new List<ProductDescImages>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=ProductDescImages.AddedBy) adby from ProductDescImages where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                allImage = (from DataRow dr in dt.Rows
                              select new ProductDescImages()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  AddedBy = Convert.ToString(dr["adby"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductDescImages", ex.Message);
        }
        return allImage;
    }


    public static List<ProductDescImages> GetAllProductDescImagesWithProductGuid(SqlConnection conAP,string ProductGuid)
    {
        List<ProductDescImages> allImage = new List<ProductDescImages>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=ProductDescImages.AddedBy) adby  from ProductDescImages where Status='Active' and ProductGuid=@ProductGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.VarChar).Value = ProductGuid;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                allImage = (from DataRow dr in dt.Rows
                            select new ProductDescImages()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                AddedIp = Convert.ToString(dr["AddedIp"]),
                                AddedBy = Convert.ToString(dr["adby"]),
                                Status = Convert.ToString(dr["Status"])
                            }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductDescImages", ex.Message);
        }
        return allImage;
    }
    public static List<ProductDescImages> GetBlogImageWithId(SqlConnection conAP, int id)
    {
        List<ProductDescImages> image = new List<ProductDescImages>();
        try
        {
            string query = "Select * from ProductDescImages Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ProductDescImages img = new ProductDescImages();
                    img.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    img.ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                    img.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                    img.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    image.Add(img);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlogImageWithId", ex.Message);
        }
        return image;
    }
    public static int InsertProductDescImages(SqlConnection conAP, ProductDescImages img)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductDescImages (ProductGuid,ImageUrl,UpdatedOn,UpdatedIp,UpdatedBy,AddedOn,AddedIP,AddedBy,Status) values (@ProductGuid,@ImageUrl,@UpdatedOn,@UpdatedIp,@UpdatedBy,@AddedOn,@AddedIP,@AddedBy,@Status) ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.Int).Value = img.ProductGuid;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = img.ImageUrl;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = img.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = img.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductDescImages", ex.Message);
        }
        return result;
    }
    public static int UpdateProductDescImages(SqlConnection conAP, ProductDescImages img)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDescImages Set ProductGuid=@ProductGuid,ImageUrl=@ImageUrl,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = img.Id;
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = img.ProductGuid;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = img.ImageUrl;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = img.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value =CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateColorMaster", ex.Message);
        }
        return result;
    }
    public static int DeleteProductDescImages(SqlConnection conAP, ProductDescImages img)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDescImages Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy  Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = img.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = img.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteColorMaster", ex.Message);
        }
        return result;
    }
    #endregion
}