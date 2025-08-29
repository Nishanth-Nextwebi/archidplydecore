using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

public class ProductDetails
{
    public int Id { set; get; }
    public string Category { set; get; }
    public string CategoryName { set; get; }
    public string SubCategory { set; get; }
    public string Brand { set; get; }
    public string PriceId { get; set; }
    public string ActualPrice { get; set; }
    public string DiscountPrice { get; set; }
    public string SKUCode { get; set; }
    public string TaxGroup { get; set; }
    public string DeliveryDays { get; set; }
    public string ProductGuid { get; set; }
    public string ProductName { get; set; }
    public string ProductUrl { get; set; }
    public string ProductImage { get; set; }
    public string ProductTags { get; set; }
    public string Ingredients { get; set; }
    public string Origin { get; set; }
    public string InStock { get; set; }
    public string BestSeller { get; set; }
    public string Featured { get; set; }
    public List<ProductPrices> productprs { get; set; }
    public List<ProductGallery> productgly { get; set; }
    public List<ProductReviews> productrvs { get; set; }
    public List<ProductFAQs> productfaqs { get; set; }
    public int WishCount { get; set; }
    public string RelatedProducts { get; set; }
    public string ShortDesc { set; get; }
    public string FullDesc { set; get; }
    public string DisplayOrder { set; get; }
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
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    public string ReviewKeyword { set; get; }
    public string ItemNumber { set; get; }
    public string Shop { set; get; }
    public string Enquiry { set; get; }
    public int TotalCount { set; get; }
    public int RowNumber { set; get; }


    #region ProductDetails Methods

