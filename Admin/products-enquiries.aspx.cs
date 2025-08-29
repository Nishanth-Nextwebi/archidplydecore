using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web;


public partial class Admin_products_enquiries : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strRequests = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllEnquiries();
    }
    public void GetAllEnquiries()
    {
        try
        {
            List<Enquiry> lcs = Enquiry.GetAllEnquiry(conAP);
            int i = 0;
            string size = "";
            foreach (Enquiry pro in lcs)
            {
                if(pro.Size != "")
                {
                    size = "<td>" + pro.Size + " X " + pro.Thickness + "</td>";
                }
                else
                {
                    size = "<td>NA</td>";
                }
                strRequests += @"<tr>   
                                <td>" + (i + 1) + @"</td> 
                                <td>" + pro.Products + @"</td> 
                                <td>" + pro.ProductType + @"</td> 
                                "+ size + @"
                                <td>" + pro.UserName + @"</td>
                               <td><a href='CompanyEmail:" + pro.EmailId + "'>" + pro.EmailId + @"</a></td>
                                <td>" + pro.ContactNo + @"</td> 
                                <td>" + pro.City + @"</td> 
                                <td><a href='javascript:void(0);' data-bs-toggle='modal' data-bs-target='#fadeInRightModal' data-msg='"+pro.Message+@"' class='btn btn-sm btn-secondary badge-gradient-secondary btnmsg' data-id=" + pro.Id + @" data-name=" + pro.UserName + @">View Message</a></td>
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
            Enquiry BD = new Enquiry();
            BD.Id = Convert.ToInt32(id);
            int exec = Enquiry.DeleteEnquiry(conAP, BD);
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
}
