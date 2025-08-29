
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class StoriesGallery
{
    public int Id { get; set; }

    public string StoryGuid { get; set; }

    public string Images { get; set; }

    public string GalleryOrder { get; set; }

    public DateTime AddedOn { get; set; }

    public string AddedIp { get; set; }

    public string AddedBy { get; set; }

    public string Status { get; set; }

    public static List<StoriesGallery> GetAllGallery(SqlConnection conGV)
    {
        List<StoriesGallery> result = new List<StoriesGallery>();
        try
        {
            string cmdText = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=StoriesGallery.AddedBy) AddedBy1 from StoriesGallery where Status='Active'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conGV))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new StoriesGallery
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              StoryGuid = Convert.ToString(dr["StoryGuid"]),
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllStoriesGallery", ex.Message);
        }

        return result;
    }

    public static List<StoriesGallery> GetGallery(SqlConnection conGV, string pdid)
    {
        List<StoriesGallery> result = new List<StoriesGallery>();
        try
        {
            string cmdText = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=StoriesGallery.AddedBy) AddedBy1 from StoriesGallery where Status='Active' and StoryGuid=@pdid order by GalleryOrder";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conGV))
            {
                sqlCommand.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = pdid;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new StoriesGallery
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              StoryGuid = Convert.ToString(dr["StoryGuid"]),
                              Images = Convert.ToString(dr["Images"]),
                              GalleryOrder = ((Convert.ToString(dr["GalleryOrder"]) == "") ? "0" : Convert.ToString(dr["GalleryOrder"])),
                              AddedBy = Convert.ToString(dr["AddedBy1"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }
                
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetStoriesGallery", ex.Message);
        }

        return result;
    }

    public static int InsertGallery(SqlConnection conGV, StoriesGallery cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into StoriesGallery (StoryGuid,Images,AddedBy,AddedOn,AddedIp,Status,GalleryOrder) values (@StoryGuid,@Images,@AddedBy,@AddedOn,@AddedIp,@Status,@GalleryOrder)";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conGV))
            {
                sqlCommand.Parameters.AddWithValue("@StoryGuid", SqlDbType.NVarChar).Value = cat.StoryGuid;
                sqlCommand.Parameters.AddWithValue("@Images", SqlDbType.NVarChar).Value = cat.Images;
                sqlCommand.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conGV.Open();
                result = sqlCommand.ExecuteNonQuery();
                conGV.Close();
            }
                
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertGallery", ex.Message);
        }

        return result;
    }

    public static int DeleteGallery(SqlConnection conGV, StoriesGallery cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update StoriesGallery Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conGV))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conGV.Open();
                result = sqlCommand.ExecuteNonQuery();
                conGV.Close();
            }
               
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteStoriesGallery", ex.Message);
        }

        return result;
    }

    public static int UpdateGalleryOrder(SqlConnection conGV, StoriesGallery cat)
    {
        int result = 0;
        try
        {
            string cmdText = "Update StoriesGallery Set GalleryOrder=@GalleryOrder,AddedOn=@AddedOn,AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conGV))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                sqlCommand.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                sqlCommand.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conGV.Open();
                result = sqlCommand.ExecuteNonQuery();
                conGV.Close();
            }
              
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteStoriesGallery", ex.Message);
        }

        return result;
    }
}

