using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Scrolling
{
    public int Id { set; get; }
    public string Title { set; get; }
    public string RedirectLink { set; get; }

    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string Status { set; get; }
    #region methods
    public static List<Scrolling> GetAllScrollingText(SqlConnection conAP)
    {
        List<Scrolling> scrl = new List<Scrolling>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ScrollingText.AddedBy) AddedBy1 from ScrollingText Order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                scrl = (from DataRow dr in dt.Rows
                                  select new Scrolling()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      Title = Convert.ToString(dr["ScrollText"]),
                                      RedirectLink = Convert.ToString(dr["LinkUrl"]),
                                      AddedBy = Convert.ToString(dr["AddedBy1"]),
                                      AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                      AddedIp = Convert.ToString(dr["AddedIp"]),
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllScrollingText", ex.Message);
        }
        return scrl ;
    }
    public static int UpdateScroll(SqlConnection conAP, Scrolling cat)
    {
        int result = 0;
        try
        {
            string query = "Update ScrollingText Set ScrollText=@ScrollText,LinkUrl=@LinkUrl,AddedIp=@AddedIp,AddedOn=@AddedOn,AddedBy=@AddedBy Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ScrollText", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@LinkUrl", SqlDbType.NVarChar).Value = cat.RedirectLink;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateScroll", ex.Message);
        }
        return result;
    }
    public static int InsertScroll(SqlConnection conAP, Scrolling cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ScrollingText (ScrollText,LinkUrl,AddedBy,AddedOn,AddedIp) values (@ScrollText,@LinkUrl,@AddedBy,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ScrollText", SqlDbType.NVarChar).Value = cat.Title;
                cmd.Parameters.AddWithValue("@LinkUrl", SqlDbType.NVarChar).Value = cat.RedirectLink;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertScroll", ex.Message);
        }
        return result;
    }
    #endregion
}