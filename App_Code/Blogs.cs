using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

public class Blogs
{
    #region Blog Properties
    public int RowNumber { get; set; }
    public int TotalCount { get; set; }
    public DateTime PostedOn { get; set; }
    public string PostedBy { get; set; }
    public string DetailImage { get; set; }
    public int Id { set; get; }
    public string Category { set; get; }
    public string SubCategory { set; get; }
    public string Title { set; get; }
    public string Url { set; get; }
    public string ImageUrl { set; get; }
    public string ShortDesc { set; get; }
    public string FullDesc { set; get; }
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
    public string BlogImg { set; get; }
    public string BlogUpdateTime { set; get; }

    #endregion

    #region Blog region

    public static DataTable GetAllBlogsMenu(SqlConnection conAP)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "dbo.GetAllBlogs";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogsMenu", ex.Message);
        }
        return dt;
    }
    public static List<Blogs> GetTop5LatestBlog(SqlConnection conAP)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
            var qry = @"Select top 6 * from (Select ROW_NUMBER() OVER(ORDER BY Id DESC) AS RowNo,(select count(id) from blogs where status='Active') as TotalCount,*,
(Select Top 1 UserName From CreateUser Where UserGuid=Blogs.AddedBy) as AddedBy1,
(Select Top 1 UserName From CreateUser Where UserGuid=Blogs.UpdatedBy) as UpdatedBy1  from Blogs
where Status='Active') x" ;
            using (SqlCommand cmd = new SqlCommand(qry, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                             TotalCount = Convert.ToInt32(Convert.ToString(dr["TotalCount"])),
                             ImageUrl = Convert.ToString(dr["BlogImg"]),
                             Title = Convert.ToString(dr["BlogName"]),
                             Url = Convert.ToString(dr["BlogUrl"]),
                             DetailImage = Convert.ToString(dr["DetailImg"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             MetaKey = Convert.ToString(dr["MetaKeys"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             AddedBy = Convert.ToString(dr["AddedBy1"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             BlogUpdateTime = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])).ToString("dd MMM , yyyy"),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                             UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                             UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTop5LatestBlog", ex.Message);
        }
        return Blogs;
    }
    public static List<Blogs> GetAllListBlogs(SqlConnection conAP, int cPage)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
            string qrury = @"Select top 6 * from 
(Select ROW_NUMBER() OVER(Order by  PostedOn desc) AS RowNo,
(select count(id) from Blogs where status='Active') as TotalCount,
* from Blogs
where Status='Active') x where RowNo >" + (6 * (cPage - 1));
            using (SqlCommand cmd = new SqlCommand(qrury, conAP))
            {
               // cmd.Parameters.AddWithValue("@cPage", SqlDbType.NVarChar).Value = cPage;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                             TotalCount = Convert.ToInt32(Convert.ToString(dr["TotalCount"])),
                             ImageUrl = Convert.ToString(dr["BlogImg"]),
                             Title = Convert.ToString(dr["BlogName"]),
                             Url = Convert.ToString(dr["BlogUrl"]),
                             DetailImage = Convert.ToString(dr["DetailImg"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             MetaKey = Convert.ToString(dr["MetaKeys"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             BlogUpdateTime = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])).ToString("MMM dd,yyyy"),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                             UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                             UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogs", ex.Message);
        }
        return Blogs;
    }

    public static List<Blogs> GetAllBlogs(SqlConnection conAP)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
           //string query = "dbo.GetAllBlogs";
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=Blogs.UpdatedBy) UpdatedBy1 from Blogs where Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                            select new Blogs()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                //RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                                ImageUrl = Convert.ToString(dr["BlogImg"]),
                                Title = Convert.ToString(dr["BlogName"]),
                                Url = Convert.ToString(dr["BlogUrl"]),
                                DetailImage = Convert.ToString(dr["DetailImg"]),
                                BlogImg = Convert.ToString(dr["BlogImg"]),
                                ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                FullDesc = Convert.ToString(dr["FullDesc"]),
                                PageTitle = Convert.ToString(dr["PageTitle"]),
                                MetaKey = Convert.ToString(dr["MetaKeys"]),
                                MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                PostedBy = Convert.ToString(dr["PostedBy"]),
                                PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                                AddedBy = Convert.ToString(dr["AddedBy"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogs", ex.Message);
        }
        return Blogs;
    }

    public static List<Blogs> GetBlogById(SqlConnection conAP, int id)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
            string query = "Select * from Blogs Where Status!='Deleted' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                            select new Blogs()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                ImageUrl = Convert.ToString(dr["BlogImg"]),
                                Title = Convert.ToString(dr["BlogName"]),
                                Url = Convert.ToString(dr["BlogUrl"]),
                                DetailImage = Convert.ToString(dr["DetailImg"]),
                                FullDesc = Convert.ToString(dr["FullDesc"]),
                                MetaKey = Convert.ToString(dr["MetaKeys"]),
                                MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                PageTitle = Convert.ToString(dr["PageTitle"]),
                                PostedBy = Convert.ToString(dr["PostedBy"]),
                                PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                                ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                AddedBy = Convert.ToString(dr["AddedBy"]),
                                AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                AddedIp = Convert.ToString(dr["AddedIP"]),
                                UpdatedBy = Convert.ToString(dr["UpdatedBy"]),
                                UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                Status = Convert.ToString(dr["Status"])
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlogById", ex.Message);
        }
        return Blogs;
    }

    public static List<Blogs> GetBlogByName(SqlConnection conAP, string Blog)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=Blogs.AddedBy) AddedBy1,(Select Top 1 UserName From CreateUser Where UserGuid = Blogs.UpdatedBy) UpdatedBy1 from Blogs Where Status='Active' and (BlogUrl=@BlogUrl or BlogName=@BlogName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = Blog;
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = Blog;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                            select new Blogs()
                            {
                                Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                ImageUrl = Convert.ToString(dr["BlogImg"]),
                                Title = Convert.ToString(dr["BlogName"]),
                                MetaKey = Convert.ToString(dr["MetaKeys"]),
                                AddedBy = Convert.ToString(dr["AddedBy1"]),
                                MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                PageTitle = Convert.ToString(dr["PageTitle"]),
                                Url = Convert.ToString(dr["BlogUrl"]),
                                DetailImage = Convert.ToString(dr["DetailImg"]),
                                FullDesc = Convert.ToString(dr["FullDesc"]),
                                PostedBy = Convert.ToString(dr["PostedBy"]),
                                PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                                ShortDesc = Convert.ToString(dr["ShortDesc"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlogByName", ex.Message);
        }
        return Blogs;
    }

    public static int AddBlogImage(SqlConnection conAP, Blogs banner)
    {
        int result = 0;
        try
        {
            string query = "Update Blogs Set BlogImg=@BlogImg,DetailImg=@dig Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = banner.Id;
                cmd.Parameters.AddWithValue("@BlogImg", SqlDbType.NVarChar).Value = banner.ImageUrl;
                cmd.Parameters.AddWithValue("@dig", SqlDbType.NVarChar).Value = banner.DetailImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddBlogImage", ex.Message);
        }
        return result;
    }

    public static int WriteBlog(SqlConnection conAP, Blogs Blog)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Blogs (BlogImg,DetailImg,BlogName,BlogUrl, PostedOn,PostedBy,FullDesc,MetaKeys,MetaDesc,PageTitle,AddedOn,AddedIP,Status,AddedBy,UpdatedOn,UpdatedIp,UpdatedBy,ShortDesc) values(@BlogImg,@DetailImg,@BlogName,@BlogUrl, @PostedOn,@PostedBy,@FullDesc,@MetaKeys,@MetaDesc,@PageTitle,@AddedOn,@AddedIp,@Status,@AddedBy,@UpdatedOn,@UpdatedIp,@UpdatedBy,@ShortDesc) select SCOPE_IDENTITY()";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = Blog.Title;
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = Blog.Url;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = Blog.PostedOn;
                cmd.Parameters.AddWithValue("@PostedBy", SqlDbType.NVarChar).Value = Blog.PostedBy;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = Blog.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = Blog.ShortDesc;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = Blog.AddedBy;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = Blog.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = Blog.MetaDesc;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = Blog.PageTitle;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = Blog.AddedBy;
                cmd.Parameters.AddWithValue("@BlogImg", SqlDbType.NVarChar).Value = Blog.ImageUrl;
                cmd.Parameters.AddWithValue("@DetailImg", SqlDbType.NVarChar).Value = Blog.DetailImage;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = Blog.Status;
                conAP.Open();
                result =Convert.ToInt32(cmd.ExecuteScalar());
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "WriteBlog", ex.Message);
        }
        return result;
    }

    public static int UpdateBlog(SqlConnection conAP, Blogs Blog)
    {
        int result = 0;
        try
        {
            string query = "Update Blogs Set BlogImg=@BlogImg,DetailImg=@DetailImg, UpdatedBy=@UpdatedBy,BlogName=@BlogName,BlogUrl=@BlogUrl, MetaKeys=@MetaKeys,MetaDesc=@MetaDesc,PageTitle=@PageTitle,PostedOn=@PostedOn,PostedBy=@PostedBy,FullDesc=@FullDesc,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,ShortDesc=@ShortDesc Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Blog.Id;
                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = Blog.Title;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = Blog.UpdatedBy;
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = Blog.Url;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = Blog.PostedOn;
                cmd.Parameters.AddWithValue("@PostedBy", SqlDbType.NVarChar).Value = Blog.PostedBy;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = Blog.MetaKey;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = Blog.MetaDesc;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = Blog.PageTitle;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = Blog.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = Blog.ShortDesc;

                cmd.Parameters.AddWithValue("@BlogImg", SqlDbType.NVarChar).Value = Blog.ImageUrl;
                cmd.Parameters.AddWithValue("@DetailImg", SqlDbType.NVarChar).Value = Blog.DetailImage;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBlog", ex.Message);
        }
        return result;
    }

    public static int DeleteBlog(SqlConnection conAP, Blogs cat)
    {
        int result = 0;
        try
        {
            string query = "Update Blogs Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy,UpdatedIp=@UpdatedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBlog", ex.Message);
        }
        return result;
    }
    #endregion

    public static int PublishBlogs(SqlConnection conAP, Blogs cat)
    {
        int result = 0;
        try
        {
            string query = "Update Blogs Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "PublishBlogs", ex.Message);
        }
        return result;
    }

}