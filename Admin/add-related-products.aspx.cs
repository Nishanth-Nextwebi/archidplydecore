using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_related_products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            BindCategory();
        }
    }
    public void BindCategory()
    {
        try
        {
            List<SubCategory> comps = SubCategory.GetAllSubCategory(conAP).OrderBy(s => s.SubCategoryName).ToList();
            DropDownList1.Items.Clear();
            if (comps.Count > 0)
            {
                DropDownList1.DataSource = comps;
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataTextField = "SubCategoryName";
                DropDownList1.DataBind();
            }
            DropDownList1.Items.Insert(0, new ListItem("Select Category", "0"));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindCategory", ex.Message);
        }
    }
    public void BindProducts()
    {
        try
        {
         List<ProductDetails> comps = ProductDetails.GetAllProducts(conAP).Where(s => s.CategoryName.ToLower() == DropDownList1.SelectedItem.Text.ToLower() && s.Id != Convert.ToInt32(ddlProduct.SelectedValue)).OrderBy(s => s.ProductName).ToList();
            ddlProduct.Items.Clear();
            if (comps.Count > 0)
            {
                ddlProduct.DataSource = comps;
                ddlProduct.DataValueField = "Id";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindProducts", ex.Message);
        }
    }
    public void BindMapProducts()
    {
        try
        {
            List<ProductDetails> comps = ProductDetails.GetAllProducts(conAP).Where(s => s.CategoryName.ToLower() == DropDownList1.SelectedItem.Text.ToLower() && s.Id != Convert.ToInt32(ddlProduct.SelectedValue)).OrderBy(s => s.ProductName).ToList();
            chkMapProducts.Items.Clear();
            if (comps.Count > 0)
            {
                chkMapProducts.DataSource = comps;
                chkMapProducts.DataValueField = "Id";
                chkMapProducts.DataTextField = "ProductName";
                chkMapProducts.DataBind();
            }
            chkMapProducts.Items.Insert(0, new ListItem("Select MapProducts", "0"));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindProducts", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ProductDetails pd = new ProductDetails();
        try
        {
            if (Page.IsValid)
            {
                if (CreateUser.CheckAccess(conAP, "add-related-products.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
                {
                    string mappedProducts = "";
                    foreach (ListItem li in chkMapProducts.Items)
                    {
                        if (li.Selected == true)
                        {
                            mappedProducts += li.Value + "|";
                        }
                    }
                    pd.Id = Convert.ToInt32(ddlProduct.SelectedValue);
                    pd.RelatedProducts = mappedProducts.TrimEnd('|');
                    int exec = ProductDetails.UpdateAlternateProducts(conAP, pd);
                    if (exec > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Products Mapped successfully',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindMapProducts();
            List<ProductDetails> products = ProductDetails.GetProductDetailsByDDlID(conAP,ddlProduct.SelectedValue);//.Where(x => x.Id == Convert.ToInt32(ddlProduct.SelectedValue)).ToList();
            if (products.Count > 0)
            {
                string[] tags = products[0].RelatedProducts.ToString().Split('|');
                if (tags[0] != "")
                {
                    foreach (string tag in tags)
                    {
                        foreach (ListItem li in chkMapProducts.Items)
                        {
                            if (li.Value.Trim() == tag.Trim())
                            {
                                li.Selected = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ddlProduct_SelectedIndexChanged", ex.Message);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindProducts();
            //BindMapProducts();
        }
        catch (Exception es)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DropDownList1_SelectedIndexChanged", es.Message);
        }
    }
}