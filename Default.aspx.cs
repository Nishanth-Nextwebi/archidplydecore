using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strBlog, strMobileBanner, strClientStories, strProductStories, strFeatureProducts, strBannerImages, strTags, strResources = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BindTop3Blog();
        GetTop3Stories();
        BindClientStories();
        BindFeatureProducts();
        BindBannerImages();
        BindShopCategories();
        GetAllResources();
    }
    public void GetAllResources()
    {
        try
        {
            strResources = "";
            string Resources = "";
            List<Brochures> cas = Brochures.GetAllBrochures(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (Brochures nb in cas)
            {

                Resources += @"<div class='col-xl-3 col-md-4 col-4 animate__fadeInUp animate__animated' data-animate='fadeInUp'>
                        <div class='icon-box icon-box-style-1 card border-0 text-center'>
                            <div class='icon-box-icon card-img text-primary resource-img'>
                                <img src='/" + nb.Image + @"' width='65' alt='"+nb.Title + @"' />
                            </div>
                            <div class='icon-box-content card-body pt-4'>
                                <h3 class='icon-box-title card-title fs-5 mb-0 pb-0'>" + nb.Title + @"</h3>
                                <a href='javascript:void(0)' data-id='" + nb.Id + @"'  class='btn btn-link p-0 mt-2 text-decoration-none green-text fw-semibold hidenId new-font-sm' data-bs-target='#exampleModal' data-bs-toggle='modal'>Download<i class='far fa-download ps-2 fs-13px'></i>
                                </a>
                            </div>
                        </div>
                    </div>";
                i++;
            }
            strResources += @" <section class='pt-lg-10 pb-lg-10 py-10 bg-warning-hover'>
            <div class='container'>
                <div class='text-center section-title' data-animate='fadeInUp'>
                    <h2 class='mb-6'>Resources</h2>
                    <p class='new-style1'>
                        Where design meets Innovation!.
                    </p>
                </div>
            </div>
            <div class='container-xxl mt-10'>
                <div class='row gy-4 justify-content-center text-center'>" + Resources + @"</div>
            </div>
        </section>";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBrochures", ex.Message);
        }
    }
    public void GetTop3Stories()
    {
        try
        {
            strProductStories = "";
            string galImgs = "";
            List<ProductStories> stories = ProductStories.GetTop3Stories(conAP);
            if (stories.Count > 0)
            {
                foreach (ProductStories s in stories)
                {
                    var vlink = "";
                    if (s.Link != "")
                    {
                        vlink += @"<div class='bg-image new-story-img video-01 d-flex justify-content-center align-items-center h-lg-85 position-relative py-18 py-lg-0 py-md-23 lazy-bg' data-bg-src='/" + s.Image + @"' alt='"+s.Title + @"'><a href='" + s.Link + @"' class='view-video iframe-link video-btn d-flex justify-content-center align-items-center fs-30px lh-115px btn btn-outline-light border border-white border-2 rounded-circle transition-all-1'><svg class='icon'><use xlink:href='#icon-play-fill'></use></svg></a></div>";
                    }
                    else
                    {
                        vlink += @"<a class='new-story-img' href='/" + s.Image + @"' data-gallery='gal22' data-thumb-src='/" + s.Image + @"' alt='"+s.Title + @"'><img  src='#' data-src='/" + s.Image + "' class='img-fluid lazy-image h-auto new-story-img'  alt='"+s.Title + @"'></a>";
                    }
                    List<StoriesGallery> StoreGal = StoriesGallery.GetGallery(conAP, Convert.ToString(s.Id));
                    if (StoreGal.Count > 0)
                    {
                        foreach (StoriesGallery sg in StoreGal)
                        {
                            var id = "galleries" + sg.Id;
                            galImgs += @"<a class='new-story-img' href='/" + sg.Images + @"' data-gallery='"+id+@"' data-thumb-src='/" + sg.Images + @"'><img src='/" + sg.Images + @"' data-src='/" + sg.Images + "' class='img-fluid lazy-image h-auto' alt='Story Gallery'></a>";
                        }


                        strProductStories += @"<div class='new-bg-product '>
                        <div class='row align-items-center justify-content-between '>
                            <div class='col-lg-6 px-lg-10 py-lg-0 py-6 productStoryWrap order-lg-0 order-1'>
                                <div class='text-left new-pro-stories'>
                                    <div class='section-title'>
                                        <h2 >""" + s.Title + @"""</h2>
                                    </div>
                                    " + s.FullDesc + @"
                                </div>
                            </div>
                            <div class='col-lg-6 position-relative order-lg-1 order-0'>
                                <div class='about-img'>
                                    <div class='container-fluid'>
                                        <div class='px-md-6'>
<div class=""mx-n6 slick-slider"" data-slick-options='{""slidesToShow"": 1,""infinite"":false,""autoplay"":true,""dots"":true,""arrows"":false,""responsive"":[{""breakpoint"": 1366,""settings"": {""slidesToShow"":1 }},{""breakpoint"": 992,""settings"": {""slidesToShow"":1}},{""breakpoint"": 768,""settings"": {""slidesToShow"": 1}},{""breakpoint"": 576,""settings"": {""slidesToShow"": 1}}]}'>
                                                " + galImgs + vlink + @" alt='"+s.Title+@"'                                           
                                                 </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>";
                    }
                }

            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop3Blog", ex.Message);
        }
    }
    public void BindTop3Blog()
    {
        try
        {
            strBlog = "";
            List<Blogs> blogs = Blogs.GetAllListBlogs(conAP, 1);
            if (blogs.Count > 0)
            {
                foreach (Blogs blog in blogs)
                {
                    strBlog += @"<div class='slick-slide slick-current slick-active' data-slick-index='0' aria-hidden='false' tabindex='0' style='width: 480px;'>
                    <article class='card card-post-grid-3 bg-transparent border-0 animate__fadeInUp animate__animated' data-animate='fadeInUp'>
                        <figure class='card-img-top mb-8 position-relative'>
                            <a href='/blog/" + blog.Url + "' class='hover-shine hover-zoom-in d-block' title='" + blog.Title + @"' tabindex='0'>
                                <img data-src='" + blog.ImageUrl + @"' class='img-fluid w-100 loaded' alt='"+blog.Title + @"' width='450' height='290' src='" + blog.ImageUrl + @"' loading='lazy' alt='"+blog.Title+@"' data-ll-status='loaded'>
                            </a>
                        </figure>
                        <div class='card-body p-0'>
                            <ul class='post-meta list-inline lh-1 d-flex flex-wrap fs-13px text-uppercase ls-1 fw-semibold m-0'>

                                <li class='border-start list-inline-item'>" + (blog.PostedOn).ToString("MMM dd, yyyy") + @"</li>
                            </ul>
                            <h4 class='card-title lh-base mt-5 pt-2 mb-0 fs-20px'>
                                <a class='text-decoration-none' href='/blog/" + blog.Url + "' title='" + blog.Title + @"' tabindex='0'>" + blog.Title + @"</a>
                            </h4>
                        </div>
                    </article>
                </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop3Blog", ex.Message);
        }
    }
    public void BindClientStories()
    {
        try
        {
            strClientStories = "";
            List<ClientStories> Stories = ClientStories.GetAllClientStories(conAP);
            if (Stories.Count > 0)
            {
                foreach (ClientStories story in Stories)
                {
                    strClientStories += @" <div class='mb-6'>
                    <div class='card card-product grid-2 bg-transparent border-0'>
                        <div class='testimonial-card' data-animate='fadeInUp'>
                            <p>" + story.Details + @"</p>
                            <div class='meta-details'>
                                <span>" + story.Name + @"</span>
                                <span>" + story.Designation + @"</span>
                            </div>
                        </div>
                    </div>
                </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop3Blog", ex.Message);
        }
    }
    public void BindShopCategories()
    {
        try
        {
            strTags = "";
            List<SubCategory> tag = SubCategory.GetAllShopCategory(conAP);
            if (tag.Count > 0)
            {
                foreach (SubCategory t in tag)
                {
                    strTags += @" <div>
                        <div class='card border-0 rounded-0 hover-zoom-in hover-shine'>
                            <img class='lazy-image card-img object-fit-cover new-heigh' src='/" + t.ImageUrl + @"'  width='330' height='450' alt='"+t.SubCategoryName + @"' />
                            <div class='card-img-overlay d-inline-flex flex-column p-lg-8 p-4 justify-content-end text-center bg-dark bg-opacity-25'>
                                <h3 class='card-title new-oneline text-white lh-25px lh-lg-45px font-primary fw-normal fs-6 fs-lg-4'>" + t.SubCategoryName + @"
                                </h3>
                                <div>
                                    <a href='/shop/" + t.Url + @"' class='btn btn btn-link new-font p-0 fw-semibold text-white border-bottom border-0 rounded-0 border-currentColor text-decoration-none'>Shop Now</a>
                                </div>
                            </div>
                        </div>
                    </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindShopCategories", ex.Message);
        }
    }
    public void BindFeatureProducts()
    {
        try
        {
            strFeatureProducts = "";
            List<ProductDetails> products = ProductDetails.GetAllFeaturedProducts(conAP);
            if (products.Count > 0)
            {
                foreach (ProductDetails pd in products)
                {
                    strFeatureProducts += @" <div data-animate='fadeInUp'>
                    <div class='card card-product grid-1 bg-transparent border-0'>
                        <figure class='card-img-top position-relative mb-7 overflow-hidden'>
                            <a href='/shop-products/" + pd.ProductUrl + @"'
                               class='hover-zoom-in d-block'
                               title='Perfecting Facial Oil'>
                                <img src='/" + pd.ProductImage + @"' class='img-fluid lazy-image w-100' alt='" + pd.ProductName + @"' width='330' height='440' />
                            </a>
                        </figure>
                        <div class='card-body text-center p-0'>
                            <div class='product-details'>
                               <a href='/shop-products/" + pd.ProductUrl + @"'><h3>" + pd.ProductName + @"</h3></a>
                            </div>
                        </div>
                    </div>
                </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop3Blog", ex.Message);
        }
    }

    public void BindBannerImages()
    {
        try
        {
            string Deskimages = "";
            string Mobimages = "";
            List<BannerImages> banner = BannerImages.GetBannerImage(conAP);
            if (banner.Count > 0)
            {
                foreach (BannerImages ban in banner)
                {
                    //desktop
                    Deskimages += @"<div class='d-lg-block d-block'>
<div class='vh-100 d-flex align-items-center'>
      <div class='z-index-2 container container-xxl py-4 pt-xl-10 pb-xl-11'>
          <div class='hero-content text-start'>" + ban.Description + @"
              <div class='cta-btn d-lg-block d-none' data-animate='fadeInDown'>
                  <a href='/" + ban.Link + @"' class='btn orange-btn '>
                      Explore Now <i class='fa-solid fa-arrow-circle-right'></i>
                  </a>
              </div>
          </div>
      </div>
 
       <div class='w-100 h-100    z-index-1 bg-overlay position-absolute'>
                        <img src='" + ban.DesktopImage + @"' alt='"+ban.BannerTitle + @"' class=""w-100"" />
                    </div>
  </div></div>";
                    //mobile
                    Mobimages += @"<div class='d-lg-none d-none'>
<div class='vh-100 d-flex align-items-center'>
      <div class='z-index-2 container container-xxl py-21 pt-xl-10 pb-xl-11'>
          <div class='hero-content text-start'>" + ban.Description + @"
              <div class='cta-btn' data-animate='fadeInDown'>
                  <a href='/" + ban.Link + @"' class='btn orange-btn'>  
                      Explore Now <i class='fa-solid fa-arrow-circle-right'></i>
                  </a>
              </div>
          </div>
      </div>
       <div class='w-100 h-100   z-index-1 bg-overlay position-absolute'>
                        <img src='" + ban.MobileImage + @"' alt='"+ ban.BannerTitle + @"' class=""w-100 bg-overlay"" />
                    </div>
  </div></div>";


                }
                strBannerImages += @"<section class='home-banner '>
         <div class='slick-slider hero hero-header-02 slick-slider-dots-inside'
              data-slick-options='{&#34;arrows&#34;:true,&#34;autoplay&#34;:true,&#34;autoplaySpeed&#34;:9000,&#34;cssEase&#34;:&#34;ease-in-out&#34;,&#34;dots&#34;:false,&#34;fade&#34;:true,&#34;infinite&#34;:true,&#34;slidesToShow&#34;:1,&#34;speed&#34;:600}'>
           " + Deskimages + @"
         </div>
     </section>";

                strMobileBanner += @"<section class='home-banner d-lg-none d-none'>
         <div class='slick-slider hero hero-header-02 slick-slider-dots-inside'
              data-slick-options='{&#34;arrows&#34;:true,&#34;autoplay&#34;:true,&#34;autoplaySpeed&#34;:9000,&#34;cssEase&#34;:&#34;ease-in-out&#34;,&#34;dots&#34;:false,&#34;fade&#34;:true,&#34;infinite&#34;:true,&#34;slidesToShow&#34;:1,&#34;speed&#34;:600}'>
           " + Mobimages + @"
         </div>
     </section>";

            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop3Blog", ex.Message);
        }
    }


    [WebMethod(EnableSession = true)]
    public static string SaveDownloadEnquiry(string name, string email, string contact, int Id, string prof)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

        try
        {
            ResourceRequests RE = new ResourceRequests();
            List<Brochures> ResName = Brochures.GetBrochures(conAP, Id);
            RE.Name = name;
            RE.ResourceName = ResName[0].Title;
            RE.EmailId = email;
            RE.Profession = prof;
            RE.ContactNo = contact;
            RE.AddedIp = CommonModel.IPAddress();
            RE.AddedOn = TimeStamps.UTCTime();
            RE.Status = "Active";
            int result = ResourceRequests.InserResourceRequests(conAP, RE);

            if (result > 0)
            {

                return "Success | " + ResName[0].PDF;
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

    [WebMethod(EnableSession = true)]
    public static string SaveEnquiry(string name, string phone, string email, string message)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString))
            {
                ContactUs cat = new ContactUs();
                cat.UserName = name;
                cat.ContactNo = phone;
                cat.EmailId = email;
                cat.Message = message;
                int result = ContactUs.InserContactUs(con, cat);
                if (result > 0)
                {
                   Emails.ContactRequest(cat);
                    Emails.ContactUSRequestToCustomer(name, email);
                    return "Success";
                }
                else
                {
                    return "Error";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SaveEnquiry", ex.Message);
            return "Error";
        }
    }
    //protected void btnSendMessage_click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            ContactUs cat = new ContactUs();
    //            cat.UserName = txtName.Text.Trim();
    //            cat.ContactNo = txtPhone.Text.Trim();
    //            cat.EmailId = txtEmail.Text.Trim();
    //            cat.Message = txtMessage.Text.Trim();
    //            int result = ContactUs.InserContactUs(conAP, cat);
    //            if (result > 0)
    //            {

    //                Emails.ContactRequest(cat);
    //                Emails.ContactUSRequestToCustomer(txtName.Text.Trim(), txtEmail.Text.Trim());
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'We’ll get back to you shortly. Thank you for reaching out!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
    //                ScriptManager.RegisterStartupScript(this, GetType(), "CloseModalScript", "$('#quickEnquiryModal').modal('hide');", true);
    //                txtName.Text = "";
    //                txtPhone.Text = "";
    //                txtEmail.Text = "";
    //                txtMessage.Text = "";
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSendMessage_click", ex.Message);
    //    }

    //}
}