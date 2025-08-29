using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProductsList = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        GetAllProducts();
    }

    public void GetAllProducts()
    {
        try
        {
            strProductsList = "";
            if (!CreateUser.CheckAccess(conAP, "view-products.aspx", "View", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }
            else
            {
                List<ProductDetails> productDetails = ProductDetails.GetAllProducts(conAP).OrderByDescending(s => s.UpdatedOn).ToList();
                int i = 0;
                foreach (ProductDetails pro in productDetails)
                {
                    string ft1 = pro.Status == "Active" ? "checked" : "";
                    string sts = pro.Status == "Active" ? "<span id='sts_" + pro.Id + @"' class='badge badge-outline-success shadow fs-13'>Published</span>" : "<span id='sts_" + pro.Id + @"' class='badge badge-outline-warning shadow fs-13'>Draft</span>";
                    string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox' data-id='" + pro.Id + @"' class='form-check-input publishProduct' id='chk_" + pro.Id + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";

                    strProductsList += @"<tr>
                                        <td>" + (i + 1) + @"</td>
                                        <td><a target='_blank' class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Preview Product' href='/shop-products/" + pro.ProductUrl + @"'>" + pro.ProductName + @"</a></td>
                                        <td>" + pro.CategoryName + @"</td>
                                        <td><a class='bs-tooltip' data-id='" + pro.Id + @"' data-toggle='tooltip' data-placement='top' title='' data-original-title='Add/Manage FAQs' href='product-faqs.aspx?id=" + pro.ProductGuid + @"' ><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-help-circle'><circle cx='12' cy='12' r='10'></circle><path d='M9.09 9a3 3 0 0 1 5.83 1c0 2-3 3-3 3'></path><line x1='12' y1='17' x2='12.01' y2='17'></line></svg></a></td>
                                        <td>" + sts + @"</td>
                                        <td><span class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + pro.UpdatedBy + @"' >" + pro.UpdatedOn.ToString("dd/MMM/yyyy") + @"</td>
                                        <td>" + chk + @"</td>
                                        <td class='text-center'> 
                                            <a href='add-products.aspx?id=" + pro.Id + @"' class='bs-tooltip fs-18' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                        </td>
                                            </tr>";
                    i++;
                }
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
            if (CreateUser.CheckAccess(conAP, "view-products.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductDetails pro = new ProductDetails();
                pro.Id = Convert.ToInt32(id);
                pro.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                pro.Status = "Deleted";
                int exec = ProductDetails.DeleteProductDetails(conAP, pro);
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
            if (CreateUser.CheckAccess(conAP, "view-products.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductDetails cat = new ProductDetails();
                cat.Id = Convert.ToInt32(id);
                cat.Status = ftr == "Yes" ? "Active" : "Draft";
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ProductDetails.DeleteProductDetails(conAP, cat);
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