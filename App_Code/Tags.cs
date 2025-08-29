using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Tags
{
    public int Id { set; get; }
    public string Title { set; get; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    public string TagURL { set; get; }
    public string TagImage { set; get; }
    public string DisplayHome { set; get; }
    public string DisplayOrder { set; get; }
    #region Admin Tags region

    public static List<string> GetAllDistictTagsFromProduct(SqlConnection conAP, string pram)
    {
        List<string> ccas = new List<string>();
        try
        {

            string query = "Select distinct ProductTags from ProductDetails where (ProductTags is not null and ProductTags != '')  and (@Param='' or Category=@Param or SubCategory=@Param or ProductTags=@Param) and Status ='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Param", SqlDbType.NVarChar).Value = pram;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string ss = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        ss += dr["ProductTags"].ToString() + "|";
                    }

                    string[] st = ss.TrimEnd('|').Split('|');
                    ccas = st.Distinct().ToList();
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDistictTagsFromProduct", ex.Message);
        }
        return ccas;
    }
    public static List<Tags> GetTop4Tags(SqlConnection conAP)
    {
        List<Tags> categories = new List<Tags>();
        try
        {
            string query = "select Top 4 *,(select UserName from CreateUser where UserGuid=Tags.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=Tags.UpdatedBy) UpdatedBy1 from Tags where Status='Active' and DisplayHome='Yes' Order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Tags()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  TagURL = Convert.ToString(dr["TagURL"]),
                                  TagImage = Convert.ToString(dr["TagImage"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllTags", ex.Message);
        }
        return categories;
    }
    public static List<Tags> GetAllTags(SqlConnection conAP)
    {
        List<Tags> categories = new List<Tags>();
        try
        {
            string query = "select *,(select UserName from CreateUser where UserGuid=Tags.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=Tags.UpdatedBy) UpdatedBy1 from Tags where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Tags()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  TagURL = Convert.ToString(dr["TagURL"]),
                                  TagImage = Convert.ToString(dr["TagImage"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  UpdatedBy =  Convert.ToString(dr["UpdatedBy1"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  UpdatedIp = Convert.ToString(dr["UpdatedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllTags", ex.Message);
        }
        return categories;
    }

    public static List<Tags> GetTagsByName(SqlConnection conAP, string Title)
    {
        List<Tags> categories = new List<Tags>();
        try
        {
            string query = "Select * from Tags Where Status='Active' and Title=@Title";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = Title;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Tags cat = new Tags();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.Title = Convert.ToString(dt.Rows[0]["Title"]);
                    cat.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    cat.DisplayHome = Convert.ToString(dt.Rows[0]["DisplayHome"]);
                    cat.TagURL = Convert.ToString(dt.Rows[0]["TagURL"]);
                    cat.TagImage = Convert.ToString(dt.Rows[0]["TagImage"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIp"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAmenity", ex.Message);
        }
        return categories;
    }
    public static List<Tags> GetTags(SqlConnection conAP, string catg)
    {
        List<Tags> categories = new List<Tags>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Tags Where Status='Active' and (Id=@Id or Title=@TagName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@TagName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Tags cat = new Tags();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.Title = Convert.ToString(dt.Rows[0]["Title"]);
                    cat.DisplayOrder = Convert.ToString(dt.Rows[0]["DisplayOrder"]);
                    cat.DisplayHome = Convert.ToString(dt.Rows[0]["DisplayHome"]);
                    cat.TagURL = Convert.ToString(dt.Rows[0]["TagURL"]);
                    cat.TagImage = Convert.ToString(dt.Rows[0]["TagImage"]);
                    cat.AddedIp = Convert.ToString(dt.Rows[0]["AddedIp"]);
                    cat.AddedOn = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                    cat.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    categories.Add(cat);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAmenity", ex.Message);
        }
        return categories;
    }
    public static int InsertTags(SqlConnection conAP, Tags cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Tags (Title,DisplayHome,DisplayOrder,TagImage,TagURL,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp) values (@Title,@DisplayHome,@DisplayOrder,@TagImage,@TagURL,@AddedOn, @AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@TagImage", SqlDbType.NVarChar).Value = cat.TagImage;
                cmd.Parameters.AddWithValue("@TagURL", SqlDbType.NVarChar).Value = cat.TagURL;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = cat.UpdatedIp;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertTags", ex.Message);
        }
        return result;
    }
    public static int UpdateTags(SqlConnection conAP, Tags cat)
    {
        int result = 0;
        try
        {
            string query = "Update Tags Set Title=@Title,DisplayHome=@DisplayHome,DisplayOrder=@DisplayOrder,TagImage=@TagImage,TagURL=@TagURL,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@TagImage", SqlDbType.NVarChar).Value = cat.TagImage;
                cmd.Parameters.AddWithValue("@TagURL", SqlDbType.NVarChar).Value = cat.TagURL;
                cmd.Parameters.AddWithValue("@UpdatedBy", SqlDbType.NVarChar).Value = cat.UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateTags", ex.Message);
        }
        return result;
    }
    public static int DeleteTags(SqlConnection conAP, Tags cat)
    {
        int result = 0;
        try
        {
            string query = "Update Tags Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteTags", ex.Message);
        }
        return result;
    }
    #endregion
}