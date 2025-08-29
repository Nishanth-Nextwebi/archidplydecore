using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class ColorMaster
{
    public int Id { set; get; }
    public string Color { get; set; }
    public string CCode { get; set; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public DateTime AddedOn { set; get; }
    public DateTime UpdatedOn { set; get; }
    public string UpdatedIp { set; get; }
    public string UpdatedBy { set; get; }
    public string Status { set; get; }
    #region Admin ColorMaster region
    public static List<ColorMaster> GetAllColors(SqlConnection conAP)
    {
        List<ColorMaster> colors = new List<ColorMaster>();
        try
        {
            string query = "Select *,(select UserName from CreateUser where UserGuid=ColorMaster.AddedBy) adby from ColorMaster where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                colors = (from DataRow dr in dt.Rows
                              select new ColorMaster()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Color = Convert.ToString(dr["ColorName"]),
                                  CCode = Convert.ToString(dr["Code"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  AddedBy = Convert.ToString(dr["adby"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllColors", ex.Message);
        }
        return colors;
    }
    public static List<ColorMaster> GetColorMaster(SqlConnection conAP, int id)
    {
        List<ColorMaster> colors = new List<ColorMaster>();
        try
        {
            string query = "Select * from ColorMaster Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ColorMaster color = new ColorMaster();
                    color.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    color.CCode = Convert.ToString(dt.Rows[0]["Code"]);
                    color.Color = Convert.ToString(dt.Rows[0]["ColorName"]);
                    color.Status = Convert.ToString(dt.Rows[0]["Status"]);
                    colors.Add(color);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetColorMaster", ex.Message);
        }
        return colors;
    }
    public static int InsertColorMaster(SqlConnection conAP, ColorMaster color)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ColorMaster (ColorName,AddedOn,AddedIP,Status,AddedBy,Code) values (@ColorName,@AddedOn,@AddedIP,@Status,@AddedBy,@Code) ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ColorName", SqlDbType.NVarChar).Value = color.Color;
                cmd.Parameters.AddWithValue("@Code", SqlDbType.NVarChar).Value = color.CCode;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = color.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertColorMaster", ex.Message);
        }
        return result;
    }
    public static int UpdateColorMaster(SqlConnection conAP, ColorMaster color)
    {
        int result = 0;
        try
        {
            string query = "Update ColorMaster Set ColorName=@ColorName,AddedOn=@AddedOn,AddedIP=@AddedIP,AddedBy=@AddedBy,Code=@Code Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = color.Id;
                cmd.Parameters.AddWithValue("@ColorName", SqlDbType.NVarChar).Value = color.Color;
                cmd.Parameters.AddWithValue("@Code", SqlDbType.NVarChar).Value = color.CCode;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = color.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value =CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value =CommonModel.IPAddress();
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
    public static int DeleteColorMaster(SqlConnection conAP, ColorMaster color)
    {
        int result = 0;
        try
        {
            string query = "Update ColorMaster Set Status=@Status,AddedOn=@AddedOn,AddedIP=@AddedIP,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = color.Id;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = color.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
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