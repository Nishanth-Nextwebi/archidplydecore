using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

public class BannerImages
{
    #region Banner Images Properties
    public int Id { get; set; }
    public string BannerTitle { get; set; }
    public string Link { get; set; }
    public string DesktopImage { get; set; }
    public string MobileImage { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string UpdatedIp { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    #endregion

    #region Banner Images Properties
    public static DataTable GetBannerImageMenu(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "dbo.GetBannerImage";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBannerImageMenu", ex.Message);
        }
        return dt;
    } 
    public static List<BannerImages> GetBannerImage(SqlConnection conAP)
    {
        List<BannerImages> products = new List<BannerImages>();
        try
        {
            string query = "dbo.GetBannerImage";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                sda.Fill(dt);
                products = (from DataRow dr in dt.Rows
                            select new BannerImages()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                MobileImage = Convert.ToString(dr["MobImage"]),
                                DesktopImage = Convert.ToString(dr["DeskImage"]),
                                Description = Convert.ToString(dr["Description"]),
                                BannerTitle = Convert.ToString(dr["BannerTitle"]),
                                Link = Convert.ToString(dr["Link"]),
                                AddedBy = Convert.ToString(dr["AddedBy1"]),
                                AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                AddedIp = Convert.ToString(dr["AddedIP"]),
                                UpdatedBy = Convert.ToString(dr["UpdatedBy1"]),
                                UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                UpdatedIp = Convert.ToString(dr["UpdatedIP"]),
                                Status = Convert.ToString(dr["Status"])
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBannerImage", ex.Message);
        }
        return products;
    }

    public static List<BannerImages> GetBannerImageById(SqlConnection conAP,int id)
    {
        List<BannerImages> products = new List<BannerImages>();
        try
        {
            string query = "Select * from BannerImages Where Status!='Deleted' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                products = (from DataRow dr in dt.Rows
                            select new BannerImages()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                MobileImage = Convert.ToString(dr["MobImage"]),
                                DesktopImage = Convert.ToString(dr["DeskImage"]),
                                Description = Convert.ToString(dr["Description"]),
                                BannerTitle = Convert.ToString(dr["BannerTitle"]),
                                Link = Convert.ToString(dr["Link"]),
                                AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                AddedIp = Convert.ToString(dr["AddedIP"]),
                                UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                UpdatedIp = Convert.ToString(dr["UpdatedIP"]),
                                Status = Convert.ToString(dr["Status"])
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBannerImage", ex.Message);
        }
        return products;
    }
    public static int InsertBannerImage(SqlConnection conAP, BannerImages pro)
    {
        int result = 0;
        try
        {
            string query = "Insert Into BannerImages (Description,DeskImage,MobImage,BannerTitle,Link,AddedBy,AddedOn,AddedIp,UpdatedBy,UpdatedOn,UpdatedIp,Status) values(@Description,@DeskImage,@MobImage,@BannerTitle,@Link,@AddedBy,@AddedOn,@AddedIp,@UpdatedBy,@UpdatedOn,@UpdatedIp,@Status) SELECT SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BannerTitle", SqlDbType.NVarChar).Value = pro.BannerTitle;
                cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = pro.Description;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = pro.Link;
                cmd.Parameters.AddWithValue("@MobImage", SqlDbType.NVarChar).Value = pro.MobileImage;
                cmd.Parameters.AddWithValue("@DeskImage", SqlDbType.NVarChar).Value = pro.DesktopImage;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = pro.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress(); 
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = pro.AddedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result =Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertBannerImage", ex.Message);
        }
        return result;
    }

    public static int UpdateBannerImage(SqlConnection conAP, BannerImages banner)
    {
        int result = 0;
        try
        {
            string query = "Update BannerImages Set Description=@Description, DeskImage=@DeskImage,MobImage=@MobImage, BannerTitle=@BannerTitle,Link=@Link,UpdatedOn=@UpdatedOn, UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = banner.Id;
                cmd.Parameters.AddWithValue("@BannerTitle", SqlDbType.NVarChar).Value = banner.BannerTitle;
                cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = banner.Description;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = banner.Link;
                cmd.Parameters.AddWithValue("@MobImage", SqlDbType.NVarChar).Value = banner.MobileImage;
                cmd.Parameters.AddWithValue("@DeskImage", SqlDbType.NVarChar).Value = banner.DesktopImage;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();  
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = banner.UpdatedBy;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateHomeBanner", ex.Message);
        }
        return result;
    }


    public static int AddBannerImage(SqlConnection conAP, BannerImages banner)
    {
        int result = 0;
        try
        {
            string query = "Update BannerImages Set DeskImage=@desk,MobImage=@mob Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = banner.Id;
                cmd.Parameters.AddWithValue("@mob", SqlDbType.NVarChar).Value = banner.MobileImage;
                cmd.Parameters.AddWithValue("@desk", SqlDbType.NVarChar).Value = banner.DesktopImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddBannerImage", ex.Message);
        }
        return result;
    }


    public static int DeleteBannerImage(SqlConnection conAP, BannerImages bis)
    {
        int result = 0;
        try
        {
            string query = "Update BannerImages Set Status=@Status,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = bis.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = bis.Status;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = bis.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBannerImage", ex.Message);
        }
        return result;
    }
    #endregion
}