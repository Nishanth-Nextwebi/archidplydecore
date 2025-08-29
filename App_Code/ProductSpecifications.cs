using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System;
using System.Linq;

public class ProductSpecifications
{
    public ProductSpecifications()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DisplayOrder { get; set; }
    public string Grade { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }


    public static List<ProductSpecifications> GetAllFAQS(SqlConnection conAP, string id)
    {
        List<ProductSpecifications> faqs = new List<ProductSpecifications>();
        try
        {
            string query = "Select * from ProductSpecifications where ProductId=@ProductId and Status='Active' order by DisplayOrder";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                faqs = dt.AsEnumerable().Select(dr => new ProductSpecifications()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductId = Convert.ToString(dr["ProductId"]),
                    Title = Convert.ToString(dr["Title"]),
                    Description = Convert.ToString(dr["Description"]),
                    DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                    Grade = Convert.ToString(dr["Grade"]),
                    Status = Convert.ToString(dr["Status"]),
                    AddedBy = Convert.ToString(dr["AddedBy"]),
                    AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFAQS", ex.Message);
        }
        return faqs;
    }
    public static List<ProductSpecifications> GetAllFAQSById(SqlConnection conAP, int id)
    {
        List<ProductSpecifications> faqs = new List<ProductSpecifications>();
        try
        {
            string query = "Select * from ProductSpecifications where Id=@Id and Status='Active' order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                faqs = dt.AsEnumerable().Select(dr => new ProductSpecifications()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    ProductId = Convert.ToString(dr["ProductId"]),
                    Title = Convert.ToString(dr["Title"]),
                    DisplayOrder = Convert.ToString(dr["DisplayOrder"]),
                    Grade = Convert.ToString(dr["Grade"]),
                    Description = Convert.ToString(dr["Description"]),
                    Status = Convert.ToString(dr["Status"]),
                    AddedBy = Convert.ToString(dr["AddedBy"]),
                    AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFAQS", ex.Message);
        }
        return faqs;
    }
    public static int UpdateProductSpecifications(SqlConnection conAP, ProductSpecifications faq)
    {
        int result = 0;
        try
        {
            string query = "Update ProductSpecifications Set Grade=@Grade, DisplayOrder=@DisplayOrder,Status=@Status,AddedBy=@AddedBy, Title=@Title,Description=@Description,AddedOn=@AddedOn Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = faq.Id;
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = faq.Title;
                cmd.Parameters.AddWithValue("@Grade", SqlDbType.NVarChar).Value = faq.Grade;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = faq.DisplayOrder;
                cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = faq.Description;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductSpecifications", ex.Message);
        }
        return result;
    }
    public static int InsertProductSpecifications(SqlConnection conAP, ProductSpecifications faq)
    {
        int result = 0;

        try
        {
            string query = "Insert Into ProductSpecifications (ProductId,Title,Description,AddedOn,Status,AddedBy,DisplayOrder,Grade) values(@ProductId,@Title,@Description,@AddedOn,@Status,@AddedBy,@DisplayOrder,@Grade)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = faq.ProductId;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = faq.DisplayOrder;
                cmd.Parameters.AddWithValue("@Grade", SqlDbType.NVarChar).Value = faq.Grade;
                cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = faq.Title;
                cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = faq.Description;
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductSpecifications", ex.Message);
        }
        return result;
    }
    /// <summary>
    /// delete Product FAQ
    /// </summary>
    /// <param name="conAP">connection</param>
    /// <param name="cat">parameters</param>
    /// <returns>returne status</returns>
    public static int DeleteProductSpecifications(SqlConnection conAP, ProductSpecifications faq)
    {
        int result = 0;
        try
        {
            string query = "Update ProductSpecifications Set Status='Deleted', AddedBy=@AddedBy,AddedOn=@AddedOn Where Id=@Id";
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductSpecifications", ex.Message);
        }
        return result;
    }


}