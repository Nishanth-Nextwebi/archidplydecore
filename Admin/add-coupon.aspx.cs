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

public partial class Admin_add_coupon : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strColors = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        GetAllCoupon();
        if (!IsPostBack)
        {
            txtDate.Attributes.Add("min", CommonModel.UTCTime().ToString("yyyy-MM-dd"));
            if (Request.QueryString["id"] != null)
            {
                GetCoupon();
            }
        }
    }
    public void GetAllCoupon()
    {
        try
        {
            strColors = "";
            List<CouponCode> couponCodes = CouponCode.GetAllCoupon(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (CouponCode coupon in couponCodes)
            {
                var totalUsed = UserCheckout.GetCouponUsed(conAP, coupon.CCode);
                strColors += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + coupon.CType + @"</td>
                                                <td>" + coupon.CValue + @"</td>
                                                <td>" + coupon.NoOfUsage + @"</td>
                                                <td>" + totalUsed + @"</td>
                                                <td>" + coupon.CustomerId + @"</td>
                                                <td>" + coupon.CCode + @"</td>
                                                <td>" + coupon.ExpiryDate.ToString("dd/MMM/yyyy") + @"</td>
                                                  <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Added By : " + coupon.AddedBy + @"' >" + coupon.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>  
                                                <td class='text-center'>
 <a href='add-coupon.aspx?id=" + coupon.Id + @"' class='bs-tooltip fs-18' data-id='" + coupon.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + coupon.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>

                                                     </td>

                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCoupon", ex.Message);
        }
    }
    public void GetCoupon()
    {
        try
        {
            List<CouponCode> coupon = CouponCode.GetCouponByCID(conAP, Request.QueryString["id"].ToString());//.Where(s => s.Id == Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (coupon.Count > 0)
            {
                btnSave.Text = "Update";
                addCoupon.Visible = true;
                txtCode.Text = coupon[0].CCode;
                txtDate.Text = coupon[0].ExpiryDate.ToString("dd/MMM/yyyy");
                txtValue.Text = coupon[0].CValue;
                txtNoOfUsage.Text = Convert.ToString(coupon[0].NoOfUsage);
                txtCustomerId.Text = coupon[0].CustomerId=="All"?"" : coupon[0].CustomerId;
                txtDesc.Text = coupon[0].CDesc;
                txtMinValue.Text = Convert.ToString(coupon[0].minCartVal);
                ddlCouponType.SelectedIndex = ddlCouponType.Items.IndexOf(ddlCouponType.Items.FindByText(coupon[0].CType));
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
                if (ddlCouponType.SelectedItem.Text == "Percentage")
                {
                    if (Convert.ToInt32(txtValue.Text) > 99)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Percentage of offer cannot exceed 99%',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        return;
                    }
                }
                if (Convert.ToInt32(txtValue.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Coupon value cannot be less than 1',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
               
                var customers = UserDetails.GetAllCustomerIds(conAP, txtCustomerId.Text.Trim());
                if (customers == null)
                {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Enter CustomerId is not exists',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                }

                    string pageName = Path.GetFileName(Request.Path);
                CouponCode coupon = new CouponCode();
                coupon.CType = ddlCouponType.SelectedItem.Text;
                coupon.CCode = txtCode.Text;
                coupon.NoOfUsage = Convert.ToInt32(txtNoOfUsage.Text);
                coupon.CustomerId = txtCustomerId.Text.Trim()==""?"All": txtCustomerId.Text.Trim();
                coupon.CValue = txtValue.Text;
                coupon.CDesc = txtDesc.Text;
                coupon.minCartVal = Convert.ToDecimal(txtMinValue.Text);
                coupon.ExpiryDate = txtDate.Text == "" ? CommonModel.UTCTime() : Convert.ToDateTime(txtDate.Text);
                coupon.UpdatedBy = Request.Cookies["ap_aid"].Value;

                List<CouponCode> res = CouponCode.GetCouponByCode(conAP, txtCode.Text.Trim().ToLower());//.Where(s => s.CCode.ToLower().ToString() == txtCode.Text.Trim().ToLower().ToString()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Coupon already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                        else
                        {
                            coupon.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = CouponCode.UpdateCouponCode(conAP, coupon);
                            if (result > 0)
                            {
                                GetAllCoupon();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Coupon updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            }
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
                        coupon.AddedBy = Request.Cookies["ap_aid"].Value;
                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Coupon already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                        else
                        {
                            int result = CouponCode.InsertCouponCode(conAP, coupon);
                            if (result > 0)
                            {
                                GetAllCoupon();
                                txtDate.Text = txtCode.Text = txtValue.Text = txtMinValue.Text = txtDesc.Text = ddlCouponType.SelectedValue = txtNoOfUsage.Text = txtCustomerId.Text = "";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Coupon updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);


                            }
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
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
            if (CreateUser.CheckAccess(conAP, "add-coupon.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                CouponCode cat = new CouponCode();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = CouponCode.DeleteCouponCode(conAP, cat);
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

    protected void addCoupon_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-coupon.aspx");
    }
}