using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubCategory
/// </summary>
public class SubCategory
{
    public int Id { set; get; }
    public string Category { set; get; }
    public string SubCategoryName { set; get; }
    public string CategoryName { set; get; }
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

    #region Admin SubCategory region
    public static DataTable GetAllSubCategoryMenu(SqlConnection conAP, string cat)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "select*,(select UserName from CreateUser where UserGuid=SubCategory.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=SubCategory.UpdatedBy) UpdatedBy1 from SubCategory where Status='Active' and Category=@cat";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@cat", SqlDbType.NVarChar).Value = cat;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategoryMenu", ex.Message);
        }
        return dt;
    }

    public static DataTable GetAllSubCategoryByCategory(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "SELECT * FROM SubCategory WHERE Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategoryByCategory", ex.Message);
        }
        return dt;
    }
    public static List<SubCategory> GetAllShopCategory(SqlConnection conAP)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            string query = "SELECT *, (SELECT TOP 1 CategoryName FROM Category WHERE id = SubCategory.category) AS categoryName, (SELECT UserName FROM CreateUser WHERE UserGuid = SubCategory.AddedBy) AS AddedBy1, (SELECT UserName FROM CreateUser WHERE UserGuid = SubCategory.UpdatedBy) AS UpdatedBy1 FROM SubCategory WHERE Status = 'Active' and DisplayHome='Yes' Order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new SubCategory()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  SubCategoryName = Convert.ToString(dr["SubCategory"]),
                                  Category = Convert.ToString(dr["Category"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  Url = Convert.ToString(dr["SubCategoryUrl"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  MetaKey = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategory", ex.Message);
        }
        return categories;
    }
    public static List<SubCategory> GetAllSubCategory(SqlConnection conAP)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            string query = "SELECT *, (SELECT TOP 1 CategoryName FROM Category WHERE id = SubCategory.category) AS categoryName, (SELECT UserName FROM CreateUser WHERE UserGuid = SubCategory.AddedBy) AS AddedBy1, (SELECT UserName FROM CreateUser WHERE UserGuid = SubCategory.UpdatedBy) AS UpdatedBy1 FROM SubCategory WHERE Status = 'Active' Order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new SubCategory()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  SubCategoryName = Convert.ToString(dr["SubCategory"]),
                                  Category = Convert.ToString(dr["Category"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  Url = Convert.ToString(dr["SubCategoryUrl"]),
                                  ImageUrl = Convert.ToString(dr["ImageUrl"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  MetaKey = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategory", ex.Message);
        }
        return categories;
    }
    public static List<SubCategory> GetSubCategoryByCat(SqlConnection conAP, string catg)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = @category) AS categoryName from SubCategory Where Status='Active' and Category=@Category And DisplayHome='Yes' order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@SubCategoryName", SqlDbType.NVarChar).Value = catg;
                cmd.Parameters.AddWithValue("@SubCategoryUrl", SqlDbType.NVarChar).Value = catg;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubCategory cat = new SubCategory();
                        cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[i]["Id"]));
                        cat.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategory"]);
                        cat.CategoryName = Convert.ToString(dt.Rows[i]["categoryName"]);
                        cat.Url = Convert.ToString(dt.Rows[i]["SubCategoryUrl"]);
                        cat.FullDesc = Convert.ToString(dt.Rows[i]["FullDesc"]);
                        cat.ShortDesc = Convert.ToString(dt.Rows[i]["ShortDesc"]);
                        cat.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        cat.PageTitle = Convert.ToString(dt.Rows[i]["PageTitle"]);
                        cat.MetaKey = Convert.ToString(dt.Rows[i]["MetaKeys"]);
                        cat.MetaDesc = Convert.ToString(dt.Rows[i]["MetaDesc"]);
                        cat.DisplayHome = Convert.ToString(dt.Rows[i]["DisplayHome"]);
                        cat.DisplayOrder = Convert.ToString(dt.Rows[i]["DisplayOrder"]);
                        cat.Category = Convert.ToString(dt.Rows[i]["Category"]);
                        cat.AddedIp = Convert.ToString(dt.Rows[i]["AddedIP"]);
                        cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[i]["AddedOn"]));
                        cat.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        categories.Add(cat);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
        return categories;
    }
    public static List<SubCategory> GetSubCategory(SqlConnection conAP, string catg)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from SubCategory Where Status='Active' and (Id=@Id or SubCategory=@SubCategoryName or SubCategoryUrl=@SubCategoryUrl or Category=@Category)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@SubCategoryName", SqlDbType.NVarChar).Value = catg;
                cmd.Parameters.AddWithValue("@SubCategoryUrl", SqlDbType.NVarChar).Value = catg;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubCategory cat = new SubCategory();
                        cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[i]["Id"]));
                        cat.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategory"]);
                        cat.Url = Convert.ToString(dt.Rows[i]["SubCategoryUrl"]);
                        cat.FullDesc = Convert.ToString(dt.Rows[i]["FullDesc"]);
                        cat.ShortDesc = Convert.ToString(dt.Rows[i]["ShortDesc"]);
                        cat.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        cat.PageTitle = Convert.ToString(dt.Rows[i]["PageTitle"]);
                        cat.MetaKey = Convert.ToString(dt.Rows[i]["MetaKeys"]);
                        cat.MetaDesc = Convert.ToString(dt.Rows[i]["MetaDesc"]);
                        cat.DisplayHome = Convert.ToString(dt.Rows[i]["DisplayHome"]);
                        cat.DisplayOrder = Convert.ToString(dt.Rows[i]["DisplayOrder"]);
                        cat.Category = Convert.ToString(dt.Rows[i]["Category"]);
                        cat.AddedIp = Convert.ToString(dt.Rows[i]["AddedIP"]);
                        cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[i]["AddedOn"]));
                        cat.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        categories.Add(cat);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
        return categories;
    }
    public static int InsertSubCategory(SqlConnection conAP, SubCategory cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into SubCategory (Category,SubCategory,SubCategoryUrl,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp,DisplayHome,DisplayOrder,PageTitle,MetaKeys,MetaDesc,FullDesc,ShortDesc,ImageUrl) values(@Category,@SubCategoryName,@ur,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp,@dh,@do,@PageTitle,@MetaKeys,@MetaDesc,@FullDesc,@ShortDesc,@ImageUrl) SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@SubCategoryName", SqlDbType.NVarChar).Value = cat.SubCategoryName;
                cmd.Parameters.AddWithValue("@ur", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                cmd.Parameters.AddWithValue("@dh", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@do", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                conAP.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertSubCategory", ex.Message);
        }
        return result;
    }
    public static int UpdateSubCategory(SqlConnection conAP, SubCategory cat)
    {
        int result = 0;
        try
        {
            string query = "Update SubCategory Set Category=@Category,FullDesc=@FullDesc,ShortDesc=@ShortDesc,SubCategory=@SubCategoryName,SubCategoryUrl=@ur,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy,ImageUrl=@img,DisplayHome=@dh,DisplayOrder=@do,PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@SubCategoryName", SqlDbType.NVarChar).Value = cat.SubCategoryName;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@img", SqlDbType.NVarChar).Value = cat.ImageUrl;
                cmd.Parameters.AddWithValue("@ur", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@dh", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@do", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateSubCategory", ex.Message);
        }
        return result;
    }
    public static int DeleteSubCategory(SqlConnection conAP, SubCategory cat)
    {
        int result = 0;
        try
        {
            string query = "Update SubCategory Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel
                    .UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteSubCategory", ex.Message);
        }
        return result;
    }
    public static int AddSubCatImage(SqlConnection conAP, SubCategory pds)
    {
        int result = 0;
        try
        {
            string query = "Update SubCategory Set ImageUrl=@img Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pds.Id;
                cmd.Parameters.AddWithValue("@img", SqlDbType.NVarChar).Value = pds.ImageUrl;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddSubCatImage", ex.Message);
        }
        return result;
    }
    public static List<SubCategory> GetSubCategoryByCatAndSub(SqlConnection conAP, string subcat)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            decimal ids = 0;
            string query = "Select * from SubCategory Where Status='Active' and SubCategory=@SubCategory";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@SubCategory", SqlDbType.NVarChar).Value = subcat;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubCategory cat = new SubCategory();
                        cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[i]["Id"]));
                        cat.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategory"]);
                        cat.Url = Convert.ToString(dt.Rows[i]["SubCategoryUrl"]);
                        cat.FullDesc = Convert.ToString(dt.Rows[i]["FullDesc"]);
                        cat.ShortDesc = Convert.ToString(dt.Rows[i]["ShortDesc"]);
                        cat.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        cat.PageTitle = Convert.ToString(dt.Rows[i]["PageTitle"]);
                        cat.MetaKey = Convert.ToString(dt.Rows[i]["MetaKeys"]);
                        cat.MetaDesc = Convert.ToString(dt.Rows[i]["MetaDesc"]);
                        cat.DisplayHome = Convert.ToString(dt.Rows[i]["DisplayHome"]);
                        cat.DisplayOrder = Convert.ToString(dt.Rows[i]["DisplayOrder"]);
                        cat.Category = Convert.ToString(dt.Rows[i]["Category"]);
                        cat.AddedIp = Convert.ToString(dt.Rows[i]["AddedIP"]);
                        cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[i]["AddedOn"]));
                        cat.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        categories.Add(cat);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
        return categories;
    }
    public static List<SubCategory> GetSubCategoryByID(SqlConnection conAP, string Id)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            string query = "Select *,(SELECT TOP 1 CategoryName FROM Category WHERE id = SubCategory.category) AS categoryName from SubCategory Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubCategory cat = new SubCategory();
                        cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[i]["Id"]));
                        cat.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategory"]);
                        cat.Url = Convert.ToString(dt.Rows[i]["SubCategoryUrl"]);
                        cat.FullDesc = Convert.ToString(dt.Rows[i]["FullDesc"]);
                        cat.ShortDesc = Convert.ToString(dt.Rows[i]["ShortDesc"]);
                        cat.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        cat.PageTitle = Convert.ToString(dt.Rows[i]["PageTitle"]);
                        cat.MetaKey = Convert.ToString(dt.Rows[i]["MetaKeys"]);
                        cat.MetaDesc = Convert.ToString(dt.Rows[i]["MetaDesc"]);
                        cat.DisplayHome = Convert.ToString(dt.Rows[i]["DisplayHome"]);
                        cat.DisplayOrder = Convert.ToString(dt.Rows[i]["DisplayOrder"]);
                        cat.Category = Convert.ToString(dt.Rows[i]["Category"]);
                        cat.AddedIp = Convert.ToString(dt.Rows[i]["AddedIP"]);
                        cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[i]["AddedOn"]));
                        cat.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        categories.Add(cat);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
        return categories;
    }
    public static List<SubCategory> GetSubCategoryByUrl(SqlConnection conAP, string url)
    {
        List<SubCategory> categories = new List<SubCategory>();
        try
        {
            string query = "Select * from SubCategory Where Status='Active' and SubCategoryUrl=@SubCategoryUrl";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@SubCategoryUrl", SqlDbType.NVarChar).Value = url;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SubCategory cat = new SubCategory();
                        cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[i]["Id"]));
                        cat.SubCategoryName = Convert.ToString(dt.Rows[i]["SubCategory"]);
                        cat.Url = Convert.ToString(dt.Rows[i]["SubCategoryUrl"]);
                        cat.FullDesc = Convert.ToString(dt.Rows[i]["FullDesc"]);
                        cat.ShortDesc = Convert.ToString(dt.Rows[i]["ShortDesc"]);
                        cat.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                        cat.PageTitle = Convert.ToString(dt.Rows[i]["PageTitle"]);
                        cat.MetaKey = Convert.ToString(dt.Rows[i]["MetaKeys"]);
                        cat.MetaDesc = Convert.ToString(dt.Rows[i]["MetaDesc"]);
                        cat.DisplayHome = Convert.ToString(dt.Rows[i]["DisplayHome"]);
                        cat.DisplayOrder = Convert.ToString(dt.Rows[i]["DisplayOrder"]);
                        cat.Category = Convert.ToString(dt.Rows[i]["Category"]);
                        cat.AddedIp = Convert.ToString(dt.Rows[i]["AddedIP"]);
                        cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[i]["AddedOn"]));
                        cat.Status = Convert.ToString(dt.Rows[i]["Status"]);
                        categories.Add(cat);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
        return categories;
    }
    #endregion

}