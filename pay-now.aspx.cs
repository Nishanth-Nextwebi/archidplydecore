using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pay_now : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strDeskMenu, strMobMenu, strUserLoggedIn, strUserLoggedOut, strFooterCat, strFooterbrand = "";

    public string strOrders = "", strDeskNavCategory = "", strMobNavCategories = "", strDelivery = "", strBilling = "", strSubTotal = "", strShipping = "", strDiscount = "", strCoupnDiscount = "", strTax = "", strTotal = "", buyerAmount = "", orderIdd = "", buyerName = "", BuyerMobile = "", buyerEmail = "", paybleAmount = "", strBrands = "", strAdv = "", strBal = "", strAllOrderItems = "", strSubTotalWithDiscount = "", strTotalDiscount = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["arch_i"] == null)
        {
            CreateVisitedUser();
        }
        else
        {

        }

        bindMenu();
        CreateOrder();

    }
    public void CreateVisitedUser()
    {
        if (HttpContext.Current.Request.Cookies["arch_v"] == null)
        {
            HttpCookie cookie = new HttpCookie("arch_v");
            cookie.Value = Guid.NewGuid().ToString();
            cookie.Expires = CommonModel.UTCTime().AddDays(30);
            Response.Cookies.Add(cookie);
        }
    }

    public void bindMenu()
    {
        try
        {
            strDeskMenu = "";
            strMobMenu = "";
            List<Category> categories = Category.GetAllCategory(conAP).Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
            if (categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    strFooterCat += @"<li class='pt-3 mb-4'><a href='/products-categories/" + category.CategoryUrl + @"'>" + category.CategoryName + @"</a></li>";
                    string subCat = "";
                    string subMobilCat = "";
                    List<EnquiryProduct> ep = EnquiryProduct.GetAllEnquiryProductByCategory(conAP, Convert.ToString(category.Id)).ToList();//OrderBy(s => s.DisplayOrder).ToList();
                    if (ep.Count > 0)
                    {
                        foreach (EnquiryProduct e in ep)
                        {
                            subCat += @"<ul class='list-unstyled mb-0'><li><a href='/products/" + e.ProductUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + e.ProductName + @"</span></a></li></ul>";
                            subMobilCat += @"<ul><li><a href='/products/" + e.ProductUrl + @"' class='border-hover text-decoration-none py-3 d-block' ><span class='border-hover-target'>" + e.ProductName + @"</span></a></li></ul>";
                        }
                    }
                    subCat = "<div class='col'><a href='/products-categories/" + category.CategoryUrl + @"'><h6 class='fs-18px'>" + category.CategoryName + @"</h6></a>" + subCat + "</div>";
                    subMobilCat = "<div class='col'><a href='/products-categories/" + category.CategoryUrl + @"'><h6>" + category.CategoryName + @"</h6></a>" + subMobilCat + "</div>";
                    strDeskMenu += subCat;
                    strMobMenu += subMobilCat;
                }

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindMenu()", ex.Message);

        }


    }
    protected void CreateOrder()
    {
        if (Request.QueryString["order"] == null)
        {
            Response.Redirect("/");
        }
        try
        {
            double cost = 0;
            string orderid = Convert.ToString(Request.QueryString["order"]);
            Session["orderid"] = orderid;
            string cart = string.Empty;
            var ord = Reports.GetSingleOrderDetails(conAP, Request.QueryString["order"]).FirstOrDefault();
            if (ord != null)
            {
                strBilling = ord.BillingAddress;
                strDelivery = ord.DeliveryAddress;
                /*  string Company = "";
                  if (!string.IsNullOrEmpty(ord.BillAdd.CompanyName))
                  {
                      Company = "<p><span class='left'>GST Number : </span><span>" + ord.BillAdd.CompanyName + "</span></p>";

                  }
                  string CustomerGstn = "";
                  if (!string.IsNullOrEmpty(ord.BillAdd.CustomerGSTN))
                  {
                      CustomerGstn = "<p><span class='left'>Company Name : </span><span>" + ord.BillAdd.CustomerGSTN + @"</span></p>";

                  }*/
                strDelivery = @"<p><span class=""left"">Name : </span><span>" + ord.DelivAdd.FirstName + @"</span></p>
                                    <p><span class=""left"">Email Id : </span><span>" + ord.DelivAdd.Email + @"</span></p>
                                    <p><span class=""left"">Mobile No : </span><span>" + ord.DelivAdd.Mobile + @"</span></p>
                                    <p><span class=""left"">Address : </span><span>" + ord.DelivAdd.Address1 + @"</span></p>
                                    <p><span class=""left"">City : </span><span>" + ord.DelivAdd.City + @"</span></p>
                                    <p><span class=""left"">State : </span><span>" + ord.DelivAdd.State + @"</span></p><p><span class=""left"">Country : </span><span>" + ord.DelivAdd.Country + @"</span></p>";


                List<OrderItems> allItem = Reports.GetOrderItems(conAP, ord.OrderGuid);
                if (allItem.Count > 0)
                {
                    foreach (OrderItems item in allItem)
                    {
                        string priceBlock = "";
                        decimal ActualPrice = Convert.ToDecimal(item.ActualPrice) * Convert.ToDecimal(item.Quantity);
                        decimal discountPrice = Convert.ToDecimal(item.Price) * Convert.ToDecimal(item.Quantity);
                        decimal off = (((ActualPrice - discountPrice) / ActualPrice) * 100);
                        decimal discount = (ActualPrice - discountPrice);

                        if (ActualPrice == discountPrice)
                        {
                            priceBlock = @"<h5 class=""price""><span class=""theme-color"">" + discountPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</span></h5>
                                             <h6 class=""price"">Quantity: " + item.Quantity + @"</h6>                                                
                                            <h6 class=""price"">" + item.Size + " x " + item.Thickness + @"</h6>
                                                ";
                        }
                        else
                        {
                            priceBlock = @"<h5 class=""price""><span class=""theme-color"">" + discountPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</span> <del>" + ActualPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</del><span class=""OfferApplied"">" + Math.Floor(off) + @"% off</span></h5>
                                                <h6 class=""price"">Quantity: " + item.Quantity + @"</h6>
                                                <h6 class=""price"">" + item.Size + " x " + item.Thickness + @"</h6>
                                                <p class=""amountSaved text-success"">You saved " + discount.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</p>";
                        }

                        strAllOrderItems += @"<div class=""cartBox wow fadeInUp"" data-wow-delay=""0.4s"" style=""visibility: visible; animation-delay: 0.4s; animation-name: fadeInUp;"">
                                    <div class=""row align-items-center"">
                                        <div class=""col-lg-3 col-md-3 col-6"">
                                            <div class=""ImgBox"">
                                                <img src='/" + item.ProductImage + @"' class=""img-fluid"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-9 col-md-9 col-6 "">
                                            <div class=""detailBox"">
                                                <h3>" + item.ProductName + @"</h3>
                                    " + priceBlock + @"            
                                            </div>
                                        </div>
                                    </div>
                                </div>";
                    }
                }

                strAdv = Convert.ToDecimal(ord.AdvAmount).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                strBal = Convert.ToDecimal(ord.BalAmount).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                cost = Convert.ToDouble(ord.AdvAmount);


                if (ord.SubTotal == "0" || ord.SubTotal == "" || ord.SubTotal == null)
                {
                    divsub.Visible = false;
                }
                else
                {
                    divsub.Visible = true;
                    strSubTotal = Convert.ToDecimal(ord.SubTotal).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));

                }
                if (ord.ShippingPrice == "0" || ord.ShippingPrice == "" || ord.ShippingPrice == null)
                {
                    divship.Visible = true;
                    strShipping = "Free";
                }
                else
                { divship.Visible = true; strShipping = Convert.ToDecimal(ord.ShippingPrice).ToString("C", CultureInfo.CreateSpecificCulture("en-IN")); }

                if (ord.Discount == "0" || ord.Discount == "00" || ord.Discount == "" || ord.Discount == null)
                {
                    divDiscount.Visible = false;
                }
                else
                {
                    decimal discountP = ord.Discount == "" ? 0 : Convert.ToDecimal(ord.Discount);
                    divDiscount.Visible = true; strDiscount = "- " + discountP.ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                    strSubTotalWithDiscount = (Convert.ToDecimal(ord.SubTotal) + discountP).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                    strTotalDiscount = discountP.ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                }
                if (ord.AddDiscount == "0" || ord.AddDiscount == "" || ord.AddDiscount == null)
                {
                    divCouponDis.Visible = false;
                }
                else
                {
                    decimal discountP = ord.Discount == "" ? 0 : Convert.ToDecimal(ord.Discount);
                    divCouponDis.Visible = true; strCoupnDiscount = "- " + Convert.ToDecimal(ord.AddDiscount).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                    strTotalDiscount = (discountP + Convert.ToDecimal(ord.AddDiscount)).ToString("C", CultureInfo.CreateSpecificCulture("en-IN"));
                }
                if (ord.TotalPrice == "0" || ord.TotalPrice == "" || ord.TotalPrice == null) { divgrand.Visible = false; }
                else { divgrand.Visible = true; strTotal = Convert.ToDecimal(ord.TotalPrice).ToString("C", CultureInfo.CreateSpecificCulture("en-IN")); }

                buyerEmail = ord.EmailId;
                buyerName = ord.UserName;
                BuyerMobile = ord.Contact;

                paybleAmount = cost.ToString(); ;
                buyerAmount = (cost * 100).ToString();


                Session["payble"] = buyerAmount;

                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", buyerAmount); // this amount should be same as transaction amount
                input.Add("currency", "INR");
                input.Add("receipt", orderid);
                input.Add("payment_capture", 1);
                RazorpayClient client = new RazorpayClient(ConfigurationManager.AppSettings["razorid"], ConfigurationManager.AppSettings["razorsecret"]);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Razorpay.Api.Order order = client.Order.Create(input);
                orderIdd = order["id"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CreateOrder", ex.Message);
        }

    }
}