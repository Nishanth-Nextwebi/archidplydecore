using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Brand
/// </summary>
public class Brand
{
    public int Id { set; get; }
    public string BrandName { set; get; }
    public string BrandUrl { set; get; }
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

    #region Admin Brand region
    public static DataTable GetAllBrandMenu(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "dbo.GetAllBrand";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBrandMenu", ex.Message);
        }
        return dt;
    }
    public static List<Brand> GetAllBrand(SqlConnection conAP)
    {
        List<Brand> categories = new List<Brand>();
        try
        {
            string query = "dbo.GetAllBrand";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Brand()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  BrandName = Convert.ToString(dr["BrandName"]),
                                  BrandUrl = Convert.ToString(dr["BrandUrl"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBrand", ex.Message);
        }
        return categories;
    }
    public static List<Brand> GetBrandByName(SqlConnection conAP, string catg)
    {
        List<Brand> categories = new List<Brand>();
        try
        {
            string query = "Select * from Brand Where Status='Active' and BrandName=@BrandName or BrandUrl=@BrandName";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@BrandName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Brand cat = new Brand();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.BrandName = Convert.ToString(dt.Rows[0]["BrandName"]);
                    cat.BrandUrl = Convert.ToString(dt.Rows[0]["BrandUrl"]);
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBrand", ex.Message);
        }
        return categories;
    }
    public static List<Brand> GetBrand(SqlConnection conAP, string catg)
    {
        List<Brand> categories = new List<Brand>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Brand Where Status='Active' and (Id=@Id or BrandName=@BrandName or BrandUrl=@BrandName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@BrandName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Brand cat = new Brand();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.BrandName = Convert.ToString(dt.Rows[0]["BrandName"]);
                    cat.BrandUrl = Convert.ToString(dt.Rows[0]["BrandUrl"]);
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBrand", ex.Message);
        }
        return categories;
    }
    public static int InsertBrand(SqlConnection conAP, Brand cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Brand (BrandName,BrandUrl,ShortDesc,FullDesc,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp,DisplayHome,DisplayOrder,PageTitle,MetaKeys,MetaDesc,ImageUrl) values(@BrandName,@BrandUrl,@ShortDesc,@FullDesc,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp,@DisplayHome,@DisplayOrder,@PageTitle,@MetaKeys,@MetaDesc,@ImageUrl)  SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BrandName", SqlDbType.NVarChar).Value = cat.BrandName;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@BrandUrl", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertBrand", ex.Message);
        }
        return result;
    }
    public static int UpdateBrand(SqlConnection conAP, Brand cat)
    {
        int result = 0;
        try
        {
            string query = "Update Brand Set BrandName=@BrandName, BrandUrl=@BrandUrl, ShortDesc=@ShortDesc, FullDesc=@FullDesc, UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp, UpdatedBy=@UpdatedBy, DisplayHome=@DisplayHome, ImageUrl=@ImageUrl, DisplayOrder=@DisplayOrder, PageTitle=@PageTitle, MetaKeys=@MetaKeys, MetaDesc=@MetaDesc Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@BrandName", SqlDbType.NVarChar).Value = cat.BrandName;
                cmd.Parameters.AddWithValue("@BrandUrl", SqlDbType.NVarChar).Value = cat.Url;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = cat.ImageUrl;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBrand", ex.Message);
        }
        return result;
    }
    public static int DeleteBrand(SqlConnection conAP, Brand cat)
    {
        int result = 0;
        try
        {
            string query = "Update Brand Set Status=@Status, UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp, UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBrand", ex.Message);
        }
        return result;
    }
    public static int AddCatImage(SqlConnection conAP, Brand pds)
    {
        int result = 0;
        try
        {
            string query = "Update Brand Set ImageUrl=@ImageUrl Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddCatImage", ex.Message);
        }
        return result;
    }
    public static int UpdateBrandOrder(SqlConnection conAP, Brand cat)
    {
        int result = 0;
        try
        {
            string query = "Update Brand Set UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,DisplayOrder=@DisplayOrder Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBrandOrder", ex.Message);
        }
        return result;
    }
    #endregion
}