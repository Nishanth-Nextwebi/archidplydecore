using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myorders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["arch_i"] == null)
        {
            Response.Redirect("/");
        }
    }

    [WebMethod]
    public static SearchReports BindUserReports(string pNo, string pSize,string oStatus)
    {
        SearchReports reports = new SearchReports();
        try
        {
            string strReport = "";
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            int pNo2 = 0, pSize2 = 0;
            int.TryParse(pNo, out pNo2);
            int.TryParse(pSize, out pSize2);
            var uGuid = HttpContext.Current.Request.Cookies["arch_i"].Value;
            var order = Reports.GetAllUserMyOrders(conAP, (pNo2), pSize2, oStatus,uGuid);

            if (order.Rows.Count > 0)
            {
                for (int i = 0; i < order.Rows.Count; i++)
                {
                    string oSts = "";
                    var OrderGuid = Convert.ToString(order.Rows[i]["OrderGuid"]);
                    var OrderStatus = Convert.ToString(order.Rows[i]["OrderStatus"]);

                    if (OrderStatus == "In-Process")
                    {
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='In-Process' class='badge badge-outline-info bg-info'>In-Process</a>";
                    }
                    if (OrderStatus == "Initiated")
                    {
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Initiated' class='badge badge-outline-warning bg-warning'>Initiated</a>";
                    }
                    else if (OrderStatus == "Dispatched")
                    {
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Dispatched' class='badge badge-outline-primary bg-primary'>Dispatched</a>";
                    }
                    else if (OrderStatus == "Delivered")
                    {
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Delivered' class='badge badge-outline-success bg-success'>Delivered</a>";
                    }
                    else if (OrderStatus == "Cancelled")
                    {
                        oSts = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='Cancelled' class='badge badge-outline-danger bg-danger'>Cancelled</a>";
                    }
                    var deldate = Convert.ToString(order.Rows[i]["DeliveryDate"]);
                    if (deldate!="")
                    {
                        deldate = Convert.ToString(order.Rows[i]["DeliveryDate"]);
                    }
                    else
                    {
                        deldate = "Not updated yet.";
                    }
                    double price = 0;
                    double.TryParse(Convert.ToString(order.Rows[i]["SubTotal"]), out price);
                    int PageIndex = ((pNo2 - 1) * pSize2);
                    strReport += @"<tr>
                                    <td><span>" + (PageIndex + (i + 1)) + @"</span></td>
                                    <td><a href='/shop-products/"+ Convert.ToString(order.Rows[i]["ProductUrl"]) + @"'>" + Convert.ToString(order.Rows[i]["ProductName"]) + @"</a></td>
                                    <td>" + Convert.ToString(order.Rows[i]["OrderId"]) + @"</td>
                                    <td>₹ " + price.ToString("##,###.00") + @"</td>
                                    <td id='o_r_" + OrderGuid + @"'>" + oSts + @"</td>
                                    <td>" + Convert.ToDateTime(Convert.ToString(order.Rows[i]["OrderOn"])).ToString("dd-MMM-yyyy") + @"</td>
                                    <td>" + deldate + @"</td>
                                    <td> <div class='d-flex flex-nowrap justify-content-center'>
                                        <a href='/view-invoice.aspx?o=" + Convert.ToString(order.Rows[i]["OrderGuid"]) + @"' target='_blank' title='view invoice' class='btn btn-primary py-4 fs-13px btn-xs me-4'>Detail</a>
                                    </div> </td>
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

}