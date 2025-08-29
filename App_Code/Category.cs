using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Category
{
    public int Id { set; get; }
    public string CategoryName { set; get; }
    public string CategoryUrl { set; get; }
    public string FullDesc { set; get; }
    public string ShortDesc { set; get; }
    public string Url { set; get; }
    public string ImageUrl { set; get; }
    public string DisplayOrder { set; get; }
    public string DisplayHome { set; get; }
    public string PageTitle { set; get; }
    public string MetaDesc { set; get; }
    public string MetaKey { set; get; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin category region
    public static DataTable GetAllCategoryMenu(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "dbo.GetAllCategory";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategoryMenu", ex.Message);
        }
        return dt;
    }

    public static List<Category> GetAllEnquiryCategory(SqlConnection conAP)
    {
        List<Category> categories = new List<Category>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=Category.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid=Category.UpdatedBy) UpdatedBy1 from Category where Status!='Deleted' order by try_convert(decimal,DisplayOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Category()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  CategoryName = Convert.ToString(dr["CategoryName"]),
                                  CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  MetaKey = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
        return categories;
    }
    public static List<Category> GetAllCategory(SqlConnection conAP)
    {
        List<Category> categories = new List<Category>();
        try
        {
            string query = "dbo.GetAllCategory";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Category()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  CategoryName = Convert.ToString(dr["CategoryName"]),
                                  CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  MetaKey = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
        return categories;
    }
    public static List<Category> GetCategoryByCategory(SqlConnection conAP, string catg)
    {
        List<Category> categories = new List<Category>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Category Where Status='Active' and CategoryName=@CategoryName";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Category cat = new Category();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    cat.CategoryUrl = Convert.ToString(dt.Rows[0]["CategoryUrl"]);
                    cat.FullDesc = Convert.ToString(dt.Rows[0]["FullDesc"]);
                    cat.ShortDesc = Convert.ToString(dt.Rows[0]["ShortDesc"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIP"]);
                    cat.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    cat.MetaKey = Convert.ToString(dt.Rows[0]["MetaKeys"]);
                    cat.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                    cat.DisplayHome = Convert.ToString(dt.Rows[0]["DisplayHome"]);
                    cat.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
        return categories;
    }
    public static List<Category> GetCategoryByCategoryUrl(SqlConnection conAP, string catg)
    {
        List<Category> categories = new List<Category>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Category Where Status='Active' and CategoryUrl=@CategoryUrl and DisplayHome='Yes' order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@CategoryUrl", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Category cat = new Category();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    cat.CategoryUrl = Convert.ToString(dt.Rows[0]["CategoryUrl"]);
                    cat.FullDesc = Convert.ToString(dt.Rows[0]["FullDesc"]);
                    cat.ShortDesc = Convert.ToString(dt.Rows[0]["ShortDesc"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIP"]);
                    cat.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    cat.MetaKey = Convert.ToString(dt.Rows[0]["MetaKeys"]);
                    cat.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                    cat.DisplayHome = Convert.ToString(dt.Rows[0]["DisplayHome"]);
                    cat.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
        return categories;
    }
    public static List<Category> GetCategory(SqlConnection conAP, string catg)
    {
        List<Category> categories = new List<Category>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Category Where Status='Active' and (Id=@Id or CategoryName=@CategoryName or CategoryUrl=@CategoryName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Category cat = new Category();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    cat.CategoryUrl = Convert.ToString(dt.Rows[0]["CategoryUrl"]);
                    cat.FullDesc = Convert.ToString(dt.Rows[0]["FullDesc"]);
                    cat.ShortDesc = Convert.ToString(dt.Rows[0]["ShortDesc"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIP"]);
                    cat.PageTitle = Convert.ToString(dt.Rows[0]["PageTitle"]);
                    cat.MetaKey = Convert.ToString(dt.Rows[0]["MetaKeys"]);
                    cat.MetaDesc = Convert.ToString(dt.Rows[0]["MetaDesc"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                    cat.DisplayHome = Convert.ToString(dt.Rows[0]["DisplayHome"]);
                    cat.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
        return categories;
    }
    public static string GetCategoryUrl(SqlConnection conAP, string catg)
    {
        string result = "";

        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select top 1 * from Category Where Status='Active' and (Id=@Id or CategoryName=@CategoryName or CategoryUrl=@CategoryName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = Convert.ToString(dt.Rows[0]["CategoryUrl"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategoryUrl", ex.Message);
        }
        return result;
    }
    public static int InsertCategory(SqlConnection conAP, Category cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Category (CategoryName,CategoryUrl,ShortDesc,FullDesc,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp,DisplayHome,DisplayOrder,PageTitle,MetaKeys,MetaDesc,ImageUrl) values(@CategoryName,@CategoryUrl,@ShortDesc,@FullDesc,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp,@DisplayHome,@DisplayOrder,@PageTitle,@MetaKeys,@MetaDesc,@ImageUrl)  SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = cat.CategoryName;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@CategoryUrl", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                conAP.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertCategory", ex.Message);
        }
        return result;
    }
    public static int UpdateCategory(SqlConnection conAP, Category cat)
    {
        int result = 0;
        try
        {
            string query = "Update Category Set CategoryName=@CategoryName,CategoryUrl=@CategoryUrl,ShortDesc=@ShortDesc,FullDesc=@FullDesc,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy,DisplayHome=@DisplayHome,ImageUrl=@ImageUrl,DisplayOrder=@DisplayOrder,PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = cat.CategoryName;
                cmd.Parameters.AddWithValue("@CategoryUrl", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCategory", ex.Message);
        }
        return result;
    }
    public static int DeleteCategory(SqlConnection conAP, Category cat)
    {
        int result = 0;
        try
        {
            string query = "Update Category Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteCategory", ex.Message);
        }
        return result;
    }
    public static int AddCatImage(SqlConnection conAP, Category pds)
    {
        int result = 0;
        try
        {
            string query = "Update Category Set ImageUrl=@ImageUrl Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pds.Id;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = pds.ImageUrl;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddCatImage", ex.Message);
        }
        return result;
    }
    public static int UpdateCategoryOrder(SqlConnection conAP, Category cat)
    {
        int result = 0;
        try
        {
            string query = "Update Category Set UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,DisplayOrder=@DisplayOrder Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCategoryOrder", ex.Message);
        }
        return result;
    }
    #endregion
}
