using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_product_faqs : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProductFAQss = "", strThumbImage = "", strProductName = "", strProductId = "";
    public List<ProductFAQs> ProductFAQss = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetProductDetails();
        if (Request.QueryString["id"] != null)
        {
            GetAllProductFAQss();
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["fid"] != null)
            {
                GetProductFAQs();
            }
        }
    }

    public void GetAllProductFAQss()
    {
        try
        {
            strProductFAQss = "";
            ProductFAQss = ProductFAQs.GetAllFAQS(conAP, strProductId);
            int i = 0;
            foreach (ProductFAQs cat in ProductFAQss)
            {
                strProductFAQss += @"<tr>
                                                <td>" + (i + 1) + @"</td>
<td>" + cat.Question + @"</td> 
                                                <td>" + cat.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                                                <td class='text-center'>
<a href='Product-faqs.aspx?fid=" + cat.Id + "&id=" + Request.QueryString["id"] + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                          </td>

                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductFAQss", ex.Message);
        }
    }

    public void GetProductFAQs()
    {
        try
        {
            List<ProductFAQs> faqs = ProductFAQs.GetAllFAQSNyId(conAP, Convert.ToInt32(Request.QueryString["fid"]));//.Where(x => x.Id == Convert.ToInt32(Request.QueryString["fid"])).ToList();
            if (faqs.Count > 0)
            {
                btnSave.Text = "Update";
                txtTitle.Text = faqs[0].Question;
                txtDesc.Text = faqs[0].Answer;

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductFAQs", ex.Message);
        }
    }

    public void GetProductDetails()
    {
        try
        {
            ProductDetails pro = ProductDetails.GetProductNameByGuid(conAP, Request.QueryString["id"]);
            if (pro != null)
            {
                strProductId = pro.Id.ToString();
                strProductName = pro.ProductName;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductDetails", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            string pageName = Path.GetFileName(Request.Path);
            if (Page.IsValid)
            {
                ProductFAQs cat = new ProductFAQs();
                if (btnSave.Text == "Update")
                {

                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        cat.Id = Convert.ToInt32(Request.QueryString["fid"]);
                        cat.ProductId = strProductId;
                        cat.Question = txtTitle.Text.Trim();
                        cat.Answer = txtDesc.Text.Trim();
                        cat.AddedBy = Request.Cookies["ap_aid"].Value;
                        cat.AddedOn = CommonModel.UTCTime();
                        cat.Status = "Active";
                        int result = ProductFAQs.UpdateProductFAQs(conAP, cat);
                        if (result > 0)
                        {
                            GetAllProductFAQss();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product FAQ updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                        cat.ProductId = strProductId;
                        cat.Question = txtTitle.Text.Trim();
                        cat.Answer = txtDesc.Text.Trim();
                        cat.AddedBy = Request.Cookies["ap_aid"].Value;
                        cat.AddedOn = CommonModel.UTCTime();
                        cat.Status = "Active";
                        int result = ProductFAQs.InsertProductFAQs(conAP, cat);
                        if (result > 0)
                        {
                            GetAllProductFAQss();
                            txtTitle.Text = txtDesc.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product FAQ added successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "Product-faqs.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductFAQs cat = new ProductFAQs();
                cat.Id = Convert.ToInt32(id);
                cat.AddedOn = CommonModel.UTCTime();
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ProductFAQs.DeleteProductFAQs(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteP", ex.Message);
        }
        return x;
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        txtDesc.Text = "";
    }

    }