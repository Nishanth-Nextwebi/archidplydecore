using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web;

public partial class Admin_view_contactus : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strRequests = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllContactRequests();
    }
    public void GetAllContactRequests()
    {
        try
        {
            List<ContactUs> lcs = ContactUs.GetAllContactUs(conAP);
            int i = 0;
            foreach (ContactUs pro in lcs)
            {
                strRequests += @"<tr>   
                                <td>" + (i + 1) + @"</td>                                           
                                <td>" + pro.UserName + @"</td>
                               <td><a href='CompanyEmail:" + pro.EmailId + "'>" + pro.EmailId + @"</a></td>
                                <td>" + pro.ContactNo + @"</td> 
                                <td><a href='javascript:void(0);' data-bs-toggle='modal' data-bs-target='#fadeInRightModal' class='btn btn-sm btn-secondary badge-gradient-secondary btnmsg' data-id=" + pro.Id + @" data-name=" + pro.UserName + @">View Message</a></td>
                                <td>" + pro.AddedOn.ToString("dd/MM/yyyy hh:mm tt") + @"</td> 
                                <td class='text-center'>
                                <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                <i class='mdi mdi-trash-can-outline'></i></a></td>
                                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(Request.Url.PathAndQuery, "GetAllContactRequests", ex.Message);
        }
    }
    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-contactus.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ContactUs BD = new ContactUs();
                BD.Id = Convert.ToInt32(id);
                int exec = ContactUs.DeleteContactUs(conAP, BD);
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
    [WebMethod(EnableSession = true)]
    public static string GetMessage(string id)
    {
        var message = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            message = ContactUs.GetMessageById(conAP, id);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMessage", ex.Message);
        }
        return message;
    }
}
