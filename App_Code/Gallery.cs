using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
public class Gallery
{
    public int Id { set; get; }
    public string Images { get; set; }
    public string GalleryOrder { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string Status { set; get; }
 
    public static List<Gallery> GetAllGallery(SqlConnection conAP)
    {
        List<Gallery> categories = new List<Gallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=Gallery.AddedBy) AddedBy1 from Gallery where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Gallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Images = Convert.ToString(dr["Images"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllGallery", ex.Message);
        }
        return categories;
    }
    public static List<Gallery> GetGallery(SqlConnection conAP, string pdid)
    {
        List<Gallery> categories = new List<Gallery>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=Gallery.AddedBy) AddedBy1 from Gallery where Status='Active' order by GalleryOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Gallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Images = Convert.ToString(dr["Images"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGallery", ex.Message);
        }
        return categories;
    }
    public static int InsertGallery(SqlConnection conAP, Gallery cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Gallery (Images,AddedBy,AddedOn,AddedIp,Status,GalleryOrder) values (@Images,@AddedBy,@AddedOn,@AddedIp,@Status,@GalleryOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Images", SqlDbType.NVarChar).Value = cat.Images;
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertGallery", ex.Message);
        }
        return result;
    }
    public static int DeleteGallery(SqlConnection conAP, Gallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update Gallery Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteGallery", ex.Message);
        }
        return result;
    }
    public static int UpdateGalleryOrder(SqlConnection conAP, Gallery cat)
    {
        int result = 0;
        try
        {
            string query = "Update Gallery Set GalleryOrder=@GalleryOrder,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteGallery", ex.Message);
        }
        return result;
    }
}