using DocumentFormat.OpenXml.Wordprocessing;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pay_Success : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strOrderId = "";
    public string strDeskMenu="", strMobMenu="", strUserLoggedIn="", strUserLoggedOut="", strFooterCat="", strFooterbrand = "";

    public string strOrders = "", strDeskNavCategory = "", strMobNavCategories = "", strDelivery = "", strBilling = "", strSubTotal = "", strShipping = "", strDiscount = "", strCoupnDiscount = "", strTax = "", strTotal = "", buyerAmount = "", orderIdd = "", buyerName = "", BuyerMobile = "", buyerEmail = "", paybleAmount = "", strBrands = "", strAdv = "", strBal = "", strAllOrderItems = "", strSubTotalWithDiscount = "", strTotalDiscount = "",strInvoiceLink="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["o"]!=null)
        {
            BindOrders();
        }
        else
        {
            Response.Redirect("/");
        }
    }
    protected void BindOrders()
    {
        if (Request.QueryString["o"] == null)
        {
            Response.Redirect("/");
        }
        try
        {
            string orderid = Convert.ToString(Request.QueryString["o"]);
            strInvoiceLink = "/view-invoice.aspx?o="+ orderid;
            string cart = string.Empty;
            var ord = Reports.GetSingleOrderDetails(conAP, Request.QueryString["o"]).FirstOrDefault();
            if (ord != null)
            {
                strOrderId = ord.OrderId;
                strBilling = ord.BillingAddress;
                strDelivery = ord.DeliveryAddress;
                string Company = "";
                if (!string.IsNullOrEmpty(ord.BillAdd.CompanyName))
                {
                    Company = "<p><span class='left'>GST Number : </span><span>" + ord.BillAdd.CompanyName + "</span></p>";

                }
                string CustomerGstn = "";
                if (!string.IsNullOrEmpty(ord.BillAdd.CustomerGSTN))
                {
                    CustomerGstn = "<p><span class='left'>Company Name : </span><span>" + ord.BillAdd.CustomerGSTN + @"</span></p>";

                }

                strBilling = @"<p><span class=""left"">Name : </span><span>" + ord.BillAdd.FirstName + @"</span></p>
                                    <p><span class=""left"">Email Id : </span><span>" + ord.BillAdd.EmailId + @"</span></p>
                                    <p><span class=""left"">Mobile No : </span><span>" + ord.BillAdd.Mobile + @"</span></p>
                                    <p><span class=""left"">Address : </span><span>" + ord.BillAdd.Address1 + @"</span></p>
                                    <p><span class=""left"">City : </span><span>" + ord.BillAdd.City + @"</span></p>
                                    <p><span class=""left"">State : </span><span>" + ord.BillAdd.State + @"</span></p><p><span class=""left"">Country : </span><span>" + ord.BillAdd.Country + @"</span></p>" + CustomerGstn + Company + "";

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
                                                <h6 class=""price"">Quantity:" + item.Quantity + @"</h6>
                                                <h6 class=""price"">" + item.Size + " x " + item.Thickness + @"</h6>
                                                ";
                        }
                        else
                        {
                            priceBlock = @"<h5 class=""price""><span class=""theme-color"">" + discountPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</span> <del>" + ActualPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</del><span></span><span class=""OfferApplied"">" + Math.Floor(off) + @"% off</span></h5>
                                                <h6 class=""price"">Quantity: " + item.Quantity + @"</h6>
                                                <h6 class=""price"">" + item.Size + " x " + item.Thickness + @"</h6>
                                                <p class=""amountSaved text-success"">You saved " + discount.ToString("C", CultureInfo.CreateSpecificCulture("en-IN")) + @"</p>";
                        }

                        strAllOrderItems += @"<div class=""cartBox wow fadeInUp"" data-wow-delay=""0.4s"" style=""visibility: visible; animation-delay: 0.4s; animation-name: fadeInUp;"">
                                    <div class=""row align-items-center"">
                                        <div class=""col-lg-3 col-md-3"">
                                            <div class=""ImgBox"">
                                                <img src='/" + item.ProductImage + @"' class=""img-fluid"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-9 col-md-9 "">
                                            <div class=""detailBox"">
                                                <h3>" + item.ProductName + @"</h3>
                                    " + priceBlock + @"            
                                            </div>
                                        </div>
                                    </div>
                                </div>";
                    }
                }


                buyerEmail = ord.EmailId;
                buyerName = ord.UserName;
                BuyerMobile = ord.Contact;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindOrders()", ex.Message);
        }

    }
}