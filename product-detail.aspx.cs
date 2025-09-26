using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class product_detail : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProductGallery, strProductName, strProducts, strMetaDescription, strScmeaImages, strRelatedProducts, strProductCategory, strUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strUrl = Convert.ToString(RouteData.Values["subcaturl"]);
        if (RouteData.Values["subcaturl"] == null)
        {
            Response.Redirect("/404");
        }
        if (!IsPostBack)
        {
            bindProduct();
            bindRelatedPrdoucts();
            HiddenField1.Value = strProductName;
        }
    }
    public void bindProduct()
    {
        try
        {
            string currentUrl = Request.Url.AbsoluteUri;
            string[] segments = currentUrl.Split('/');
            var divCatelog = "";
            string url = segments.Length > 4 ? segments[segments.Length - 1] : string.Empty;
            List<EnquiryProduct> products = EnquiryProduct.GetEnquiryProductByUrl(conAP, strUrl).ToList();
            if (products.Count > 0)
            {
                foreach (EnquiryProduct prod in products)
                {
                    strProductName = prod.ProductName;
                    HiddenField1.Value = strProductName;
                    strProductCategory = prod.Category;
                    Page.Title = prod.PageTitle;
                    Page.MetaDescription = prod.MetaDesc;
                    strMetaDescription = prod.MetaDesc;
                    Page.MetaKeywords = prod.MetaKey;
                    string productName = "";
                    if (prod.Category == "2")
                    {
                        divCatelog = @"<div class='col-lg-6 col-xl-5 col-6'>
                    <a href='/images_/others/venner-catelog.pdf'  class='btn w-100 bg-new-color btn-hover-bg-white btn-hover-border-warning hidenId' title='catalogue'>Catalogue <i class='far fa-download ps-2 fs-13px'></i></a>
                        </div>";
                    }
                    else if (prod.Category == "3")
                    {
                        divCatelog = @"<div class='col-lg-6 col-xl-5 col-6'>
<a href='/images_/others/Pre-Lam-Catelog.pdf' class='btn w-100 bg-new-color btn-hover-bg-white btn-hover-border-warning' title='catalogue'>Catalogue <i class='far fa-download ps-2 fs-13px'></i></a>
                        </div>";
                    }
                    else
                    {

                        divCatelog = "";
                    }
                    #region features divided
                    List<ProductFeatures> features = ProductFeatures.GetProductFeatures(conAP, prod.Features);
                    var sksloop = "";
                    var sksloop1 = "";

                    int i = 1;
                    if (features.Count > 0)
                    {
                        foreach (ProductFeatures x in features)
                        {
                            if (i % 2 == 0)
                            {
                                sksloop1 += @"<li>
                                        <img src='/" + x.Image + @"' width='45' class='img-fluid' alt='img'/>
                                        <span class='icon-box-title card-title'>" + x.Title + @"</span>
                                     </li>";
                            }
                            else
                            {
                                sksloop += @"<li>
                                        <img src='/" + x.Image + @"' width='45' class='img-fluid' alt='img'/>
                                        <span class='icon-box-title card-title'>" + x.Title + @"</span>
                                     </li>";
                            }
                            i++;
                        }


                    }
                    #endregion

                    #region Specification divided
                    List<ProductSpecifications> specifications = ProductSpecifications.GetAllFAQS(conAP, Convert.ToString(prod.Id));
                    var specLoop = "";

                    int j = 1;
                    if (specifications.Count > 0)
                    {
                        foreach (ProductSpecifications x in specifications)
                        {
                            if (j % 2 == 0)
                            {
                                specLoop += @"<div class='d-flex align-items-center justify-content-between bg-transparent-white custom-width'>
                                                    <span class='font-bold'>" + x.Title + @"</span>
                                                    <span class='d-block ml-auto text-body-emphasis fw-bold fea'>" + x.Description + @"</span>
                                                </div>";

                            }
                            else
                            {
                                specLoop += @"<div class='d-flex align-items-center justify-content-between mb-0 bg-transparent-blue custom-width'>
                                                    <span>" + x.Title + @"</span>
                                                    <span class='d-block ml-auto text-body-emphasis fw-bold fea'>" + x.Description + @"</span>
                                                </div>";
                            }
                            j++;
                        }


                    }
                    #endregion

                    List<EnquiryProductGallery> galleries = EnquiryProductGallery.GetEnquiryProductGallery(conAP, products[0].ProductGuid);
                    if (galleries.Count > 0)
                    {
                        productName = prod.ProductName;
                        string menuImg = "";
                        string mainImg = "";
                        foreach (EnquiryProductGallery gal in galleries)
                        {
                            menuImg += @"<img src='/" + gal.Images + @"' lass='cursor-pointer lazy-image mx-3 mx-xl-0 px-0 mb-xl-7' width='75' height='100' title='' alt='img'/>";
                            mainImg += @"<a href='/" + gal.Images + @"' data-gallery='product-gallery' data-thumb-src='/" + gal.Images + @"' alt='img'><img src='/" + gal.Images + @"' width='540' height='720' title='' class='h-auto lazy-image' alt='img'/></a>";
                            strScmeaImages += @"'https://archidplydecor.com/" + gal.Images + @"',";
                        }


                        strProductGallery += @"<div class='col-lg-6 col-xl-6 col-md-12 pe-lg-13'>
                    <div class='position-sticky top-0'>
                        <div class='row'>
                            <div class='col-xl-2 pe-xl-0 order-1 order-xl-0 mt-5 mt-xl-0'>
                                <div id='vertical-slider-thumb' class='slick-slider slick-slider-thumb ps-1 ms-n3 me-n4 mx-xl-0' data-slick-options='{&#34;arrows&#34;:false,&#34;asNavFor&#34;:&#34;#vertical-slider-slides&#34;,&#34;dots&#34;:false,&#34;focusOnSelect&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1260,&#34;settings&#34;:{&#34;vertical&#34;:false}}],&#34;slidesToShow&#34;:4,&#34;vertical&#34;:true}'>
                                " + menuImg + @"
                               </div>
                            </div>
                            <div class='col-xl-10 ps-xl-8 pe-xl-0 order-0 order-xl-1'>
                                <div id='vertical-slider-slides' class='slick-slider slick-slider-arrow-inside slick-slider-dots-inside slick-slider-dots-light g-0' data-slick-options='{&#34;arrows&#34;:false,&#34;asNavFor&#34;:&#34;#vertical-slider-thumb&#34;,&#34;dots&#34;:false,&#34;slidesToShow&#34;:1,&#34;vertical&#34;:true}'>
                                    " + mainImg + @"
                                </div>
                            </div>
                        </div>
                    </div>
                </div>";
                    }
                    string image = "";
                    if (prod.Category == "7")
                    {
                        productName = "";
                        switch (prod.ProductName.ToLower())
                        {
                            case "pure platinum":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pureplatinum.PNG' alt='img'/> ";
                                break;
                            case "pure gold":
                                image += @"<img class='w-100' src='/images_/pureply-logos/puregold.PNG' alt='img'/> ";
                                break;
                            case "pure 16 plus":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pure16plus.PNG' alt='img'/> ";
                                break;
                            case "pure pro plus":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pureproplus.PNG' alt='img'/> ";
                                break;
                            case "pure pro":
                                image += @"<img class='w-100' src='/images_/pureply-logos/purepro.PNG' alt='img'/> ";
                                break;
                            case "pure 16":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pure16.PNG' alt='img'/>";
                                break;
                            case "pure classic":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pureclassic.PNG' alt='img'/>";
                                break;
                            case "pure liner":
                                image += @"<img class='w-100' src='/images_/pureply-logos/pureliner.PNG' alt='img'/>";
                                break;
                            case "pure star":
                                image += @"<img class='w-100' src='/images_/pureply-logos/purestar.PNG' alt='img'/ >";
                                break;
                            case "vencluster":
                                image += @"<img class='w-100' src='/images_/pureply-logos/vencluster.jpg' alt='img'/> ";
                                break;
                            case "archtik":
                                image += @"<img class='w-100' src='/images_/pureply-logos/architik.PNG' alt='img'/>";
                                break;
                        }
                    }
                    else if (prod.Category == "1")
                    {
                        productName = "";
                        switch (prod.ProductName.ToLower())
                        {
                            case "bon vivant plywood":
                                image += @"<img class='w-100' src='/images_/gurjan-logos/bonvivant.png' alt='img'/> ";
                                break;
                            case "archidply decor club":
                                image += @"<img class='w-100' src='/images_/gurjan-logos/clubb.png' alt='img'/> ";
                                break;
                            case "archidply decor club plus":
                                image += @"<img class='w-100' src='/images_/gurjan-logos/clubplus.png' alt='img'/> ";
                                break;
                            case "archidply flexible plywood":
                                image += @"<img class='w-100' src='/images_/gurjan-logos/flexiply.png' alt='img'/> ";
                                break;
                        }
                    }
                    else
                    {
                        productName = prod.ProductName;
                    }

                    strProducts += @"<div class='row justify-content-center'>" + strProductGallery + @"<div class='col-xl-4 col-lg-6 col-md-12 pt-md-0 pt-10 '><div class='new-image-pro'>
                    " + image + @"<h1 class='mb-4 pb-2 fs-3 prodName'>" + productName + @"</h1></div>
                    <p class='fs-17px'>" + prod.ShortDesc + @"</p>
<h4 class='text-left mb-8'>Features</h4>
                    <div class='product-features mb-10'>
                        <div class='row'>
                           <div class='col-md-6'>
                                <ul>" + sksloop + @"</ul>
                            </div>
                            <div class='col-md-6'>
                                <ul>" + sksloop1 + @"</ul>
                            </div>
                        </div>
                    </div>
<h4 class='text-left mb-8'>Specifications</h4>

                   <div class='card border border-2  rounded mb-8' style='box-shadow: 0 0 10px 0 rgba(0,0,0,0.1)'>
                        <div class='card-body py-0 px-0'>
                            <div class='product-features'>
                                <div class='row align-items-center'>
                                    <div class='col-md-12'>
                                        <div class='card border-2 mb-0'>
                                            <div class='card-body px-0 pt-0 pb-0'>" + specLoop + @"</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    <div class='row  justify-content-center new-enq-btn'>
                        <div class='col-md-5 col-6'>
                            <a href='javascript:void(0);' class='btn w-100 green-btn btn-hover-bg-primary btn-hover-border-primary' data-bs-toggle='modal' data-bs-target='#contactUsModal' title='Check Out'>Enquiry </a>
                        </div> 
" + divCatelog + @"
                    </div>
                    </div>
                </div>";
                }
            }
            else
            {
                Response.Redirect("/404");
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindProduct()", ex.Message);
        }
    }
    public void bindRelatedPrdoucts()
    {
        try
        {
            strRelatedProducts = "";
            List<EnquiryProduct> products = EnquiryProduct.GetAllEnquiryProductRelatedProduct(conAP, strProductCategory, strUrl).ToList();
            if (products.Count > 0)
            {
                foreach (EnquiryProduct prodd in products)
                {
                    strRelatedProducts += @"<div class='mb-6'>
                        <div class='card card-product grid-2 bg-transparent border-0'>
                            <figure class='card-img-top position-relative mb-0 overflow-hidden'>
                                <a href='/products/" + prodd.ProductUrl + @"' class='hover-zoom-in d-block' title='" + prodd.ProductName + @"'>
                                    <img src='/" + prodd.ProductImage + @"' class='img-fluid lazy-image w-100' alt='" + prodd.ProductName + @"' width='330' height='440'>
                                </a>
                            </figure>
                            <div class='card-body text-center p-0'>
                                <h4 class='product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3'><a class='text-decoration-none text-reset' href='/products/" + prodd.ProductUrl + @"'>" + prodd.ProductName + @"</a></h4>
                                <a href='/products/" + prodd.ProductUrl + @"' class='btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold'>
                                    Read More<i class='far fa-arrow-right ps-2 fs-13px'></i>
                                </a>
                            </div>
                        </div>
                    </div>";

                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindRelatedPrdoucts", ex.Message);

        }
    }

    [WebMethod(EnableSession = true)]
    public static string SendProductEnquiry(string name, string phone, string email, string message, string products,string city)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString))
            {
                Enquiry cat = new Enquiry
                {
                    UserName = name.Trim(),
                    EmailId = email.Trim(),
                    ContactNo = phone.Trim(),
                    Message = message.Trim(),
                    ProductType = "Enquiry Products",
                    Products = products,
                    City = city,
                    Size = "",
                    Thickness = ""
                };

                int result = Enquiry.InserEnquiry(con, cat);
                if (result > 0)
                {
                    Emails.sendEnquiryToCustomer(cat.UserName, cat.EmailId);
                   Emails.SendEnquiryRequestToAdmin(name, phone, email, products, message, "NA", "NA", city);
                    return "Success";
                }
                return "Error";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendEnquiry", ex.Message);
            return "Error";
        }
    }


    [WebMethod(EnableSession = true)]
    public static string SaveDownloadEnquiry(string name, string email, string contact, string prof)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        try
        {
            ResourceRequests RE = new ResourceRequests();
            RE.Name = name;
            RE.ResourceName = "";
            RE.EmailId = email;
            RE.Profession = prof;
            RE.ContactNo = contact;
            RE.AddedIp = CommonModel.IPAddress();
            RE.AddedOn = TimeStamps.UTCTime();
            RE.Status = "Active";
            int result = ResourceRequests.InserResourceRequests(conAP, RE);

            if (result > 0)
            {

                return "Success";
            }
            else
            {
                return "Error";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SaveDownloadEnquiry", ex.Message);
            return "Error";
        }
    }

}