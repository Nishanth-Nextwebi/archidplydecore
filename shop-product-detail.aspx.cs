using AjaxControlToolkit;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shop_product_detail : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strUrl, strRelatedProducts, strProductId, strFAQs, strReatings, strRatingTotalCount, strAvgRating, strProductName, strShordDesc, strProdDetails, strProdStatus, strProdSpecifications, strActalPrice, strDiscountPrice, strDiscountpercentage, strProductGallery, strProductRatingTop = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strUrl = Convert.ToString(RouteData.Values["ProdUrl"]);
        if (RouteData.Values["ProdUrl"] == null)
        {
            Response.Redirect("/404");
        }
        bindProduct();
        BindReviews();
        BindFAQs();
    }
    public void bindProduct()
    {
        try
        {
            List<ProductDetails> Prod = ProductDetails.GetProductDetailsByUrl(conAP, strUrl).ToList();
            if (Prod.Count > 0)
            {
                if (Prod[0].InStock == "Yes")
                {
                    strProdStatus = @"<button type='submit' class='btn-hover-bg-primary btn-hover-border-primary btn btn-lg btn-dark w-100 btnAddToCartP'>
                                        Add To Bag
                                    </button>";
                    quantityDiv.Visible = true;
                }
                else
                {
                    strProdStatus = @"<a class=""btn-hover-bg-primary btn-hover-border-primary btn btn-lg btn-dark w-100 Enquirybtn d-none"" href=""javascript:void(0)"" data-bs-toggle=""collapse"" data-bs-target=""#flush-collapseThree"" aria-expanded=""false"" aria-controls=""flush-collapseThree"">Quick Enquiry
                                    </a>";
                }
                HiddenStockStatus.Value = Prod[0].InStock;
                strProductName = Prod[0].ProductName;
                hfProductId.Value = Prod[0].productprs[0].ProductId;
                productIdHidden.Value = Convert.ToString(Prod[0].Id);
                strShordDesc = Prod[0].ShortDesc;
                strProdDetails = Prod[0].FullDesc;
                strProdSpecifications = Prod[0].Ingredients;
                strActalPrice = Convert.ToDouble(Prod[0].productprs[0].ActualPrice).ToString("F2");
                strDiscountPrice = Convert.ToDouble(Prod[0].productprs[0].DiscountPrice).ToString("F2");
                decimal actualPrice = Convert.ToDecimal(Prod[0].productprs[0].ActualPrice);
                decimal discountPrice = Convert.ToDecimal(Prod[0].productprs[0].DiscountPrice);
                decimal discountPercentage = ((actualPrice - discountPrice) / actualPrice) * 100;
                strDiscountpercentage = Math.Round(discountPercentage, 2) + "%";
                GetSize(Prod[0].productprs[0].ProductId);
                //GetThickness(Prod[0].productprs[0].ProductId);
                List<ProductGallery> galleries = ProductGallery.GetProductGallery(conAP, Convert.ToString(Prod[0].Id)).ToList();
                if (galleries.Count > 0)
                {
                    var img = "";
                    for (int i = 0; i < galleries.Count; i++)
                    {
                        img += @"<a href='/" + galleries[i].Images + @"' data-gallery='gallery3'>
                        <img src='/" + galleries[i].Images + "' class='lazy-image mb-7 img-fluid h-auto' width='540' height='720' alt=''></a>";
                    }
                    strProductGallery += img;
                }
                if (!string.IsNullOrEmpty(Prod[0].RelatedProducts))
                {
                    string stringrelatedprod = "";
                    List<ProductDetails> relatedProduct = ProductDetails.GetAllRelatedProductWithList(conAP, Prod[0].RelatedProducts);
                    if (relatedProduct.Count > 0)
                    {
                        foreach (var relatedPro in relatedProduct)
                        {
                            stringrelatedprod += @"<div class='mb-6'>
    <div class='card card-product grid-2 bg-transparent border-0'>
        <figure class='card-img-top position-relative mb-0 overflow-hidden'>
            <a href='" + relatedPro.ProductUrl + @"' class='hover-zoom-in d-block' title='" + relatedPro.ProductName + @"'>
                <img src='/" + relatedPro.ProductImage + @"' class='img-fluid lazy-image w-100'  alt='" + relatedPro.ProductName + @"' width='330' height='440'>
            </a>
        </figure>
        <div class='card-body text-center p-0'>
            <h4 class='product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3'><a class='text-decoration-none text-reset' href='" + relatedPro.ProductUrl + @"'>" + relatedPro.ProductName + @"</a></h4>
            <a href='" + relatedPro.ProductUrl + @"' class='btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold'>Read More<i class='far fa-arrow-right ps-2 fs-13px'></i>
            </a>
        </div>
    </div>
</div>";
                        }
                        strRelatedProducts = @"<section style='background-color: #f8f8f8'>
            <div class='container container-xxl pt-15 pb-15 pt-lg-10 pb-lg-20'>
                <div class='text-center'>
                    <h2 class='mb-12'>Related Products</h2>
                </div>
                <div class='slick-slider related-products' data-slick-options='{&#34;arrows&#34;:true,&#34;centerMode&#34;:true,&#34;centerPadding&#34;:&#34;calc((100% - 1440px) / 2)&#34;,&#34;dots&#34;:true,&#34;infinite&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:3}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}},{&#34;breakpoint&#34;:576,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:2}}],&#34;slidesToShow&#34;:4}'>
               " + stringrelatedprod + @"
                    </div>
            </div>
        </section>";
                    }

                }
                else
                {
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
    public void GetSize(string Pid)
    {
        try
        {
            ddlSize.Items.Clear();
            List<ProductPrices> pp = ProductPrices
                .GetProductPriceByPId(conAP, Pid)
                .GroupBy(x => x.ProductSize)
                .Select(g => g.First())
                .ToList();
            if (pp.Count > 0)
            {
                ddlSize.DataSource = pp;
                ddlSize.DataValueField = "ProductSize";
                ddlSize.DataTextField = "ProductSize";
                ddlSize.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "GetSize", ex.Message);
        }
    }
    [WebMethod]
    public static List<ProductPrices> GetProductPrice(string size, string pid)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        List<ProductPrices> productsdetail = null;
        try
        {
            productsdetail = ProductPrices.GetThicknessAndPriceBySize(conAP, size, pid);
        }
        catch (Exception ex)
        {

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetThicknessOptions", ex.Message);

        }
        return productsdetail;
    }

    [WebMethod]
    public static List<ProductPrices> GetPriceBySizeAndThick(string size, string pid, string thic)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        List<ProductPrices> productsdetail = null;
        try
        {
            productsdetail = ProductPrices.GetPriceBySizeAndThick(conAP, size, pid, thic);
        }
        catch (Exception ex)
        {

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetThicknessOptions", ex.Message);

        }
        return productsdetail;
    }
    protected void EnquiryButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                Enquiry cat = new Enquiry();
                cat.UserName = txtFullName.Text.Trim();
                cat.EmailId = txtEmail.Text.Trim();
                cat.City = txtCity.Text.Trim();
                cat.ContactNo = txtPhone.Text.Trim();
                cat.Message = txtMessage.Text.Trim();
                cat.ProductType = "Shop Products";
                cat.Products = strProductName;
                cat.Size = ddlSize.SelectedValue;
                cat.Thickness = Request.Form["ddlThickness"];
                int result = Enquiry.InserEnquiry(conAP, cat);
                if (result > 0)
                {
                    Emails.sendEnquiryToCustomer(cat.UserName, cat.EmailId);
                    Emails.SendEnquiryRequestToAdmin(txtFullName.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), strProductName, txtMessage.Text.Trim(), ddlSize.SelectedValue, Request.Form["ddlThickness"], txtCity.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your enquiry has been submitted successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    txtFullName.Text = txtEmail.Text = txtMessage.Text = txtPhone.Text = "";
                    Response.Redirect("/thank-you");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                }
                bindProduct();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    protected void btnRating_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["arch_i"] != null)
                {
                    ProductReviews rating = new ProductReviews();
                    rating.ProductId = productIdHidden.Value.ToString();
                    rating.UserName = reviewName.Text.Trim();
                    rating.ProductName = strProductName;
                    rating.EmailId = reviewEmail.Text.Trim();
                    rating.MobileNo = reviewPhone.Text.Trim();
                    rating.Subject = reviewSubject.Text.Trim();
                    rating.Message = reviewMessage.Text.Trim();
                    rating.Rating = SelectedRating.Value;
                    rating.AddedOn = TimeStamps.UTCTime();
                    rating.AddedIp = CommonModel.IPAddress();
                    rating.AddedBy = HttpContext.Current.Request.Cookies["arch_i"].Value;
                    rating.ReviewFeatured = "";
                    rating.ImageUrl = "";
                    rating.Status = "Active";
                    int exec = ProductReviews.InsertProductReviews(conAP, rating);
                    if (exec > 0)
                    {
                        btnRating.Focus();
                        Emails.SendReviewReply(reviewName.Text, reviewEmail.Text, strProductName);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Thank You, For Your ratings...!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        reviewName.Text = reviewEmail.Text = reviewSubject.Text = reviewMessage.Text = reviewPhone.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please login to Proceed',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "btnRating_Click", ex.Message);
        }
    }
    public void BindReviews()
    {
        try
        {
            strReatings = "";
            List<ProductReviews> reviews = ProductReviews.GetProductReviews(conAP, productIdHidden.Value.ToString()).ToList();
            if (reviews.Count > 0)
            {
                #region Average Rating
                int TotalCount = 0;
                int TotalRatings = 0;
                TotalCount = Convert.ToInt32(reviews[0].TotalCount);
                TotalRatings = Convert.ToInt32(reviews[0].TotalRatings);
                double averageRating = TotalCount > 0 ? Math.Round((double)TotalRatings / TotalCount, 2) : 0;
                int max = 5;

                int integerPart = (int)averageRating;
                double fractionPercent = (averageRating - integerPart) * 100;
                string Avgrating = "";
                for (double i = 0; i < integerPart; i++)
                {

                    Avgrating += @"<span class=""star"">
                                     <svg class=""icon star"" width=""24"" height=""24"" viewBox=""0 0 24 24"" xmlns=""http://www.w3.org/2000/svg"">
                                       <defs>
                                         <linearGradient id=""half-fill-" + i + @""" x1=""0"" x2=""1"" y1=""0"" y2=""0"">
                                           <stop offset=""100%"" stop-color=""#4E7661"" />
                                           <stop offset=""0%"" stop-color=""lightgray"" />
                                         </linearGradient>
                                       </defs>
                                       <path
                                         fill=""url(#half-fill-" + i + @")""
                                         d=""M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z""
                                       />
                                     </svg>
                                </span>";




                    //Avgrating += "<span class='star'><svg class='icon star text-primary'><use xlink:href='#star'></use></svg></span>";
                }
                if (fractionPercent > 0)
                {
                    Avgrating += @"<span class=""star"">
                                     <svg class=""icon star"" width=""24"" height=""24"" viewBox=""0 0 24 24"" xmlns=""http://www.w3.org/2000/svg"">
                                       <defs>
                                         <linearGradient id=""half-fill-" + (integerPart + 1) + @""" x1=""0"" x2=""1"" y1=""0"" y2=""0"">
                                           <stop offset=""" + fractionPercent + @"%"" stop-color=""#4E7661"" />
                                           <stop offset=""" + (100 - fractionPercent) + @"%"" stop-color=""lightgray"" />
                                         </linearGradient>
                                       </defs>
                                       <path
                                         fill=""url(#half-fill-" + (integerPart + 1) + @")""
                                         d=""M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z""
                                       />
                                     </svg>
                                </span>";
                    integerPart = integerPart + 1;

                }

                for (int j = integerPart; j < max; j++)
                {
                    Avgrating += @"<span class=""star"">
                                     <svg class=""icon star"" width=""24"" height=""24"" viewBox=""0 0 24 24"" xmlns=""http://www.w3.org/2000/svg"">
                                       <defs>
                                         <linearGradient id=""half-g-fill-" + j + @""" x1=""0"" x2=""1"" y1=""0"" y2=""0"">
                                           <stop offset=""100%"" stop-color=""lightgray"" />
                                           <stop offset=""0%"" stop-color=""#4E7661"" />
                                         </linearGradient>
                                       </defs>
                                       <path
                                         fill=""url(#half-g-fill-" + j + @")""
                                         d=""M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z""
                                       />
                                     </svg>
                                </span>";

                }

    
                strAvgRating += Avgrating;
                strRatingTotalCount += averageRating.ToString("0.0");
                #endregion
                foreach (ProductReviews rv in reviews)
                {
                    int Rating = 0;
                    int maxRating = 5;
                    int.TryParse(rv.Rating, out Rating);
                    string rating = "";
                    for (int i = 0; i < Rating; i++)
                    {
                        rating += @"<span class=""star"">
                                     <svg class=""icon star"" width=""24"" height=""24"" viewBox=""0 0 24 24"" xmlns=""http://www.w3.org/2000/svg"">
                                       <defs>
                                         <linearGradient id=""half-fill-" + i + @""" x1=""0"" x2=""1"" y1=""0"" y2=""0"">
                                           <stop offset=""100%"" stop-color=""#4E7661"" />
                                           <stop offset=""0%"" stop-color=""lightgray"" />
                                         </linearGradient>
                                       </defs>
                                       <path
                                         fill=""url(#half-fill-" + i + @")""
                                         d=""M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z""
                                       />
                                     </svg>
                                </span>";
                        //rating += "<span class='star'><svg class='icon star text-primary'><use xlink:href='#star'></use></svg></span>";
                    }
                    for (int i = Rating; i < maxRating; i++)
                    {
                        rating += @"<span class=""star"">
                                     <svg class=""icon star"" width=""24"" height=""24"" viewBox=""0 0 24 24"" xmlns=""http://www.w3.org/2000/svg"">
                                       <defs>
                                         <linearGradient id=""half-g-fill-" + i + @""" x1=""0"" x2=""1"" y1=""0"" y2=""0"">
                                           <stop offset=""100%"" stop-color=""lightgray"" />
                                           <stop offset=""0%"" stop-color=""#4E7661"" />
                                         </linearGradient>
                                       </defs>
                                       <path
                                         fill=""url(#half-g-fill-" + i + @")""
                                         d=""M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z""
                                       />
                                     </svg>
                                </span>";
                        //rating += "<span class='star'><svg class='icon star-o' style='fill:#d3d3d3;'><use xlink:href='#star-o'></use></svg></span>";
                    }
                    #region GetLastSeen
                    var ago = "";
                    var currDate = TimeStamps.UTCTime() - rv.AddedOn;
                    if (currDate.TotalMinutes < 60)
                    {
                        ago = Convert.ToInt32(currDate.TotalMinutes) + " Minutes ago";
                    }
                    else if (currDate.TotalHours < 24)
                    {
                        ago = currDate.TotalHours.ToString("N0") + " Hours ago";
                    }
                    else if (currDate.TotalDays < 30)
                    {
                        ago = currDate.TotalDays.ToString("N0") + " Days ago";
                    }
                    else if (currDate.TotalDays < 365 / 30)
                    {
                        ago = currDate.TotalDays.ToString("N0") + " Months ago";
                    }
                    else if (currDate.TotalDays > 365)
                    {
                        ago = currDate.TotalDays.ToString("N0") + " Years ago";
                    }
                    #endregion
                    var profimg = rv.ImageUrl == "" ? "images_/others/user1.png" : rv.ImageUrl;

                    strReatings += @"<div class='border-bottom pb-7 pt-10'>
                    <div class='d-flex align-items-center mb-6'>
                        <div class='d-flex align-items-center fs-15px ls-0'>
                            <div class='rating'>
                                <div class='empty-stars' style='width: 100%'>" + rating + @"</div>
                                <div class='filled-stars' style='width: 100%'>" + rating + @"</div>
                            </div>
                        </div>
                        <span class='fs-3px mx-5'><i class='fas fa-circle'></i></span>
                        <span class='fs-14'>" + ago + @"</span>
                    </div>

                    <div class='d-flex justify-content-start align-items-center mb-5'>
                        <img src='/" + profimg + @"' class='me-6 lazy-image rounded-circle' width='60' height='60' alt='Avatar'>
                        <div class=''>
                            <h5 class='mt-0 mb-4 fs-14px text-uppercase ls-1'>" + rv.UserName + @"</h5>
                            <p class='mb-0'>India</p>
                        </div>
                    </div>
                    <p class='fw-semibold fs-6 text-body-emphasis mb-5'>" + rv.Subject + @"</p>
                    <p class='mb-10 fs-6'>" + rv.Message + @"</p>
                </div>";
                }
            }
            else
            {
                strReatings += @"<div class='border-bottom no-review pb-7 pt-10'><h3 class=''>No reviews found for this product yet. Be the first to <span><a href='#customer-review' data-bs-toggle='collapse' role='button' aria-expanded='false' aria-controls='customer-review'>write a review!</a></span></h3></div>";
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindReviews", ex.Message);

        }
    }

    public void BindFAQs()
    {
        try
        {
            string faqs = "";
            List<ProductFAQs> FAQS = ProductFAQs.GetAllFAQS(conAP, productIdHidden.Value);
            if (FAQS.Count > 0)
            {
                int index = 1;
                foreach (ProductFAQs ban in FAQS)
                {
                    string questionId = "flush-heading" + index;
                    string answerId = "flush-collapse" + index;

                    faqs += @"<div class='accordion-item pb-4 pt-11'>
					<h2 class='accordion-header' id='" + questionId + @"'>
						<a class='product-info-accordion collapsed text-decoration-none' href='#' data-bs-toggle='collapse' data-bs-target='#" + answerId + @"' aria-expanded='false' aria-controls='" + answerId + @"'>
						<span class='fs-18px'>" + ban.Question + @"</span>
						</a>
					</h2>
				</div>
				<div id='" + answerId + @"' class='accordion-collapse collapse' aria-labelledby='" + questionId + @"' data-bs-parent='#accordionFlushExample2'>
					<div class='py-8'>" + ban.Answer + @"</div>
				</div>";
                    index++;
                }

                strFAQs += @"<section class='pb-16 pb-lg-18' data-animated-id='12'>
          <div class='container'>
<div class='pt-15 pt-lg-18'>
	<div class='row'>
              <div class='text-center'><h2 class='fs-36px mb-5 mb-lg-14'>Frequently Asked Questions</h2></div>
		<div class='col-md-12'>
			<div class='accordion accordion-flush' id='accordionFlushExample2'>
                           " + faqs + @"
			</div>
		</div>
	</div>
</div>
	</div>
              </section>";
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindFAQs", ex.Message);
        }
    }

}