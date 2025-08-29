using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductStories
/// </summary>
public class ProductStories
{
    public int Id { get; set; }

    public string StoryGuid { get; set; }

    public string Title { get; set; }

    public string Link { get; set; }

    public string Image { get; set; }

    public string URL { get; set; }
    public string FullDesc { get; set; }

    public DateTime AddedOn { get; set; }

    public string AddedIP { get; set; }

    public string AddedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string UpdatedIP { get; set; }

    public string UpdatedBy { get; set; }

    public string Status { get; set; }
    public List<StoriesGallery> StoriesGal { get; set; }
    public string Featured { get; set; }
    public int RowNumber { get; set; }
    public int TotalCount { get; set; }

    public static List<ProductStories> GetTop3Stories(SqlConnection conAP)
    {
        List<ProductStories> ProductStories = new List<ProductStories>();
        try
        {
            string query = "SELECT Top 3 * FROM ProductStories WHERE Status = 'Active' and Featured='Yes'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ProductStories = (from DataRow dr in dt.Rows
                                  select new ProductStories()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      URL = Convert.ToString(dr["URL"]),
                                      Title = Convert.ToString(dr["Title"]),
                                      Link = Convert.ToString(dr["Link"]),
                                      Image = Convert.ToString(dr["Image"]),
                                      FullDesc = Convert.ToString(dr["FullDesc"]),
                                      Featured = Convert.ToString(dr["Featured"]),
                                      AddedBy = Convert.ToString(dr["AddedBy"]),
                                      AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                      UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                      Status = Convert.ToString(dr["Status"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductStories", ex.Message);
        }
        return ProductStories;
    }

    public static List<ProductStories> GetAllListProductStories(SqlConnection conAP, int cPage)
    {
        List<ProductStories> ProductStories = new List<ProductStories>();
        try
        {
            string qrury = @"Select top 5 * from 
(Select ROW_NUMBER() OVER(Order by  AddedOn desc) AS RowNo,
(select count(id) from ProductStories where status='Active') as TotalCount,
* from ProductStories
where Status='Active') x where RowNo >" + (5 * (cPage - 1));
            using (SqlCommand cmd = new SqlCommand(qrury, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ProductStories = (from DataRow dr in dt.Rows
                         select new ProductStories()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                             TotalCount = Convert.ToInt32(Convert.ToString(dr["TotalCount"])),
                             URL = Convert.ToString(dr["URL"]),
                             Title = Convert.ToString(dr["Title"]),
                             Link = Convert.ToString(dr["Link"]),
                             Image = Convert.ToString(dr["Image"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             Featured = Convert.ToString(dr["Featured"]),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                             StoriesGal = StoriesGallery.GetGallery(conAP, Convert.ToString(dr["Id"])),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductStories", ex.Message);
        }
        return ProductStories;
    }
    public static int InsertProductStoriesInfo(SqlConnection conAS, ProductStories vehicle)
    {
        int result = 0;
        try
        {
            string cmdText = "Insert Into ProductStories (Featured,StoryGuid,FullDesc,Title,Link,Image,URL,AddedOn,AddedIP,AddedBy,UpdatedOn,UpdatedIP,UpdatedBy,Status) values (@Featured,@StoryGuid,@FullDesc,@Title,@Link,@Image,@URL,@AddedOn,@AddedIP,@AddedBy,@UpdatedOn,@UpdatedIP,@UpdatedBy,@Status) select SCOPE_IDENTITY()";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAS))
            {
                sqlCommand.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = vehicle.Featured;
                sqlCommand.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = vehicle.FullDesc;
                sqlCommand.Parameters.AddWithValue("@StoryGuid", SqlDbType.NVarChar).Value = vehicle.StoryGuid;
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = vehicle.Title;
                sqlCommand.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = vehicle.Link;
                sqlCommand.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = vehicle.Image;
                sqlCommand.Parameters.AddWithValue("@URL", SqlDbType.NVarChar).Value = vehicle.URL;
                sqlCommand.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = vehicle.AddedOn;
                sqlCommand.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = vehicle.AddedIP;
                sqlCommand.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = vehicle.AddedBy;
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = vehicle.AddedOn;
                sqlCommand.Parameters.AddWithValue("@UpdatedIP", SqlDbType.NVarChar).Value = vehicle.AddedIP;
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = vehicle.AddedBy;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = vehicle.Status;
                conAS.Open();
                result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                conAS.Close();
            }
                
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductStoriesInfo", ex.Message);
        }

        return result;
    }

    public static int UpdateProductStoriesInfo(SqlConnection conAS, ProductStories vehicle)
    {
        int result = 0;
        try
        {
            string cmdText = "UPDATE ProductStories SET Featured=@Featured,FullDesc=@FullDesc, Title = @Title, Link = @Link, Image = @Image, URL = @URL, UpdatedOn = @UpdatedOn, UpdatedIP = @UpdatedIP, UpdatedBy = @UpdatedBy WHERE Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAS))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = vehicle.Id;
                sqlCommand.Parameters.AddWithValue("@Featured", SqlDbType.NVarChar).Value = vehicle.Featured;
                sqlCommand.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = vehicle.FullDesc;
                sqlCommand.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = vehicle.Title;
                sqlCommand.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = vehicle.Link;
                sqlCommand.Parameters.AddWithValue("@Image", SqlDbType.NVarChar).Value = vehicle.Image;
                sqlCommand.Parameters.AddWithValue("@URL", SqlDbType.NVarChar).Value = vehicle.URL;
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = vehicle.AddedOn;
                sqlCommand.Parameters.AddWithValue("@UpdatedIP", SqlDbType.NVarChar).Value = vehicle.AddedIP;
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = vehicle.AddedBy;
                conAS.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAS.Close();
            }
               
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductStoriesInfo", ex.Message);
        }

        return result;
    }

    public static int DeleteProductStoriesInfo(SqlConnection conAS, ProductStories vehicle)
    {
        int result = 0;
        try
        {
            string cmdText = "UPDATE ProductStories SET UpdatedOn = @UpdatedOn, UpdatedIP = @UpdatedIP, UpdatedBy = @UpdatedBy, Status=@Status  WHERE Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conAS))
                {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = vehicle.Id;
                sqlCommand.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = vehicle.Status;
                sqlCommand.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = vehicle.UpdatedOn;
                sqlCommand.Parameters.AddWithValue("@UpdatedIP", SqlDbType.NVarChar).Value = vehicle.UpdatedIP;
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = vehicle.UpdatedBy;
                conAS.Open();
                result = sqlCommand.ExecuteNonQuery();
                conAS.Close();
            }
                
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductStoriesInfo", ex.Message);
        }

        return result;
    }

    public static List<ProductStories> GetAllProductStoriess(SqlConnection connection)
    {
        List<ProductStories> result = new List<ProductStories>();
        try
        {
            string cmdText = "SELECT * FROM ProductStories WHERE Status = 'Active'";
            using (SqlCommand selectCommand = new SqlCommand(cmdText, connection)){
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new ProductStories
                          {
                              Id = Convert.ToInt32(dr["Id"]),
                              Featured = Convert.ToString(dr["Featured"]),
                              StoryGuid = Convert.ToString(dr["StoryGuid"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              Title = Convert.ToString(dr["Title"]),
                              Link = Convert.ToString(dr["Link"]),
                              Image = Convert.ToString(dr["Image"]),
                              URL = Convert.ToString(dr["URL"]),
                              AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                              AddedIP = Convert.ToString(dr["AddedIP"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]),
                              UpdatedIP = Convert.ToString(dr["UpdatedIP"]),
                              UpdatedBy = Convert.ToString(dr["UpdatedBy"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }
            
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductStoriess", ex.Message);
        }

        return result;
    }

    public static List<ProductStories> GetProductStoriessbySeletedType(SqlConnection connection, string o_guid)
    {
        List<ProductStories> result = new List<ProductStories>();
        try
        {
            string cmdText = "SELECT * FROM ProductStories WHERE Title = (select top 1 vehicle_wheel from AlphaSandOrderItems where order_guid=@o_guid) and  Status = 'Active'";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
            {
                sqlCommand.Parameters.AddWithValue("@o_guid", SqlDbType.NVarChar).Value = o_guid;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new ProductStories
                          {
                              Id = Convert.ToInt32(dr["Id"]),
                              Featured = Convert.ToString(dr["Featured"]),
                              StoryGuid = Convert.ToString(dr["StoryGuid"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              Title = Convert.ToString(dr["Title"]),
                              Link = Convert.ToString(dr["Link"]),
                              Image = Convert.ToString(dr["Image"]),
                              URL = Convert.ToString(dr["URL"]),
                              AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                              AddedIP = Convert.ToString(dr["AddedIP"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]),
                              UpdatedIP = Convert.ToString(dr["UpdatedIP"]),
                              UpdatedBy = Convert.ToString(dr["UpdatedBy"]),
                              Status = Convert.ToString(dr["Status"])
                          }).ToList();
            }
               
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductStoriessbySeletedType", ex.Message);
        }

        return result;
    }

    public static DataTable GetDistinctProductStoriesType(SqlConnection _conn)
    {
        DataTable dataTable = new DataTable();
        try
        {
            SqlCommand selectCommand = new SqlCommand("SELECT distinct Title FROM ProductStories WHERE Status = 'Active'", _conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDistinctProductStoriesType", ex.Message);
        }

        return dataTable;
    }

    public static ProductStories GetProductStoriesById(SqlConnection connection, int vehicleId)
    {
        ProductStories result = null;
        try
        {
            string cmdText = "SELECT * FROM ProductStories WHERE Id = @Id AND Status = 'Active'";
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = vehicleId;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                result = (from DataRow dr in dataTable.Rows
                          select new ProductStories
                          {
                              Id = Convert.ToInt32(dr["Id"]),
                              Featured = Convert.ToString(dr["Featured"]),
                              StoryGuid = Convert.ToString(dr["StoryGuid"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              Title = Convert.ToString(dr["Title"]),
                              Link = Convert.ToString(dr["Link"]),
                              Image = Convert.ToString(dr["Image"]),
                              URL = Convert.ToString(dr["URL"]),
                              AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                              AddedIP = Convert.ToString(dr["AddedIP"]),
                              AddedBy = Convert.ToString(dr["AddedBy"]),
                              UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]),
                              UpdatedIP = Convert.ToString(dr["UpdatedIP"]),
                              UpdatedBy = Convert.ToString(dr["UpdatedBy"]),
                              Status = Convert.ToString(dr["Status"])
                          }).FirstOrDefault();
            }
                
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductStoriesById", ex.Message);
        }

        return result;
    }
}