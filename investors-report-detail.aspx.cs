using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class investors_report_detail : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strReports, strInvesterId, strInvesterTitle = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            GetInvesterRelType();
        }
        else
        {
            Response.Redirect("/404");
        }
    }
    public void GetInvesterRelType()
    {
        try
        {
            InvesterRelType pro = InvesterRelType.GetInvesterRelTypeByGuid(conAP, Request.QueryString["id"]);
            if (pro != null)
            {
                strInvesterId = pro.InversterGuid.ToString();
                strInvesterTitle = pro.Title;
                List<InvesterRelations> reports = InvesterRelations.GetInvesterRelations(conAP, strInvesterId);
                if (reports.Count > 0)
                {
                    foreach (InvesterRelations report in reports)
                    {
                        strReports += @"<tr>
                                <td class='align-middle px-10 text-body-emphasis'>" + report.Title + @"</td>
                                <td class='p-5'>
                                    <a href='/" + report.PDF + @"' download class='btn green-btn w-100 btn-hover-bg-primary btn-hover-border-primary btn-sm py-4' target='_blank'>
                                        Download PDF
                                    </a>
                                </td>
                            </tr>";
                    }
                }
                else
                {
                    strReports += @"<tr>
                    <td colspan='2' class='text-center p-5'>
                     No reports available.
                 </td>                            </tr>";
                }
            }
            else
            {
                Response.Redirect("/404");
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelType", ex.Message);
        }
    }

}