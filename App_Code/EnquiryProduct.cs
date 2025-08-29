using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnquiryProduct
/// </summary>
public class EnquiryProduct
{
    public int Id { set; get; }
    public string Category { set; get; }
    public string CategoryName { set; get; }
    public string SubCategory { set; get; }
    public string Brand { set; get; }
    public string ProductGuid { get; set; }
    public string ProductName { get; set; }
    public string ProductUrl { get; set; }
    public string ProductImage { get; set; }
    public string ProductTags { get; set; }
    public string Featured { get; set; }
    public List<ProductGallery> productgly { get; set; }
    public string RelatedProducts { get; set; }
    public string ShortDesc { set; get; }
    public string FullDesc { set; get; }
    public string DisplayHome { set; get; }
    public string PageTitle { set; get; }
    public string MetaDesc { set; get; }
    public string MetaKey { set; get; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public DateTime ExpiryDate { get; set; }
    public string UpdatedIp { set; get; }
    public string DisplayOrder { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    public string ItemNumber { set; get; }
    public string Features { set; get; }
    public static List<EnquiryProduct> GetAllEnquiryProductBySubCategory(SqlConnection conAP, string SubCategory)
    {
        List<EnquiryProduct> pds = null;
        try
        {
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProduct.UpdatedBy) UpdatedBy1 from EnquiryProduct where Status!='Deleted' and ProductName=@ProductName";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.Int).Value = SubCategory;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new EnquiryProduct()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Category = Convert.ToString(dr["Category"]),
                           CategoryName = Convert.ToString(dr["CategoryName"]),
                           SubCategory = Convert.ToString(dr["SubCategory"]),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           Features = Convert.ToString(dr["Features"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ShortDesc = Convert.ToString(dr["ShortDesc"]),
                           FullDesc = Convert.ToString(dr["FullDesc"]),
                           ProductTags = Convert.ToString(dr["ProductTags"]),
                           Featured = Convert.ToString(dr["Featured"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                           UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                           UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                           Status = Convert.ToString(dr["Status"]),
                           DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                       }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
    public static List<EnquiryProduct> GetAllEnquiryProductByCategory(SqlConnection conAP, string Category)
    {
        List<EnquiryProduct> pds = null;
        try
        {
            //string query = "SELECT *, (SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (SELECT TOP 1 UserName FROM CreateUser WHERE UserGuid = EnquiryProduct.UpdatedBy) AS UpdatedBy1 FROM EnquiryProduct INNER JOIN Subcategory ON EnquiryProduct.Subcategory = Subcategory.Id WHERE EnquiryProduct.Status != 'Deleted' AND EnquiryProduct.Category = @Category AND Subcategory.DisplayHome = 'Yes';";
          string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProduct.UpdatedBy) UpdatedBy1 from EnquiryProduct where Status='Active' and Category=@Category ORDER BY CAST(DisplayOrder AS INT)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = Category;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new EnquiryProduct()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Category = Convert.ToString(dr["Category"]),
                           CategoryName = Convert.ToString(dr["CategoryName"]),
                           SubCategory = Convert.ToString(dr["SubCategory"]),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           Features = Convert.ToString(dr["Features"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ShortDesc = Convert.ToString(dr["ShortDesc"]),
                           FullDesc = Convert.ToString(dr["FullDesc"]),
                           ProductTags = Convert.ToString(dr["ProductTags"]),
                           Featured = Convert.ToString(dr["Featured"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                           UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                           UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                           Status = Convert.ToString(dr["Status"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                           RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                       }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
    public static List<EnquiryProduct> GetEnquiryProductByUrl(SqlConnection conAP, string url)
    {
        List<EnquiryProduct> pds = null;
        try
        {
            string query = "Select top 1 *,(SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProduct.UpdatedBy) UpdatedBy1 from EnquiryProduct where Status='Active' and ProductUrl=@ProductUrl";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.Int).Value = url;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds=(from DataRow dr in dt.Rows
                 select new EnquiryProduct()
                 {
                     Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                     Category = Convert.ToString(dr["Category"]),
                     CategoryName = Convert.ToString(dr["CategoryName"]),
                     SubCategory = Convert.ToString(dr["SubCategory"]),
                     Brand = Convert.ToString(dr["Brand"]),
                     ProductGuid = Convert.ToString(dr["ProductGuid"]),
                     Features = Convert.ToString(dr["Features"]),
                     ProductName = Convert.ToString(dr["ProductName"]),
                     ProductUrl = Convert.ToString(dr["ProductUrl"]),
                     ShortDesc = Convert.ToString(dr["ShortDesc"]),
                     FullDesc = Convert.ToString(dr["FullDesc"]),
                     ProductTags = Convert.ToString(dr["ProductTags"]),
                     Featured = Convert.ToString(dr["Featured"]),
                     PageTitle = Convert.ToString(dr["PageTitle"]),
                     MetaKey = Convert.ToString(dr["MetaKey"]),
                     DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                     MetaDesc = Convert.ToString(dr["MetaDesc"]),
                     UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                     UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                     UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                     Status = Convert.ToString(dr["Status"]),
                     ProductImage = Convert.ToString(dr["ProductImage"]),
                     RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                 }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
    public static EnquiryProduct GetEnquiryProductById(SqlConnection conAP, int id)
    {
        EnquiryProduct pds = null;
        try
        {
            string query = "SELECT *, (SELECT STRING_AGG(Title, '|') FROM ProductFeatures WHERE Id IN (SELECT CAST(value AS INT) FROM STRING_SPLIT(EnquiryProduct.Features, '|'))) AS Featuress, (SELECT TOP 1 CategoryName FROM Category WHERE Id = EnquiryProduct.Category) AS CategoryName, (SELECT TOP 1 UserName FROM CreateUser WHERE UserGuid = EnquiryProduct.UpdatedBy) AS UpdatedBy1 FROM EnquiryProduct WHERE Status != 'Deleted' AND Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new EnquiryProduct();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.Category = Convert.ToString(dt.Rows[0]["Category"]);
                    pds.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    pds.SubCategory = Convert.ToString(dt.Rows[0]["SubCategory"]);
                    pds.Brand = Convert.ToString(dt.Rows[0]["Brand"]);
                    pds.ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                    pds.Features = Convert.ToString(dt.Rows[0]["Featuress"]);
                    pds.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                    pds.ProductUrl = Convert.ToString(dt.Rows[0]["ProductUrl"]);
                    pds.ShortDesc = Convert.ToString(dt.Rows[0]["ShortDesc"]);
                    pds.FullDesc = Convert.ToString(dt.Rows[0]["FullDesc"]);
                    pds.ProductTags = Convert.ToString(dt.Rows[0]["ProductTags"]);
                    pds.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    pds.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    pds.MetaKey = Convert.ToString(dt.Rows[0]["MetaKey"]);
                    pds.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    pds.UpdatedBy = Convert.ToString(dt.Rows[0]["UpdatedBy1"]);
                    pds.UpdatedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["UpdatedOn"]));
                    pds.UpdatedIp = Convert.ToString(dt.Rows[0]["UpdatedIp"]);
                    pds.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    pds.Featured = Convert.ToString(dt.Rows[0]["Featured"]);
                    pds.ItemNumber = Convert.ToString(dt.Rows[0]["ItemNumber"]);
                    pds.ProductImage = Convert.ToString(dt.Rows[0]["ProductImage"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
    public static EnquiryProduct GetEnquiryProductByIdOld(SqlConnection conAP, int id)
    {
        EnquiryProduct pds = null;
        try
        {
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProduct.UpdatedBy) UpdatedBy1 from EnquiryProduct where Status!='Deleted' and Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new EnquiryProduct();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.Category = Convert.ToString(dt.Rows[0]["Category"]);
                    pds.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    pds.SubCategory = Convert.ToString(dt.Rows[0]["SubCategory"]);
                    pds.Brand = Convert.ToString(dt.Rows[0]["Brand"]);
                    pds.ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                    pds.Features = Convert.ToString(dt.Rows[0]["Features"]);
                    pds.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                    pds.ProductUrl = Convert.ToString(dt.Rows[0]["ProductUrl"]);
                    pds.ShortDesc = Convert.ToString(dt.Rows[0]["ShortDesc"]);
                    pds.FullDesc = Convert.ToString(dt.Rows[0]["FullDesc"]);
                    pds.ProductTags = Convert.ToString(dt.Rows[0]["ProductTags"]);
                    pds.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    pds.MetaKey = Convert.ToString(dt.Rows[0]["MetaKey"]);
                    pds.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    pds.UpdatedBy = Convert.ToString(dt.Rows[0]["UpdatedBy1"]);
                    pds.UpdatedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["UpdatedOn"]));
                    pds.UpdatedIp = Convert.ToString(dt.Rows[0]["UpdatedIp"]);
                    pds.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    pds.Featured = Convert.ToString(dt.Rows[0]["Featured"]);
                    pds.ItemNumber = Convert.ToString(dt.Rows[0]["ItemNumber"]);
                    pds.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    pds.ProductImage = Convert.ToString(dt.Rows[0]["ProductImage"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
    public static List<EnquiryProduct> GetAllEnquiryProduct(SqlConnection conAP)
    {
        List<EnquiryProduct> productDetails = new List<EnquiryProduct>();
        try
        {
            string query = "SELECT pd.*, (SELECT TOP 1 CategoryName FROM Category WHERE id = pd.Category) AS CategoryName,(SELECT TOP 1 UserName FROM CreateUser WHERE UserGuid = pd.UpdatedBy) AS UpdatedBy1 FROM EnquiryProduct AS pd WHERE pd.Status != 'Deleted';";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new EnquiryProduct()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      CategoryName = Convert.ToString(dr["CategoryName"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      Features = Convert.ToString(dr["Features"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                      FullDesc = Convert.ToString(dr["FullDesc"]),
                                      ProductTags = Convert.ToString(dr["ProductTags"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKey = Convert.ToString(dr["MetaKey"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                      UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                      UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                      UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllEnquiryProduct", ex.Message);
        }
        return productDetails;
    }
    public static int DeleteEnquiryProduct(SqlConnection conAP, EnquiryProduct cat)
    {
        int result = 0;
        try
        {
            string query = "Update EnquiryProduct Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnquiryProduct", ex.Message);
        }
        return result;
    }
    public static int InsertEnquiryProduct(SqlConnection conAP, EnquiryProduct cat)
    {
        int result = 0;
        try
        {
            string query = "INSERT INTO EnquiryProduct (ProductGuid,DisplayOrder, Category, ProductName, ProductUrl, ShortDesc, FullDesc, Features, AddedOn, AddedBy, AddedIp, UpdatedOn, UpdatedBy, UpdatedIp, Status, PageTitle, MetaKey, MetaDesc, ProductImage, ProductTags, SubCategory, Featured, RelatedProducts, Brand, ItemNumber) VALUES (@ProductGuid,@DisplayOrder, @Category, @ProductName, @ProductUrl, @ShortDesc, @FullDesc, @Features, @AddedOn, @AddedBy, @AddedIp, @UpdatedOn, @UpdatedBy, @UpdatedIp, @Status, @PageTitle, @MetaKey, @MetaDesc, @ProductImage, @ProductTags, @SubCategory, @Featured, @RelatedProducts, @Brand, @ItemNumber) SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Features", SqlDbType.NVarChar).Value = cat.Features;
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = cat.ProductGuid;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = cat.SubCategory;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = cat.ProductImage;
                cmd.Parameters.AddWithValue("@ProductTags", SqlDbType.NVarChar).Value = cat.ProductTags;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKey", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.Featured;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@ItemNumber", SqlDbType.NVarChar).Value = cat.ItemNumber;
                cmd.Parameters.AddWithValue("@RelatedProducts", SqlDbType.NVarChar).Value = cat.RelatedProducts;
                conAP.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertEnquiryProduct", ex.Message);
        }
        return result;
    }
    public static int UpdateEnquiryProduct(SqlConnection conAP, EnquiryProduct cat)
    {
        int result = 0;
        try
        {
            string query = "UPDATE EnquiryProduct SET Category=@Category, DisplayOrder=@DisplayOrder,Features=@Features, Status=@sts, SubCategory=@SubCategory, Brand=@Brand, ProductName=@ProductName, ShortDesc=@ShortDesc, FullDesc=@FullDesc, ProductUrl=@ProductUrl, ProductTags=@ProductTags, ProductImage=@ProductImage, PageTitle=@PageTitle, MetaKey=@MetaKey, MetaDesc=@MetaDesc, Featured=@Featured, UpdatedBy=@UpdatedBy, ItemNumber=@ItemNumber, UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@Features", SqlDbType.NVarChar).Value = cat.Features;
                cmd.Parameters.AddWithValue("@sts", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = cat.SubCategory;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@ProductTags", SqlDbType.NVarChar).Value = cat.ProductTags;
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = cat.ProductImage;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKey", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.Featured;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@ItemNumber", SqlDbType.NVarChar).Value = cat.ItemNumber;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateEnquiryProduct", ex.Message);
        }
        return result;
    }
    public static List<EnquiryProduct> GetAllEnquiryProductRelatedProduct(SqlConnection conAP, string Category,string url)
    {
        List<EnquiryProduct> pds = null;
        try
        {
            //string query = "SELECT *, (SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (SELECT TOP 1 UserName FROM CreateUser WHERE UserGuid = EnquiryProduct.UpdatedBy) AS UpdatedBy1 FROM EnquiryProduct INNER JOIN Subcategory ON EnquiryProduct.Subcategory = Subcategory.Id WHERE EnquiryProduct.Status != 'Deleted' AND EnquiryProduct.Category = @Category AND Subcategory.DisplayHome = 'Yes';";
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = EnquiryProduct.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProduct.UpdatedBy) UpdatedBy1 from EnquiryProduct where Status='Active' and Category=@Category and ProductUrl!=@ProductUrl ORDER BY CAST(DisplayOrder AS INT)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = Category;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.Int).Value = url;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new EnquiryProduct()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Category = Convert.ToString(dr["Category"]),
                           CategoryName = Convert.ToString(dr["CategoryName"]),
                           SubCategory = Convert.ToString(dr["SubCategory"]),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           Features = Convert.ToString(dr["Features"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ShortDesc = Convert.ToString(dr["ShortDesc"]),
                           FullDesc = Convert.ToString(dr["FullDesc"]),
                           ProductTags = Convert.ToString(dr["ProductTags"]),
                           Featured = Convert.ToString(dr["Featured"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                           UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                           UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                           Status = Convert.ToString(dr["Status"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                           RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                       }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductById", ex.Message);
        }
        return pds;
    }
}