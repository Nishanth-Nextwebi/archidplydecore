using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductFAQs
/// </summary>
public class ProductFAQs
{
    public ProductFAQs()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }

    public static List<ProductFAQs> GetAllFAQS(SqlConnection conAP, string id)
    {
        List<ProductFAQs> faqs = new List<ProductFAQs>();
        try
        {
            string query = "Select * from ProductFAQs where ProductId=@ProductId and Status='Active' order by Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                faqs = (from DataRow dr in dt.Rows
                        select new ProductFAQs()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            ProductId = Convert.ToString(dr["ProductId"]),
                            Question = Convert.ToString(dr["Question"]),
                            Answer = Convert.ToString(dr["Answer"]),
                            Status = Convert.ToString(dr["Status"]),
                            AddedBy = Convert.ToString(dr["AddedBy"]),
                            AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                        }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFAQS", ex.Message);
        }
        return faqs;
    }
    public static List<ProductFAQs> GetAllFAQSNyId(SqlConnection conAP, int id)
    {
        List<ProductFAQs> faqs = new List<ProductFAQs>();
        try
        {
            string query = "Select * from ProductFAQs where Id=@Id and Status='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                faqs = (from DataRow dr in dt.Rows
                        select new ProductFAQs()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            ProductId = Convert.ToString(dr["ProductId"]),
                            Question = Convert.ToString(dr["Question"]),
                            Answer = Convert.ToString(dr["Answer"]),
                            Status = Convert.ToString(dr["Status"]),
                            AddedBy = Convert.ToString(dr["AddedBy"]),
                            AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                        }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFAQS", ex.Message);
        }
        return faqs;
    }
    public static int UpdateProductFAQs(SqlConnection conAP, ProductFAQs faq)
    {
        int result = 0;
        try
        {
            string query = "Update ProductFAQs Set Status=@Status,AddedBy=@AddedBy, Question=@Question,Answer=@Answer,AddedOn=@AddedOn Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = faq.Id;
                cmd.Parameters.AddWithValue("@Question", SqlDbType.NVarChar).Value = faq.Question;
                cmd.Parameters.AddWithValue("@Answer", SqlDbType.NVarChar).Value = faq.Answer;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = faq.AddedOn;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = faq.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = faq.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductFAQs", ex.Message);
        }
        return result;
    }
    public static int InsertProductFAQs(SqlConnection conAP, ProductFAQs faq)
    {
        int result = 0;

        try
        {
            string query = "Insert Into ProductFAQs (ProductId,Question,Answer,AddedOn,Status,AddedBy) values(@ProductId,@Question,@Answer,@AddedOn,@Status,@AddedBy)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = faq.ProductId;
                cmd.Parameters.AddWithValue("@Question", SqlDbType.NVarChar).Value = faq.Question;
                cmd.Parameters.AddWithValue("@Answer", SqlDbType.NVarChar).Value = faq.Answer;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = faq.AddedOn;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = faq.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = faq.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductFAQs", ex.Message);
        }
        return result;
    }
    /// <summary>
    /// delete Product FAQ
    /// </summary>
    /// <param name="conAP">connection</param>
    /// <param name="cat">parameters</param>
    /// <returns>returne status</returns>
    public static int DeleteProductFAQs(SqlConnection conAP, ProductFAQs faq)
    {
        int result = 0;
        try
        {
            string query = "Update ProductFAQs Set Status='Deleted', AddedBy=@AddedBy,AddedOn=@AddedOn Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = faq.Id;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = faq.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = faq.AddedOn;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductFAQs", ex.Message);
        }
        return result;
    }


}