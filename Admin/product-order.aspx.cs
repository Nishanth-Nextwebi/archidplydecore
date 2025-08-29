using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_product_order : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllCategory();
        }
    }
    public void GetAllCategory()
    {
        try
        {
            ddlCategory.Items.Clear();
            List<SubCategory> cats = SubCategory.GetAllSubCategory(conAP);
            if (cats.Count > 0)
            {
                ddlCategory.DataSource = cats;
                ddlCategory.DataTextField = "SubCategoryName";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ProductOrderUpdate(string product)
    {
        string x = "";

        try
        {
            string[] str = product.Split(',');

            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            if (CreateUser.CheckAccess(conAP, "product-order.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                int exec = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    ProductDetails catG = new ProductDetails();
                    catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                    catG.DisplayOrder = Convert.ToString(i);
                    exec += ProductDetails.UpdateProductOrder(conAP, catG);
                }
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "Permission";
            }


        }
        catch (Exception ex)
        {
            x = "W";

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductOrderUpdate", ex.Message);
        }
        return x;
    }


    [WebMethod(EnableSession = true)]
    public static List<ProductDetails> ProductOrder(string category)
    {
        List<ProductDetails> fpl = new List<ProductDetails>();
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        try
        {
            List<ProductDetails> products = ProductDetails.GetAllProducts(conAP).Where(p => p.SubCategory == category).OrderBy(x => x.DisplayOrder).ToList();
            foreach (var cat in products)
            {
                fpl.Add(new ProductDetails() { ProductImage = cat.ProductImage, Id = cat.Id, ProductName = cat.ProductName });
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductOrder", ex.Message);
        }
        return fpl;
    }
}