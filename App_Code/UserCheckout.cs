using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Linq;
using DocumentFormat.OpenXml.Office.Word;

public class UserCheckout
{
    #region UserCheckout Methods

    public static string GetOMax(SqlConnection conAP)
    {
        string x = "";
        try
        {
            SqlCommand cmd3 = new SqlCommand("Select Max(try_convert(decimal, OrderMax)) as mid from Orders", conAP);
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                string cc = Convert.ToString(dt3.Rows[0]["mid"]);
                if (cc == "")
                {
                    cc = "0000";
                }
                x = (Convert.ToInt32(cc) + 1).ToString();
                if (x.Length <= 4)
                {
                    x = Convert.ToInt32(x).ToString("0000");
                }
            }
        }
        catch (Exception ex)
        {
            x = "0001";
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOMax", ex.Message);
        }
        return x;
    }
    public static string GetCouponUsed(SqlConnection conAP, string Code)
    {
        string x = "";
        try
        {
            string query = "Select Count(PromoCode) as TotalUsed from Orders where PromoCode=@PromoCode and Status!='Deleted'";
            SqlCommand cmd3 = new SqlCommand(query, conAP);
            cmd3.Parameters.AddWithValue("@PromoCode", SqlDbType.NVarChar).Value = Code;
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                string cc = Convert.ToString(dt3.Rows[0]["TotalUsed"]);
                x = cc;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCouponUsed", ex.Message);
        }
        return x;
    }
    public static string GetRMax(SqlConnection conAP)
    {
        string x = "";
        try
        {
            SqlCommand cmd3 = new SqlCommand("Select Max(try_convert(decimal, RMax)) as mid from Orders", conAP);
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                string cc = Convert.ToString(dt3.Rows[0]["mid"]);
                if (cc == "")
                {
                    cc = "0000";
                }
                x = (Convert.ToInt32(cc) + 1).ToString();
                if (x.Length <= 4)
                {
                    x = Convert.ToInt32(x).ToString("0000");
                }
            }
        }
        catch (Exception ex)
        {
            x = "0001";
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetRMax", ex.Message);
        }
        return x;
    }
    public static List<UserBillingAddress> GetBillingAddress(SqlConnection conAP, string oGuid)
    {
        List<UserBillingAddress> address = new List<UserBillingAddress>();
        try
        {
            string query = "Select * from UserBillingAddress Where OrderGuid = @OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                address = (from DataRow dr in dt.Rows
                           select new UserBillingAddress()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               AddedDateTime = Convert.ToDateTime(Convert.ToString(dr["AddedDateTime"])),
                               Block = Convert.ToString(dr["Block"]),
                               Landmark = Convert.ToString(dr["Landmark"]),
                               Address1 = Convert.ToString(dr["Address1"]),
                               Address2 = Convert.ToString(dr["Address2"]),
                               City = Convert.ToString(dr["City"]),
                               Country = Convert.ToString(dr["Country"]),
                               EmailId = Convert.ToString(dr["EmailId"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               Mobile = Convert.ToString(dr["Mobile"]),
                               OrderGuid = Convert.ToString(dr["OrderGuid"]),
                               State = Convert.ToString(dr["State"]),
                               CustomerGSTN = Convert.ToString(dr["CustomerGSTN"]),
                               CompanyName = Convert.ToString(dr["CompanyName"]),
                               Zip = Convert.ToString(dr["Zip"]),
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBillingAddress", ex.Message);
        }
        return address;
    }
    public static List<UserDeliveryAddress> GetDeliveryAddress(SqlConnection conAP, string oGuid)
    {
        List<UserDeliveryAddress> address = new List<UserDeliveryAddress>();
        try
        {
            string query = " Select * from UserDeliveryAddress Where OrderGuid = @OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                address = (from DataRow dr in dt.Rows
                           select new UserDeliveryAddress()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               AddedDateTime = Convert.ToDateTime(Convert.ToString(dr["AddedDateTime"])),
                               Email = Convert.ToString(dr["Email"]),
                               Block = Convert.ToString(dr["Block"]),
                               Apartment = Convert.ToString(dr["Apartment"]),
                               Address1 = Convert.ToString(dr["Address1"]),
                               Address2 = Convert.ToString(dr["Address2"]),
                               State = Convert.ToString(dr["State"]),
                               City = Convert.ToString(dr["City"]),
                               Country = Convert.ToString(dr["Country"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               Mobile = Convert.ToString(dr["Mobile"]),
                               OrderGuid = Convert.ToString(dr["OrderGuid"]),
                               Zip = Convert.ToString(dr["Zip"]),
                               Landmark = Convert.ToString(dr["Landmark"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDeliveryAddress", ex.Message);
        }
        return address;
    }
    public static int InsertCheckOutItem(SqlConnection conAP, CheckoutItems cItems)
    {
        int x = 0;
        try
        {
            SqlCommand cmdItem = new SqlCommand("Insert into OrderItems (OrderGuid,ProductId,ProductName,TaxGroup,PriceId,Qty,Price,ActualPrice,Size,AddedOn,ProductUrl,ProductImage,ProductGuid,Thickness) values (@OrderGuid,@ProductId,@ProductName,@TaxGroup,@PriceId,@Qty,@Price,@ActualPrice,@Size,@AddedOn,@ProductUrl,@ProductImage,@ProductGuid,@Thickness)", conAP);
            cmdItem.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = cItems.OrderGuid;
            cmdItem.Parameters.AddWithValue("@Thickness", SqlDbType.NVarChar).Value = cItems.Thickness;
            cmdItem.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cItems.ProductId;
            cmdItem.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cItems.ProductName;
            cmdItem.Parameters.AddWithValue("@TaxGroup", SqlDbType.NVarChar).Value = cItems.TaxGroup;
            cmdItem.Parameters.AddWithValue("@PriceId", SqlDbType.NVarChar).Value = cItems.PriceId;
            cmdItem.Parameters.AddWithValue("@Qty", SqlDbType.NVarChar).Value = cItems.Qty;
            cmdItem.Parameters.AddWithValue("@Price", SqlDbType.NVarChar).Value = cItems.Price; cmdItem.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = cItems.ActualPrice;
            cmdItem.Parameters.AddWithValue("@Size", SqlDbType.NVarChar).Value = cItems.Size;
            cmdItem.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cItems.ProductUrl;
            cmdItem.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = cItems.ProductImage;
            cmdItem.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = cItems.ProductGuid;
            cmdItem.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cItems.AddedOn;
            conAP.Open();
            x = cmdItem.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertCheckOutItem", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return x;
    }

    public static int InsertBillingAddress(SqlConnection conAP, UserBillingAddress address)
    {
        int x = 0;
        try
        {
            SqlCommand cmdBuyer = new SqlCommand("Insert into UserBillingAddress (CompanyName,CustomerGSTN,Salutation,EmailId,UserGuid, OrderGuid,FirstName,LastName,Mobile,Address1,Address2,City,Country,Zip,AddedDateTime,AddedIp,State,Landmark,Block) values (@CompanyName,@CustomerGSTN,@Salutation,@EmailId,@UserGuid, @OrderGuid,@FirstName,@LastName,@Mobile,@Address1,@Address2,@City,@Country,@Zip,@AddedDateTime,@AddedIp,@State,@Landmark,@Block)", conAP);
            cmdBuyer.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = address.OrderGuid;
            cmdBuyer.Parameters.AddWithValue("@CustomerGSTN", SqlDbType.NVarChar).Value = address.CustomerGSTN; cmdBuyer.Parameters.AddWithValue("@CompanyName", SqlDbType.NVarChar).Value = address.CompanyName;
            cmdBuyer.Parameters.AddWithValue("@Salutation", SqlDbType.NVarChar).Value = address.Salutation;
            cmdBuyer.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = address.UserGuid;
            cmdBuyer.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = address.FirstName;
            cmdBuyer.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = address.LastName;
            cmdBuyer.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = address.AddedIp;
            cmdBuyer.Parameters.AddWithValue("@Country", SqlDbType.NVarChar).Value = address.Country;
            cmdBuyer.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = address.State;
            cmdBuyer.Parameters.AddWithValue("@Mobile", SqlDbType.NVarChar).Value = address.Mobile;
            cmdBuyer.Parameters.AddWithValue("@Address1", SqlDbType.NVarChar).Value = address.Address1;
            cmdBuyer.Parameters.AddWithValue("@Address2", SqlDbType.NVarChar).Value = address.Address2;
            cmdBuyer.Parameters.AddWithValue("@Block", SqlDbType.NVarChar).Value = address.Block;
            cmdBuyer.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = address.City;
            cmdBuyer.Parameters.AddWithValue("@Zip", SqlDbType.NVarChar).Value = address.Zip;
            cmdBuyer.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = address.EmailId;
            cmdBuyer.Parameters.AddWithValue("@Landmark", SqlDbType.NVarChar).Value = address.Landmark;
            cmdBuyer.Parameters.AddWithValue("@AddedDateTime", SqlDbType.NVarChar).Value = address.AddedDateTime;
            conAP.Open();
            x = cmdBuyer.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertBillingAddress", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return x;
    }

    public static int InsertDeliveryAddress(SqlConnection conAP, UserDeliveryAddress address)
    {
        int x = 0;
        try
        {
            SqlCommand cmdBuyer = new SqlCommand("Insert into UserDeliveryAddress (Salutation,UserGuid, OrderGuid,FirstName,LastName,Mobile,Address1,Address2,City,Country,Zip,AddedDateTime,AddedIp,State,Landmark,Block,Email,Apartment) values (@Salutation,@UserGuid, @OrderGuid,@FirstName,@LastName,@Mobile,@Address1,@Address2,@City,@Country,@Zip,@AddedDateTime,@AddedIp,@State,@Landmark,@Block,@Email,@Apartment)", conAP);
            cmdBuyer.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = address.OrderGuid;
            cmdBuyer.Parameters.AddWithValue("@Salutation", SqlDbType.NVarChar).Value = address.Salutation;
            cmdBuyer.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = address.UserGuid;
            cmdBuyer.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = address.FirstName;
            cmdBuyer.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = address.LastName;
            cmdBuyer.Parameters.AddWithValue("@Mobile", SqlDbType.NVarChar).Value = address.Mobile;
            cmdBuyer.Parameters.AddWithValue("@Address1", SqlDbType.NVarChar).Value = address.Address1;
            cmdBuyer.Parameters.AddWithValue("@Address2", SqlDbType.NVarChar).Value = address.Address2;
            cmdBuyer.Parameters.AddWithValue("@Country", SqlDbType.NVarChar).Value = address.Country;
            cmdBuyer.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = address.City;
            cmdBuyer.Parameters.AddWithValue("@Zip", SqlDbType.NVarChar).Value = address.Zip;
            cmdBuyer.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = address.AddedIp;
            cmdBuyer.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = address.State;
            cmdBuyer.Parameters.AddWithValue("@Landmark", SqlDbType.NVarChar).Value = address.Landmark;
            cmdBuyer.Parameters.AddWithValue("@Apartment", SqlDbType.NVarChar).Value = address.Apartment;
            cmdBuyer.Parameters.AddWithValue("@Block", SqlDbType.NVarChar).Value = address.Block;
            cmdBuyer.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = address.Email;
            cmdBuyer.Parameters.AddWithValue("@AddedDateTime", SqlDbType.NVarChar).Value = address.AddedDateTime;
            conAP.Open();
            x = cmdBuyer.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertDeliveryAddress", ex.Message);
        }
        return x;
    }

    public static int CreateUserOrder(SqlConnection conAP, Orders order)
    {
        int x = 0;
        try
        {
            SqlCommand cmdOrder = new SqlCommand("Insert into Orders (AdvAmount,BalAmount,CODAmount,OrderGuid,OrderId, UserGuid,OrderMax,ReceiptNo,RMax,SubTotal,ShippingPrice,Tax,Discount,TotalPrice,PromoCode,UserType,OrderStatus,PaymentMode,PaymentId,PaymentStatus,OrderOn,LastUpdatedOn,OrderedIp,PromoType,PromoValue,SubTotalWithoutTax,AddDiscount,Status,LastUpdatedIp) values (@AdvAmount,@BalAmount,@CODAmount,@OrderGuid,@OrderId, @UserGuid,@OrderMax,@ReceiptNo,@RMax,@SubTotal,@ShippingPrice,@Tax,@Discount,@TotalPrice,@PromoCode,@UserType,@OrderStatus,@PaymentMode,@PaymentId,@PaymentStatus,@OrderOn,@LastUpdatedOn,@OrderedIp,@PromoType,@PromoValue,@SubTotalWithoutTax,@AddDiscount,@Status,@LastUpdatedIp)", conAP);
            cmdOrder.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = order.OrderGuid; cmdOrder.Parameters.AddWithValue("@AdvAmount", SqlDbType.NVarChar).Value = order.AdvAmount; cmdOrder.Parameters.AddWithValue("@BalAmount", SqlDbType.NVarChar).Value = order.BalAmount;
            cmdOrder.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = order.UserGuid;
            cmdOrder.Parameters.AddWithValue("@CODAmount", SqlDbType.NVarChar).Value = order.CODAmount;
            cmdOrder.Parameters.AddWithValue("@OrderId", SqlDbType.NVarChar).Value = order.OrderId;
            cmdOrder.Parameters.AddWithValue("@OrderMax", SqlDbType.NVarChar).Value = order.OrderMax;
            cmdOrder.Parameters.AddWithValue("@ReceiptNo", SqlDbType.NVarChar).Value = order.ReceiptNo;
            cmdOrder.Parameters.AddWithValue("@RMax", SqlDbType.NVarChar).Value = order.RMax;
            cmdOrder.Parameters.AddWithValue("@PromoType", SqlDbType.NVarChar).Value = order.PromoType;
            cmdOrder.Parameters.AddWithValue("@PromoValue", SqlDbType.NVarChar).Value = order.PromoValue;
            cmdOrder.Parameters.AddWithValue("@SubTotal", SqlDbType.NVarChar).Value = order.SubTotal;
            cmdOrder.Parameters.AddWithValue("@SubTotalWithoutTax", SqlDbType.NVarChar).Value = order.SubTotalWithoutTax;
            cmdOrder.Parameters.AddWithValue("@ShippingPrice", SqlDbType.NVarChar).Value = order.ShippingPrice;
            cmdOrder.Parameters.AddWithValue("@Tax", SqlDbType.NVarChar).Value = order.Tax;
            cmdOrder.Parameters.AddWithValue("@Discount", SqlDbType.NVarChar).Value = order.Discount;
            cmdOrder.Parameters.AddWithValue("@AddDiscount", SqlDbType.NVarChar).Value = order.AddDiscount;
            cmdOrder.Parameters.AddWithValue("@TotalPrice", SqlDbType.NVarChar).Value = order.TotalPrice;
            cmdOrder.Parameters.AddWithValue("@PromoCode", SqlDbType.NVarChar).Value = order.PromoCode;
            cmdOrder.Parameters.AddWithValue("@UserType", SqlDbType.NVarChar).Value = order.UserType;
            cmdOrder.Parameters.AddWithValue("@OrderStatus", SqlDbType.NVarChar).Value = order.OrderStatus;
            cmdOrder.Parameters.AddWithValue("@PaymentMode", SqlDbType.NVarChar).Value = order.PaymentMode;
            cmdOrder.Parameters.AddWithValue("@PaymentId", SqlDbType.NVarChar).Value = order.PaymentId;
            cmdOrder.Parameters.AddWithValue("@PaymentStatus", SqlDbType.NVarChar).Value = order.PaymentStatus;
            cmdOrder.Parameters.AddWithValue("@OrderOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
            cmdOrder.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime(); cmdOrder.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            cmdOrder.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
            cmdOrder.Parameters.AddWithValue("@OrderedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();

            conAP.Open();
            x = cmdOrder.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CreateUserOrder", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return x;
    }
    public static string GetOrderId(SqlConnection conAP, string id)
    {
        string x = "";
        try
        {
            string query = "Select OrderId from Orders where OrderGuid=@id";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                x = dt.Rows[0]["OrderId"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderId", ex.Message);
        }
        return x;
    }
    public static int UpdateUserOrder(SqlConnection conAP, Orders order)
    {
        int x = 0;
        try
        {
            SqlCommand cmdOrder = new SqlCommand("Update Orders Set HostedCheckOutId=@hostedCheckoutId, PaymentStatus=@PaymentStatus,PaymentId=@PaymentId,ReceiptNo=@ReceiptNo,RMax=@RMax, OrderStatus=@OrderStatus Where OrderGuid=@OrderGuid", conAP);
            cmdOrder.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = order.OrderGuid;
            cmdOrder.Parameters.AddWithValue("@PaymentStatus", SqlDbType.NVarChar).Value = order.PaymentStatus;
            cmdOrder.Parameters.AddWithValue("@PaymentId", SqlDbType.NVarChar).Value = order.PaymentId;
            cmdOrder.Parameters.AddWithValue("@hostedCheckoutId", SqlDbType.NVarChar).Value = order.hostedCheckoutId;
            cmdOrder.Parameters.AddWithValue("@ReceiptNo", SqlDbType.NVarChar).Value = order.ReceiptNo;
            cmdOrder.Parameters.AddWithValue("@RMax", SqlDbType.NVarChar).Value = order.RMax;
            cmdOrder.Parameters.AddWithValue("@OrderStatus", SqlDbType.NVarChar).Value = order.OrderStatus;
            conAP.Open();
            x = cmdOrder.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUserOrder", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return x;
    }

    public static void SendToUser(SqlConnection conAP, string oGuid)
    {
        try
        {
            string query = "Select o.*, " +
               " b.FirstName+' '+b.LastName Name1, b.EmailId email1, b.Mobile Mobile1, b.Address1  Address11, b.Address2 Address21, b.City City1, b.Country Country1, b.Zip Zip1, b.State state1,b.Landmark landmark1,b.Block blblock,b.CustomerGSTN,b.CompanyName," +
               " d.FirstName+' '+d.LastName Name2, d.Email email2,d.Mobile Mobile2,d.Address1 as Address12, d.Address2 as Address22, d.City City2, d.Country Country2, d.Zip Zip2,d.State state2,d.Landmark landmark2,d.Block dlblock,d.Apartment" +
               "  from Orders o inner join UserBillingAddress b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress d on d.OrderGuid = o.OrderGuid Where o.OrderGuid=@OrderGuid";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string pType = Convert.ToString(dt.Rows[0]["PaymentMode"]);
                string pTable = ProductDetails(conAP, oGuid);

                string GSTN = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CustomerGSTN"])))
                {
                    GSTN = "</br>" + Convert.ToString(dt.Rows[0]["CustomerGSTN"]);
                }
                string Company = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CompanyName"])))
                {
                    Company = "<br/>" + Convert.ToString(dt.Rows[0]["CompanyName"]);
                }

                string address1 = "" + Convert.ToString(dt.Rows[0]["Name1"]) + "<br>" + Convert.ToString(dt.Rows[0]["blblock"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address11"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address21"]) + "<br>" + Convert.ToString(dt.Rows[0]["City1"]) + "," + Convert.ToString(dt.Rows[0]["State1"]) + "," + Convert.ToString(dt.Rows[0]["Country1"]) + "-" + Convert.ToString(dt.Rows[0]["Zip1"]) + "<br>" + Convert.ToString(dt.Rows[0]["landmark1"]) + "<br>" + Convert.ToString(dt.Rows[0]["Mobile1"]) + "<br>" + Convert.ToString(dt.Rows[0]["email1"]);

                string address2 = "" + Convert.ToString(dt.Rows[0]["Name2"]) + "<br>" + Convert.ToString(dt.Rows[0]["Apartment"]) + "<br>" + Convert.ToString(dt.Rows[0]["dlblock"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address12"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address22"]) + "<br>" + Convert.ToString(dt.Rows[0]["City2"]) + "," + Convert.ToString(dt.Rows[0]["State2"]) + "," + Convert.ToString(dt.Rows[0]["Country2"]) + "-" + Convert.ToString(dt.Rows[0]["Zip2"]) + "<br>" + Convert.ToString(dt.Rows[0]["landmark2"]) + "<br>" + Convert.ToString(dt.Rows[0]["Mobile2"]) + "<br>" + Convert.ToString(dt.Rows[0]["email2"]);

                string Disc = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Discount"])))
                {
                    Disc = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Discount </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>- ₹" + Convert.ToString(dt.Rows[0]["Discount"]) + "</th></tr>";
                }

                string AddDisc = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["AddDiscount"])))
                {
                    AddDisc = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Discount </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>- ₹" + Convert.ToString(dt.Rows[0]["AddDiscount"]) + "</th></tr>";
                }
                string ship = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ShippingPrice"])))
                {
                    ship = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Shipping  </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["ShippingPrice"]) + "</th></tr>";
                }
                string adv = "";
                if (pType == "COD20")
                {
                    adv += @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Advance Paid </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["AdvAmount"]) + "</th></tr>";
                    adv += @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Balance Amount </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["BalAmount"]) + "</th></tr>";
                }

                string discount = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Discount"])))
                {
                    discount += Convert.ToDecimal(Convert.ToString(dt.Rows[0]["Discount"]));
                }

                string pdiscount = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["AddDiscount"])))
                {
                    pdiscount += Convert.ToDecimal(Convert.ToString(dt.Rows[0]["AddDiscount"]));
                }
                decimal adisc = pdiscount == "" ? 0 : Convert.ToDecimal(pdiscount);
                decimal disc = discount == "" ? 0 : Convert.ToDecimal(discount);
                decimal totaldiscountOfProdu = adisc + disc;

                string table = pTable +
                    "<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='border-top: 1px solid #573e40!important;float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Sub Total </th><th align='left' valign='top' style='border-top: 1px solid #573e40!important;float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + ((Convert.ToDecimal(Convert.ToString(dt.Rows[0]["SubTotal"])) + totaldiscountOfProdu) - Convert.ToDecimal(Convert.ToString(dt.Rows[0]["Tax"]))).ToString() + "</th></tr>" +
                    "<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Total Tax </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["Tax"]) + "</th></tr>" +
                   AddDisc +ship + adv + Disc+  
                    "<tr style='padding-bottom:15px;'><td style='border-bottom: 1px solid #573e40!important;'><br /></td></tr>" +
                    "<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'>  Grand Total </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["TotalPrice"]) + "</th></tr>";

                Emails.BookingConfirmed(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["Name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["email2"]), Convert.ToString(dt.Rows[0]["TotalPrice"]), pType, address1, address2);
                Emails.BookingConfirmedAdmin(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["Name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["TotalPrice"]), pType, address1, address2);

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendToUser", ex.Message);
        }
    }

    public static string ProductDetails(SqlConnection conAP, string oGuid)
    {
        string pTable = "";
        try
        {
            string query = "Select * from OrderItems where OrderGuid= @OrderGuid";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    decimal p1 = Convert.ToDecimal(Convert.ToString(dr["Price"]));
                    decimal qty = Convert.ToDecimal(Convert.ToString(dr["Qty"]));
                    decimal price = (p1 * qty);
                    pTable += @"<tr valign='middle'>
                                    <td align='left' valign='middle' style='float:left;width:60%;margin-bottom:10px;margin-top:20px;margin-bottom:25px;line-height:25px;' class='flexibleContainerCell'><span style='font-size:16px;'><b>" + dr["ProductName"] + @"</b></span><br />
                        " + dr["Size"] + @"x" + dr["Thickness"] + @"</td><td align='left' valign='middle' style='float:left;width:20%;margin-bottom:10px;margin-top:20px;text-align:center' class='flexibleContainerCell'>" + dr["Qty"] + @"</td>
                                                                <td align='left' valign='middle' style='float:left;width:20%;margin-bottom:10px;margin-top:20px;text-align:center' class='flexibleContainerCell'>₹" + price + @" </td>
 </tr>";
                }
                pTable = @"<tr style='border-bottom:1px solid #573e40!important;margin-bottom:10px'>
                                                                <th align='left' valign='top' style='border-bottom:1px solid #573e40!important;float:left;width:60%;color:#573e40;margin-bottom:10px;font-size:18px;font-weight:500' class='flexibleContainerCell'>ITEM NAME </th>
                                                                <th align='left' valign='top' style='border-bottom:1px solid #573e40!important;float:left;width:20%;color:#573e40;margin-bottom:10px;font-size:18px;font-weight:500;text-align:center' class='flexibleContainerCell'> QUANTITY </th>
                                                                <th align='left' valign='top' style='border-bottom:1px solid #573e40!important;float:left;width:20%;color:#573e40;margin-bottom:10px;font-size:18px;font-weight:500;text-align:center' class='flexibleContainerCell'> SUB TOTAL </th>
                                                            </tr>" + pTable + "";
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductDetails", ex.Message);
        }
        return pTable;
    }

    #endregion


    #region shipRocket
    public static List<UserBillingAddress> GetFullUserBillingAddressByOrderGuid(SqlConnection conAP, string OrderGuid)
    {
        List<UserBillingAddress> Address = new List<UserBillingAddress>();
        try
        {
            string query = "select * from UserBillingAddress where OrderGuid=@OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = OrderGuid;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Address = (from DataRow dr in dt.Rows
                           select new UserBillingAddress()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               Address1 = Convert.ToString(dr["Address1"]),
                               Address2 = Convert.ToString(dr["Address2"]),
                               Landmark = Convert.ToString(dr["Landmark"]),
                               City = Convert.ToString(dr["City"]),
                               Country = Convert.ToString(dr["Country"]),
                               State = Convert.ToString(dr["State"]),
                               Zip = Convert.ToString(dr["Zip"]),
                               Mobile = Convert.ToString(dr["Mobile"]),
                               EmailId = Convert.ToString(dr["EmailId"]),
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserAddressByOrderGuid", ex.Message);
        }
        return Address;
    }

    public static List<UserDeliveryAddress> GetFullUserDeliveryAddressByOrderGuid(SqlConnection conAP, string OrderGuid)
    {
        List<UserDeliveryAddress> Address = new List<UserDeliveryAddress>();
        try
        {
            string query = "select * from UserDeliveryAddress where OrderGuid=@OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = OrderGuid;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Address = (from DataRow dr in dt.Rows
                           select new UserDeliveryAddress()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               Address1 = Convert.ToString(dr["Address1"]),
                               Address2 = Convert.ToString(dr["Address2"]),
                               Landmark = Convert.ToString(dr["Landmark"]),
                               City = Convert.ToString(dr["City"]),
                               Country = Convert.ToString(dr["Country"]),
                               State = Convert.ToString(dr["State"]),
                               Zip = Convert.ToString(dr["Zip"]),
                               Mobile = Convert.ToString(dr["Mobile"]),
                               Email = Convert.ToString(dr["Email"]),
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserAddressByOrderGuid", ex.Message);
        }
        return Address;
    }
    public static List<CheckoutItems> GetOrderdProductDetailByOrderGuid(SqlConnection conAP, string OrderGuid)
    {
        List<CheckoutItems> pro = new List<CheckoutItems>();
        try
        {
            string query = "select * from OrderItems where OrderGuid=@OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = OrderGuid;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pro = (from DataRow dr in dt.Rows
                       select new CheckoutItems()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           ProductId = Convert.ToString(dr["ProductId"]),
                           ProductName = Convert.ToString(dr["ProductName"]),
                           Price = Convert.ToString(dr["Price"]),
                           Qty = Convert.ToString(dr["Qty"]),
                           Size = Convert.ToString(dr["Size"]),
                           ItemNumber = Convert.ToString(dr["ProductId"]),
                           ProductImage = Convert.ToString(dr["ProductImage"]),
                       }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderdProductDetail", ex.Message);
        }
        return pro;
    }
    public static List<Orders> GetOrderDetailsByOrderGuid(SqlConnection conAP, string OrderGuid)
    {
        List<Orders> pro = new List<Orders>();
        try
        {
            string query = "select * from orders where OrderGuid=@OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = OrderGuid;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pro = (from DataRow dr in dt.Rows
                       select new Orders()
                       {
                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                           OrderId = Convert.ToString(dr["OrderId"]),
                           SubTotal = Convert.ToString(dr["SubTotal"]),
                           TotalPrice = Convert.ToString(dr["TotalPrice"]),
                           Discount = Convert.ToString(dr["Discount"]),
                           OrderOn = Convert.ToDateTime(Convert.ToString(dr["OrderOn"])),
                           Tax = Convert.ToString(dr["Tax"]),
                           TrackingCode = Convert.ToString(dr["TrackingCode"]),
                           PaymentMode = Convert.ToString(dr["PaymentMode"]),
                           AdvAmount = Convert.ToString(dr["AdvAmount"]),
                           BalAmount = Convert.ToString(dr["BalAmount"]),
                       }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderDetailsByOrderGuid", ex.Message);
        }
        return pro;
    }

    public static int AddTrackingCodeToOrder(SqlConnection conAP, string OrderGuid, string TrackingCode)
    {
        int x = 0;
        try
        {
            SqlCommand cmdOrder = new SqlCommand("Update orders set TrackingCode=@TrackingCode where OrderGuid=@OrderGuid ", conAP);
            cmdOrder.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = OrderGuid;
            cmdOrder.Parameters.AddWithValue("@TrackingCode", SqlDbType.NVarChar).Value = TrackingCode;
            conAP.Open();
            x = cmdOrder.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddTrackingCodeToOrder", ex.Message);
        }
        finally
        {
            conAP.Close();
        }
        return x;
    }

    public static string GetTrackingIDByOrderId(SqlConnection conAP, string OrderId)
    {
        string x = "";
        try
        {
            string query = "Select TrackingCode from Orders where OrderId=@OrderId and paymentstatus='paid'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@OrderId", SqlDbType.NVarChar).Value = OrderId;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                x = dt.Rows[0]["TrackingCode"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTrackingIDByOrderId", ex.Message);
        }
        return x;
    }

    public static string GetPaidOrderByOrderId(SqlConnection conAP, string OrderId)
    {
        string x = "";
        try
        {
            string query = "Select OrderId from Orders where OrderId=@OrderId and paymentstatus='paid'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@OrderId", SqlDbType.NVarChar).Value = OrderId;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                x = dt.Rows[0]["OrderId"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetPaidOrderByOrderId", ex.Message);
        }
        return x;
    }
    public static string GetOrderGuid(SqlConnection conAP, string OrderId)
    {
        string x = "";
        try
        {
            string query = "Select OrderGuid from Orders where OrderId=@OrderId";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@OrderId", SqlDbType.NVarChar).Value = OrderId;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                x = dt.Rows[0]["OrderGuid"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderGuid", ex.Message);
        }
        return x;
    }
    #endregion
}

public class CheckoutItems
{
    public int Id { get; set; }
    public string OrderGuid { get; set; }
    public string ProductGuid { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string TaxGroup { get; set; }
    public string PriceId { get; set; }
    public string Qty { get; set; }
    public string Price { get; set; }
    public string ActualPrice { get; set; }
    public string Size { get; set; }
    public string Thickness { get; set; }

    public DateTime AddedOn { get; set; }
    public string Discount { get; set; }
    public string ItemNumber { get; set; }
    public string ProductUrl { get; set; }
    public string ProductImage { get; set; }
}

public class UserBillingAddress
{
    public int Id { get; set; }
    public string UserGuid { get; set; }
    public string Salutation { get; set; }
    public string EmailId { get; set; }
    public string OrderGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Block { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Mobile { get; set; }
    public string CustomerGSTN { get; set; }
    public string CompanyName { get; set; }
    public string Landmark { get; set; }
    public string AddedIp { get; set; }
    public DateTime AddedDateTime { get; set; }
}

public class UserDeliveryAddress
{
    public int Id { get; set; }
    public string UserGuid { get; set; }
    public string Salutation { get; set; }
    public string OrderGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Landmark { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Block { get; set; }
    public string Apartment { get; set; }
    public string Mobile { get; set; }
    public DateTime AddedDateTime { get; set; }
    public string AddedIp { get; set; }
}

public class Orders
{
    public int Id { get; set; }
    public string UserGuid { get; set; }
    public string hostedCheckoutId { get; set; }
    public string OrderGuid { get; set; }
    public string OrderId { get; set; }
    public string OrderMax { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    public string Contact { get; set; }
    public string ReceiptNo { get; set; }
    public string RMax { get; set; }
    public string SubTotal { get; set; }
    public string SubTotalWithoutTax { get; set; }
    public string ShippingPrice { get; set; }
    public string ShippingType { get; set; }
    public string Tax { get; set; }
    public string Discount { get; set; }
    public string CODAmount { get; set; }
    public string AddDiscount { get; set; }
    public string TotalPrice { get; set; }
    public string PromoCode { get; set; }
    public string UserType { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentMode { get; set; }
    public string PaymentId { get; set; }
    public string PaymentStatus { get; set; }
    public string Status { get; set; }
    public DateTime OrderOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public string OrderedIp { get; set; }
    public string LastUpdatedIp { get; set; }
    public string PromoType { get; set; }
    public string PromoValue { get; set; }
    public string BillingAddress { get; set; }
    public string DeliveryAddress { get; set; }
    public string OState { get; set; }
    public string AdvAmount { get; set; }
    public string BalAmount { get; set; }
    public string TrackingCode { get; set; }

    public UserBillingAddress BillAdd { get; set; }
    public UserDeliveryAddress DelivAdd { get; set; }
}

public class B2BCustomerOrders
{
    public int Id { get; set; }
    public string UserGuid { get; set; }
    public string BusinessName { get; set; }
    public string hostedCheckoutId { get; set; }
    public string OrderGuid { get; set; }
    public string OrderId { get; set; }
    public string OrderMax { get; set; }
    public string UserName { get; set; }
    public string GSTNumber { get; set; }
    public string EmailId { get; set; }
    public string Contact { get; set; }
    public string LandLine { get; set; }
    public string ReceiptNo { get; set; }
    public string RMax { get; set; }
    public string SubTotalWithoutTax { get; set; }
    public string SubTotal { get; set; }
    public string Shipping { get; set; }
    public string CODCharges { get; set; }
    public string ShippingType { get; set; }
    public string Tax { get; set; }
    public string Discount { get; set; }
    public string TotalPrice { get; set; }
    public string PromoCode { get; set; }
    public string UserType { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentMode { get; set; }
    public string PaymentId { get; set; }
    public string PaymentStatus { get; set; }
    public string CourierName { get; set; }
    public string TrackingCode { get; set; }
    public string TrackingLink { get; set; }
    public DateTime OrderOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public string OrderedIP { get; set; }
    public string OtherInfo { get; set; }
    public string BillingAddress { get; set; }
    public string AddressLine1 { get; set; }
    public string DeliveryAddress { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }

}

public class B2BTaxReport1
{
    //public int Id { get; set; }
    public string OrderOn { get; set; }
    public string InvoiceNo { get; set; }
    public string OrderNo { get; set; }
    public string BusinessName { get; set; }

    public string GSTNumber { get; set; }
    public string SubTotalWithoutTax { get; set; }
    public string OrderStatus { get; set; }

    public string TaxGroup { get; set; }

    public string IGST { get; set; }
    public string CGST { get; set; }
    public string SGST { get; set; }
    public string TotalPrice { get; set; }
}

public class TaxReport1
{
    //public int Id { get; set; }
    public string OrderOn { get; set; }
    public string InvoiceNo { get; set; }
    public string UserName { get; set; }
    public string GSTNumber { get; set; }
    public string TAXABLESALE { get; set; }
    //public string Tax { get; set; }

    public string TaxGroup { get; set; }
    //public string NTaxGroup { get; set; }
    public string IGST { get; set; }
    public string CGST { get; set; }
    public string SGST { get; set; }
    public string TotalPrice { get; set; }
}

public class TaxReport2
{
    public string HSN { get; set; }
    public string DESCRIPTION { get; set; }
    public string UQC { get; set; }
    public string TOTALQUANTITY { get; set; }
    public string TAXABLEVALUE { get; set; }
    public string TAXRATE { get; set; }
    public string IGST { get; set; }
    public string CGST { get; set; }
    public string SGST { get; set; }
    public string TotalValue { get; set; }
}