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

public partial class investors : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strInvestor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BindInvesterRelType();
    }
    public void BindInvesterRelType()
    {
        try
        {
            strInvestor = "";
            List<InvesterRelType> reports = InvesterRelType.GetAllInvesterRelType(conAP);
            if (reports.Count > 0)
            {
                foreach (InvesterRelType report in reports)
                {
                    strInvestor += @"<div class='col-xl-3 col-lg-4 col-md-6'>
                                    <a href='investors-report-detail?id=" + report.InversterGuid+@"'>
                                   <div class='bd-callout bd-callout-info'>"+report.Title+@"</div>
                    </a>
                </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAnnualReport", ex.Message);
        }
    }
}