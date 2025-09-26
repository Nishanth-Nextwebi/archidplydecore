using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strHeading, strCategoryDesc, strCategoriesDetails, strUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strUrl = Convert.ToString(RouteData.Values["caturl"]);
        if (RouteData.Values["caturl"] == null)
        {
            Response.Redirect("/404");
        }
        if (!IsPostBack)
        {
            bindProductCategory();
        }

    }
    public void bindProductCategory()
    {
        try
        {
            /*string currentUrl = Request.Url.AbsoluteUri;
            string[] segments = currentUrl.Split('/');
            string caturl = segments.Length > 4 ? segments[segments.Length - 1] : string.Empty;
*/

            strCategoriesDetails = "";
            List<Category> categories = Category.GetCategoryByCategoryUrl(conAP, strUrl).ToList();//Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
            if (categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    strHeading = category.CategoryName;
                    strCategoryDesc = category.FullDesc;
                    Page.Title = category.PageTitle;
                    Page.MetaDescription = category.MetaDesc;
                    Page.MetaKeywords = category.MetaKey;
                    List<EnquiryProduct> ep = EnquiryProduct.GetAllEnquiryProductByCategory(conAP, Convert.ToString(category.Id)).ToList();//Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
                    if (ep.Count > 0)
                    {
                        foreach (EnquiryProduct e in ep)
                        {
                            strCategoriesDetails += @"<div class='col-lg-4 col-md-4 col-6  mb-lg-8 mb-0 mb-md-0 p-lg-4 p-md-3 p-1 ' data-animate='fadeInUp'>
                                    <div class='card border-0 mb-10'>
                                        <div class='image-box-4'>
                                            <img class='lazy-image img-fluid lazy-image light-mode-img' src='/" + e.ProductImage + @"' width='960' height='640' alt='"+e.ProductName + @"'>
                                        </div>
                                        <div class='card-body text-body-emphasis pt-9 mt-2'>
                                            <h5 class='card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold'><a class='color-inherit text-decoration-none' href='/products/" + e.ProductUrl + @"'>" + e.ProductName + @"</a></h5>
                                            <a href='/products/" + e.ProductUrl + @"' class='btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold'>Read More<i class='far fa-arrow-right ps-2 fs-13px'></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>";
                        }
                    }
                }

            }
            else
            {
                Response.Redirect("/404");
            }

        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindMenu()", ex.Message);

        }


    }
}