using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;


public partial class Admin_view_enquiry_products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProductsList = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllProduct();
    }

    public void GetAllProduct()
    {
        try
        {
            strProductsList = "";
            List<EnquiryProduct> productDetails = EnquiryProduct.GetAllEnquiryProduct(conAP).OrderByDescending(s => s.UpdatedOn).ToList();
            int i = 0;
            foreach (EnquiryProduct pro in productDetails)
            {
                string ft1 = pro.Status == "Active" ? "checked" : "";
                string sts = pro.Status == "Active" ? "<span id='sts_" + pro.Id + @"' class='badge badge-outline-success shadow fs-13'>Published</span>" : "<span id='sts_" + pro.Id + @"' class='badge badge-outline-warning shadow fs-13'>Draft</span>";
                string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox' data-id='" + pro.Id + @"' class='form-check-input publishProduct' id='chk_" + pro.Id + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";

                strProductsList += @"<tr>
                                        <td>" + (i + 1) + @"</td>
                                        <td><a target='_blank' class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Preview Product' href='/products/" + pro.ProductUrl + @"'>" + pro.ProductName + @"</a></td>
                                        <td>" + pro.CategoryName + @"</td>
                                        <td class='text-center'><a class='bs-tooltip' data-id='" + pro.Id + @"' data-toggle='tooltip' data-placement='top' title='' data-original-title='Add/Gallery' href='enquiry-product-gallery.aspx?id=" + pro.ProductGuid + @"'><img src='assets/images/image-gallery.png' width='30' height='30' /></a></td>
                                        <td class='text-center'><a class='' data-id='" + pro.Id + @"' data-toggle='tooltip' data-placement='top' title='' data-original-title='click' href='product-specifiaction.aspx?id=" + pro.Id + @"'><img src='assets/images/features.png' width='30' height='30' /></a></td>
                                        <td>" + sts + @"</td>
                                        <td><span class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + pro.UpdatedBy + @"' >" + pro.UpdatedOn.ToString("dd/MMM/yyyy") + @"</td>
                                        <td>" + chk + @"</td>
                                        <td class='text-center'> 
<a href='add-enquiry-products.aspx?id=" + pro.Id + @"' class='bs-tooltip fs-18' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                        </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProducts", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-enquiry-products.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                EnquiryProduct pro = new EnquiryProduct();
                pro.Id = Convert.ToInt32(id);
                pro.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                pro.Status = "Deleted";
                int exec = EnquiryProduct.DeleteEnquiryProduct(conAP, pro);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }


    [WebMethod(EnableSession = true)]
    public static string PublishProduct(string id, string ftr)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-enquiry-products.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                EnquiryProduct cat = new EnquiryProduct();
                cat.Id = Convert.ToInt32(id);
                cat.Status = ftr == "Yes" ? "Active" : "Draft";
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = EnquiryProduct.DeleteEnquiryProduct(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "PublishProduct", ex.Message);
        }
        return x;
    }
}