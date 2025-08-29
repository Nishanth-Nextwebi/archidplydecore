using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DashBoard
/// </summary>
public class DashBoard
{
    public DashBoard()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    /// <summary>
    /// Get all  orders from db 
    /// </summary>
    /// <param name="conAP">DB connection</param>
    /// <returns>All list</returns>

    public static decimal NoOfBlogs(SqlConnection conAP)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) cntB from Blogs Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }

    public static decimal ContactUs(SqlConnection conAP)
    {
        decimal x = 0;
        try
        {
            string query = "Select Count(Id) as cntB from ContactUs";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }

    public static decimal GetTotalSales(SqlConnection conAP)
    {
        decimal x = 0;
        try
        {
            string query = "Select Sum(try_convert(decimal, TotalPrice)) as TotalPrice from Orders Where OrderStatus!='Cancelled' and OrderStatus!='Deleted' and OrderStatus!='Initiated'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalPrice"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }
    public static int GetProductCount(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = " Select * from ProductDetails Where Status!= 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductCount", ex.Message);
        }
        return x;
    }
    public static int GetOrderCount(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = " Select * from Orders Where OrderStatus!= 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderCount", ex.Message);
        }
        return x;
    }
    public static int GetCustomerCount(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = " Select * from customers Where Status!='Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetStudentCount", ex.Message);
        }
        return x;
    }
    public static List<Orders> GetLast10Orders(SqlConnection conAP)
    {
        List<Orders> orders = new List<Orders>();
        try
        {
            string query = "Select top 10 o.*, " +
               " b.FirstName+' '+b.LastName name1, b.EmailId email1, b.Mobile Mobile1, b.Address1  Address11, b.Address2 Address21, b.City City1, b.Country Country1, b.Zip Zip1, b.State state1,b.Landmark landmark1," +
               " d.FirstName+' '+d.LastName name2, d.Email email2,d.Mobile Mobile2,d.Address1 as Address12, d.Address2 as Address22, d.City City2, d.Country Country2, d.Zip as Zip2,d.State state2,d.Landmark landmark2,d.Block,d.Apartment" +
               "  from Orders o inner join UserBillingAddress b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress d on d.OrderGuid = o.OrderGuid Where (o.Status != 'Deleted' or o.Status is  null) order by o.id desc";

            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                orders = (from DataRow dr in dt.Rows
                          select new Orders()
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              OrderOn = Convert.ToDateTime(Convert.ToString(dr["OrderOn"])),
                              LastUpdatedOn = Convert.ToString(dr["LastUpdatedOn"]) == "" ? Convert.ToDateTime(Convert.ToString(dr["OrderOn"])) : Convert.ToDateTime(Convert.ToString(dr["LastUpdatedOn"])),
                              OrderedIp = Convert.ToString(dr["OrderedIp"]),
                              OrderGuid = Convert.ToString(dr["OrderGuid"]),
                              OrderId = Convert.ToString(dr["OrderId"]),
                              OrderMax = Convert.ToString(dr["OrderMax"]),
                              OrderStatus = Convert.ToString(dr["OrderStatus"]),
                              PaymentId = Convert.ToString(dr["PaymentId"]),
                              PaymentMode = Convert.ToString(dr["PaymentMode"]),
                              PaymentStatus = Convert.ToString(dr["PaymentStatus"]),
                              PromoCode = Convert.ToString(dr["PromoCode"]),
                              ReceiptNo = Convert.ToString(dr["ReceiptNo"]),
                              RMax = Convert.ToString(dr["RMax"]),
                              ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                              ShippingType = Convert.ToString(dr["ShippingType"]),
                              SubTotal = Convert.ToString(dr["SubTotal"]),
                              Tax = Convert.ToString(dr["Tax"]),
                              Discount = Convert.ToString(dr["Discount"]),
                              TotalPrice = Convert.ToString(dr["TotalPrice"]),
                              AdvAmount = Convert.ToString(dr["AdvAmount"]),
                              BalAmount = Convert.ToString(dr["BalAmount"]),
                              UserGuid = Convert.ToString(dr["UserGuid"]),
                              UserType = Convert.ToString(dr["UserType"]),
                              UserName = Convert.ToString(dr["name1"]),
                              EmailId = Convert.ToString(dr["email1"]),
                              Contact = Convert.ToString(dr["Mobile1"]),
                              BillingAddress = Convert.ToString(dr["Address11"]) + "| " + Convert.ToString(dr["Address21"]) + "| " + Convert.ToString(dr["City1"]) + "| " + Convert.ToString(dr["state1"]) + " - " + Convert.ToString(dr["Zip1"]),
                              DeliveryAddress = Convert.ToString(dr["Name2"]) + "| " + Convert.ToString(dr["Mobile2"]) + "| " + Convert.ToString(dr["email2"]) + "| " + Convert.ToString(dr["Apartment"]) + "| " + Convert.ToString(dr["Block"]) + "| " + Convert.ToString(dr["Address12"]) + "| " + Convert.ToString(dr["Address22"]) + "| " + Convert.ToString(dr["City2"]) + "| " + Convert.ToString(dr["state2"]) + " - " + Convert.ToString(dr["Zip2"]),
                          }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLast10Orders", ex.Message);
        }
        return orders;
    }
    public static int GetCustomerCountbyAguid(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = "Select * from customers Where Status='Active' and ArchitectGuid=@ArchitectGuid";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@ArchitectGuid", SqlDbType.NVarChar).Value = Convert.ToString(HttpContext.Current.Request.Cookies["arc_aaid"].Value);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCustomerCountbyAguid", ex.Message);
        }
        return x;
    }
    public static int GetArchitectCount(SqlConnection conAP)
    {
        int x = 0;
        try
        {
            string query = " Select * from ArchitectDetails Where Status='Published'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetStudentCount", ex.Message);
        }
        return x;
    }
    public static decimal NoOfContacts(SqlConnection conAP)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ContactUs Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }
    public static decimal TodaysOrder(SqlConnection conAP, string tDay)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from Orders Where  PaymentStatus = 'Paid' and Try_Convert(date, AddedOn)=@tDay";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@tDay", SqlDbType.NVarChar).Value = tDay;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfEmailSubscribed", ex.Message);
        }
        return x;
    }
    public static decimal MonthsOrder(SqlConnection conAP, string mnth, string yr)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from Orders Where  PaymentStatus = 'Paid' and (Month(AddedOn) = @mnth and Year(AddedOn) = @yr)";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@mnth", SqlDbType.NVarChar).Value = mnth;
            cmd.Parameters.AddWithValue("@yr", SqlDbType.NVarChar).Value = yr;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfEmailSubscribed", ex.Message);
        }
        return x;
    }
    public static DataTable GetDashboardWidgetValues(SqlConnection conDS)
    {
        var dt = new DataTable();
        try
        {
            string query = "AdminDashboardWidgetsValue";
            using (SqlCommand cmd = new SqlCommand(query, conDS))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDashboardWidgetValues", ex.Message);
        }
        return dt;
    }
    public static DataTable GetDashboardMontlyRevnue(SqlConnection conDS)
    {
        var dt = new DataTable();
        try
        {
            string query = "AdminMonthlyRevenue";
            using (SqlCommand cmd = new SqlCommand(query, conDS))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDashboardMontlyRevnue", ex.Message);
        }
        return dt;
    }


}

