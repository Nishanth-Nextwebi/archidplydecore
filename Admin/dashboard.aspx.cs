using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Services;

public partial class Admin_dashboard : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string Strusername = "", strOrders="", strTotalSales = "", strTotalProduct = "", strTotalOrder = "", strTotalCustomer = "", strBlogs = "", strContact = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["ap_aid"] == null)
        {
            Response.Redirect("Default.aspx", false);
        }
        else
        {
            BindUserName();
            strTotalSales = DashBoard.GetTotalSales(conAP).ToString(".##");
            strTotalProduct = DashBoard.GetProductCount(conAP).ToString();
            strTotalOrder = DashBoard.GetOrderCount(conAP).ToString();
            strTotalCustomer = DashBoard.GetCustomerCount(conAP).ToString();
            strBlogs = DashBoard.NoOfBlogs(conAP).ToString();
            strContact = DashBoard.ContactUs(conAP).ToString();
            BindTop10();
        }
    }
    public void BindTop10()
    {
        try
        {
            strOrders = "";
            List<Orders> orders = DashBoard.GetLast10Orders(conAP);
            int i = 0;
            foreach (Orders ord in orders)
            {
                string oStatus = "";
                switch (ord.OrderStatus)
                {
                    case "Initiated":
                        oStatus = "<span class='shadow badge badge-outline-warning'>" + ord.OrderStatus + "</span>";
                        break;
                    case "In-Process":
                        oStatus = "<span class='shadow badge badge-outline-info shadow'>" + ord.OrderStatus + "</span>";
                        break;
                    case "Cancelled":
                        oStatus = "<span class='shadow badge badge-outline-danger'>" + ord.OrderStatus + "</span>";
                        break;
                    case "Dispatched":
                        oStatus = "<span class='shadow badge badge-outline-primary'>" + ord.OrderStatus + "</span>";
                        break;
                    case "Delivered":
                        oStatus = "<span class='shadow badge badge-outline-success'>" + ord.OrderStatus + "</span>";
                        break;
                }
                string pStatus = "";
                switch (ord.PaymentStatus)
                {
                    case "Initiated":
                        pStatus = "<span class='shadow badge bg-warning'>" + ord.PaymentStatus + "</span>";
                        break;
                    case "Paid":
                        pStatus = "<span class='shadow badge bg-success'>" + ord.PaymentStatus + "</span>";
                        break;
                    case "Not Paid":
                        pStatus = "<span class='shadow badge bg-primary'>" + ord.PaymentStatus + "</span>";
                        break;
                    case "Failed":
                        pStatus = "<span class='shadow badge bg-danger'>" + ord.PaymentStatus + "</span>";
                        break;

                }

                strOrders += @"<tr>
                                 <td>" + (i + 1) + @"</td>
                                    <td><a class='badge badge-outline-primary' href='../view-invoice.aspx?o=" + ord.OrderGuid + @"' target='_blank'>" + ord.OrderId + @"</a></td>
                                 <td>" + ord.UserName + @"</td> 
                                 <td>" + ord.EmailId + @"</td>
                                 <td>" + ord.Contact + @"</td> 
                                 <td>₹" + ord.SubTotal + @"</td> 
                                <td>" + oStatus + @"</td>
                                <td>" + pStatus + @"</td>
                                <td>" + ord.PaymentId + @"</td>
                                <td>" + ord.OrderOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</td>
                              </tr>";
                i++;

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTop10", ex.Message);
        }
    }
    public void BindUserName()
    {
        try
        {
            Strusername = CreateUser.GetLoggedUserName(conAP, Request.Cookies["ap_aid"].Value);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindUserName", ex.Message);
        }
    }


    [WebMethod(EnableSession = true)]
    public static MonthlyChart DashBoardChart()
    {
        MonthlyChart slp = new MonthlyChart();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            slp = Reports.GetMonthlynYearlyValueDefault(conAP);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DashBoardChart", ex.Message);
        }
        return slp;
    }
    [WebMethod(EnableSession = true)]
    public static MonthlyChart FilterDashBoardChart(string fValue)
    {
        MonthlyChart slp = new MonthlyChart();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            if (fValue == "1Y")
            {
                slp = Reports.GetMonthlynYearlyValue(conAP, "");
            }
            else if (fValue == "6M")
            {
                slp = Reports.GetMonthlynYearlyValue(conAP, "6M");
            }
            else if (fValue == "All")
            {
                slp = Reports.GetYearlyValue(conAP);
            }
            else if (fValue == "1M")
            {
                slp = Reports.GetMonthlyValue(conAP, "1M");
            }
            else if (fValue == "1W")
            {
                slp = Reports.GetMonthlyValue(conAP, "1W");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DashBoardChart", ex.Message);
        }
        return slp;
    }

    [WebMethod(EnableSession = true)]
    public static MonthlyChart FilterDashBoardChartStatus(string fValue)
    {
        MonthlyChart slp = new MonthlyChart();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            slp = Reports.GetMonthlynYearlyValueStatus(conAP, fValue);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DashBoardChart", ex.Message);
        }
        return slp;
    }
}