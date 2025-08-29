using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_view_notifyme : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strUserDetails = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllReviews();
    }
    public void GetAllReviews()
    {
        try
        {
            strUserDetails = "";
            List<ProductNotifyMe> cas = ProductNotifyMe.GetAllProductNotifyMe(conAP).OrderByDescending(s => s.Id).ToList();
            int i = 0;
            foreach (ProductNotifyMe ca in cas)
            {
                //Product clickable code:<a href='/product/" + ca.ProductUrl + @"' class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='View Product' target='_blank'>" + ca.ProductName + @"</a><
                var name = ca.UserName == "" ? ca.NUserName : ca.UserName;
                var mail = ca.EmailId == "" ? ca.NEmail : ca.EmailId;
                var contact = ca.ContactNo == "" ? ca.NPhone : ca.ContactNo;
                strUserDetails += @"<tr>
                                       <td>" + (i + 1) + @"</td>  
                                       <td>"+ca.ProductName+@"</td>
                                       <td>" + name + @"</td>
                                        <td><a href='mailto:mail'>" + mail + @"</a></td>
                                        <td><a href='tel:contact'>" + contact + @"</a></td> 
                                        <td>" + ca.NMessage + @"</td> 
                                        <td>" + ca.AddedOn.ToString("dd MMM yyyy hh:mm tt") + @"</td>  
                               <td class='text-center'> 
                                            <a href = 'javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" +ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'><i class='mdi mdi-trash-can-outline'></i></a> 
                                         </a>
                                        </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUsers", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteNotifyMe(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-notifyme.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductNotifyMe pro = new ProductNotifyMe();
                pro.Id = Convert.ToInt32(id);
                int exec = ProductNotifyMe.DeleteProductNotifyme(conAP, pro);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateReview", ex.Message);
        }
        return x;
    }
}