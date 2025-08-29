using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class order_report : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime cDate = TimeStamps.UTCTime();
            string sFrom = "";
            string sTo = "";

            sFrom = txtFrom.Text == "" ? "" : Convert.ToDateTime(txtFrom.Text).ToString("dd-MMM-yyyy");
            sTo = txtTo.Text == "" ? "" : Convert.ToDateTime(txtTo.Text).ToString("dd-MMM-yyyy");

            if (ddlDay.SelectedValue != "")
            {
                switch (ddlDay.SelectedValue)
                {
                    case "1":
                        sFrom = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        sTo = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                    case "2":
                        sFrom = TimeStamps.UTCTime().AddDays(-7).ToString("dd-MMM-yyyy");
                        sTo = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                    case "3":
                        sFrom = TimeStamps.UTCTime().AddDays(-30).ToString("dd-MMM-yyyy");
                        sTo = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                }
            }

            DateTime currentDateTime = DateTime.Now;

            var reports = Reports.GetAllOrdersExcelReportWithFilters(conAP, sFrom, sTo, ddlStatus.SelectedItem.Value, ddlPayStatus.SelectedItem.Value, txtSearch.Text.Trim());

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "OrderReport_" + currentDateTime.ToString("ddMMyy") + ".xlsx";

            // Create and style the workbook
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("OrderReport");

                worksheet.Cell(1, 1).Value = "Sl. No.";
                worksheet.Cell(1, 2).Value = "Order Id";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Phone";
                worksheet.Cell(1, 6).Value = "Order Status";
                worksheet.Cell(1, 7).Value = "Ordered On";
                worksheet.Cell(1, 8).Value = "Payment Type";
                worksheet.Cell(1, 9).Value = "Payment Status";
                worksheet.Cell(1, 10).Value = "PaymentId";
                worksheet.Cell(1, 11).Value = "Advance Paid";
                worksheet.Cell(1, 12).Value = "Balance Amount";
                worksheet.Cell(1, 13).Value = "Total Price";

                #region Colours
                // Define styles
                var headerStyle = XLWorkbook.DefaultStyle;
                headerStyle.Fill.SetBackgroundColor(XLColor.Yellow);
                headerStyle.Font.SetBold(true);
                headerStyle.Font.SetFontSize(12);
                headerStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                headerStyle.Border.BottomBorder = XLBorderStyleValues.Medium;
                headerStyle.Border.TopBorder = XLBorderStyleValues.Medium;
                headerStyle.Border.LeftBorder = XLBorderStyleValues.Medium;
                headerStyle.Border.RightBorder = XLBorderStyleValues.Medium;

                var defaultCellStyle = XLWorkbook.DefaultStyle;
                defaultCellStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                defaultCellStyle.Border.BottomBorder = XLBorderStyleValues.Thin;
                defaultCellStyle.Border.TopBorder = XLBorderStyleValues.Thin;
                defaultCellStyle.Border.LeftBorder = XLBorderStyleValues.Thin;
                defaultCellStyle.Border.RightBorder = XLBorderStyleValues.Thin;

                var successCellStyle = XLWorkbook.DefaultStyle;
                successCellStyle.Fill.BackgroundColor = XLColor.LightGreen;
                successCellStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                successCellStyle.Border.BottomBorder = XLBorderStyleValues.Thin;
                successCellStyle.Border.TopBorder = XLBorderStyleValues.Thin;
                successCellStyle.Border.LeftBorder = XLBorderStyleValues.Thin;
                successCellStyle.Border.RightBorder = XLBorderStyleValues.Thin;

                var infoCellStyle = XLWorkbook.DefaultStyle;
                infoCellStyle.Fill.BackgroundColor = XLColor.SkyBlue;
                infoCellStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                infoCellStyle.Border.BottomBorder = XLBorderStyleValues.Thin;
                infoCellStyle.Border.TopBorder = XLBorderStyleValues.Thin;
                infoCellStyle.Border.LeftBorder = XLBorderStyleValues.Thin;
                infoCellStyle.Border.RightBorder = XLBorderStyleValues.Thin;

                var warningCellStyle = XLWorkbook.DefaultStyle;
                warningCellStyle.Fill.BackgroundColor = XLColor.OrangePeel;
                warningCellStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                warningCellStyle.Border.BottomBorder = XLBorderStyleValues.Thin;
                warningCellStyle.Border.TopBorder = XLBorderStyleValues.Thin;
                warningCellStyle.Border.LeftBorder = XLBorderStyleValues.Thin;
                warningCellStyle.Border.RightBorder = XLBorderStyleValues.Thin;

                var dangerCellStyle = XLWorkbook.DefaultStyle;
                dangerCellStyle.Fill.BackgroundColor = XLColor.Red;
                dangerCellStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                dangerCellStyle.Border.BottomBorder = XLBorderStyleValues.Thin;
                dangerCellStyle.Border.TopBorder = XLBorderStyleValues.Thin;
                dangerCellStyle.Border.LeftBorder = XLBorderStyleValues.Thin;
                dangerCellStyle.Border.RightBorder = XLBorderStyleValues.Thin;
                #endregion

                // Apply header style

                worksheet.Range("A1:N1").Style = headerStyle;

                // Populate worksheet with data
                for (int index = 0; index < reports.Rows.Count; index++)
                {
                    var row = reports.Rows[index];

                    decimal totalAmount = 0, paidAmount = 0, balanceAmount = 0;
                    decimal.TryParse(Convert.ToString(row["TotalPrice"]), out totalAmount);
                    decimal.TryParse(Convert.ToString(row["AdvAmount"]), out paidAmount);
                    balanceAmount = (totalAmount - paidAmount);

                    worksheet.Cell(index + 2, 1).Value = index + 1;
                    worksheet.Cell(index + 2, 2).Value = Convert.ToString(row["OrderId"]);
                    worksheet.Cell(index + 2, 3).Value = Convert.ToString(row["name1"]);
                    worksheet.Cell(index + 2, 4).Value = Convert.ToString(row["email1"]);
                    worksheet.Cell(index + 2, 5).Value = Convert.ToString(row["mobile1"]);
                    worksheet.Cell(index + 2, 6).Value = Convert.ToString(row["OrderStatus"]);
                    worksheet.Cell(index + 2, 7).Value = Convert.ToDateTime(Convert.ToString(row["OrderOn"]));
                    worksheet.Cell(index + 2, 8).Value = Convert.ToString(row["PaymentMode"]);
                    worksheet.Cell(index + 2, 9).Value = Convert.ToString(row["PaymentStatus"]);
                    worksheet.Cell(index + 2, 10).Value = Convert.ToString(row["PaymentId"]);
                    worksheet.Cell(index + 2, 11).Value = paidAmount;
                    worksheet.Cell(index + 2, 12).Value = balanceAmount;
                    worksheet.Cell(index + 2, 13).Value = totalAmount;

                    // Apply styles to cells
                    worksheet.Range("A" + (index + 2) + ":N" + (index + 2)).Style = defaultCellStyle;
                    worksheet.Cell(index + 2, 6).Style = row["OrderStatus"].ToString() == "In-Process" ? infoCellStyle : row["OrderStatus"].ToString() == "Initiated" ? warningCellStyle : row["OrderStatus"].ToString() == "Dispatched" ? warningCellStyle : row["OrderStatus"].ToString() == "Delivered" ? successCellStyle : row["OrderStatus"].ToString() == "Cancelled" ? dangerCellStyle : defaultCellStyle;
                    worksheet.Cell(index + 2, 9).Style = row["PaymentStatus"].ToString() == "Not Paid" ? dangerCellStyle : row["PaymentStatus"].ToString() == "Partially Paid" ? warningCellStyle : row["PaymentStatus"].ToString() == "Initiated" ? warningCellStyle : row["PaymentStatus"].ToString() == "Paid" ? successCellStyle : defaultCellStyle;
                }

                // Send the workbook to the response
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnExport_Click", ex.Message);
        }
    }

    [WebMethod]
    public static SearchReports BindReports(string pNo, string pSize, string fromDate, string toDate, string ddlDay, string oStatus, string pStatus, string oParam)
    {
        SearchReports reports = new SearchReports();
        try
        {
            string strReport = "";
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            int pNo2 = 0, pSize2 = 0;
            int.TryParse(pNo, out pNo2);
            int.TryParse(pSize, out pSize2);

            if (ddlDay != "")
            {
                switch (ddlDay)
                {
                    case "1":
                        fromDate = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        toDate = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                    case "2":
                        fromDate = TimeStamps.UTCTime().AddDays(-7).ToString("dd-MMM-yyyy");
                        toDate = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                    case "3":
                        fromDate = TimeStamps.UTCTime().AddDays(-30).ToString("dd-MMM-yyyy");
                        toDate = TimeStamps.UTCTime().ToString("dd-MMM-yyyy");
                        break;
                }
            }

            var order = Reports.GetAllOrdersWithFilters(conAP, (pNo2), pSize2, fromDate, toDate, oStatus, pStatus, oParam);

            if (order.Rows.Count > 0)
            {
                for (int i = 0; i < order.Rows.Count; i++)
                {

                    string oSts = "";
                    string dispatchItem = "";
                    string deliverItem = "";
                    string PayStatus = "";
                    string cancelItem = "";
                    var OrderGuid = Convert.ToString(order.Rows[i]["OrderGuid"]);
                    var OrderStatus = Convert.ToString(order.Rows[i]["OrderStatus"]);


                    string pSts = "";
                    var PaymentStatus = Convert.ToString(order.Rows[i]["PaymentStatus"]);
                    if (PaymentStatus == "Initiated")
                    {
                        pSts = "<span class='badge badge-soft-danger badge-border'>Initiated</span>";
                    }
                    else if (PaymentStatus == "Paid")
                    {
                        pSts = "<span class='badge badge-soft-success badge-border'>Paid</span>";
                        PayStatus = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success' data-id='" + OrderGuid + @"'  data-bs-toggle='tooltip' data-placement='top'  title='Paid'><i class='mdi mdi-currency-inr text-muted'></i></a>";
                    }
                    else if (PaymentStatus == "Pending")
                    {
                        pSts = "<span class='badge badge-soft-warning badge-border'>Pending</span>";
                        PayStatus = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success UpdatePayment' data-id='" + OrderGuid + @"'  data-bs-toggle='tooltip' data-placement='top'  title='Update Payment'><i class='mdi mdi-currency-inr text-black'></i></a>";
                    }

                  
                    if (OrderStatus == "In-Process")
                    {
                        dispatchItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-primary dispatchItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Dispatch Order'><i class='ri-truck-line'></i></a>";
                        deliverItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success deliverItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top'  title='Deliver Order'><i class='ri-checkbox-circle-line'></i></a>";
                        cancelItem = " <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger cancelItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Cancel Order'><i class='ri-close-circle-line'></i></a>";
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='In-Process' class='badge badge-outline-info'>In-Process</a>";
                    }
                    if (OrderStatus == "Initiated")
                    {
                        dispatchItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-primary dispatchItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Dispatch Order'><i class='ri-truck-line'></i></a>";
                        deliverItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success deliverItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top'  title='Deliver Order'><i class='ri-checkbox-circle-line'></i></a>";
                        cancelItem = " <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger cancelItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Cancel Order'><i class='ri-close-circle-line'></i></a>";
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Initiated' class='badge badge-outline-warning'>Initiated</a>";
                    }
                    else if (OrderStatus == "Dispatched")
                    {
                        dispatchItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-primary text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top'><i class='ri-truck-line'></i></a>";
                        deliverItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success deliverItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Deliver Order'><i class='ri-checkbox-circle-line'></i></a>";
                        cancelItem = " <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger cancelItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Cancel Order'><i class='ri-close-circle-line'></i></a>";
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Dispatched' class='badge badge-outline-primary'>Dispatched</a>";
                    }
                    else if (OrderStatus == "Delivered")
                    {
                        dispatchItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-primary text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' ><i class='ri-truck-line'></i></a>";
                        deliverItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' ><i class='ri-checkbox-circle-line'></i></a>";
                        cancelItem = " <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' ><i class='ri-close-circle-line'></i></a>";
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Delivered' class='badge badge-outline-success'>Delivered</a>";
                    }
                    else if (OrderStatus == "Cancelled")
                    {
                        dispatchItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-primary text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top'><i class='ri-truck-line'></i></a>";
                        deliverItem = "<a href='javascript:void(0);' class='bs-tooltip fs-18 link-success text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top'><i class='ri-checkbox-circle-line'></i></a>";
                        cancelItem = " <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger text-muted' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' ><i class='ri-close-circle-line'></i></a>";
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Cancelled' class='badge badge-outline-danger'>Cancelled</a>";
                    }

                    decimal totalAmount = 0, paidAmount = 0, balanceAmount = 0, productPrice = 0;
                    decimal.TryParse(Convert.ToString(order.Rows[i]["TotalPrice"]), out totalAmount);
                    decimal.TryParse(Convert.ToString(order.Rows[i]["AdvAmount"]), out paidAmount);
                    decimal.TryParse(Convert.ToString(order.Rows[i]["SubTotal"]), out productPrice);
                    balanceAmount = (totalAmount - paidAmount);
                    string totalAmountIn = String.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N2}", totalAmount);
                    string paidAmountIn = String.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N2}", paidAmount);
                    string balanceAmountIn = String.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N2}", balanceAmount);
                    string PriceForProduct = String.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N2}", productPrice);
                    int PageIndex = ((pNo2 - 1) * pSize2);

                    strReport += @"<tr>
                                    <td><span>" + (PageIndex + (i + 1)) + @"</span></td>
                                    <td><a class='badge badge-outline-primary' href='/view-invoice.aspx?o=" + OrderGuid + @"' target='_blank'>" + Convert.ToString(order.Rows[i]["OrderId"]) + @"</a></td>
                                    <td>" + Convert.ToString(order.Rows[i]["name1"]) + @"</td>
                                    <td>" + Convert.ToString(order.Rows[i]["email1"]) + @"</td>
                                    <td>" + Convert.ToString(order.Rows[i]["mobile1"]) + @"</td>
                                    <td id='o_r_" + OrderGuid + @"'>" + oSts + @"</td>
                                    <td>
                                    <a href='javascript:void(0);' data-id='" + OrderGuid + @"'  class='ViewDeliverAddress fs-18 link-success bs-tooltip' data-bs-toggle='tooltip' data-placement='top' title='Shipping Address'><i class='mdi mdi-map-marker-path'></i></a>&nbsp; 
                                    <a href='javascript:void(0);' data-id='" + OrderGuid + @"' data-Name='" + HttpUtility.HtmlEncode(Convert.ToString(order.Rows[i]["CourierName"])) + @"' data-Code='" + HttpUtility.HtmlEncode(Convert.ToString(order.Rows[i]["TrackingCode"])) + @"' data-date='"+ Convert.ToString(order.Rows[i]["DeliveryDate"]) + @"' data-Link='" + HttpUtility.HtmlEncode(Convert.ToString(order.Rows[i]["TrackingLink"])) + @"'  class='ViewShippingDetails fs-18 link-secondary bs-tooltip' data-bs-toggle='tooltip' data-placement='top' title='Deliver info'><i class='mdi mdi-truck-fast'></i></a>
                                    </td>
                                    <td>" + Convert.ToDateTime(Convert.ToString(order.Rows[i]["OrderOn"])).ToString("dd-MMM-yyyy") + @"</td>
                                    <td>" + Convert.ToString(order.Rows[i]["PaymentMode"]) + @"</td>
                                    <td>" + pSts + @"</td> 
                                    <td>" + Convert.ToString(order.Rows[i]["PaymentId"]) + @"</td> 
                                    <td>" + paidAmountIn + @"</td> 
                                    <td>" + balanceAmountIn + @"</td> 
                                    <td>" + totalAmountIn + @"</td> 
                                    <td>" + Convert.ToDateTime(Convert.ToString(order.Rows[i]["LastUpdatedOn"])).ToString("dd-MMM-yyyy hh:mm tt") + @"</td>
                                    <td class='fixed-column'> 
