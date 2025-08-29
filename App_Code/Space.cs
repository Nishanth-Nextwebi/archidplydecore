using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Space
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
    #region Admin Space region

    public static List<string> GetAllDistictSpacesFromProduct(SqlConnection conAP, string pram)
    {
        List<string> ccas = new List<string>();
        try
        {

            string query = "Select distinct Spaces from ProductDetails where (Spaces is not null and Spaces != '')  and (@Param='' or Category=@Param or SubCategory=@Param or ProductTags=@Param) and Status ='Active'";
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
                        ss += dr["Spaces"].ToString() + "|";
                    }

                    string[] st = ss.TrimEnd('|').Split('|');
                    ccas = st.Distinct().ToList();
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDistictSpacesFromProduct", ex.Message);
        }
        return ccas;
    }

    public static List<Space> GetAllSpace(SqlConnection conAP)
    {
        List<Space> categories = new List<Space>();
        try
        {
            string query = "select *,(select UserName from CreateUser where UserGuid=Space.AddedBy) AddedBy1,(select UserName from CreateUser where UserGuid=Space.UpdatedBy) UpdatedBy1 from Space where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Space()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Title = Convert.ToString(dr["Title"]),
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSpace", ex.Message);
        }
        return categories;
    }
    public static List<Space> GetSpace(SqlConnection conAP, string catg)
    {
        List<Space> categories = new List<Space>();
        try
        {
            decimal ids = 0;
            decimal.TryParse(catg, out ids);
            string query = "Select * from Space Where Status='Active' and (Id=@Id or Title=@SpaceName)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = ids;
                cmd.Parameters.AddWithValue("@SpaceName", SqlDbType.NVarChar).Value = catg;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Space cat = new Space();
                    cat.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    cat.Title = Convert.ToString(dt.Rows[0]["Title"]);
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
    public static int InsertSpace(SqlConnection conAP, Space cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into Space (Title,AddedOn,AddedIp,Status,AddedBy,UpdatedBy,UpdatedOn,UpdatedIp) values (@SpaceName,@AddedOn, @AddedIp,@Status,@AddedBy,@UpdatedBy,@UpdatedOn,@UpdatedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@SpaceName", SqlDbType.NVarChar).Value = cat.Title;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertSpace", ex.Message);
        }
        return result;
    }
    public static int UpdateSpace(SqlConnection conAP, Space cat)
    {
        int result = 0;
        try
        {
            string query = "Update Space Set Title=@SpaceName,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@SpaceName", SqlDbType.NVarChar).Value = cat.Title;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateSpace", ex.Message);
        }
        return result;
    }
    public static int DeleteSpace(SqlConnection conAP, Space cat)
    {
        int result = 0;
        try
        {
            string query = "Update Space Set Status=@Status,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,UpdatedBy=@UpdatedBy Where Id=@Id ";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteSpace", ex.Message);
        }
        return result;
    }
    #endregion
}