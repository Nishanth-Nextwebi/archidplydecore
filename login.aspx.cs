using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["arch_i"] != null)
        {
            Response.Redirect("/my-profile");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblLoginStatus.Visible = true;
                UserDetails ud = UserDetails.UserLoginWithEmail(conAP, txtEmail.Text.Trim(), CommonModel.Encrypt(txtPassword.Text.Trim()));
                if (ud.UserGuid != null)
                {
                    if (ud.Status == "Verified")
                    {
                        string uid = HttpContext.Current.Request.Cookies["arch_v"].Value;
                        UserDetails.UpdateLastLogDetails(conAP, ud.UserGuid);
                        lblLoginStatus.Text = "Logged-in successfully.";
                        lblLoginStatus.Attributes.Add("class", "alert alert-success d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Logged-in successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        HttpCookie cookie = new HttpCookie("arch_i");

                        cookie.Value = Convert.ToString(ud.UserGuid);
                        if (chkStaySignedIn.Checked)
                        {
                            cookie.Expires = CommonModel.UTCTime().AddDays(30);

                        }

                        HttpCookie cookie_pass_key = new HttpCookie("arch_pkey");
                        cookie_pass_key.Value = Convert.ToString(ud.PassKey);
                        Response.Cookies.Add(cookie_pass_key);
                        Response.Cookies.Add(cookie);
                        CartDetails.UpdateCartAfterLogin(conAP, uid, ud.UserGuid);
                        Response.Redirect("/my-profile");
                    }
                    else if (ud.Status == "Blocked")
                    {
                        lblLoginStatus.Text = "Your profile is temporarily blocked. Please contact admin.";
                        lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your profile is temporarily blocked. Please contact admin.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                    else
                    {
                        lblLoginStatus.Text = "Your email address is not verified.";
                        lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your email address is not verified.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {

                    lblLoginStatus.Text = "E-mail address or password is incorrect.";
                    lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'E-mail address or password is incorrect.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblLoginStatus.Text = "There is some problem now. Please try after some time.";
            lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
        }
    }


}