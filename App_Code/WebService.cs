using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    #region Cart
    [WebMethod]
    public string GetCartQuantity()
    {
        string ret = "";
        try
        {
                ret = CartDetails.GetCartQunatity(conAP);
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCartQuantity", ex.Message);
        }
        return ret;
    }

    [WebMethod]
    public string AddToCart(string pdid, string price, string size, string Thickness, string pncd, string qty)
    {
        string res = "";
        try
        {
            if (qty == "undefined")
            {
                qty = "1";
            }
            List<ProductPrices> pp = ProductPrices.GetProductPriceIdByDetails(conAP, pdid, price, size, Thickness).ToList();
            if (pp.Count > 0)
            {
                string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
                if (uid != "")
                {
                    CartDetails cds = CartDetails.GetCartDetails(conAP, pdid, Convert.ToString(pp[0].Id)).SingleOrDefault();
                    int x;
                    CartInputs ci = new CartInputs();
                    ci.pdid = pdid;
                    ci.prid = Convert.ToString(pp[0].Id);
                    ci.pncd = pncd == "" ? cds != null ? cds.Pincode : "" : pncd;
                    ci.uguid = uid;
                    ci.thickness = Thickness;
                    if (cds != null)
                    {
                        ci.qty = Convert.ToInt32(qty) + cds.Qty;
                        if (ci.qty > 20)
                        {
                            ci.qty = 20;
                        }

                        x = CartDetails.UpdateCart(conAP, ci);
                    }
                    else
                    {
                        ci.qty = Convert.ToInt32(qty);
                        if (ci.qty > 20)
                        {
                            ci.qty = 20;
                        }
                        ci.isUser = HttpContext.Current.Request.Cookies["arch_i"] == null ? false : true;
                        x = CartDetails.SaveCart(conAP, ci);
                    }
                    if (x > 0)
                    {
                        res = "Success";
                    }
                    else if (x == 0)
                    {
                        res = "Not found";
                    }
                    else
                    {
                        res = "Error";
                    }
                }

            }
            else
            {
                res = "Error";
            }

        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddToCart", ex.Message);
            return "Error";
        }
        return res;
    }

   
    [WebMethod]
    public List<CartDetails> GetUserCart()
    {
        List<CartDetails> lcds = new List<CartDetails>();
        try
        {
                SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
                lcds = CartDetails.GetAllCartDetails(conAP);
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserCart", ex.Message);
        }
        return lcds;
    }

    [WebMethod]
    public List<CartDetails> UpdateCart( string pdid, string prid, string qty)
    {
        List<CartDetails> newCart = new List<CartDetails>();
        try
        {
                string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
                if (uid != "")
                {
                    CartDetails cds = CartDetails.GetCartDetails(conAP, pdid, prid).SingleOrDefault();
                    CartInputs ci = new CartInputs();
                    ci.pdid = pdid;
                    ci.prid = prid;
                    ci.uguid = uid;
                    ci.qty = Convert.ToInt32(qty);
                    if (ci.qty > 20)
                    {
                        ci.qty = 20;
                    }
                    CartDetails.UpdateCart(conAP, ci);
                }
                newCart = CartDetails.GetAllCartDetails(conAP);
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCart", ex.Message);
        }
        return newCart;
    }

    [WebMethod]
    public List<CartDetails> RemoveCart(string pid, string prid)
    {
        List<CartDetails> newCart = new List<CartDetails>();
        try
        {
                CartDetails.DeleteCart(conAP, pid, prid);
                newCart = CartDetails.GetAllCartDetails(conAP);
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "RemoveCart", ex.Message);
        }
        return newCart;
    }

    #endregion

    #region Auto Complete
    [WebMethod(EnableSession = true)]
    public List<AutoCompleteSearch> AutoComplete(string para)
    {
        List<AutoCompleteSearch> slp = AutoCompleteSearch.GetSearchedProduct(conAP, para);
        return slp;
    }
    #endregion

    #region Coupon Code
    [WebMethod(EnableSession = true)]
    public List<CouponCode> GetCouponcode()
    {
        List<CouponCode> newCop = new List<CouponCode>();
        try
        {
            newCop = CouponCode.GetAllCoupon(conAP).Where(s => s.ExpiryDate.Date >= CommonModel.UTCTime()).ToList();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCouponcode", ex.Message);
        }
        return newCop;
    }
    [WebMethod(EnableSession = true)]
    public List<CouponCode> GetCouponById(string code)
    {
        List<CouponCode> newCop = new List<CouponCode>();
        try
        {
            newCop = CouponCode.GetAllCoupon(conAP).Where(s => s.ExpiryDate.Date >= CommonModel.UTCTime() && s.CCode.Trim() == code).ToList();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCouponcode", ex.Message);
        }
        return newCop;
    }
    #endregion

}