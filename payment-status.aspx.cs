using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using Razorpay.Api;

public partial class payment_status : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string payStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        #region   4. Get details of this payment order Using TransactionId
        ////  Get details of this payment order Using TransactionId
        try
        {

            string paymentId = Request.Form["razorpay_payment_id"];

            string orderid = Request.Form["orderIdd"].ToString();
            string buyerAmount = Request.Form["buyerAmount"].ToString();
            string key = ConfigurationManager.AppSettings["razorid"];
            string secret = ConfigurationManager.AppSettings["razorsecret"];

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToInt32(Request.Form["buyerAmount"]));
            RazorpayClient client = new RazorpayClient(key, secret);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", paymentId);
            attributes.Add("razorpay_order_id", Request.Form["razorpay_order_id"]);
            attributes.Add("razorpay_signature", Request.Form["razorpay_signature"]);
            Utils.verifyPaymentSignature(attributes);
            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            var sts = payment.Attributes;
            NameValueCollection nvc = Request.Form;

            //Dictionary<string, object> input = new Dictionary<string, object>();
            //input.Add("amount", Convert.ToInt32(buyerAmount)); // this amount should be same as transaction amount


            //RazorpayClient client = new RazorpayClient(ConfigurationManager.AppSettings["razorid"], ConfigurationManager.AppSettings["razorsecret"]);

           

            //Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            //Razorpay.Api.Payment capturedPayment = payment.Capture(input);
            //string sts = capturedPayment["status"];

            //NameValueCollection nvc = Request.Form;

            Orders orders = new Orders();
            if (sts.status == "captured")
            {
                string Oid = UserCheckout.GetOrderId(conAP, orderid);
                string rId = UserCheckout.GetRMax(conAP);
                orders.OrderGuid = orderid;
                orders.PaymentStatus = "Paid";
                orders.OrderStatus = "In-Process";
                orders.PaymentId = paymentId;
                orders.hostedCheckoutId = "";
                orders.RMax = rId;
                orders.ReceiptNo = "APDREC" + GenerateNumber() + rId;
                int x = UserCheckout.UpdateUserOrder(conAP, orders);
                if (x > 0)
                {
                    string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"].Value;
                    CartDetails.RemoveAllItemsFromCart(conAP, uid);
                    UserCheckout.SendToUser(conAP, orderid);
                    Response.Redirect("pay-success.aspx?o=" + orderid);
                }
            }
            else
            {
                string Oid = UserCheckout.GetOrderId(conAP, orderid);
                string rId = UserCheckout.GetRMax(conAP);
                orders.OrderGuid = Request.QueryString["oGuid"];
                orders.PaymentStatus = "Failed";
                orders.OrderStatus = "Failed";
                orders.PaymentId = "";
                orders.hostedCheckoutId = "";
                orders.RMax = "";
                orders.ReceiptNo = "";
                int x = UserCheckout.UpdateUserOrder(conAP, orders);
                Response.Redirect("Pay-Error.aspx");
            }
        }
        catch (ArgumentNullException ex)
        {
            Response.Write(ex.Message);
            payStatus = @"There is some problem now. Please try again later ";
        }
        catch (WebException ex)
        {
            Response.Write(ex.Message);
            payStatus = @"There is some problem now. Please try again later ";
        }
        catch (Exception ex)
        {
            Response.Write("Error:" + ex.Message);
            payStatus = @"There is some problem now. Please try again later ";
        }
        #endregion
    }
    public string GenerateNumber()
    {
        Random random = new Random();
        string r = "";
        int i;
        for (i = 1; i < 6; i++)
        {
            r += random.Next(0, 9).ToString();
        }
        return r;
    }
}