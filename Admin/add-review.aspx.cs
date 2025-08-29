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
public partial class Admin_add_review : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strColors = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        GetAllReviews();
        if (!IsPostBack)
        {
            BindProducts();

            if (Request.QueryString["id"] != null)
            {
                GetReview();
            }
        }
    }

    public void BindProducts()
    {
        try
        {
            List<ProductDetails> comps = ProductDetails.GetAllProductsToddl(conAP);
            if (comps.Count > 0)
            {
                ddlProduct.Items.Clear();
                ddlProduct.DataSource = comps;
                ddlProduct.DataValueField = "Id";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, new ListItem("Select Product", ""));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindProducts", ex.Message);
        }
    }
    public void GetAllReviews()
    {
        try
        {
            strColors = "";
            List<ProductReviews> couponCodes = ProductReviews.GetAllProductReviews(conAP).OrderByDescending(s => s.Id).ToList();
            int i = 0;
            foreach (ProductReviews pr in couponCodes)
            {
                strColors += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + pr.ProductName + @"</td>
                                                <td>" + pr.UserName + @"</td>
                                                <td>" + pr.MobileNo + @"</td>
                                                    <td>" + pr.EmailId + @"</td>
                                <td><a href='javascript:void(0);' data-bs-toggle='modal' data-bs-target='#fadeInRightModal' class='btn btn-sm btn-secondary badge-gradient-secondary btnmsg' data-id=" + pr.Id + @" data-name=" + pr.UserName + @">View Message</a></td>

                                                  <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Added By : " + pr.AddedBy + @"' >" + pr.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>  
                                                <td class='text-center'> 
                                            <a href='add-review.aspx?id=" + pr.Id + @"' class='bs-tooltip fs-18' data-id='" + pr.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pr.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                        </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllReviews", ex.Message);
        }
    }
    public void GetReview()
    {
        try
        {
            List<ProductReviews> pr = ProductReviews.GetProductReviewsById(conAP, Request.QueryString["id"]);
            if (pr.Count > 0)
            {
                btnSave.Text = "Update";
                addCoupon.Visible = true;
                txtName.Text = pr[0].UserName;
                txtSubject.Text = pr[0].Subject;
                txtBoxEmail.Text = pr[0].EmailId;
                txtPhone.Text = pr[0].MobileNo;
                txtMessage.Text = pr[0].Message;
                txtAddedOn.Text = pr[0].AddedOn.ToString("dd/MMM/yyyy");
                ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(pr[0].ProductId));
                ddlRating.SelectedIndex = ddlRating.Items.IndexOf(ddlRating.Items.FindByValue(pr[0].Rating));
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCoupon", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                lblStatus.Visible = true;

                string pageName = Path.GetFileName(Request.Path);
              
                    ProductReviews pr = new ProductReviews();
                pr.UserName = txtName.Text.Trim();
                pr.EmailId = txtBoxEmail.Text.Trim();
                pr.Subject = txtSubject.Text.Trim();
                pr.MobileNo = txtPhone.Text.Trim();
                pr.Message = txtMessage.Text.Trim();
                pr.Rating = ddlRating.SelectedValue;
                pr.Status = "Approved";
                pr.AddedOn = Convert.ToDateTime(txtAddedOn.Text.Trim());
                pr.AddedBy = Request.Cookies["ap_aid"].Value;
                pr.ReviewFeatured = "Yes";
                pr.ProductId = ddlProduct.SelectedValue;
                pr.ImageUrl = "";
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {


                        pr.Id = Convert.ToInt32(Request.QueryString["id"]);
                        int result = ProductReviews.UpdateReviews(conAP, pr);
                        if (result > 0)
                        {
                            GetAllReviews();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Review updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                                     }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
                        pr.AddedBy = Request.Cookies["ap_aid"].Value;

                        int result = ProductReviews.InsertProductReviews(conAP, pr);
                        if (result > 0)
                        {
                            GetAllReviews();
                            txtName.Text = txtPhone.Text = txtMessage.Text = txtBoxEmail.Text = "";
                            ddlRating.SelectedIndex = ddlProduct.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Review added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
            lblStatus.Text = "There is some problem now. Please try after some time";
            lblStatus.Attributes.Add("class", "alert alert-danger");
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
            if (CreateUser.CheckAccess(conAP, "add-review.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductReviews cat = new ProductReviews();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                cat.Status = "Deleted";
                int exec = ProductReviews.DeleteProductReviews(conAP, cat);
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
    protected void addCoupon_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-review.aspx");
    }
}