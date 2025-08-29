using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_invoice : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strOrderId = "", strOrderDate = "", strInvoiceNo = "", strPaymentStatus = "", strSubTotal = "", strPaymentId = "", strTax = "", strFinalAmount = "";
    public string strName = "", strEmail = "", strContactNo = "", strAddressLine1 = "", strAddressLine2 = "", strAddressLine3 = "", strAddressLine4 = "", strItems = "";
    public string strSunTotalWithoutTax = "", strDiscount = "";
    public string strOffer = "", strShippingCharges = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["o"] != null)
        {
            BindOrders();
        }
    }
    public void BindOrders()
    {
        try
        {
            var oDetails = Reports.GetOrderDetails(conAP, Request.QueryString["o"]);
            if (oDetails.Rows.Count > 0)
            {
                double subTotal = 0, subTotalWithoutTax = 0, Discount = 0, tax = 0, totalAmount = 0,shipCharge=0;
                double.TryParse(Convert.ToString(oDetails.Rows[0]["SubTotalWithoutTax"]), out subTotalWithoutTax);
                double.TryParse(Convert.ToString(oDetails.Rows[0]["SubTotal"]), out subTotal);
                double.TryParse(Convert.ToString(oDetails.Rows[0]["AddDiscount"]), out Discount);
                double.TryParse(Convert.ToString(oDetails.Rows[0]["Tax"]), out tax);
                double.TryParse(Convert.ToString(oDetails.Rows[0]["TotalPrice"]), out totalAmount);
                double.TryParse(Convert.ToString(oDetails.Rows[0]["ShippingPrice"]), out shipCharge);

                strOrderDate = Convert.ToDateTime(Convert.ToString(oDetails.Rows[0]["OrderOn"])).ToString("dd-MMM-yyyy");

                strOrderId = Convert.ToString(oDetails.Rows[0]["OrderId"]);
                strInvoiceNo = Convert.ToString(oDetails.Rows[0]["ReceiptNo"]);
                strPaymentStatus = Convert.ToString(oDetails.Rows[0]["PaymentStatus"]);

                strName = Convert.ToString(oDetails.Rows[0]["name1"]);
                strEmail = Convert.ToString(oDetails.Rows[0]["email1"]);
                strContactNo = Convert.ToString(oDetails.Rows[0]["Mobile1"]);
                if (!string.IsNullOrEmpty(Convert.ToString(oDetails.Rows[0]["PaymentId"])))
                {
                    strPaymentId = Convert.ToString(oDetails.Rows[0]["PaymentId"]);
                    paymentId.Visible = true;
                }

                var oItems = Reports.GetOrderItems(conAP, Request.QueryString["o"]);
                if (oItems.Count > 0)
                {
                    for (int i = 0; i < oItems.Count; i++)
                    {
                        double price = 0, qty = 0, finalAmount = 0;
                        double.TryParse(oItems[i].Price, out price);
                        double.TryParse(oItems[i].Quantity, out qty);
                        finalAmount = (qty * price);

                        strItems += @"<tr><td>" + (i+1) + @"</td>
                                          <td>" + oItems[i].ProductName + @" </br>
                                          <small style=' font-size: 12px;'>" + oItems[i].Thickness +@"  " + oItems[i].Size +@"</small> </br>
                                          </td>
                                          <td>₹" + price.ToString("##,###.00") + @"</td>
                                          <td class='text-center'>" + oItems[i].Quantity + @"</td>
                                          <td class='text-end'>₹" + finalAmount.ToString("##,###.00") + @"</td></tr>";
                    }
                }

                List<UserBillingAddress> billingAddress = UserCheckout.GetBillingAddress(conAP, Request.QueryString["o"]);
                List<UserDeliveryAddress> deliveryAddress = UserCheckout.GetDeliveryAddress(conAP, Request.QueryString["o"]);
                if (billingAddress.Count > 0)
                {
                    strAddressLine1 = billingAddress[0].Address1;
                    strAddressLine2 = billingAddress[0].Address2;
                    strAddressLine3 = billingAddress[0].City == "" ? "" : billingAddress[0].City;
                    if (!string.IsNullOrEmpty(billingAddress[0].City))
                    {
                        strAddressLine3 += billingAddress[0].State == "" ? "" : "," + billingAddress[0].State;
                    }
                    else
                    {
                        strAddressLine3 += billingAddress[0].State == "" ? "" : "" + billingAddress[0].State;
                    }
                    strAddressLine4 = billingAddress[0].Country == "" ? "" : billingAddress[0].Country;
                    strAddressLine4 += billingAddress[0].Zip == "" ? "" : " - " + billingAddress[0].Zip;
                }
                if (Discount > 0)
                {
                    discountWrap.Visible = true;
                }
                strSubTotal = "₹" + subTotal.ToString("##,###.00");
                strSunTotalWithoutTax = "₹" + subTotalWithoutTax.ToString("##,###.00");
                strDiscount = "-₹" + Discount.ToString("##,###.00");
                strTax = "₹" + tax.ToString("##,###.00");
                strFinalAmount = "₹" + totalAmount.ToString("##,###.00");
                strShippingCharges = "₹" + shipCharge.ToString("N2");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindOrders", ex.Message);
        }
    }
}