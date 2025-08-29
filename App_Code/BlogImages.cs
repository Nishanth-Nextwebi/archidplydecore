using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class BlogImages
{
    public int Id { set; get; }
    public int BlogId { get; set; }
    public string ImageUrl { get; set; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime AddedOn { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin BlogImages region
    public static List<BlogImages> GetAllBlogImages(SqlConnection conAP)
    {
        List<BlogImages> allImage = new List<BlogImages>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=BlogImages.AddedBy) adby from BlogImages where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                allImage = (from DataRow dr in dt.Rows
                              select new BlogImages()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  BlogId = Convert.ToInt32(Convert.ToString(dr["BlogId"])),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogImages", ex.Message);
        }
        return allImage;
    }


    public static List<BlogImages> GetAllBlogImagesWithBlogId(SqlConnection conAP,int BlogId)
    {
        List<BlogImages> allImage = new List<BlogImages>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=BlogImages.AddedBy) adby  from BlogImages where Status='Active' and BlogId=@BlogId";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BlogId", SqlDbType.Int).Value = BlogId;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                allImage = (from DataRow dr in dt.Rows
                            select new BlogImages()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                BlogId = Convert.ToInt32(Convert.ToString(dr["BlogId"])),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogImages", ex.Message);
        }
        return allImage;
    }
    public static List<BlogImages> GetBlogImageWithId(SqlConnection conAP, int id)
    {
        List<BlogImages> image = new List<BlogImages>();
        try
        {
            string query = "Select * from BlogImages Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    BlogImages img = new BlogImages();
                    img.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    img.BlogId = Convert.ToInt32(Convert.ToString(dt.Rows[0]["BlogId"]));
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
    public static int InsertBlogImages(SqlConnection conAP, BlogImages img)
    {
        int result = 0;
        try
        {
            string query = "Insert Into BlogImages (BlogId,ImageUrl,UpdatedOn,UpdatedIp,UpdatedBy,AddedOn,AddedIP,AddedBy,Status) values (@BlogId,@ImageUrl,@UpdatedOn,@UpdatedIp,@UpdatedBy,@AddedOn,@AddedIP,@AddedBy,@Status) ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BlogId", SqlDbType.Int).Value = img.BlogId;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertBlogImages", ex.Message);
        }
        return result;
    }
    public static int UpdateBlogImages(SqlConnection conAP, BlogImages img)
    {
        int result = 0;
        try
        {
            string query = "Update BlogImages Set BlogId=@BlogId,ImageUrl=@ImageUrl,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = img.Id;
                cmd.Parameters.AddWithValue("@BlogId", SqlDbType.NVarChar).Value = img.BlogId;
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
    public static int DeleteBlogImages(SqlConnection conAP, BlogImages img)
    {
        int result = 0;
        try
        {
            string query = "Update BlogImages Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy  Where Id=@Id ";
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