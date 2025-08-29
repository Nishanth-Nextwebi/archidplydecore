using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Presentation;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Reports
{
    public static DataTable GetAllOrdersExcelReportWithFilters(SqlConnection _conn, string sFrom, string sTo, string oStatus, string pStatus, string oParam)
    {
        DataTable result = new DataTable();
        try
        {
            string query = "GetAllOrdersExcelReportWithFilters";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sFrom", SqlDbType.NVarChar).Value = sFrom;
                cmd.Parameters.AddWithValue("@sTo", SqlDbType.NVarChar).Value = sTo;
                cmd.Parameters.AddWithValue("@oStatus", SqlDbType.NVarChar).Value = oStatus;
                cmd.Parameters.AddWithValue("@pStatus", SqlDbType.NVarChar).Value = pStatus;
                cmd.Parameters.AddWithValue("@param", SqlDbType.NVarChar).Value = oParam;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(result);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllOrdersWithFilters", ex.Message);
        }
        return result;
    }
    public static List<Orders> GetSingleOrderDetails(SqlConnection conAP, string oGuid)
    {
        List<Orders> orders = new List<Orders>();
        UserBillingAddress billingAdd = new UserBillingAddress();
        UserDeliveryAddress deliveryAdd = new UserDeliveryAddress();
        try
        {
            string query = "Select o.*, " +
               " b.FirstName+' '+b.LastName name1, b.EmailId email1, b.Mobile mobile1, b.Address1 address11, b.Address2 address12, b.City city1, b.Country country1, b.Zip zip1, b.State state1,b.Landmark landmark1,b.Block blblock,b.CustomerGSTN,b.CompanyName," +
               "d.FirstName+' '+d.LastName name2, d.Email email2,d.Mobile mobile2,d.Address1 address21, d.Address2 address22, d.City city2, d.Country country2, d.Zip zip2,d.State state2,d.Landmark landmark2,d.Block dlblock,d.Apartment" +
               "  from Orders o inner join UserBillingAddress b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress d on d.OrderGuid = o.OrderGuid Where (o.Status != 'Deleted' or o.Status is null) and o.OrderGuid=@OrderGuid order by o.id desc";

            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
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
                              OState = Convert.ToString(dr["state1"]),
                              PaymentId = Convert.ToString(dr["PaymentId"]),
                              PaymentMode = Convert.ToString(dr["PaymentMode"]),
                              PaymentStatus = Convert.ToString(dr["PaymentStatus"]),
                              PromoCode = Convert.ToString(dr["PromoCode"]),
                              ReceiptNo = Convert.ToString(dr["ReceiptNo"]),
                              RMax = Convert.ToString(dr["RMax"]),
                              ShippingPrice = Convert.ToString(dr["ShippingPrice"]),
                              ShippingType = Convert.ToString(dr["ShippingType"]),
                              SubTotal = Convert.ToString(dr["SubTotal"]),
                              SubTotalWithoutTax = Convert.ToString(dr["SubTotalWithoutTax"]),
                              Tax = Convert.ToString(dr["Tax"]),
                              Discount = Convert.ToString(dr["Discount"]),
                              AddDiscount = Convert.ToString(dr["AddDiscount"]),

                              TotalPrice = Convert.ToString(dr["TotalPrice"]),
                              UserGuid = Convert.ToString(dr["UserGuid"]),
                              UserType = Convert.ToString(dr["UserType"]),
                              UserName = Convert.ToString(dr["name1"]),
                              EmailId = Convert.ToString(dr["email1"]),
                              Contact = Convert.ToString(dr["Mobile1"]),
                              CODAmount = Convert.ToString(dr["CODAmount"]),
                              AdvAmount = Convert.ToString(dr["AdvAmount"]),
                              BalAmount = Convert.ToString(dr["BalAmount"]),

                              BillAdd = new UserBillingAddress()
                              {
                                  FirstName = Convert.ToString(dr["name1"]),
                                  EmailId = Convert.ToString(dr["email1"]),
                                  Mobile = Convert.ToString(dr["mobile1"]),
                                  Address1 = Convert.ToString(dr["address11"]),
                                  City = Convert.ToString(dr["city1"]),
                                  State = Convert.ToString(dr["state1"]),
                                  Zip = Convert.ToString(dr["zip1"]),
                                  CustomerGSTN = Convert.ToString(dr["CustomerGSTN"]),
                                  CompanyName = Convert.ToString(dr["CompanyName"]),
                                  Country = Convert.ToString(dr["country1"]),
                              },
                              DelivAdd = new UserDeliveryAddress()
                              {
                                  FirstName = Convert.ToString(dr["name1"]),
                                  Email = Convert.ToString(dr["email1"]),
                                  Mobile = Convert.ToString(dr["mobile1"]),
                                  Address1 = Convert.ToString(dr["address11"]),
                                  City = Convert.ToString(dr["city1"]),
                                  State = Convert.ToString(dr["state1"]),
                                  Zip = Convert.ToString(dr["zip1"]),
                                  Country = Convert.ToString(dr["country1"]),
                              }
                              ,
                              BillingAddress = Convert.ToString(dr["name1"]) + "| " + Convert.ToString(dr["email1"]) + "| " + Convert.ToString(dr["mobile1"]) + "| " + Convert.ToString(dr["blblock"]) + "| " + Convert.ToString(dr["address11"]) + "| " + Convert.ToString(dr["address12"]) + "| " + Convert.ToString(dr["city1"]) + "| " + Convert.ToString(dr["state1"]) + "-" + Convert.ToString(dr["zip1"]),
                              DeliveryAddress = Convert.ToString(dr["name2"]) + "| " + Convert.ToString(dr["email2"]) + "| " + Convert.ToString(dr["mobile2"]) + "| " + Convert.ToString(dr["Apartment"]) + "| " + Convert.ToString(dr["dlblock"]) + "| " + Convert.ToString(dr["address21"]) + "| " + Convert.ToString(dr["address22"]) + "| " + Convert.ToString(dr["city2"]) + "| " + Convert.ToString(dr["state2"]) + "-" + Convert.ToString(dr["zip2"]),
                          }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetaAllOrders", ex.Message);
        }
        return orders;
    }
    public static DataTable GetAllOrdersWithFilters(SqlConnection _conn, int pNo, int pSize, string sFrom, string sTo, string oStatus, string pStatus, string oParam)
    {
        DataTable result = new DataTable();
        try
        {

            string query = "GetAllOrdersWithFilters";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pNo", SqlDbType.NVarChar).Value = pNo;
                cmd.Parameters.AddWithValue("@pSize", SqlDbType.NVarChar).Value = pSize;
                cmd.Parameters.AddWithValue("@sFrom", SqlDbType.NVarChar).Value = sFrom;
                cmd.Parameters.AddWithValue("@sTo", SqlDbType.NVarChar).Value = sTo;
                cmd.Parameters.AddWithValue("@oStatus", SqlDbType.NVarChar).Value = oStatus;
                cmd.Parameters.AddWithValue("@pStatus", SqlDbType.NVarChar).Value = pStatus;
                cmd.Parameters.AddWithValue("@param", SqlDbType.NVarChar).Value = oParam;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(result);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllOrdersWithFilters", ex.Message);
        }
        return result;
    }
    public static DataTable GetAllUserMyOrders(SqlConnection _conn, int pNo, int pSize, string oStatus, string uGuid)
    {
        DataTable result = new DataTable();
        try
        {
            string query = "GetAllUserMyOrders";
            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pNo", SqlDbType.NVarChar).Value = pNo;
                cmd.Parameters.AddWithValue("@pSize", SqlDbType.NVarChar).Value = pSize;
                cmd.Parameters.AddWithValue("@oStatus", SqlDbType.NVarChar).Value = oStatus;
                cmd.Parameters.AddWithValue("@uGuid", SqlDbType.NVarChar).Value = uGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(result);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserMyOrders", ex.Message);
        }
        return result;
    }
    public static int DispatchOrder(SqlConnection _conn, string oGuid, string cName, string trkCode, string cLink, string Status, DateTime addedon, string addedip,string DelDate)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update Orders Set OrderStatus=@OrderStatus, LastUpdatedOn=@LastUpdatedOn, CourierName=@CourierName,DeliveryDate=@DeliveryDate, TrackingCode=@TrackingCode, TrackingLink=@TrackingLink, LastUpdatedIp=@LastUpdatedIp Where OrderGuid=@id and OrderStatus='In-Process'", _conn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@OrderStatus", SqlDbType.NVarChar).Value = Status;
            cmd.Parameters.AddWithValue("@DeliveryDate", SqlDbType.NVarChar).Value = DelDate;
            cmd.Parameters.AddWithValue("@CourierName", SqlDbType.NVarChar).Value = cName;
            cmd.Parameters.AddWithValue("@TrackingCode", SqlDbType.NVarChar).Value = trkCode;
            cmd.Parameters.AddWithValue("@TrackingLink", SqlDbType.NVarChar).Value = cLink;
            cmd.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = addedon;
            cmd.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.DateTime).Value = addedip;

            _conn.Open();
            x = cmd.ExecuteNonQuery();
            _conn.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DispatchOrder", ex.Message);
        }
        return x;
    }

    public static int UpdatePaymentStatus(SqlConnection _conn, string oGuid, string payId, DateTime addedon, string addedip)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update Orders Set PaymentStatus=@PaymentStatus,PaymentId=@PaymentId, LastUpdatedOn=@LastUpdatedOn, LastUpdatedIp=@LastUpdatedIp Where OrderGuid=@id", _conn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@PaymentId", SqlDbType.NVarChar).Value = payId;
            cmd.Parameters.AddWithValue("@PaymentStatus", SqlDbType.NVarChar).Value = "Paid";
            cmd.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = addedon;
            cmd.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.DateTime).Value = addedip;

            _conn.Open();
            x = cmd.ExecuteNonQuery();
            _conn.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateDeliveryDate", ex.Message);
        }
        return x;
    }
    public static int DeliveredOrder(SqlConnection _conn, string oGuid, string Status, DateTime addedon, string addedip)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update Orders Set OrderStatus=@OrderStatus, LastUpdatedOn=@LastUpdatedOn, LastUpdatedIp=@LastUpdatedIp Where OrderGuid=@id and OrderStatus='Dispatched'", _conn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@OrderStatus", SqlDbType.NVarChar).Value = Status;
            cmd.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = addedon;
            cmd.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.DateTime).Value = addedip;

            _conn.Open();
            x = cmd.ExecuteNonQuery();
            _conn.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeliveredOrder", ex.Message);
        }
        return x;
    }
    public static int CancelOrder(SqlConnection conAP, string oGuid, string Status, DateTime addedon, string addedip)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update Orders Set OrderStatus=@OrderStatus, LastUpdatedOn=@LastUpdatedOn, LastUpdatedIp=@LastUpdatedIp Where OrderGuid=@OrderGuid and OrderStatus!='Delivered'", conAP);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@OrderStatus", SqlDbType.NVarChar).Value = Status;
            cmd.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = addedon;
            cmd.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.DateTime).Value = addedip;
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CancelOrder", ex.Message);
        }
        return x;
    }
    public static int DeleteOrder(SqlConnection conAP, string oGuid, string Status, DateTime addedon, string addedip)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update Orders Set Status=@Status, LastUpdatedOn=@LastUpdatedOn, LastUpdatedIp=@LastUpdatedIp Where OrderGuid=@OrderGuid", conAP);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = Status;
            cmd.Parameters.AddWithValue("@LastUpdatedOn", SqlDbType.NVarChar).Value = addedon;
            cmd.Parameters.AddWithValue("@LastUpdatedIp", SqlDbType.DateTime).Value = addedip;
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteOrder", ex.Message);
        }
        return x;
    }
    public static DataTable BindDashboardPaymentStatus(SqlConnection conAP, string fromDate, string toDate)
    {
        DataTable result = new DataTable();
        try
        {
            string query = "BindDashboardOrderStatus";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@fromDate", SqlDbType.NVarChar).Value = fromDate;
                cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDate;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(result);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindDashboardPaymentStatus", ex.Message);
        }
        return result;
    }
    public static MonthlyChart GetMonthlynYearlyValueDefault(SqlConnection conAP)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {
            int[] lStatus = new int[5];
            int totalPaid = 0, totalInitiated = 0, totalInProcess = 0, totalDispatched = 0, totalDelivered = 0, totalCancelled = 0, totalOrder = 0;
            decimal allTotalPaid = 0, allTotalInitiated = 0, allPercent = 0;


            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = toDayDate.AddMonths(-11);
            DateTime newStDate = new DateTime(stDate.Year, stDate.Month, 1);

            var pStatus = BindDashboardPaymentStatus(conAP, "", "");
            if (pStatus.Rows.Count > 0)
            {
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalInitiated"]), out totalInitiated);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalInProcess"]), out totalInProcess);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalDispatched"]), out totalDispatched);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalDelivered"]), out totalDelivered);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalCancelled"]), out totalCancelled);

                lStatus[0] = totalInitiated;
                lStatus[1] = totalInProcess;
                lStatus[2] = totalDispatched;
                lStatus[3] = totalDelivered;
                lStatus[4] = totalCancelled;
            }
            else
            {
                lStatus[0] = 0;
                lStatus[1] = 0;
                lStatus[2] = 0;
                lStatus[3] = 0;
                lStatus[4] = 0;
            }
            chart.PayStatus = lStatus;

            string query = "RevenueChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalAmount"]), out allTotalPaid);
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalInitiatedAmount"]), out allTotalInitiated);
                for (int o = 0; o < dt.Rows.Count; o++)
                {
                    int tOrder = 0;
                    int.TryParse(Convert.ToString(dt.Rows[o]["OCount"]), out tOrder);
                    totalOrder += tOrder;
                }

                int i = 0;
                string[] dmons = new string[12];
                decimal[] sMts = new decimal[12];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {


                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {
                string[] dmons = null;
                decimal[] sMts = null;
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddMonths(1))
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

            if (dt2.Rows.Count > 0)
            {


                int i = 0;
                string[] dmons = new string[12];
                decimal[] sMts = new decimal[12];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt2.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("InitiatedAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.MonthInitiated = sMts;

            }
            else
            {
                string[] dmons = new string[12];
                decimal[] sMts = new decimal[12];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddMonths(1))
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                }
                chart.MonthInitiated = sMts;
            }

            if (allTotalPaid > 0 && allTotalInitiated > 0)
            {
                allPercent = allTotalPaid / ((allTotalInitiated + allTotalPaid) / (decimal)100);
            }
            if (allPercent > 0)
            {
                chart.ConvPercent = allPercent;
            }
            chart.TotalInitiated = allTotalInitiated;
            chart.TotalPaid = allTotalPaid;
            chart.TotalOrder = totalOrder;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlynYearlyValueDefault", ex.Message);
        }
        return chart;
    }
    public static MonthlyChart GetMonthlynYearlyValueStatus(SqlConnection conAP, string fValue)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {
            int[] lStatus = new int[5];
            int totalPaid = 0, totalInitiated = 0, totalInProcess = 0, totalDispatched = 0, totalDelivered = 0, totalCancelled = 0, totalOrder = 0;


            DateTime toDayDate = TimeStamps.UTCTime();
            //starts from 
            string stDate = "";
            string newStDate = "";

            if (fValue == "1W")
            {
                stDate = toDayDate.AddDays(-7).ToString("dd/MMM/yyyy");
                newStDate = toDayDate.ToString("dd/MMM/yyyy");
            }
            else if (fValue == "1M")
            {
                stDate = toDayDate.AddDays(-30).ToString("dd/MMM/yyyy");
                newStDate = toDayDate.ToString("dd/MMM/yyyy");
            }
            else if (fValue == "6M")
            {
                stDate = toDayDate.AddDays(-180).ToString("dd/MMM/yyyy");
                newStDate = toDayDate.ToString("dd/MMM/yyyy");
            }
            else if (fValue == "1Y")
            {
                stDate = toDayDate.AddDays(-365).ToString("dd/MMM/yyyy");
                newStDate = toDayDate.ToString("dd/MMM/yyyy");
            }


            var pStatus = BindDashboardPaymentStatus(conAP, stDate, newStDate);
            if (pStatus.Rows.Count > 0)
            {
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalInitiated"]), out totalInitiated);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalInProcess"]), out totalInProcess);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalDispatched"]), out totalDispatched);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalDelivered"]), out totalDelivered);
                int.TryParse(Convert.ToString(pStatus.Rows[0]["TotalCancelled"]), out totalCancelled);

                lStatus[0] = totalInitiated;
                lStatus[1] = totalInProcess;
                lStatus[2] = totalDispatched;
                lStatus[3] = totalDelivered;
                lStatus[4] = totalCancelled;
            }
            else
            {
                lStatus[0] = 0;
                lStatus[1] = 0;
                lStatus[2] = 0;
                lStatus[3] = 0;
                lStatus[4] = 0;
            }
            chart.PayStatus = lStatus;


        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlynYearlyValueStatus", ex.Message);
        }
        return chart;
    }
    public static MonthlyChart GetMonthlynYearlyValue(SqlConnection conAP, string tps)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {
            int totalOrder = 0;
            int mIndex = tps == "6M" ? 6 : 12;
            decimal allTotalPaid = 0, allTotalInitiated = 0, allPercent = 0;


            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = tps == "6M" ? toDayDate.AddMonths(-5) : toDayDate.AddMonths(-11);
            DateTime newStDate = new DateTime(stDate.Year, stDate.Month, 1);

            string query = "RevenueChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = tps;
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalAmount"]), out allTotalPaid);
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalInitiatedAmount"]), out allTotalInitiated);
                for (int o = 0; o < dt.Rows.Count; o++)
                {
                    int tOrder = 0;
                    int.TryParse(Convert.ToString(dt.Rows[o]["OCount"]), out tOrder);
                    totalOrder += tOrder;
                }

                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {


                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddMonths(1))
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

            if (dt2.Rows.Count > 0)
            {


                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt2.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("InitiatedAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.MonthInitiated = sMts;

            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddMonths(1))
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                }
                chart.MonthInitiated = sMts;
            }

            if (allTotalPaid > 0 && allTotalInitiated > 0)
            {
                allPercent = allTotalPaid / ((allTotalInitiated + allTotalPaid) / (decimal)100);
            }
            if (allPercent > 0)
            {
                chart.ConvPercent = allPercent;
            }
            chart.TotalInitiated = allTotalInitiated;
            chart.TotalPaid = allTotalPaid;
            chart.TotalOrder = totalOrder;


        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlynYearlyValue", ex.Message);
        }
        return chart;
    }
    public static MonthlyChart GetYearlyValue(SqlConnection conAP)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {

            int totalOrder = 0;
            decimal allTotalPaid = 0, allTotalInitiated = 0, allPercent = 0;


            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime newStDate = new DateTime(2022, 1, 1);
            int mIndex = (toDayDate.Year - newStDate.Year) + 1;

            string query = "RevenueChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "All";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = "";

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalAmount"]), out allTotalPaid);
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalInitiatedAmount"]), out allTotalInitiated);
                for (int o = 0; o < dt.Rows.Count; o++)
                {
                    int tOrder = 0;
                    int.TryParse(Convert.ToString(dt.Rows[o]["OCount"]), out tOrder);
                    totalOrder += tOrder;
                }

                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => (v.Field<int>("Year_") == dtts.Year));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddYears(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddYears(1))
                {
                    dmons[i] = dtts.ToString("yyyy");
                    sMts[i] = 0;
                    i++;
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

            if (dt2.Rows.Count > 0)
            {


                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt2.AsEnumerable().Where(v => (v.Field<int>("Year_") == dtts.Year));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("InitiatedAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddYears(1);
                }

                chart.MonthInitiated = sMts;

            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts.AddYears(1))
                {
                    dmons[i] = dtts.ToString("yyyy");
                    sMts[i] = 0;
                    i++;
                }
                chart.MonthInitiated = sMts;
            }

            if (allTotalPaid > 0 && allTotalInitiated > 0)
            {
                allPercent = allTotalPaid / ((allTotalInitiated + allTotalPaid) / (decimal)100);
            }
            if (allPercent > 0)
            {
                chart.ConvPercent = allPercent;
            }
            chart.TotalInitiated = allTotalInitiated;
            chart.TotalPaid = allTotalPaid;
            chart.TotalOrder = totalOrder;



        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetYearlyValue", ex.Message);
        }
        return chart;
    }
    public static MonthlyChart GetMonthlyValue(SqlConnection conAP, string tps)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {
            int totalOrder = 0;
            decimal allTotalPaid = 0, allTotalInitiated = 0, allPercent = 0;
            DateTime toDayDate = TimeStamps.UTCTime();
            //DateTime stDate = toDayDate.AddMonths(-1);
            DateTime newStDate = tps == "1W" ? toDayDate.AddDays(-7) : toDayDate.AddMonths(-1);


            string query = "RevenueChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "1M";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = ds.Tables[0];
            dt2 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalAmount"]), out allTotalPaid);
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalInitiatedAmount"]), out allTotalInitiated);
                for (int o = 0; o < dt.Rows.Count; o++)
                {
                    int tOrder = 0;
                    int.TryParse(Convert.ToString(dt.Rows[o]["OCount"]), out tOrder);
                    totalOrder += tOrder;
                }

                int i = 0, dateDiff = 0;

                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));

                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Day_") == dtts.Day) && (v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal ldCnt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = ldCnt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddYears(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {


                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];

                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

            if (dt2.Rows.Count > 0)
            {
                int i = 0, dateDiff = 0;

                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));

                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt2.AsEnumerable().Where(v => ((v.Field<int>("Day_") == dtts.Day) && (v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { InitiatedAmount = x.Field<decimal>("InitiatedAmount") }).FirstOrDefault();

                        decimal ldCnt = sl != null ? sl.InitiatedAmount : 0;
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = ldCnt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.MonthInitiated = sMts;


            }
            else
            {
                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];

                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.MonthInitiated = sMts;
            }

            if (allTotalPaid > 0 && allTotalInitiated > 0)
            {
                allPercent = allTotalPaid / ((allTotalInitiated + allTotalPaid) / (decimal)100);
            }
            if (allPercent > 0)
            {
                chart.ConvPercent = allPercent;
            }
            chart.TotalInitiated = allTotalInitiated;
            chart.TotalPaid = allTotalPaid;
            chart.TotalOrder = totalOrder;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlyValue", ex.Message);
        }
        return chart;
    }
    public static MonthlyChartUser GetMonthlynYearlyValueDefaultUser(SqlConnection conAP, string uGuid)
    {
        MonthlyChartUser chart = new MonthlyChartUser();
        try
        {


            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = toDayDate.AddMonths(-11);
            DateTime newStDate = new DateTime(stDate.Year, stDate.Month, 1);


            string queryState = "UserStateWiseSales";
            SqlCommand cmdState = new SqlCommand(queryState, conAP);
            cmdState.CommandType = CommandType.StoredProcedure;
            cmdState.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            SqlDataAdapter sdaState = new SqlDataAdapter(cmdState);
            DataTable dtState = new DataTable();
            sdaState.Fill(dtState);
            int[] lStatus = new int[dtState.Rows.Count];
            string[] lState = new string[dtState.Rows.Count];
            if (dtState.Rows.Count > 0)
            {
                for (int i = 0; i < dtState.Rows.Count; i++)
                {
                    int stCount = 0;
                    int.TryParse(Convert.ToString(dtState.Rows[i]["StateCount"]), out stCount);
                    lStatus[i] = stCount;
                    lState[i] = Convert.ToString(dtState.Rows[i]["States"]);
                }

            }
            chart.StateCounts = lStatus;
            chart.StateNames = lState;

            string query = "UserSalesChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                string[] dmons = new string[12];
                decimal[] sMts = new decimal[12];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {
                string[] dmons = new string[12];
                decimal[] sMts = new decimal[12];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddMonths(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

        }
        catch (Exception ex)
        {

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlynYearlyValueDefaultUser", ex.Message + " ---------- " + uGuid);
        }
        return chart;
    }
    public static List<OrderItems> GetOrderItems(SqlConnection conAP, string oGuid)
    {
        List<OrderItems> orders = new List<OrderItems>();
        try
        {
            string query = "select * from OrderItems where OrderGuid=@OrderGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                orders = (from DataRow dr in dt.Rows
                          select new OrderItems()
                          {
                              Id = Convert.ToString(dr["Id"]),
                              ProductId = Convert.ToString(dr["ProductId"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              ProductName = Convert.ToString(dr["ProductName"]),
                              Thickness = Convert.ToString(dr["Thickness"]),
                              ProductGuid = Convert.ToString(dr["ProductGuid"]),
                              Quantity = Convert.ToString(dr["Qty"]),
                              OrderGuid = Convert.ToString(dr["OrderGuid"]),
                              ProductImage = Convert.ToString(dr["ProductImage"]),
                              Size = Convert.ToString(dr["Size"]),
                              TaxPercent = Convert.ToString(dr["TaxGroup"]),
                              Price = Convert.ToString(dr["Price"]),
                              ActualPrice = Convert.ToString(dr["ActualPrice"]),
                              ProductUrl = Convert.ToString(dr["ProductUrl"])
                          }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderItems", ex.Message);
        }
        return orders;
    }
    public static DataTable GetOrderDetails(SqlConnection _conn, string o_guid)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "SELECT o.*, b.FirstName + ' ' + b.LastName AS name1, b.EmailId AS email1, b.Mobile AS Mobile1, b.Address1 AS Address11, b.Address2 AS Address21, b.City AS City1, b.Country AS Country1, b.Zip AS Zip1, b.State AS state1, b.Landmark AS landmark1, b.CustomerGSTN, b.CompanyName, d.FirstName + ' ' + d.LastName AS name2, d.Email AS email2, d.Mobile AS Mobile2, d.Address1 AS Address12, d.Address2 AS Address22, d.City AS City2, d.Country AS Country2, d.Zip AS Zip2, d.State AS state2, d.Landmark AS landmark2, d.Block, d.Apartment FROM Orders o INNER JOIN UserBillingAddress b ON b.OrderGuid = o.OrderGuid INNER JOIN UserDeliveryAddress d ON d.OrderGuid = o.OrderGuid WHERE o.OrderGuid = @OrderGuid;";
            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = o_guid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderDetails", ex.Message);
        }
        return dt;
    }
    public static List<Orders> GetaAllOrders(SqlConnection conAP)
    {
        List<Orders> orders = new List<Orders>();
        try
        {
            string query = "Select o.*, " +
               " b.FirstName+' '+b.LastName name1, b.EmailId email1, b.Mobile mobile1, b.Address1 address11, b.Address2 address12, b.City city1, b.Country country1, b.Zip zip1, b.State state1,b.Landmark landmark1,b.Block blblock," +
               " d.FirstName+' '+d.LastName name2, d.Email email2,d.Mobile mobile2,d.Address1 address21, d.Address2 address22, d.City city2, d.Country country2, d.Zip zip2,d.State state2,d.Landmark landmark2,d.Block dlblock,d.Apartment" +
               "  from Orders o inner join UserBillingAddress b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress d on d.OrderGuid = o.OrderGuid Where (o.Status != 'Deleted' or o.Status is null) order by o.id desc";

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
                              SubTotalWithoutTax = Convert.ToString(dr["SubTotalWithoutTax"]),
                              Tax = Convert.ToString(dr["Tax"]),
                              Discount = Convert.ToString(dr["Discount"]),
                              AddDiscount = Convert.ToString(dr["AddDiscount"]),
                              AdvAmount = Convert.ToString(dr["AdvAmount"]),
                              BalAmount = Convert.ToString(dr["BalAmount"]),

                              TotalPrice = Convert.ToString(dr["TotalPrice"]),
                              UserGuid = Convert.ToString(dr["UserGuid"]),
                              UserType = Convert.ToString(dr["UserType"]),
                              UserName = Convert.ToString(dr["name1"]),
                              EmailId = Convert.ToString(dr["email1"]),
                              Contact = Convert.ToString(dr["Mobile1"]),
                              BillingAddress = Convert.ToString(dr["name1"]) + "| " + Convert.ToString(dr["email1"]) + "| " + Convert.ToString(dr["mobile1"]) + "| " + Convert.ToString(dr["blblock"]) + "| " + Convert.ToString(dr["address11"]) + "| " + Convert.ToString(dr["address12"]) + "| " + Convert.ToString(dr["city1"]) + "| " + Convert.ToString(dr["state1"]) + "-" + Convert.ToString(dr["zip1"]),
                              DeliveryAddress = Convert.ToString(dr["name2"]) + "| " + Convert.ToString(dr["email2"]) + "| " + Convert.ToString(dr["mobile2"]) + "| " + Convert.ToString(dr["Apartment"]) + "| " + Convert.ToString(dr["dlblock"]) + "| " + Convert.ToString(dr["address21"]) + "| " + Convert.ToString(dr["address22"]) + "| " + Convert.ToString(dr["city2"]) + "| " + Convert.ToString(dr["state2"]) + "-" + Convert.ToString(dr["zip2"]),
                          }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetaAllOrders", ex.Message);
        }
        return orders;
    }
    public static MonthlyChartUser GetMonthlynYearlyValueUser(SqlConnection conAP, string tps, string uGuid)
    {
        MonthlyChartUser chart = new MonthlyChartUser();
        try
        {
            int mIndex = tps == "6M" ? 6 : 12;


            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = tps == "6M" ? toDayDate.AddMonths(-5) : toDayDate.AddMonths(-11);
            DateTime newStDate = new DateTime(stDate.Year, stDate.Month, 1);

            string query = "UserSalesChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddMonths(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddMonths(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;

            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlynYearlyValueUser", ex.Message + " ---------- " + uGuid);
        }
        return chart;
    }
    public static MonthlyChartUser GetYearlyValueUser(SqlConnection conAP, string uGuid)
    {
        MonthlyChartUser chart = new MonthlyChartUser();
        try
        {

            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime newStDate = new DateTime(2022, 1, 1);
            int mIndex = (toDayDate.Year - newStDate.Year) + 1;

            string query = "UserSalesChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "All";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = "";

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => (v.Field<int>("Year_") == dtts.Year));
                    if (calls.Count() > 0)
                    {

                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal saleAmt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = saleAmt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("yyyy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddYears(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {
                string[] dmons = new string[mIndex];
                decimal[] sMts = new decimal[mIndex];
                int i = 0;
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("yyyy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddYears(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetYearlyValueUser", ex.Message + " ---------- " + uGuid);
        }
        return chart;
    }
    public static MonthlyChartUser GetMonthlyValueUser(SqlConnection conAP, string uGuid)
    {
        MonthlyChartUser chart = new MonthlyChartUser();
        try
        {
            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = toDayDate.AddMonths(-1);
            DateTime newStDate = toDayDate.AddMonths(-1);


            string query = "UserSalesChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "1M";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));

                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Day_") == dtts.Day) && (v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal ldCnt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = ldCnt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddDays(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {


                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];

                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlyValueUser", ex.Message + " ---------- " + uGuid);
        }
        return chart;
    }
    public static MonthlyChartUser GetMonthlyValueUser1W(SqlConnection conAP, string uGuid)
    {
        MonthlyChartUser chart = new MonthlyChartUser();
        try
        {
            DateTime toDayDate = TimeStamps.UTCTime();

            DateTime newStDate = toDayDate.AddDays(-7);


            string query = "UserSalesChart";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmd.Parameters.AddWithValue("@chartType", SqlDbType.NVarChar).Value = "1M";
            cmd.Parameters.AddWithValue("@startDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@toDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));

                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Day_") == dtts.Day) && (v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { SaleAmount = x.Field<decimal>("SaleAmount") }).FirstOrDefault();

                        decimal ldCnt = sl != null ? sl.SaleAmount : 0;
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = ldCnt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddDays(1);
                }

                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;


            }
            else
            {


                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];

                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.DayMonthNYear = dmons;
                chart.MonthSale = sMts;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlyValueUser1W", ex.Message + " ---------- " + uGuid);
        }
        return chart;
    }

    public static void SendToUserMail(SqlConnection conGV, string oGuid, string type, string delprname, string delprnumber, string link)
    {
        try
        {
            string query = "Select o.*, " +
               " b.FirstName+' '+b.LastName name1, b.EmailId email1, b.Mobile Mobile1, b.Address1  Address11, b.Address2 Address21, b.City City1, b.Country Country1, b.Zip Zip1, b.State state1,b.Landmark landmark1,b.CustomerGSTN,b.CompanyName," +
               " d.FirstName+' '+d.LastName name2, d.Email email2,d.Mobile Mobile2,d.Address1 as Address12, d.Address2 as Address22, d.City City2, d.Country Country2, d.Zip as Zip2,d.State state2,d.Landmark landmark2,d.Block,d.Apartment" +
               "  from Orders o inner join UserBillingAddress b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress d on d.OrderGuid = o.OrderGuid where o.OrderGuid=@OrderGuid";

            SqlCommand cmd = new SqlCommand(query, conGV);
            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string pType = Convert.ToString(dt.Rows[0]["PaymentMode"]);
                string pTable = UserCheckout.ProductDetails(conGV, oGuid);

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

                string address1 = "" + Convert.ToString(dt.Rows[0]["name1"]) + "<br>" + Convert.ToString(dt.Rows[0]["Apartment"]) + "<br>" + Convert.ToString(dt.Rows[0]["Block"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address11"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address21"]) + "<br>" + Convert.ToString(dt.Rows[0]["landmark2"]) + "<br>" + Convert.ToString(dt.Rows[0]["City1"]) + "," + Convert.ToString(dt.Rows[0]["state1"]) + "-" + Convert.ToString(dt.Rows[0]["Zip1"]) + "<br>" + Convert.ToString(dt.Rows[0]["Mobile1"]) + "<br>" + Convert.ToString(dt.Rows[0]["email1"]);
                string address2 = "" + Convert.ToString(dt.Rows[0]["name2"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address12"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address22"]) + "<br>" + Convert.ToString(dt.Rows[0]["City2"]) + "," + Convert.ToString(dt.Rows[0]["Country2"]) + "-" + Convert.ToString(dt.Rows[0]["Zip2"]) + "<br>" + Convert.ToString(dt.Rows[0]["state2"]) + "<br>" + Convert.ToString(dt.Rows[0]["Mobile2"]) + "<br>" + Convert.ToString(dt.Rows[0]["email2"]);

                string Disc = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Discount"])))
                {
                    Disc = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Discount </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>- ₹" + Convert.ToString(dt.Rows[0]["Discount"]) + "</th></tr>";
                }

                string AddDisc = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["AddDiscount"])))
                {
                    AddDisc = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Applied Offer </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>- ₹" + Convert.ToString(dt.Rows[0]["AddDiscount"]) + "</th></tr>";
                }
                string ship = "";
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ShippingPrice"])))
                {
                    ship = @"<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'> Shipping & Handling </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["ShippingPrice"]) + "</th></tr>";
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
                    AddDisc + ship + adv + Disc +
                    "<tr style='padding-bottom:15px;'><td style='border-bottom: 1px solid #573e40!important;'><br /></td></tr>" +
                    "<tr style='padding-bottom:5px;border-top:1px solid #856869'><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;padding:1%;' class='flexibleContainerCell'>  Grand Total </th><th align='left' valign='top' style='float:left;width:46%;color:#856869;font-size:15px;text-align:right;padding:1%;' class='flexibleContainerCell'>₹" + Convert.ToString(dt.Rows[0]["TotalPrice"]) + "</th></tr>";
                if (type.ToLower() == "dispatch")
                {
                    string TotalPrice = "";
                    if (pType == "COD20")
                    {
                        TotalPrice = Convert.ToString(dt.Rows[0]["BalAmount"]);
                    }
                    else
                    {
                        TotalPrice = Convert.ToString(dt.Rows[0]["TotalPrice"]);
                    }
                    Emails.OrderDispatched(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["Mobile1"]), TotalPrice, pType, delprname, delprnumber, link, address2);
                }
                else if (type.ToLower() == "deliver")
                {
                    Emails.OrderDelivered(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["Mobile1"]), pType, address2, Convert.ToString(dt.Rows[0]["TotalPrice"]));
                }
                else if (type.ToLower() == "cancel")
                {
                    Emails.CancellationMail(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["Mobile1"]), pType, address2, Convert.ToString(dt.Rows[0]["OrderOn"]));
                }
                else if (type.ToLower() == "return")
                {
                    Emails.ReturnMail(Convert.ToString(dt.Rows[0]["OrderId"]), table + "", Convert.ToString(dt.Rows[0]["name1"]), Convert.ToString(dt.Rows[0]["email1"]), Convert.ToString(dt.Rows[0]["Mobile1"]), pType, address2, Convert.ToString(dt.Rows[0]["OrderOn"]));
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendToUserMail", ex.Message);
        }
    }

}
public class MonthlyChart
{
    public string[] DayMonthNYear { get; set; }
    public decimal[] MonthSale { get; set; }
    public decimal[] MonthInitiated { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalOrder { get; set; }
    public decimal TotalInitiated { get; set; }
    public decimal ConvPercent { get; set; }
    public int[] PayStatus { get; set; }

}

public class MonthlyChartUser
{
    public string[] DayMonthNYear { get; set; }
    public decimal[] MonthSale { get; set; }
    public int[] StateCounts { get; set; }
    public string[] StateNames { get; set; }

}

public class SearchReports
{
    public string LineItems { get; set; }
    public int TotalCount { get; set; }
    public string Status { get; set; }
    public string StatusMessage { get; set; }
}
public class OrderItems
{
    public string Id { get; set; }
    public string OrderGuid { get; set; }
    public string ProductGuid { get; set; }
    public string ProductId { get; set; }
    public string ProductUrl { get; set; }
    public string CategoryUrl { get; set; }
    public string PriceId { get; set; }
    public string ProductImage { get; set; }
    public string Size { get; set; }
    public string ProductName { get; set; }
    public string Thickness { get; set; }
    public string TaxPercent { get; set; }
    public string Quantity { get; set; }
    public string Price { get; set; }
    public string ActualPrice { get; set; }
    public DateTime AddedOn { get; set; }

}