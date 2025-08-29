using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static List<ProductListing> BindSearchProducts(string apiKey, string sort, string pno)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);


        List<ProductListing> listings = new List<ProductListing>();
        if (sort == "")
        {
            return listings;
        }
        try
        {
            string key = ConfigurationManager.AppSettings["siteApiKey"] + CommonModel.IPAddress();
            if (apiKey == key)
            {
                ProductListingFilter filter = new ProductListingFilter();
                filter.SortBy = sort;
                filter.PageNo = pno;
                listings = ProductListing.GetAllSearchProductListed(conAP, filter);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindSearchProducts", ex.Message);
        }
        return listings;
    }
}