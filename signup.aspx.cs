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
public partial class signup : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["arch_i"] != null)
        {
            Response.Redirect("/my-profile");
        }
    }
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblSighUpStatus.Visible = true;
                if (!chkAgreePolicy.Checked)
                {
                    lblSighUpStatus.Text = "Please accept the terms and condition.";
                    lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please accept the terms and condition.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                //Random rnd = new Random();
                //int otp = rnd.Next(100000, 999999);
                //Session["otp"] = otp;
                string uGuid = Guid.NewGuid().ToString();
                if (CheckEmail() == false)
                {
                    string orid = UserDetails.GetMaxId(conAP);
                    txtUGuid.Value = uGuid;
                    UserDetails ud = new UserDetails();
                    ud.UserGuid = uGuid;
                    ud.Gender = "";
                    ud.FirstName = txtFirstName.Text.Trim();
                    ud.LastName = txtLastName.Text.Trim();
                    ud.ContactNo = txtSignUpMobileNo.Text.Trim();
                    ud.EmailId = txtSignUpEmail.Text.Trim();
                    ud.Password = CommonModel.Encrypt(txtSignUpPassword.Text.Trim());
                    ud.ForgotId = "";
                    ud.CustomerId = "APDUSER0"+ orid;
                    ud.PassKey = Guid.NewGuid().ToString();
                    ud.Status = "Unverified";
                    int exUD = UserDetails.CreateUser(conAP, ud);
                    if (exUD > 0)
                    {
                        UserAddress ua = new UserAddress();
                        ua.FirstName = txtFirstName.Text.Trim();
                        ua.LastName = txtLastName.Text.Trim();
                        ua.Phone = txtSignUpMobileNo.Text.Trim();
                        ua.Email = txtSignUpEmail.Text.Trim();
                        ua.ShortName = "My Address";
                        ua.Status = "Active";
                        ua.UserGuid = uGuid;
                        ua.Zip = "";
                        ua.AddressLine1 = "";
                        ua.AddressLine2 = "";
                        ua.City = "";
                        ua.State = "";
                        ua.Country = "India";
                        int exAD = UserDetails.AddUserAddress(conAP, ua);
                        if (exAD > 0)
                        {
                            lblSighUpStatus.Text = "Email verification link is sent Please confirm your email address to complete sign up process.";
                            lblSighUpStatus.Attributes.Add("class", "alert alert-success d-block");
                            Emails.SendEmailVerifyLink(txtSignUpEmail.Text, txtFirstName.Text, ConfigurationManager.AppSettings["domain"] + "/confirm-e-mail.aspx?u=" + uGuid);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Email verification link is sent Please confirm your email address to complete sign up process.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            txtFirstName.Text = txtSignUpEmail.Text = txtSignUpMobileNo.Text = txtLastName.Text = txtSignUpPassword.Text = "";

                        }
                        else
                        {
                            lblSighUpStatus.Text = "There is some problem now. Please try after some time.";
                            lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblSighUpStatus.Text = "There is some problem now. Please try after some time.";
            lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
        }
    }
    public bool CheckEmail()
    {
        bool result = false;
        try
        {
            lblSighUpStatus.Visible = true;
            UserDetails ud = UserDetails.CheckUserByEmail(conAP, txtSignUpEmail.Text.Trim());
            if (ud.UserGuid != null)
            {
                if (ud.Status == "Unverified")
                {
                    lblSighUpStatus.Text = "Please confirm your email address to complete sign up process.";
                    lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please confirm your email address to complete sign up process.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    Emails.SendEmailVerifyLink(txtSignUpEmail.Text.Trim(), ud.FirstName, ConfigurationManager.AppSettings["domain"] + "/confirm-e-mail.aspx?u=" + ud.UserGuid);
                    result = true;
                }
                else
                {
                    lblSighUpStatus.Text = "Email Already registered. Try with a different email id.";
                    lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Email Already registered. Try with a different email id.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindYears", ex.Message);
        }
        return result;
    }
}