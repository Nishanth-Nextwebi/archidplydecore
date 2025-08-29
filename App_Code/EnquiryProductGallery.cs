using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnquiryEnquiryProductGallery
/// </summary>
public class EnquiryProductGallery
{
    public int Id { set; get; }
    public string ProductGuid { get; set; }
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
    public static List<EnquiryProductGallery> GetAllEnquiryProductGallery(SqlConnection conAP)
    {
        List<EnquiryProductGallery> categories = new List<EnquiryProductGallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProductGallery.AddedBy) AddedBy1 from EnquiryProductGallery where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new EnquiryProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductGuid = Convert.ToString(dr["ProductGuid"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllEnquiryProductGallery", ex.Message);
        }
        return categories;
    }
    public static List<EnquiryProductGallery> GetTop4EnquiryProductGallery(SqlConnection conAP, string pdid)
    {

        List<EnquiryProductGallery> galleries = new List<EnquiryProductGallery>();
        try
        {
            string query = "Select Top 4*,(Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProductGallery.AddedBy) AddedBy1 from EnquiryProductGallery where Status='Active' and ProductGuid=@pdid order by GalleryOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = pdid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                galleries = (from DataRow dr in dt.Rows
                              select new EnquiryProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductGuid = Convert.ToString(dr["ProductGuid"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductGallery", ex.Message);
        }
        return galleries;
    }
    public static List<EnquiryProductGallery> GetEnquiryProductGallery(SqlConnection conAP, string pdid)
    {
        
        List<EnquiryProductGallery> categories = new List<EnquiryProductGallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=EnquiryProductGallery.AddedBy) AddedBy1 from EnquiryProductGallery where Status='Active' and ProductGuid=@pdid order by GalleryOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = pdid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new EnquiryProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  ProductGuid = Convert.ToString(dr["ProductGuid"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductGallery", ex.Message);
        }
        return categories;
    }
    public static int InsertEnquiryProductGallery(SqlConnection conAP, EnquiryProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into EnquiryProductGallery (ProductGuid,Images,AddedBy,AddedOn,AddedIp,Status,GalleryOrder,Type) values (@ProductGuid,@Images,@AddedBy,@AddedOn,@AddedIp,@Status,@GalleryOrder,@type)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = cat.ProductGuid;
                cmd.Parameters.AddWithValue("@Images", SqlDbType.NVarChar).Value = cat.Images;
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                cmd.Parameters.AddWithValue("@type", SqlDbType.NVarChar).Value ="Images";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertEnquiryProductGallery", ex.Message);
        }
        return result;
    }
    public static int DeleteEnquiryProductGallery(SqlConnection conAP, EnquiryProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update EnquiryProductGallery Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnquiryProductGallery", ex.Message);
        }
        return result;
    }
    public static int UpdateEnquiryProductGalleryOrder(SqlConnection conAP, EnquiryProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update EnquiryProductGallery Set GalleryOrder=@GalleryOrder,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnquiryProductGallery", ex.Message);
        }
        return result;
    }
}