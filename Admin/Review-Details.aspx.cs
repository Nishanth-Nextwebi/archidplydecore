using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Review_Details : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strReviewDetails = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllReviews();
        }
        
    }
    public void GetAllReviews()
    {
        try
        {
            strReviewDetails = "";
            List<ProductReviews> cas = ProductReviews.GetAllProductReviews(conAP).OrderByDescending(s => s.AddedOn).ToList();
            int i = 0;
            foreach (ProductReviews ca in cas)
            {
                var gallery = "";
                if (ca.ReviewUrl != "")
                {
                    int a = 1;
                    foreach (string url in ca.ReviewUrl.Split('|'))
                    {
                        var txt = "Image" + a;
                        if (url.Split('.')[1].ToLower() == "mp4")
                        {
                            txt = "Video" + a;
                        }
                        gallery += @"<a target='_blank' href='/" + url + @"'>" + txt + @"</a><br/>";
                        a++;
                    }
                }
                else
                {
                    gallery = "No Gallery";
                }

            
                var ft1 = ca.ReviewFeatured == "Yes" ? "checked" : "";
                string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox' data-id='" + ca.Id + @"' class='form-check-input ReviewFeatured' id='chkRev_" + ca.Id + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";
                var sts = ca.Status == "Approved" ? "<span id='sts_" + ca.Id + @"' class='badge badge-outline-success shadow fs-13'>Approved</span>" : ca.Status == "Active" ? "<span id='sts_" + ca.Id + @"' class='badge badge-outline-primary shadow fs-13'>Active</span>" : "<span id='sts_" + ca.Id + @"' class='badge badge-outline-danger shadow fs-13'>Rejected</span>";
                
                strReviewDetails += @"<tr>
                                       <td>" + (i + 1) + @"</td>  
                                       <td><a href='/shop-products/" + ca.ProductUrl + @"' class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='View Product' target='_blank'>" + ca.ProductName + @"</a></td>
                                           <td>" + ca.UserName + @"</td>
                                            <td>" + ca.Rating + @"</td>
                                <td><a href='javascript:void(0);' data-bs-toggle='modal' data-bs-target='#fadeInRightModal' class='btn btn-sm btn-secondary badge-gradient-secondary btnmsg' data-id=" + ca.Id + @" data-name=" + ca.UserName + @">View Message</a></td>
                                                                                    <td>" + gallery + @"</td>
                                            <td>" + sts + @"</td>
                                            <td>" + chk + @"</td> 
                                            <td>" + ca.AddedOn.ToString("dd MMM yyyy hh:mm tt") + @"</td>
                                               <td class='text-center'>
<a href='javascript:void(0);' class='text-success btn-approve bs-tooltip fs-18'  data-id='" + ca.Id + @"' data-bs-toggle='tooltip' title='Approve'><i class='mdi mdi-checkbox-marked-circle-outline text-success'></i></a>
<a href='javascript:void(0);' class='text-danger btn-reject bs-tooltip fs-18'  data-id='" + ca.Id + @"' data-bs-toggle='tooltip' title='Reject'><i class='mdi mdi-cancel'></i></a>
                                                     <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a></td>
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
    public static string ReviewFeatured(string id, string ftr)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "Review-Details.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductReviews cat = new ProductReviews();
                cat.Id = Convert.ToInt32(id);
                cat.ReviewFeatured = ftr == "Yes" ? "Yes" : "No";
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ProductReviews.ChangeReviewFeatured(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ReviewFeatured", ex.Message);
        }
        return x;
    }
    [WebMethod(EnableSession = true)]
    public static string UpdateReview(string id, string sts)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "Review-Details.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                var status = "";
                switch (sts)
                {
                    case "Reject": status = "Rejected"; break;
                    case "Approve": status = "Approved"; break;
                    case "Delete": status = "Deleted"; break;
                }
                ProductReviews pro = new ProductReviews();
                pro.Id = Convert.ToInt32(id);
                pro.Status = status;
                int exec = ProductReviews.UpdateProductReviews(conAP, pro);
                if (exec > 0)
                {
                    x = "S";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "P";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateReview", ex.Message);
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
            message = ProductReviews.GetMessageById(conAP, id);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMessage", ex.Message);
        }
        return message;
    }
}