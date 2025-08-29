using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web;

public partial class Admin_view_emailsubscribe : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strRequests = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllEmailRequests();
    }
    public void GetAllEmailRequests()
    {
        try
        {
            List<EmailSubscribe> lcs = EmailSubscribe.GetAllEmailSubscribe(con);
            int i = 0;
            foreach (EmailSubscribe pro in lcs)
            {
                strRequests += @"<tr>
                                <td>" + (i + 1) + @"</td>                                           
                                <td>" + pro.EmailId + @"</td> 
                                <td>" + pro.AddedOn.ToString("dd/MM/yyyy hh:mm tt") + @"</td> 
 <td class='text-center'> 
                                            <a href = 'javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' aria-label='Delete Contacts'><i class='mdi mdi-trash-can-outline'></i></a> 
                                        </td>
                                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(Request.Url.PathAndQuery, "GetAllEmailRequests", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-emailsubscriber.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                EmailSubscribe BD = new EmailSubscribe();
                BD.Id = Convert.ToInt32(id);
                int exec = EmailSubscribe.DeleteEmailSubscribe(conAP, BD);
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
                x = "Permission";
            }

        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
}