    public static List<ProductDetails> GetAllProductsToddl(SqlConnection conAP)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select Id,ProductName from ProductDetails where Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),

                                      ProductName = Convert.ToString(dr["ProductName"]),

                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsToddl", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductDetails> GetAllProductsByFilter(SqlConnection conAP, int cPage,string Pcat)
    {
        List<ProductDetails> Products = new List<ProductDetails>();
        try
        {
            //  var queryold = @"SELECT TOP 8 * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ProductOrder DESC) AS RowNo, (SELECT COUNT(ID) FROM ProductDetails WHERE Status = 'Active' AND (@SubCategory = '' OR SubCategory = @SubCategory) AND (@ProductTags='' or ProductTags=@ProductTags )) AS TotalCount, * FROM ProductDetails WHERE Status = 'Active' AND (@Category = '' OR Category = @Category) AND (@ProductTags='' or ProductTags=@ProductTags )) x WHERE RowNo > " + (8 * (cPage - 1));
            var query = @"SELECT TOP 8 * FROM (SELECT ROW_NUMBER() OVER (ORDER BY CAST(ProductOrder as int)) AS RowNo, (SELECT COUNT(ID) FROM ProductDetails WHERE Status = 'Active' AND (@SubCategory = '' OR SubCategory = @SubCategory)) AS TotalCount, * FROM ProductDetails WHERE Status = 'Active' AND (@SubCategory = '' OR SubCategory = @SubCategory)) x WHERE RowNo > " + (8 * (cPage - 1));
      
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.Int).Value = Pcat;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Products = (from DataRow dr in dt.Rows
                         select new ProductDetails()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                             TotalCount = Convert.ToInt32(Convert.ToString(dr["TotalCount"])),
                             Category = Convert.ToString(dr["Category"]),
                             SubCategory = Convert.ToString(dr["SubCategory"]),
                             Brand = Convert.ToString(dr["Brand"]),
                             Ingredients = Convert.ToString(dr["Ingredients"]),
                             Origin = Convert.ToString(dr["Origin"]),
                             ProductGuid = Convert.ToString(dr["ProductGuid"]),
                             SKUCode = Convert.ToString(dr["SKUCode"]),
                             TaxGroup = Convert.ToString(dr["TaxGroup"]),
                             ProductName = Convert.ToString(dr["ProductName"]),
                             DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                             ProductUrl = Convert.ToString(dr["ProductUrl"]),
                             ShortDesc = Convert.ToString(dr["ProductDesc"]),
                             FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                             ProductTags = Convert.ToString(dr["ProductTags"]),
                             BestSeller = Convert.ToString(dr["BestSeller"]),
                             Featured = Convert.ToString(dr["Featured"]),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             MetaKey = Convert.ToString(dr["MetaKey"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             InStock = Convert.ToString(dr["InStock"]),
                             UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                             UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                             Status = Convert.ToString(dr["Status"]),
                             ProductImage = Convert.ToString(dr["ProductImage"]),
                             RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                             Shop = Convert.ToString(dr["Shop"]),
                             productprs = ProductPrices.GetTop1ProductPriceDetailByPid(conAP, Convert.ToString(dr["Id"])).OrderBy(s => Convert.ToDouble(s.DiscountPrice)).ToList(),
                             Enquiry = Convert.ToString(dr["Enquiry"]),
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsByPageNo", ex.Message);
        }
        return Products;
    }
    public static List<ProductDetails> GetAllProducts(SqlConnection conAP)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "SELECT pd.*, (SELECT TOP 1 SubCategory FROM SubCategory WHERE id = pd.SubCategory) AS CategoryName,(SELECT TOP 1 UserName FROM CreateUser WHERE UserGuid = pd.UpdatedBy) AS UpdatedBy1 FROM ProductDetails AS pd WHERE pd.Status != 'Deleted';";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      CategoryName = Convert.ToString(dr["CategoryName"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      Ingredients = Convert.ToString(dr["Ingredients"]),
                                      Origin = Convert.ToString(dr["Origin"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      TaxGroup = Convert.ToString(dr["TaxGroup"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      ShortDesc = Convert.ToString(dr["ProductDesc"]),
                                      FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                                      ProductTags = Convert.ToString(dr["ProductTags"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKey = Convert.ToString(dr["MetaKey"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                      UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                      UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                      Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProducts", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductDetails> GetAllFeaturedProducts(SqlConnection conAP)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "select * from ProductDetails where status='Active' and Featured='Yes'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      Ingredients = Convert.ToString(dr["Ingredients"]),
                                      Origin = Convert.ToString(dr["Origin"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      TaxGroup = Convert.ToString(dr["TaxGroup"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      ShortDesc = Convert.ToString(dr["ProductDesc"]),
                                      FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                                      ProductTags = Convert.ToString(dr["ProductTags"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKey = Convert.ToString(dr["MetaKey"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                      UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                      Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProducts", ex.Message);
        }
        return productDetails;
    }
    public static DataTable GetAllProductsDataTable(SqlConnection conAP)
    {
        DataTable productDetails = new DataTable();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductDetails.UpdatedBy) UpdatedBy1,(select Top 1  DiscountPrice from ProductPrices Where ProductId=ProductDetails.Id order by DiscountPrice) as Price from ProductDetails where Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = dt;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProducts", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductDetails> GetAllProductsByCatAndSubCat(SqlConnection conAP, string Cat, string SubCat)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select * from ProductDetails where Status!='Deleted' and Category=@Category and SubCategory=@SubCategory ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = Cat;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.Int).Value = SubCat;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),

                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),

                                      ProductName = Convert.ToString(dr["ProductName"]),

                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),

                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsByCatAndSubCat", ex.Message);
        }
        return productDetails;
    }

    public static List<ProductDetails> GetAllTopSellerProducts(SqlConnection conAP)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductDetails.UpdatedBy) UpdatedBy1 from ProductDetails where Status!='Deleted' and BestSeller='Yes'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      Ingredients = Convert.ToString(dr["Ingredients"]),
                                      Origin = Convert.ToString(dr["Origin"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                      Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProducts", ex.Message);
        }
        return productDetails;
    }
    public static ProductDetails GetProductDetailsById(SqlConnection conAP, int id)
    {
        ProductDetails pds = null;
        try
        {
            string query = "Select *,(SELECT TOP 1 SubCategory FROM SubCategory WHERE id = ProductDetails.SubCategory) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=ProductDetails.UpdatedBy) UpdatedBy1 from ProductDetails where Status!='Deleted' and Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new ProductDetails();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.Category = Convert.ToString(dt.Rows[0]["Category"]);
                    pds.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    pds.SubCategory = Convert.ToString(dt.Rows[0]["SubCategory"]);
                    pds.Brand = Convert.ToString(dt.Rows[0]["Brand"]);
                    pds.Ingredients = Convert.ToString(dt.Rows[0]["Ingredients"]);
                    pds.Origin = Convert.ToString(dt.Rows[0]["Origin"]);
                    pds.ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                    pds.SKUCode = Convert.ToString(dt.Rows[0]["SKUCode"]);
                    pds.TaxGroup = Convert.ToString(dt.Rows[0]["TaxGroup"]);
                    pds.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                    pds.DisplayOrder = Convert.ToString(dt.Rows[0]["ProductOrder"]);
                    pds.ProductUrl = Convert.ToString(dt.Rows[0]["ProductUrl"]);
                    pds.ShortDesc = Convert.ToString(dt.Rows[0]["ProductDesc"]);
                    pds.FullDesc = Convert.ToString(dt.Rows[0]["ProductFullDesc"]);
                    pds.ProductTags = Convert.ToString(dt.Rows[0]["ProductTags"]);
                    pds.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    pds.MetaKey = Convert.ToString(dt.Rows[0]["MetaKey"]);
                    pds.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    pds.InStock = Convert.ToString(dt.Rows[0]["InStock"]);
                    pds.BestSeller = Convert.ToString(dt.Rows[0]["BestSeller"]);
                    pds.UpdatedBy = Convert.ToString(dt.Rows[0]["UpdatedBy1"]);
                    pds.UpdatedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["UpdatedOn"]));
                    pds.UpdatedIp = Convert.ToString(dt.Rows[0]["UpdatedIp"]);
                    pds.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    pds.Featured = Convert.ToString(dt.Rows[0]["Featured"]);
                    pds.ReviewKeyword = Convert.ToString(dt.Rows[0]["ReviewKeyword"]);
                    pds.ItemNumber = Convert.ToString(dt.Rows[0]["ItemNumber"]);
                    pds.ProductImage = Convert.ToString(dt.Rows[0]["ProductImage"]);
                    pds.DeliveryDays = Convert.ToString(dt.Rows[0]["DeliveryDays"]);
                    pds.Shop = Convert.ToString(dt.Rows[0]["Shop"]);
                    pds.Enquiry = Convert.ToString(dt.Rows[0]["Enquiry"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductDetailsById", ex.Message);
        }
        return pds;
    }
    public static ProductDetails GetProductCat(SqlConnection conAP, int id)
    {
        ProductDetails pds = null;
        try
        {
            string query = "Select Category from ProductDetails where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new ProductDetails();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.Category = Convert.ToString(dt.Rows[0]["Category"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductCat", ex.Message);
        }
        return pds;
    }
    public static List<ProductDetails> GetProductDetailsByUrl(SqlConnection conAP, string url)
    {
        List<ProductDetails> pds = new List<ProductDetails>();
        try
        {
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = ProductDetails.Category) AS CategoryName, (Select Top 1 UserName From CreateUser Where UserGuid=ProductDetails.UpdatedBy) UpdatedBy1 from ProductDetails where Status='Active' and ProductUrl=@ProductUrl";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.Int).Value = url;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new ProductDetails()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Category = Convert.ToString(dr["CategoryName"]),
                           SubCategory = Convert.ToString(dr["SubCategory"]),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           Ingredients = Convert.ToString(dr["Ingredients"]),
                           SKUCode = Convert.ToString(dr["SKUCode"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           TaxGroup = Convert.ToString(dr["TaxGroup"]),
                           ShortDesc = Convert.ToString(dr["ProductDesc"]),
                           ProductTags = Convert.ToString(dr["ProductTags"]),
                           FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           InStock = Convert.ToString(dr["InStock"]),
                           productprs = ProductPrices.GetProductPriceByPId(conAP, Convert.ToString(dr["Id"])).OrderBy(s => Convert.ToDouble(s.DiscountPrice)).ToList(),
                           productgly = ProductGallery.GetProductGallery(conAP, Convert.ToString(dr["Id"])),
                           productrvs = ProductReviews.GetProductReviews(conAP, Convert.ToString(dr["Id"])),
                           productfaqs = ProductFAQs.GetAllFAQS(conAP, Convert.ToString(dr["Id"])),
                           RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                           ReviewKeyword = Convert.ToString(dr["ReviewKeyword"]),
                           ItemNumber = Convert.ToString(dr["ItemNumber"]),
                           Shop = Convert.ToString(dr["Shop"]),
                           Enquiry = Convert.ToString(dr["Enquiry"])
                       }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }
    public static List<ProductDetails> GetProductDetailsByDDlID(SqlConnection conAP, string Id)
    {
        List<ProductDetails> pds = null;
        try
        {
            string query = "Select * from ProductDetails where Status!='Deleted' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      Ingredients = Convert.ToString(dr["Ingredients"]),
                                      Origin = Convert.ToString(dr["Origin"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                      Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductDetailsByUrl", ex.Message);
        }
        return pds;
    }

    public static string GetImageUrlWithId(SqlConnection conAP, int id)
    {
        string result = "";
        try
        {
            string query = "select producturl + '_'+ CONVERT(NVARCHAR, (select count(ProductId) from productgallery where productid=@id )+1 ) as GalleryImageName from productdetails where id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                result = Convert.ToString(dt.Rows[0]["GalleryImageName"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetImageUrlWithId", ex.Message);
        }
        return result;
    }

    public static int GetProductLastId(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = "Select Count(Id) as mid from ProductDetails";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int mid = 0;
                    int.TryParse(Convert.ToString(dt.Rows[0]["mid"]), out mid);
                    x = (mid + 1);
                }
                else
                {
                    x = 1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductLastId", ex.Message);
        }
        return x;
    }
    public static int InsertProductDetails(SqlConnection conAP, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductDetails (Origin,Ingredients,Brand,SKUCode,TaxGroup,Featured,ProductGuid,Category,SubCategory,ProductName,ProductUrl,ProductDesc,PageTitle,MetaKey,MetaDesc,InStock,AddedBy,AddedOn,AddedIp,UpdatedBy,UpdatedOn,UpdatedIp,Status,ProductOrder,BestSeller,ProductTags,ProductFullDesc,ProductImage,ReviewKeyword,ItemNumber,DeliveryDays) values(@Origin,@Ingredients,@Brand,@SKUCode,@TaxGroup,@Featured,@ProductGuid,@Category,@SubCategory,@ProductName,@ProductUrl,@ProductDesc,@PageTitle,@MetaKeys,@MetaDesc,@InStock,@AddedBy,@AddedOn,@AddedIp,@UpdatedBy,@UpdatedOn,@UpdatedIp,@Status,@ProductOrder,@BestSeller,@ProductTags,@ProductFullDesc,@ProductImage,@ReviewKeyword,@ItemNumber,@DeliveryDays) SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@SKUCode", SqlDbType.NVarChar).Value = cat.SKUCode;
                cmd.Parameters.AddWithValue("@Ingredients", SqlDbType.NVarChar).Value = cat.Ingredients;
                cmd.Parameters.AddWithValue("@Origin", SqlDbType.NVarChar).Value = cat.Origin;
                cmd.Parameters.AddWithValue("@TaxGroup", SqlDbType.NVarChar).Value = cat.TaxGroup;
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = cat.ProductGuid;
                cmd.Parameters.AddWithValue("@ProductOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = cat.SubCategory;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@ProductDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@ProductFullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = cat.ProductImage;
                cmd.Parameters.AddWithValue("@ProductTags", SqlDbType.NVarChar).Value = cat.ProductTags;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@InStock", SqlDbType.NVarChar).Value = cat.InStock;
                cmd.Parameters.AddWithValue("@BestSeller", SqlDbType.NVarChar).Value = cat.BestSeller;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.Featured;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@ReviewKeyword", SqlDbType.NVarChar).Value = cat.ReviewKeyword;
                cmd.Parameters.AddWithValue("@ItemNumber", SqlDbType.NVarChar).Value = cat.ItemNumber;
                cmd.Parameters.AddWithValue("@DeliveryDays", SqlDbType.NVarChar).Value = cat.DeliveryDays;
                cmd.Parameters.AddWithValue("@Shop", SqlDbType.NVarChar).Value = cat.Shop;
                cmd.Parameters.AddWithValue("@Enquiry", SqlDbType.NVarChar).Value = cat.Enquiry;
                conAP.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductDetails", ex.Message);
        }
        return result;
    }
    public static int UpdateProductDetails(SqlConnection conAP, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set Origin=@Origin,Ingredients=@Ingredients,ProductOrder=@ProductOrder,DeliveryDays=@DeliveryDays, Brand=@Brand, SKUCode=@SKUCode, TaxGroup=@TaxGroup,Featured=@Featured,Category=@Category,SubCategory=@SubCategory,ProductName=@ProductName,ProductUrl=@ProductUrl,ProductDesc=@ProductDesc,PageTitle=@PageTitle,MetaKey=@MetaKey,MetaDesc=@MetaDesc,InStock=@InStock,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,BestSeller=@BestSeller,ProductTags=@ProductTags,ProductImage=@ProductImage,ProductFullDesc=@ProductFullDesc,Status=@sts,ReviewKeyword=@ReviewKeyword,ItemNumber=@ItemNumber Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Ingredients", SqlDbType.NVarChar).Value = cat.Ingredients;
                cmd.Parameters.AddWithValue("@Origin", SqlDbType.NVarChar).Value = cat.Origin;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@TaxGroup", SqlDbType.NVarChar).Value = cat.TaxGroup;
                cmd.Parameters.AddWithValue("@SKUCode", SqlDbType.NVarChar).Value = cat.SKUCode;
                cmd.Parameters.AddWithValue("@sts", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = cat.SubCategory;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ProductDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@ProductFullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@ProductTags", SqlDbType.NVarChar).Value = cat.ProductTags;
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = cat.ProductImage;
                cmd.Parameters.AddWithValue("@DeliveryDays", SqlDbType.NVarChar).Value = cat.DeliveryDays;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@Enquiry", SqlDbType.NVarChar).Value = cat.Enquiry;
                cmd.Parameters.AddWithValue("@Shop", SqlDbType.NVarChar).Value = cat.Shop;
                cmd.Parameters.AddWithValue("@MetaKey", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@InStock", SqlDbType.NVarChar).Value = cat.InStock;
                cmd.Parameters.AddWithValue("@BestSeller", SqlDbType.NVarChar).Value = cat.BestSeller;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.Featured;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@ReviewKeyword", SqlDbType.NVarChar).Value = cat.ReviewKeyword;
                cmd.Parameters.AddWithValue("@ItemNumber", SqlDbType.NVarChar).Value = cat.ItemNumber;
                cmd.Parameters.AddWithValue("@ProductOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductDetails", ex.Message);
        }
        return result;
    }
    public static int UpdateProductSeoDetails(SqlConnection conAP, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set PageTitle=@PageTitle,MetaKey=@MetaKey,MetaDesc=@MetaDesc,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKey", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductSeoDetails", ex.Message);
        }
        return result;
    }
    public static int AddProductImage(SqlConnection conAP, ProductDetails pds)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set ProductImage=@ProductImage Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pds.Id;
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = pds.ProductImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddProductImage", ex.Message);
        }
        return result;
    }
    public static int DeleteProductDetails(SqlConnection conAP, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductDetails", ex.Message);
        }
        return result;
    }
    public static int UpdateProductOrder(SqlConnection conAP, ProductDetails pro)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set ProductOrder=@ProductOrder Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductOrder", SqlDbType.NVarChar).Value = pro.DisplayOrder;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pro.Id;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductOrder", ex.Message);
        }
        return result;
    }
    public static int UpdateAlternateProducts(SqlConnection conAP, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set RelatedProducts=@RelatedProducts Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@RelatedProducts", SqlDbType.NVarChar).Value = cat.RelatedProducts;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductDetails", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return result;
    }
    #endregion
    #region Home Page Product Details
    public static DataTable GetAllProductListingsMenu(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "Select top 4 pds.*,pps.Id priceId,pps.DiscountPrice,pps.ActualPrice from ProductDetails pds inner join ProductPrices pps on pps.ProductId=pds.Id and pps.Id= (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = try_convert(nvarchar,pds.Id) and pps.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) where pds.Status='Active' and pds.BestSeller='Yes' order by try_convert(decimal,pds.ProductOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListingsMenu", ex.Message);
        }
        return dt;
    }
    public static DataTable GetAllProductListingsMenuDecor(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "Select top 4 pds.*,pps.Id priceId,pps.DiscountPrice,pps.ActualPrice from ProductDetails pds inner join ProductPrices pps on pps.ProductId=pds.Id and pps.Id= (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = try_convert(nvarchar,pds.Id) and pps.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) where pds.Status='Active' and pds.Demand like '%Decor%' order by try_convert(decimal,pds.ProductOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListingsMenuDecor", ex.Message);
        }
        return dt;
    }
    public static DataTable GetAllProductListingsMenuGifting(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "Select top 4 pds.*,pps.Id priceId,pps.DiscountPrice,pps.ActualPrice from ProductDetails pds inner join ProductPrices pps on pps.ProductId=pds.Id and pps.Id= (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = try_convert(nvarchar,pds.Id) and pps.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) where pds.Status='Active' and pds.Demand like '%Gifting%' order by try_convert(decimal,pds.ProductOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListingsMenuGifting", ex.Message);
        }
        return dt;
    }
    public static List<ProductDetails> GetAllProductListings(SqlConnection conAP)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select pds.*,pps.Id priceId,pps.DiscountPrice,pps.ActualPrice from ProductDetails pds inner join ProductPrices pps on pps.ProductId=pds.Id and pps.Id= (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = try_convert(nvarchar,pds.Id) and pps.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) where pds.Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      PriceId = Convert.ToString(dr["priceId"]),
                                      DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                      ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      ShortDesc = Convert.ToString(dr["ProductDesc"]),
                                      FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                                      ProductTags = Convert.ToString(dr["ProductTags"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                       Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListings", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductDetails> GetAllRelatedProducts(SqlConnection conAP, string ids)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select pds.*,pps.Id priceId,pps.DiscountPrice,pps.ActualPrice from ProductDetails pds inner join ProductPrices pps on pps.ProductId=pds.Id and pps.Id= (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = try_convert(nvarchar,pds.Id) and pps.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) where pds.Status='Active' and pds.Id in (" + ids + ")";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      PriceId = Convert.ToString(dr["priceId"]),
                                      DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                      ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                      Category = Convert.ToString(dr["Category"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      ShortDesc = Convert.ToString(dr["ProductDesc"]),
                                      FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                                      ProductTags = Convert.ToString(dr["ProductTags"]),
                                      BestSeller = Convert.ToString(dr["BestSeller"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                      Shop = Convert.ToString(dr["Shop"]),
                                      Enquiry = Convert.ToString(dr["Enquiry"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllRelatedProducts", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductDetails> GetAllRelatedProductWithList(SqlConnection conAP, string ids)
    {
        List<ProductDetails> categories = new List<ProductDetails>();
        try
        {
            string query = "select * from ProductDetails where Id in (SELECT CAST(value AS INT) FROM STRING_SPLIT(@Related, '|')) and Status='Active' order by Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Related", SqlDbType.NVarChar).Value = ids;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductDetails()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Category = Convert.ToString(dr["Category"]),
                                  SubCategory = Convert.ToString(dr["SubCategory"]),
                                  Brand = Convert.ToString(dr["Brand"]),
                                  ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                  SKUCode = Convert.ToString(dr["SKUCode"]),
                                  ProductName = Convert.ToString(dr["ProductName"]),
                                  DisplayOrder = Convert.ToString(dr["ProductOrder"]),
                                  ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                  ShortDesc = Convert.ToString(dr["ProductDesc"]),
                                  FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                                  ProductTags = Convert.ToString(dr["ProductTags"]),
                                  BestSeller = Convert.ToString(dr["BestSeller"]),
                                  InStock = Convert.ToString(dr["InStock"]),
                                  ProductImage = Convert.ToString(dr["ProductImage"]),
                                  RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                                  Shop = Convert.ToString(dr["Shop"]),
                                  Enquiry = Convert.ToString(dr["Enquiry"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAmenity", ex.Message);
        }
        return categories;
    }
    public static List<ProductListing> GetAllProductListedSearched(SqlConnection conAP, string qry)
    {
        List<ProductListing> products = new List<ProductListing>();
        try
        {
            string query = "Select * from (select ROW_NUMBER() OVER(ORDER BY pd.ProductName) RowNum, pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize,(Select Count(pd.id) from ProductDetails as pd inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and (pd.Category like '%'+@cat+'%' or pd.SubCategory like '%'+@cat+'%' or pd.ProductTags like '%'+@cat+'%' or pd.ProductOccasions like '%'+@cat+'%' or pd.ProductName like '%'+@cat+'%' or pd.Color like '%'+@cat+'%' or pd.Demand like '%'+@cat+'%' or pd.Spaces like '%'+@cat+'%') " +
              " ) as recordCount " +
              " from ProductDetails as pd inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id  and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and (pd.Category like '%'+@cat+'%' or pd.SubCategory like '%'+@cat+'%' or pd.ProductTags like '%'+@cat+'%' or pd.ProductOccasions like '%'+@cat+'%' or pd.ProductName like '%'+@cat+'%' or pd.Color like '%'+@cat+'%' or pd.Demand like '%'+@cat+'%' or pd.Spaces like '%'+@cat+'%') " +
              " ) as x WHERE RowNum > 0 ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@cat", SqlDbType.NVarChar).Value = qry;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                products = (from DataRow dr in dt.Rows
                            select new ProductListing()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                //Category = Convert.ToString(dr["Category"]),
                                ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                //Demand = Convert.ToString(dr["Demand"]),
                                //Space = Convert.ToString(dr["Spaces"]),
                                //CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                                ItemNumber = Convert.ToString(dr["ItemNumber"]),
                                SKUCode = Convert.ToString(dr["SKUCode"]),
                                //TotalProducts = Convert.ToString(dr["recordCount"]),
                                //Tags = Convert.ToString(dr["ProductTags"]),
                                Stock = Convert.ToString(dr["InStock"]),
                                //Featured = Convert.ToString(dr["Featured"]),
                                ProductImage = Convert.ToString(dr["ProductImage"]),
                                PriceId = Convert.ToString(dr["Pid"]),
                                ProductSize = Convert.ToString(dr["ProductSize"]),
                                ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListed", ex.Message);
        }
        return products;
    }
    public static List<ProductDetails> GetProductByGuid(SqlConnection conAP, string id)
    {
        List<ProductDetails> pds = new List<ProductDetails>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            //if (uid != "")
            //{
            string query = "Select *,(select Count(pid) cnt from Wishlist where UserGuid=@uid and Pid=ProductDetails.Id) WishCount from ProductDetails where Status='Active' and (ProductGuid=@id or ProductUrl=@id)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.AddWithValue("@uid", SqlDbType.Int).Value = uid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new ProductDetails()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Category = Convert.ToString(dr["Category"]),
                           SubCategory = Convert.ToString(dr["SubCategory"]),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           SKUCode = Convert.ToString(dr["SKUCode"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           TaxGroup = Convert.ToString(dr["TaxGroup"]),
                           ShortDesc = Convert.ToString(dr["ProductDesc"]),
                           ProductTags = Convert.ToString(dr["ProductTags"]),
                           FullDesc = Convert.ToString(dr["ProductFullDesc"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           InStock = Convert.ToString(dr["InStock"]),
                           productprs = ProductPrices.GetProductPriceByPId(conAP, Convert.ToString(dr["Id"])).OrderBy(s => Convert.ToDouble(s.DiscountPrice)).ToList(),
                           productgly = ProductGallery.GetProductGallery(conAP, Convert.ToString(dr["Id"])),
                           productrvs = ProductReviews.GetProductReviews(conAP, Convert.ToString(dr["Id"])),
                           productfaqs = ProductFAQs.GetAllFAQS(conAP, Convert.ToString(dr["Id"])),
                           WishCount = Convert.ToInt32(dr["WishCount"]),
                           RelatedProducts = Convert.ToString(dr["RelatedProducts"]),
                           ReviewKeyword = Convert.ToString(dr["ReviewKeyword"]),
                           ItemNumber = Convert.ToString(dr["ItemNumber"]),
                           Shop = Convert.ToString(dr["Shop"]),
                           Enquiry = Convert.ToString(dr["Enquiry"])
                       }).ToList();

            }
            //}
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }
    public static List<ProductDetails> GetProductByGuidForSchema(SqlConnection conAP, string id)
    {
        List<ProductDetails> pds = new List<ProductDetails>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            //if (uid != "")
            //{
            string query = "Select *,(select pp.discountprice from ProductPrices pp where pp.productid=pd.id) as discountprice from ProductDetails pd where pd.Status='Active' and  pd.ProductUrl=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pds = (from DataRow dr in dt.Rows
                       select new ProductDetails()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           Brand = Convert.ToString(dr["Brand"]),
                           ProductGuid = Convert.ToString(dr["ProductGuid"]),
                           SKUCode = Convert.ToString(dr["SKUCode"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           ProductUrl = Convert.ToString(dr["ProductUrl"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                           ShortDesc = Convert.ToString(dr["ProductDesc"]),
                           PageTitle = Convert.ToString(dr["PageTitle"]),
                           MetaKey = Convert.ToString(dr["MetaKey"]),
                           MetaDesc = Convert.ToString(dr["MetaDesc"]),
                           DiscountPrice = Convert.ToString(dr["discountprice"]),
                           Shop = Convert.ToString(dr["Shop"]),
                           Enquiry = Convert.ToString(dr["Enquiry"])
                       }).ToList();

            }
            //}
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }

    public static ProductDetails GetProductNameByGuid(SqlConnection conAP, string id)
    {
        ProductDetails pds = null;
        try
        {

            string query = "Select *  from ProductDetails where Status!='Deleted' and ProductGuid=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pds = new ProductDetails();
                    pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    pds.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);

                }
            }
        }

        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }

    public static ProductDetails GetProductById(SqlConnection conAP, string id)
    {
        ProductDetails pds = null;
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            if (uid != "")
            {
                string query = "Select *,(select Count(pid) cnt from Wishlist where UserGuid=@uid and Pid=ProductDetails.Id) WishCount from ProductDetails where Status='Active' and (Id=@id or ProductUrl=@id)";
                using (SqlCommand cmd = new SqlCommand(query, conAP))
                {
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.AddWithValue("@uid", SqlDbType.Int).Value = uid;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        pds = new ProductDetails();
                        pds.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                        pds.Category = Convert.ToString(dt.Rows[0]["Category"]);
                        pds.SubCategory = Convert.ToString(dt.Rows[0]["SubCategory"]);
                        pds.Brand = Convert.ToString(dt.Rows[0]["Brand"]);
                        pds.ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                        pds.SKUCode = Convert.ToString(dt.Rows[0]["SKUCode"]);
                        pds.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                        pds.ProductUrl = Convert.ToString(dt.Rows[0]["ProductUrl"]);
                        pds.ProductImage = Convert.ToString(dt.Rows[0]["ProductImage"]);
                        pds.TaxGroup = Convert.ToString(dt.Rows[0]["TaxGroup"]);
                        pds.ShortDesc = Convert.ToString(dt.Rows[0]["ProductDesc"]);
                        pds.FullDesc = Convert.ToString(dt.Rows[0]["ProductFullDesc"]);
                        pds.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                        pds.MetaKey = Convert.ToString(dt.Rows[0]["MetaKey"]);
                        pds.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                        pds.InStock = Convert.ToString(dt.Rows[0]["InStock"]);
                        pds.Shop = Convert.ToString(dt.Rows[0]["Shop"]);
                        pds.Enquiry = Convert.ToString(dt.Rows[0]["Enquiry"]);
                        pds.productprs = ProductPrices.GetProductPriceByPId(conAP, Convert.ToString(dt.Rows[0]["Id"])).OrderBy(s => Convert.ToDouble(s.DiscountPrice)).ToList();
                        pds.productgly = ProductGallery.GetProductGallery(conAP, Convert.ToString(dt.Rows[0]["Id"]));
                        pds.productrvs = ProductReviews.GetProductReviews(conAP, Convert.ToString(dt.Rows[0]["Id"]));
                        pds.productfaqs = ProductFAQs.GetAllFAQS(conAP, Convert.ToString(dt.Rows[0]["Id"]));
                        pds.WishCount = Convert.ToInt32(dt.Rows[0]["WishCount"]);
                        pds.RelatedProducts = Convert.ToString(dt.Rows[0]["RelatedProducts"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductByGuid", ex.Message);
        }
        return pds;
    }

    public static List<ProductDetails> GetPDetails(SqlConnection conAP, string id)
    {
        List<ProductDetails> categories = new List<ProductDetails>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";

            string query = "Select *,(select Count(pid) cnt from Wishlist where UserGuid=@uid and Pid=ProductDetails.Id) WishCount from ProductDetails where Status='Active' and (Id=@id)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.AddWithValue("@uid", SqlDbType.Int).Value = uid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductDetails()
                              {

                                  Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"])),
                                  Category = Convert.ToString(dt.Rows[0]["Category"]),
                                  SubCategory = Convert.ToString(dt.Rows[0]["SubCategory"]),
                                  Brand = Convert.ToString(dt.Rows[0]["Brand"]),
                                  ProductGuid = Convert.ToString(dt.Rows[0]["ProductGuid"]),
                                  SKUCode = Convert.ToString(dt.Rows[0]["SKUCode"]),
                                  ProductName = Convert.ToString(dt.Rows[0]["ProductName"]),
                                  ProductUrl = Convert.ToString(dt.Rows[0]["ProductUrl"]),
                                  ProductImage = Convert.ToString(dt.Rows[0]["ProductImage"]),
                                  TaxGroup = Convert.ToString(dt.Rows[0]["TaxGroup"]),
                                  ShortDesc = Convert.ToString(dt.Rows[0]["ProductDesc"]),
                                  FullDesc = Convert.ToString(dt.Rows[0]["ProductFullDesc"]),
                                  PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]),
                                  MetaKey = Convert.ToString(dt.Rows[0]["MetaKey"]),
                                  MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]),
                                  Enquiry = Convert.ToString(dt.Rows[0]["Enquiry"]),
                                  Shop = Convert.ToString(dt.Rows[0]["Shop"]),
                                  InStock = Convert.ToString(dt.Rows[0]["InStock"]),
                                  productprs = ProductPrices.GetProductPriceByPId(conAP, Convert.ToString(dt.Rows[0]["Id"])).OrderBy(s => Convert.ToDouble(s.DiscountPrice)).ToList(),
                                  productgly = ProductGallery.GetProductGallery(conAP, Convert.ToString(dt.Rows[0]["Id"])),
                                  productrvs = ProductReviews.GetProductReviews(conAP, Convert.ToString(dt.Rows[0]["Id"])),
                                  productfaqs = ProductFAQs.GetAllFAQS(conAP, Convert.ToString(dt.Rows[0]["Id"])),
                                  WishCount = Convert.ToInt32(dt.Rows[0]["WishCount"]),
                                  RelatedProducts = Convert.ToString(dt.Rows[0]["RelatedProducts"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategory", ex.Message);
        }
        return categories;
    }

    #endregion
}
public class ProductPrices
{
    public int Id { set; get; }
    public string ProductId { get; set; }
    public string ProductSize { get; set; }
    public string ProductCcode { get; set; }
    public string ActualPrice { get; set; }
    public string ProductThickness { get; set; }
    public string ProductLength { get; set; }
    public string DiscountPrice { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string MinPrice { set; get; }
    public string MaxPrice { set; get; }
    public DateTime UpdatedOn { set; get; }

    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Product Prices Methods
    public static List<ProductPrices> GetAllProductPrices(SqlConnection conAP)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select *, (Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1 from ProductPrices where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetPriceBySizeAndThick(SqlConnection conAP, string size, string id, string thic)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select*,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductSize=@ProductSize and ProductId=@id and ProductThickness=@ProductThickness";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = Convert.ToInt32(id);
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = size;
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value = thic;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }

    public static List<ProductPrices> GetProductPriceIdByDetails(SqlConnection conAP, string pdid, string price, string size, string Thickness,string prid)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select*,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductSize=@ProductSize  and ProductThickness=@ProductThickness and ProductId=@ProductId and Id!=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = Convert.ToInt32(pdid);
                cmd.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = price;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = prid;
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = size;
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value = Thickness;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetProductPriceIdByDetails(SqlConnection conAP, string pdid, string price, string size, string Thickness)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select*,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductSize=@ProductSize and ProductThickness=@ProductThickness and ProductId=@ProductId";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = Convert.ToInt32(pdid);
                cmd.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = price;
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = size;
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value = Thickness;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetThicknessAndPriceBySize(SqlConnection conAP,string size, string id)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select*,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductSize=@ProductSize and ProductId=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = Convert.ToInt32(id);
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = size;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetProductPriceByPId(SqlConnection conAP, string id)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductId=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = Convert.ToInt32(id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetTop1ProductPriceDetailByPid(SqlConnection conAP, string id)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select *,(SELECT MAX(DiscountPrice) FROM ProductPrices WHERE Status = 'Active' AND ProductId = @id) AS MaxDiscountPrice,    (SELECT MIN(DiscountPrice) FROM ProductPrices WHERE Status = 'Active' AND ProductId = @id) AS MinDiscountPrice,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and ProductId=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = Convert.ToInt32(id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  MaxPrice = Convert.ToString(dr["MaxDiscountPrice"]),
                                  MinPrice = Convert.ToString(dr["MinDiscountPrice"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ProductThickness = Convert.ToString(dr["ProductThickness"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static List<ProductPrices> GetProductPriceByPriceId(SqlConnection conAP, int id)
    {
        List<ProductPrices> categories = new List<ProductPrices>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=ProductPrices.UpdatedBy) UpdatedBy1  from ProductPrices where Status='Active' and id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductPrices()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  ProductSize = Convert.ToString(dr["ProductSize"]),
                                  ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                  DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductPrices", ex.Message);
        }
        return categories;
    }
    public static int InsertProductPrice(SqlConnection conAP, ProductPrices cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductPrices (ProductId,DiscountPrice,ActualPrice,ProductSize,AddedBy,AddedOn,AddedIp,Status,UpdatedBy,UpdatedOn,UpdatedIp,ProductLength,ProductThickness) values(@ProductId,@DiscountPrice,@ActualPrice,@ProductSize,@AddedBy,@AddedOn,@AddedIp,@Status,@UpdatedBy,@UpdatedOn,@UpdatedIp,@ProductLength,@ProductThickness)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = cat.ActualPrice;
                cmd.Parameters.AddWithValue("@DiscountPrice", SqlDbType.NVarChar).Value = cat.DiscountPrice;
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = cat.ProductSize;
                cmd.Parameters.AddWithValue("@ProductLength", SqlDbType.NVarChar).Value = cat.ProductLength;
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value = cat.ProductThickness;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductPrice", ex.Message);
        }
        return result;
    }
    public static int UpdateProductPrice(SqlConnection conAP, ProductPrices cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductPrices Set ProductId=@ProductId,ProductLength=@ProductLength,ProductThickness=@ProductThickness,ProductSize=@ProductSize,DiscountPrice=@DiscountPrice,ActualPrice=@ActualPrice,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp where Id=@Id and ProductId=@ProductId";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = cat.ActualPrice;
                cmd.Parameters.AddWithValue("@DiscountPrice", SqlDbType.NVarChar).Value = cat.DiscountPrice;
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = cat.ProductSize;
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value = cat.ProductThickness;
                cmd.Parameters.AddWithValue("@ProductLength", SqlDbType.NVarChar).Value = cat.ProductLength;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductPrice", ex.Message);
        }
        return result;
    }
    public static int DeleteProductPrice(SqlConnection conAP, ProductPrices cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductPrices Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductPrice", ex.Message);
        }
        return result;
    }
    #endregion
}
public class ProductGallery
{
    public int Id { set; get; }
    public string ProductId { get; set; }
    public string Images { get; set; }
    public string GType { get; set; }
    public string GalleryOrder { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public DateTime ExpiryDate { get; set; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin ProductGallery region
    public static List<ProductGallery> GetAllProductGallery(SqlConnection conAP)
    {
        List<ProductGallery> categories = new List<ProductGallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductGallery.AddedBy) AddedBy1 from ProductGallery where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  Images = Convert.ToString(dr["Images"]),
                                  GType = Convert.ToString(dr["Type"]),
                                  GalleryOrder = Convert.ToString(dr["GalleryOrder"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductGallery", ex.Message);
        }
        return categories;
    }
    public static List<ProductGallery> GetProductGallery(SqlConnection conAP, string pdid)
    {
        List<ProductGallery> categories = new List<ProductGallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductGallery.AddedBy) AddedBy1 from ProductGallery where Status='Active' and ProductId=@pdid order by GalleryOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = pdid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  Images = Convert.ToString(dr["Images"]),
                                  GType = Convert.ToString(dr["Type"]),
                                  GalleryOrder = Convert.ToString(dr["GalleryOrder"]) == "" ? "0" : Convert.ToString(dr["GalleryOrder"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductGallery", ex.Message);
        }
        return categories;
    }
    public static int InsertProductGallery(SqlConnection conAP, ProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductGallery (ProductId,Images,AddedBy,AddedOn,AddedIp,Status,GalleryOrder,Type) values (@ProductId,@Images,@AddedBy,@AddedOn,@AddedIp,@Status,@GalleryOrder,@type)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@Images", SqlDbType.NVarChar).Value = cat.Images;
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                cmd.Parameters.AddWithValue("@type", SqlDbType.NVarChar).Value = cat.GType;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductGallery", ex.Message);
        }
        return result;
    }
    public static int DeleteProductGallery(SqlConnection conAP, ProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductGallery Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductGallery", ex.Message);
        }
        return result;
    }
    public static int UpdateProductGalleryOrder(SqlConnection conAP, ProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductGallery Set GalleryOrder=@GalleryOrder,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductGallery", ex.Message);
        }
        return result;
    }
    #endregion
}
public class ProductReviews
{
    public int Id { set; get; }

    public string UserName { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string Subject { get; set; }
    public string ProductUrl { get; set; }
    public string MobileNo { get; set; }
    public string EmailId { get; set; }
    public string Message { get; set; }
    public string Rating { get; set; }
    public string ImageUrl { get; set; }
    public string ReviewUrl { get; set; }
    public string ReviewFeatured { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string TotalCount { set; get; }
    public string TotalRatings { set; get; }
    public string UpdatedBy { set; get; }

    public string Status { set; get; }
    #region Admin category region
    public static DataTable GetAllFeaturedReviews(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "Select top 5 prs.*,cs.EmailId  Email2, prs.UName AddedBy1,cs.ImageUrl,pds.ProductName,pds.ProductUrl from ProductReviews prs left outer join Customers cs on cs.UserGuid=prs.UserGuid left outer join ProductDetails pds on pds.Id=prs.ProductId where prs.Status='Approved' and prs.Featured='Yes'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFeaturedReviews", ex.Message);
        }
        return dt;
    }


    public static List<ProductReviews> GetAllProductReviews(SqlConnection conAP)
    {
        List<ProductReviews> categories = new List<ProductReviews>();
        try
        {
            string query = "Select prs.*,cs.EmailId Email2, prs.UName AddedBy1,cs.ImageUrl,pds.ProductName,pds.ProductUrl from ProductReviews prs left outer join Customers cs on cs.UserGuid=prs.UserGuid left outer join ProductDetails pds on pds.Id=prs.ProductId where prs.Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductReviews()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  Subject = Convert.ToString(dr["Subject"]),
                                  ProductName = Convert.ToString(dr["ProductName"]),
                                  ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                  MobileNo = Convert.ToString(dr["MobileNo"]),
                                  EmailId = Convert.ToString(dr["Email"]),
                                  Message = Convert.ToString(dr["Message"]),
                                  Rating = Convert.ToString(dr["Rating"]),
                                  ReviewFeatured = Convert.ToString(dr["Featured"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  ReviewUrl = Convert.ToString(dr["RevUrl"]),
                                  UserName = Convert.ToString(dr["AddedBy1"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductReviews", ex.Message);
        }
        return categories;
    }

    public static List<ProductReviews> GetProductReviewsById(SqlConnection conAP, string id)
    {
        List<ProductReviews> categories = new List<ProductReviews>();
        try
        {
            string query = "Select prs.*,cs.EmailId  Email2,  prs.UName AddedBy1,cs.ImageUrl from ProductReviews prs left outer join Customers cs on cs.UserGuid=prs.AddedBy where prs.Status!='Deleted' and prs.Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductReviews()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  Subject = Convert.ToString(dr["Subject"]),
                                  MobileNo = Convert.ToString(dr["MobileNo"]),
                                  EmailId = Convert.ToString(dr["Email"]),
                                  Message = Convert.ToString(dr["Message"]),
                                  Rating = Convert.ToString(dr["Rating"]) == "" ? "0" : Convert.ToString(dr["Rating"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  UserName = Convert.ToString(dr["AddedBy1"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductReviews", ex.Message);
        }
        return categories;
    }
    public static List<ProductReviews> GetProductReviews(SqlConnection conAP, string pdid)
    {
        List<ProductReviews> categories = new List<ProductReviews>();
        try
        {
            string query = "Select prs.*,cs.EmailId  Email2,prs.UName AddedBy1,cs.ImageUrl,(SELECT COUNT(*) FROM ProductReviews WHERE ProductId = @pdid AND Status = 'Approved') AS TotalCount,(SELECT SUM(TRY_CAST(Rating AS INT)) FROM ProductReviews WHERE ProductId = @pdid AND Status = 'Approved') AS TotalRatings from ProductReviews prs left outer join Customers cs on cs.UserGuid=prs.AddedBy where prs.Status='Approved' and prs.ProductId=@pdid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = pdid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductReviews()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["ProductId"]),
                                  TotalCount = Convert.ToString(dr["TotalCount"]),
                                  TotalRatings = Convert.ToString(dr["TotalRatings"]),
                                  MobileNo = Convert.ToString(dr["MobileNo"]),
                                  Subject = Convert.ToString(dr["Subject"]),
                                  EmailId = Convert.ToString(dr["Email2"]),
                                  Message = Convert.ToString(dr["Message"]),
                                  Rating = Convert.ToString(dr["Rating"]) == "" ? "0" : Convert.ToString(dr["Rating"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  UserName = Convert.ToString(dr["AddedBy1"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductReviews", ex.Message);
        }
        return categories;
    }
    public static int InsertProductReviews(SqlConnection conAP, ProductReviews cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductReviews (Subject,ProductId,UName,MobileNo,Email,Message,Rating,AddedBy,AddedOn,AddedIp,Status,RevUrl,UserGuid,Featured) values (@Subject,@ProductId,@UName,@MobileNo,@Email,@Message,@Rating,@AddedBy,@AddedOn,@AddedIp,@Status,@RevUrl,@UserGuid,@Featured)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@UName", SqlDbType.NVarChar).Value = cat.UserName;
                cmd.Parameters.AddWithValue("@Subject", SqlDbType.NVarChar).Value = cat.Subject;
                cmd.Parameters.AddWithValue("@MobileNo", SqlDbType.NVarChar).Value = cat.MobileNo;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = cat.EmailId;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.NVarChar).Value = cat.Rating;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = cat.Message;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.ReviewFeatured;
                cmd.Parameters.AddWithValue("@RevUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductReviews", ex.Message);
        }
        return result;
    }

    public static int UpdateReviews(SqlConnection conAP, ProductReviews cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductReviews Set Subject=@Subject, ProductId=@ProductId,UName=@UName,MobileNo=@MobileNo,Email=@Email,Message=@Message,Rating=@Rating,AddedBy=@AddedBy,AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status,RevUrl=@RevUrl,UserGuid=@UserGuid,Featured=@Featured  Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@Subject", SqlDbType.NVarChar).Value = cat.Subject;
                cmd.Parameters.AddWithValue("@UName", SqlDbType.NVarChar).Value = cat.UserName;
                cmd.Parameters.AddWithValue("@MobileNo", SqlDbType.NVarChar).Value = cat.MobileNo;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = cat.EmailId;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.NVarChar).Value = cat.Rating;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = cat.Message;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.ReviewFeatured;
                cmd.Parameters.AddWithValue("@RevUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductReviews", ex.Message);
        }
        return result;
    }
    public static int UpdateProductReviews(SqlConnection conAP, ProductReviews cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductReviews Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductReviews", ex.Message);
        }
        return result;
    }

    public static int DeleteProductReviews(SqlConnection conAP, ProductReviews cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductReviews Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductReviews", ex.Message);
        }
        return result;
    }
    public static int ChangeReviewFeatured(SqlConnection conAP, ProductReviews cat)
    {
        int result = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand(@"Update ProductReviews Set Featured=@Featured,AddedOn=@UpdatedOn,AddedIp=@UpdatedIp  Where Id=@Id ", conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = cat.ReviewFeatured;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ChangeReviewFeatured", ex.Message);
        }
        return result;
    }

    public static string GetMessageById(SqlConnection conAP, string id)
    {
        string result = null;
        try
        {
            string cmdText = "select Message from ProductReviews where Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAP))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                conAP.Open();
                object obj = sqlCommand.ExecuteScalar();
                if (obj != DBNull.Value)
                {
                    result = obj.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMessageById", ex.Message);
        }
        finally
        {
            if (conAP.State == ConnectionState.Open)
            {
                conAP.Close();
            }
        }

        return result;
    }


    #endregion
}
public class ProductNotifyMe
{
    public int Id { set; get; }
    public string ProductId { get; set; }
    public string UserGuid { get; set; }

    public string ProductName { get; set; }
    public string PriceId { get; set; }
    public string ContactNo { get; set; }
    public string ProductUrl { get; set; }
    public string Title { get; set; }
    public string EmailId { get; set; }
    public string NUserName { get; set; }
    public string UserName { get; set; }
    public string NEmail { get; set; }
    public string NPhone { get; set; }
    public string NMessage { get; set; }

    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string UpdatedBy { set; get; }

    public string Status { set; get; }

    #region Admin category region
    public static List<ProductNotifyMe> GetAllProductNotifyMe(SqlConnection conAP)
    {
        List<ProductNotifyMe> categories = new List<ProductNotifyMe>();
        try
        {
            string query = "Select prs.*,cs.FirstName+' '+cs.LastName UserName,cs.EmailId,cs.ContactNo,cs.ImageUrl,pds.ProductName,pds.ProductUrl from NotifyMe prs left outer join Customers cs on cs.UserGuid=prs.UserGuid left outer join ProductDetails pds on pds.Id=prs.PId where prs.Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductNotifyMe()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductId = Convert.ToString(dr["PId"]),
                                  PriceId = Convert.ToString(dr["PrId"]),
                                  ProductName = Convert.ToString(dr["ProductName"]),
                                  ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                  EmailId = Convert.ToString(dr["EmailId"]),
                                  ContactNo = Convert.ToString(dr["ContactNo"]),
                                  UserName = Convert.ToString(dr["UserName"]),
                                  NUserName = Convert.ToString(dr["NUserName"]),
                                  NEmail = Convert.ToString(dr["NEmail"]),
                                  NPhone = Convert.ToString(dr["NPhone"]),
                                  NMessage = Convert.ToString(dr["NMessage"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductNotifyMe", ex.Message);
        }
        return categories;
    }
    public static int InsertProductNotifyMe(SqlConnection conAP, ProductNotifyMe cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into NotifyMe (PId,PrId,UserGuid,AddedOn,AddedIp,Status,NUserName,NEmail,NPhone,NMessage) values (@PId,@PrId,@uid,@AddedOn,@AddedIp,@Status,@NUserName,@NEmail,@NPhone,@NMessage)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@PId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@NUserName", SqlDbType.NVarChar).Value = cat.NUserName;
                cmd.Parameters.AddWithValue("@NEmail", SqlDbType.NVarChar).Value = cat.NEmail;
                cmd.Parameters.AddWithValue("@NPhone", SqlDbType.NVarChar).Value = cat.NPhone;
                cmd.Parameters.AddWithValue("@NMessage", SqlDbType.NVarChar).Value = cat.NMessage;
                cmd.Parameters.AddWithValue("@PrId", SqlDbType.NVarChar).Value = cat.PriceId;
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = cat.UserGuid;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductNotifyMe", ex.Message);
        }
        return result;
    }
    public static int DeleteProductNotifyme(SqlConnection conAP, ProductNotifyMe cat)
    {
        int result = 0;
        try
        {
            string query = "Update NotifyMe Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductNotifyme", ex.Message);
        }
        return result;
    }
    #endregion
}
public class ProductListingFilter
{
    public string Type { get; set; }
    public string CategoryUrl { get; set; }
    public string SubCats { get; set; }
    public string MinPrice { get; set; }
    public string MaxPrice { get; set; }
    public string SortBy { get; set; }
    public string PageNo { get; set; }
}
public class ProductListing
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string SKUCode { get; set; }
    public string ProductGuid { get; set; }
    public string Category { get; set; }
    public string CategoryUrl { get; set; }
    public string ProductName { get; set; }
    public string ProductUrl { get; set; }
    public int ProductOrder { get; set; }
    public string SubCategory { get; set; }
    public string Tags { get; set; }
    public string Brand { get; set; }
    public string Stock { get; set; }
    public string Demand { get; set; }
    public string Space { get; set; }
    public string ProductOccasions { get; set; }
    public string Featured { get; set; }
    public string ProductImage { get; set; }
    public string PriceId { get; set; }
    public string ProductSize { get; set; }
    public string PriceDesc { get; set; }
    public string ActualPrice { get; set; }
    public string DiscountPrice { get; set; }
    public string TotalProducts { get; set; }
    public string Rating { get; set; }
    public string ReviewCount { get; set; }

    public int WishCount { get; set; }
    public static List<ProductListing> GetAllRelatedProducts(SqlConnection conAP, string AllId)
    {
        List<ProductListing> productDetails = new List<ProductListing>();
        try
        {
            string IdQuery = "pd.id=''";
            if (AllId != "")
            {
                IdQuery = "";
                List<string> ids = AllId.Split('|').ToList();
                if (ids.Count == 1)
                {
                    IdQuery += "pd.id=" + ids[0];

                }
                else if (ids.Count > 1)
                {
                    int i = 1;
                    foreach (string id in ids)
                    {
                        if (i == ids.Count)
                        {
                            IdQuery += "pd.id=" + id;
                        }
                        else
                        {
                            IdQuery += "pd.id=" + id + " or ";

                        }
                        i++;
                    }
                }
            }
            string query = "Select * from (select ROW_NUMBER() OVER(ORDER BY pd.ProductName) RowNum,pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize,(Select Count( pd.id ) from ProductDetails as pd inner join ProductPrices as pp on pp.ProductId= pd.Id inner join category ca on ca.CategoryName=pd.Category  inner join SubCategory sc on  sc.SubCategory=pd.SubCategory  and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active') as recordCount from ProductDetails as pd inner join  ProductPrices as pp on pp.ProductId= pd.Id inner join category ca on ca.CategoryName=pd.Category inner join SubCategory sc on  sc.SubCategory=pd.SubCategory and sc.DisplayHome='Yes' and pp.Id =(SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and(" + IdQuery + @")) as x";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductListing()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      TotalProducts = Convert.ToString(dr["recordCount"]),
                                      Stock = Convert.ToString(dr["InStock"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      PriceId = Convert.ToString(dr["Pid"]),
                                      ProductSize = Convert.ToString(dr["ProductSize"]),
                                      ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                      ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                      DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                  }).ToList();

            }


        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllTopSellerProductsList", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductListing> GetAllTopSellerProductsList(SqlConnection conAP)
    {
        List<ProductListing> productDetails = new List<ProductListing>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";

            string query = "Select Top 10 * from (select ROW_NUMBER() OVER(ORDER BY pd.ProductName) RowNum, (select Count(pid) cnt from Wishlist where  UserGuid=@UserGuid and pid=pd.Id )  as WishCount ,pd.*, pp.Id Pid,pp.ActualPrice, pp.DiscountPrice,pp.ProductSize,(Select Count( pd.id ) from  ProductDetails as pd inner join ProductPrices as pp on pp.ProductId= pd.Id inner join category ca on ca.CategoryName=pd.Category and  ca.DisplayHome='Yes' inner join SubCategory sc on  sc.SubCategory=pd.SubCategory and sc.DisplayHome='Yes' and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id and p.Status='Active'  ORDER BY try_convert(decimal(10,2), p.DiscountPrice))  Where pd.Status='Active' and pd.InStock='Yes') as recordCount  from ProductDetails as pd inner join  ProductPrices as pp on  pp.ProductId= pd.Id inner join category ca on ca.CategoryName=pd.Category  and ca.DisplayHome='Yes' inner join SubCategory sc on  sc.SubCategory=pd.SubCategory and sc.DisplayHome='Yes'  and pp.Id =(SELECT TOP 1 p.id FROM ProductPrices p WHERE  p.ProductId = pd.Id and p.Status='Active'  ORDER BY try_convert(decimal(10,2), p.DiscountPrice))   Where pd.Status='Active' and pd.BestSeller='Yes' and pd.InStock='Yes')  as x ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductListing()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      SubCategory = Convert.ToString(dr["SubCategory"]),
                                      SKUCode = Convert.ToString(dr["SKUCode"]),
                                      TotalProducts = Convert.ToString(dr["recordCount"]),
                                      Stock = Convert.ToString(dr["InStock"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      ProductImage = Convert.ToString(dr["ProductImage"]),
                                      PriceId = Convert.ToString(dr["Pid"]),
                                      ProductSize = Convert.ToString(dr["ProductSize"]),
                                      ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                      ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                      DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                      WishCount = Convert.ToInt32(Convert.ToString(dr["WishCount"])),
                                  }).ToList();

            }


        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllTopSellerProductsList", ex.Message);
        }
        return productDetails;
    }
    public static List<ProductListing> GetAllSearchProductListed(SqlConnection conAP, ProductListingFilter filter)
    {
        List<ProductListing> products = new List<ProductListing>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            int pNo = 0, pageN = 12;
            int.TryParse(filter.PageNo, out pNo);




            string parameter = "";
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                parameter = filter.SortBy;

            }


            string query = "";
            if (parameter != "")
            {
                query = @"Select top 12 * from (select ROW_NUMBER() OVER (ORDER BY pd.ProductName) RowNum, pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize,(Select Count( pd.id ) from ProductDetails as pd inner join ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active'  and (pd.ProductName like '%'+@para+'%' or pd.Category like '%'+@para+'%' or pd.Brand like '%'+@para+'%' or pd.SubCategory like '%'+@para+'%' or pd.ProductTags like '%'+@para+'%') 
              ) as recordCount,( select Count(pid) cnt from Wishlist where UserGuid=@UserGuid and pid=pd.Id ) as WishCount
              from ProductDetails as pd inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id  and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and (pd.ProductName like '%'+@para+'%' or pd.Category like '%'+@para+'%' or pd.Brand like '%'+@para+'%' or pd.SubCategory like '%'+@para+'%' or pd.ProductTags like '%'+@para+'%')
              ) as x WHERE RowNum >" + (pageN * (pNo - 1));
            }


            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@para", SqlDbType.NVarChar).Value = parameter;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                products = (from DataRow dr in dt.Rows
                            select new ProductListing()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                Category = Convert.ToString(dr["Category"]),
                                Brand = Convert.ToString(dr["Brand"]),
                                ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                SubCategory = Convert.ToString(dr["SubCategory"]),
                                SKUCode = Convert.ToString(dr["SKUCode"]),
                                TotalProducts = Convert.ToString(dr["recordCount"]),
                                Stock = Convert.ToString(dr["InStock"]),
                                Featured = Convert.ToString(dr["Featured"]),
                                ProductImage = Convert.ToString(dr["ProductImage"]),
                                PriceId = Convert.ToString(dr["Pid"]),
                                ProductSize = Convert.ToString(dr["ProductSize"]),
                                ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                WishCount = Convert.ToInt32(Convert.ToString(dr["WishCount"])),
                            }).ToList();
            }


        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSearchProductListed", ex.Message);
        }
        return products;
    }
    public static List<ProductListing> GetAllProductListed(SqlConnection conAP, ProductListingFilter filter)
    {
        List<ProductListing> products = new List<ProductListing>();
        try
        {
            var uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            int pNo = 0, pageN = 12;
            int.TryParse(filter.PageNo, out pNo);



            string s_cats = filter.SubCats == null ? "" : filter.SubCats.Trim().TrimEnd('|');
            string tps = filter.Type == null ? "" : filter.Type;
            string cats = filter.CategoryUrl == null ? "" : filter.CategoryUrl.Split('?')[0];
            string order = " ORDER BY pd.InStock Desc, pd.ProductName ";
            if (filter.SortBy == "Low to High")
            {
                order = " ORDER BY try_convert(decimal, pp.DiscountPrice) ";
            }
            else if (filter.SortBy == "High to Low")
            {
                order = " ORDER BY try_convert(decimal, pp.DiscountPrice) desc ";
            }

            string featured = "", bestSelling = "";
            if (filter.SortBy == "Featured")
            {
                featured = " and Featured = 'Yes' ";
            }
            if (filter.SortBy == "Best Selling")
            {
                bestSelling = " and BestSeller = 'Yes' ";
            }
            string query = "";
            if (tps.ToLower() == "sub-category")
            {
                query = @"Select top 12 * from (select ROW_NUMBER() OVER(" + order + @") RowNum, pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize,ct.Category CategoryName,(Select Count( pd.id ) from ProductDetails as pd inner join Category as c on c.CategoryName = pd.Category inner join SubCategory ct on ct.SubCategory=pd.SubCategory and ct.Category = c.CategoryName inner join ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and ct.Status='Active' and (ct.SubCategoryUrl=@SubCategory and ct.displayHome='Yes') and (c.CategoryUrl=@Category and c.displayhome='Yes')" + featured + bestSelling + @"
              ) as recordCount,( select Count(pid) cnt from Wishlist where UserGuid=@UserGuid and pid=pd.Id ) as WishCount
              from ProductDetails as pd inner join Category as c on c.CategoryName = pd.Category inner join SubCategory ct on ct.SubCategory=pd.SubCategory and ct.Category = c.CategoryName inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id  and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and ct.Status='Active' and (ct.SubCategoryUrl=@SubCategory and ct.displayhome='Yes') and (c.CategoryUrl=@Category and c.displayhome='Yes')" + featured + @" " + bestSelling + @"
              ) as x WHERE RowNum >  " + (pageN * (pNo - 1));
            }
            else if (tps == "brand")
            {
                query = @"Select top 12 * from (select ROW_NUMBER() OVER(" + order + @") RowNum, pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize, br.BrandName,(Select Count( pd.id ) from ProductDetails as pd inner join Brand br on br.BrandName=pd.Brand inner join ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and br.Status='Active' and (br.BrandUrl=@Brand and br.displayhome='Yes')" + featured + bestSelling + @"
              ) as recordCount,( select Count(pid) cnt from Wishlist where UserGuid=@UserGuid and pid=pd.Id ) as WishCount
              from ProductDetails as pd inner join brand br on br.BrandName=pd.Brand inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id  and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and br.Status='Active' and (br.BrandUrl=@Brand and br.displayhome='Yes')" + featured + bestSelling + @"
              ) as x WHERE RowNum >  " + (pageN * (pNo - 1));
            }
            else
            {
                query = @"Select top 12 * from (select ROW_NUMBER() OVER(" + order + @") RowNum, pd.*, pp.Id Pid,pp.ActualPrice,pp.DiscountPrice,pp.ProductSize, ct.CategoryName,(Select Count( pd.id ) from ProductDetails as pd inner join category ct on ct.CategoryName=pd.Category inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE p.ProductId = pd.Id and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and ct.Status='Active' and (ct.CategoryUrl=@Category and ct.displayhome='Yes')" + featured + bestSelling + @"
              ) as recordCount,( select Count(pid) cnt from Wishlist where UserGuid=@UserGuid and pid=pd.Id ) as WishCount
              from ProductDetails as pd inner join category ct on ct.CategoryName=pd.Category inner join  ProductPrices as pp on pp.ProductId= pd.Id and pp.Id = (SELECT TOP 1 p.id FROM ProductPrices p  WHERE  p.ProductId = pd.Id  and p.Status='Active' ORDER BY try_convert(decimal(10,2), p.DiscountPrice)) Where pd.Status='Active' and ct.Status='Active' and (ct.CategoryUrl=@Category and ct.displayhome='Yes')" + featured + bestSelling + @"
              ) as x WHERE RowNum >  " + (pageN * (pNo - 1));
            }

            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cats;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cats;
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = s_cats;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (tps == "brand")
                {
                    products = (from DataRow dr in dt.Rows
                                select new ProductListing()
                                {
                                    Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                    ProductName = Convert.ToString(dr["ProductName"]),
                                    Brand = Convert.ToString(dr["Brand"]),
                                    ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                    SubCategory = Convert.ToString(dr["SubCategory"]),
                                    SKUCode = Convert.ToString(dr["SKUCode"]),
                                    TotalProducts = Convert.ToString(dr["recordCount"]),
                                    Stock = Convert.ToString(dr["InStock"]),
                                    Featured = Convert.ToString(dr["Featured"]),
                                    ProductImage = Convert.ToString(dr["ProductImage"]),
                                    PriceId = Convert.ToString(dr["Pid"]),
                                    ProductSize = Convert.ToString(dr["ProductSize"]),
                                    ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                    ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                    DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                    WishCount = Convert.ToInt32(Convert.ToString(dr["WishCount"])),
                                }).ToList();
                }
                else
                {
                    products = (from DataRow dr in dt.Rows
                                select new ProductListing()
                                {
                                    Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                    ProductName = Convert.ToString(dr["ProductName"]),
                                    Category = Convert.ToString(dr["CategoryName"]),
                                    Brand = Convert.ToString(dr["Brand"]),
                                    ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                    SubCategory = Convert.ToString(dr["SubCategory"]),
                                    SKUCode = Convert.ToString(dr["SKUCode"]),
                                    TotalProducts = Convert.ToString(dr["recordCount"]),
                                    Stock = Convert.ToString(dr["InStock"]),
                                    Featured = Convert.ToString(dr["Featured"]),
                                    ProductImage = Convert.ToString(dr["ProductImage"]),
                                    PriceId = Convert.ToString(dr["Pid"]),
                                    ProductSize = Convert.ToString(dr["ProductSize"]),
                                    ProductOrder = Convert.ToString(dr["ProductOrder"]) == "" ? 10000 : Convert.ToInt32(Convert.ToString(dr["ProductOrder"])),
                                    ActualPrice = Convert.ToString(dr["ActualPrice"]),
                                    DiscountPrice = Convert.ToString(dr["DiscountPrice"]),
                                    WishCount = Convert.ToInt32(Convert.ToString(dr["WishCount"])),
                                }).ToList();
                }

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductListed", ex.Message);
        }
        return products;
    }
}