" + dispatchItem + deliverItem + PayStatus+cancelItem + @"

                                                 <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + OrderGuid + @"' data-status='" + OrderStatus + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'><i class='ri-delete-bin-2-line'></i></a>
                                    </td>
                                    </tr>";
                }

                int totalN = 0;
                int.TryParse(Convert.ToString(order.Rows[0]["TotalCount"]), out totalN);

                reports.Status = "Success";
                reports.TotalCount = totalN;
                reports.LineItems = strReport;
            }
            else
            {
                reports.Status = "error";
                reports.StatusMessage = "No Data";
            }
        }
        catch (Exception ex)
        {
            reports.Status = "error";
            reports.StatusMessage = ex.Message;
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindReport", ex.Message);
        }
        return reports;
    }

    [WebMethod(EnableSession = true)]
    public static List<UserBillingAddress> BindAddress(string uGuid)
    {
        List<UserBillingAddress> adds_ = new List<UserBillingAddress>();
        try
        {
            if (String.IsNullOrEmpty(uGuid))
            {
                return adds_;
            }

            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "View", aid))
            {
                return adds_;
            }

            adds_ = UserCheckout.GetBillingAddress(conAP, uGuid);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAddress", ex.Message);
        }
        return adds_;
    }

    [WebMethod(EnableSession = true)]
    public static string DispatchOrder(string OrderGuid, string courierName, string trackingCode, string trackingLink, string oStatus,string DelDate)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var oDetails = Reports.GetOrderDetails(conAP, OrderGuid);
            if (oDetails.Rows.Count > 0)
            {
                if (Convert.ToString(oDetails.Rows[0]["OrderStatus"]) != "Dispatched")
                {
                    var UpdatedOn = TimeStamps.UTCTime();
                    var UpdatedIP = CommonModel.IPAddress();

                    int exec = 0;
                    exec = Reports.DispatchOrder(conAP, OrderGuid, courierName, trackingCode, trackingLink, oStatus, UpdatedOn, UpdatedIP, DelDate);
                    if (exec > 0)
                    {
                        Reports.SendToUserMail(conAP, OrderGuid, "dispatch", courierName, trackingCode, trackingLink);
                        x = "Success";
                    }
                    else
                    {
                        x = "W";
                    }
                }
                else
                {
                    x = "Dispatched";
                }
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DispatchOrder", ex.Message);
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateOrderStatusDelivered(string OrderGuid, string oStatus)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var oDetails = Reports.GetOrderDetails(conAP, OrderGuid);
            if (oDetails.Rows.Count > 0)
            {
                var UpdatedOn = TimeStamps.UTCTime();
                var UpdatedIP = CommonModel.IPAddress();

                int exec = Reports.DeliveredOrder(conAP, OrderGuid, oStatus, UpdatedOn, UpdatedIP);
                if (exec > 0)
                {
                    Reports.SendToUserMail(conAP, OrderGuid, "deliver", "", "", "");
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateOrderStatusDelivered", ex.Message);
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string CancelOrder(string OrderGuid, string remarks, string remarksVisible, string oStatus)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }


            var oDetails = Reports.GetOrderDetails(conAP, OrderGuid);
            if (oDetails.Rows.Count > 0)
            {
                var UpdatedOn = TimeStamps.UTCTime();
                var UpdatedIP = CommonModel.IPAddress();

                int exec = Reports.CancelOrder(conAP, OrderGuid, oStatus, UpdatedOn, UpdatedIP);
                if (exec > 0)
                {
                    string CancellationRemarks = "";
                    if (remarksVisible == "Yes")
                    {
                        CancellationRemarks = remarks;
                    }
                    Reports.SendToUserMail(conAP, OrderGuid, "cancel", CancellationRemarks, "", "");
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CancelOrder", ex.Message);
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "Delete", aid))
            {
                x = "Permission";
                return x;
            }

            var UpdatedOn = TimeStamps.UTCTime();
            var UpdatedIP = CommonModel.IPAddress();

            int exec = Reports.DeleteOrder(conAP, id, "Deleted", UpdatedOn, UpdatedIP);
            if (exec > 0)
            {
                x = "Success";
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }


    [WebMethod(EnableSession = true)]
    public static string UpdatePaymentStatus(string OrderGuid, string payId)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "order-report.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var oDetails = Reports.GetOrderDetails(conAP, OrderGuid);
            if (oDetails.Rows.Count > 0)
            {
                var UpdatedOn = TimeStamps.UTCTime();
                var UpdatedIP = CommonModel.IPAddress();

                int exec = Reports.UpdatePaymentStatus(conAP, OrderGuid, payId, UpdatedOn, UpdatedIP);
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateDeliveryDate", ex.Message);
        }
        return x;
    }

}