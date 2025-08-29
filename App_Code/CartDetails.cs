using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
public class CartDetails
{
    #region CartDetails Properties
    public string UserId { get; set; }
    public string ProductGuid { get; set; }
    public string Category { get; set; }
    public int ProductId { get; set; }
    public int PriceId { get; set; }
    public string ProductName { get; set; }
    public string ProductThickness { get; set; }
    public int Qty { get; set; }
    public string ProductUrl { get; set; }
    public string Pincode { get; set; }
    public string ProductSize { get; set; }
    public string ActualPrice { get; set; }
    public string DiscountPrice { get; set; }
    public string ProductImage { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string TaxGroup { get; set; }
    public string MinCartPrice { get; set; }
    public string ShippingCharge { get; set; }
    public bool IsUser { get; set; }
    #endregion

    #region CartDetails Methods
    public static string GetCartQunatity(SqlConnection con)
    {
        string qty = "";
        try
        {
            string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            if (uid != "")
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select count(try_convert(decimal,rowno)) cartcnt from (select ROW_NUMBER() OVER(ORDER BY cds.ProductId) rowno from CartDetails cds left outer join ProductDetails pds on pds.Id=cds.ProductId where UserId=@UserId group by cds.ProductId,cds.PriceId) dt", con);
                cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = uid;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    qty = Convert.ToString(dt.Rows[0]["cartcnt"]) == "0" ? "" : Convert.ToString(dt.Rows[0]["cartcnt"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCartDetails", ex.Message);
        }
        return qty;
    }
    public static List<CartDetails> GetAllCartDetails(SqlConnection conAP)
    {
        List<CartDetails> cart = new List<CartDetails>();
        try
        {
            string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            if (uid != "")
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("dbo.GetAllCartDetails", conAP);
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = uid;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CartDetails product = new CartDetails();
                    product.UserId = Convert.ToString(dr["UserId"]);
                    product.Category = Convert.ToString(dr["Category"]);
                    product.ProductId = Convert.ToInt32(Convert.ToString(dr["ProductId"]));
                    product.PriceId = Convert.ToInt32(Convert.ToString(dr["PriceId"]));
                    product.ProductName = Convert.ToString(dr["ProductName"]);
                    product.ProductThickness = Convert.ToString(dr["ProductThickness"]);
                    product.ProductGuid = Convert.ToString(dr["ProductGuid"]);
                    product.TaxGroup = Convert.ToString(dr["TaxGroup"]);
                    product.Qty = Convert.ToInt32(Convert.ToString(dr["Qty"]));
                    product.ProductUrl = Convert.ToString(dr["ProductUrl"]) ;
                    product.ActualPrice = Convert.ToString(dr["ActualPrice"]);
                    product.DiscountPrice = Convert.ToString(dr["Discountprice"]);
                    product.ProductImage = Convert.ToString(dr["GalImage"])==""? Convert.ToString(dr["ProductImage"]): Convert.ToString(dr["GalImage"]);
                    product.Pincode = Convert.ToString(dr["Pincode"]);
                    product.ProductSize = Convert.ToString(dr["ProductSize"]);
                    product.ShippingCharge = Convert.ToString(dr["ShippingCharge"]);
                    product.MinCartPrice = Convert.ToString(dr["MinCartPrice"]);
                    cart.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCartDetails", ex.Message);
        }
        return cart;
    }
    public static List<CartDetails> GetCartDetails(SqlConnection conAP, string pdid, string prid)
    {
        List<CartDetails> cart = new List<CartDetails>();
        try
        {
            string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            if (uid != "")
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("dbo.GetCartDetails", conAP);
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = pdid;
                cmd.Parameters.AddWithValue("@PriceId", SqlDbType.NVarChar).Value = prid;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CartDetails product = new CartDetails();
                    product.UserId = Convert.ToString(dr["UserId"]);
                    product.ProductId = Convert.ToInt32(Convert.ToString(dr["ProductId"]));
                    product.PriceId = Convert.ToInt32(Convert.ToString(dr["PriceId"]));
                    product.ProductName = Convert.ToString(dr["ProductName"]);
                    product.Qty = Convert.ToInt32(Convert.ToString(dr["Qty"]));
                    product.ProductUrl = Convert.ToString(dr["ProductUrl"]);
                    product.ActualPrice = Convert.ToString(dr["ActualPrice"]);
                    product.DiscountPrice = Convert.ToString(dr["DiscountPrice"]);
                    product.ProductImage = Convert.ToString(dr["ProductImage"]);
                    product.Pincode = Convert.ToString(dr["Pincode"]);
                    product.ProductSize = Convert.ToString(dr["ProductSize"]);
                    cart.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCartDetails", ex.Message);
        }
        return cart;
    }
    public static int UpdateCart(SqlConnection conAP, CartInputs cart_)
    {
        int x = 0;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd2 = new SqlCommand("dbo.CheckProduct", conAP);
            cmd2.Parameters.AddWithValue("@PId", SqlDbType.NVarChar).Value = cart_.pdid;
            cmd2.Parameters.AddWithValue("@PrId", SqlDbType.NVarChar).Value = cart_.prid;
            cmd2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("Update CartDetails Set Qty=@Qty,UpdatedOn=@UpdatedOn,UpdatedIp=@UpdatedIp,Pincode=@pncd where UserId=@uid and ProductId=@pdid and PriceId=@prid", conAP);
                cmd.Parameters.AddWithValue("@Qty", SqlDbType.NVarChar).Value = cart_.qty;
                cmd.Parameters.AddWithValue("@prid", SqlDbType.NVarChar).Value = cart_.prid;
                cmd.Parameters.AddWithValue("@pdid", SqlDbType.NVarChar).Value = cart_.pdid;
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = cart_.uguid;
                cmd.Parameters.AddWithValue("@pncd", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCart", ex.Message);
            return -101;
        }
        return x;
    }
    public static int SaveCart(SqlConnection conAP, CartInputs inputs)
    {
        int x = 0;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd2 = new SqlCommand("dbo.CheckProduct", conAP);
            cmd2.Parameters.AddWithValue("@PId", SqlDbType.NVarChar).Value = inputs.pdid;
            cmd2.Parameters.AddWithValue("@PrId", SqlDbType.NVarChar).Value = inputs.prid;
            cmd2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("insert into CartDetails (ProductThickness,UserId, ProductId,PriceId,ProductName,Qty,ProductUrl,ActualPrice,DiscountPrice,ProductImage,AddedOn,AddedIp,IsUser,ProductSize,Pincode,UpdatedOn,UpdatedIp,ProductGuid) values (@ProductThickness,@UserId, @ProductId,@PriceId,@ProductName,@Qty,@ProductUrl,@ActualPrice,@DiscountPrice,@ProductImage,@AddedOn,@AddedIp,@IsUser,@ProductSize,@Pincode,@UpdatedOn,@UpdatedIp,@pgid)", conAP);
                cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = inputs.uguid;
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = inputs.pdid;
                cmd.Parameters.AddWithValue("@PriceId", SqlDbType.NVarChar).Value = inputs.prid;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ProductName"]);
                cmd.Parameters.AddWithValue("@pgid", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ProductGuid"]);
                cmd.Parameters.AddWithValue("@Qty", SqlDbType.NVarChar).Value = inputs.qty;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ProductUrl"]);
                cmd.Parameters.AddWithValue("@ActualPrice", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ActualPrice"]);
                cmd.Parameters.AddWithValue("@DiscountPrice", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["DiscountPrice"]);
                cmd.Parameters.AddWithValue("@ProductImage", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ProductImage"]);
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@IsUser", SqlDbType.Bit).Value = inputs.isUser;
                cmd.Parameters.AddWithValue("@ProductSize", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[0]["ProductSize"]);
                cmd.Parameters.AddWithValue("@ProductThickness", SqlDbType.NVarChar).Value =inputs.thickness==""? Convert.ToString(dt.Rows[0]["ProductThickness"]) : inputs.thickness;
                cmd.Parameters.AddWithValue("@Pincode", SqlDbType.NVarChar).Value = inputs.pncd;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SaveCart", ex.Message);
            return -101;
        }
        return x;
    }
    public static int DeleteCart(SqlConnection conAP, string pid, string prid)
    {
        int x = 0;
        try
        {
            string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
            if (uid != "")
            {
                SqlCommand cmd = new SqlCommand("Delete from CartDetails where UserId=@id and ProductId=@pid and PriceId=@prid", conAP);
                cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = uid;
                cmd.Parameters.AddWithValue("@pid", SqlDbType.NVarChar).Value = pid;
                cmd.Parameters.AddWithValue("@prid", SqlDbType.NVarChar).Value = prid;
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteCart", ex.Message);
        }
        return x;
    }
    public static int UpdateCartAfterLogin(SqlConnection conAP, string uid, string nGuid)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update CartDetails Set UserId=@NUserId,IsUser=@IsUser Where UserId=@UserId", conAP);
            cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = uid;
            cmd.Parameters.AddWithValue("@NUserId", SqlDbType.NVarChar).Value = nGuid;
            cmd.Parameters.AddWithValue("@IsUser", SqlDbType.Bit).Value = true;
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCartAfterLogin", ex.Message);
        }
        return x;
    }
    public static int RemoveAllItemsFromCart(SqlConnection conAP, string uid)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Delete From CartDetails Where UserId=@UserId", conAP);
            cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = uid;
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "RemoveAllItemsFromCart", ex.Message);
        }
        return x;
    }
    #endregion
}

public class CartInputs
{
    public string id { get; set; }
    public string pdid { get; set; }
    public string prid { get; set; }
    public int qty { get; set; }
    public string thickness { get; set; }
    public string uguid { get; set; }
    public string pncd { get; set; }
    public string gft { get; set; }
    public bool isUser { get; set; }
}

public class CheckoutDetails
{
    public string Id { get; set; }
    public string UserGuid { get; set; }
    public DateTime EstDelDate { get; set; }
    public string GreetingCardText { get; set; }
    public string CouponCode { get; set; }
    public DateTime AddedOn { get; set; }
}