using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class forgot_password : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                string logins = UserDetails.GetUserGuidWithEmail(conAP, txtBoxEmail.Text.Trim());
                if (logins != "")
                {
                    string r_id = Guid.NewGuid().ToString();
                    int reset = UserDetails.SetForgotUserId(conAP, logins, r_id);
                    Emails.SendPasswordResetUser(UserDetails.GetUserNameWithGuid(conAP, logins), txtBoxEmail.Text.Trim(), r_id);
                    if (reset >= 1)
                    {
                        lblStatus.Text = "Password reset link has been sent to your email address.";
                        lblStatus.Attributes.Add("class", "alert alert-success d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Password reset link has been sent to your email address.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    }
                    else
                    {
                        lblStatus.Text = "There is some problem now. Please try after some time.";
                        lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
                else
                {
                    lblStatus.Text = "Entered email is not registered.";
                    lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Entered email is not registered.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "There is some problem now. Please try after some time.";
            lblStatus.Attributes.Add("class", "alert alert-danger d-block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
        }
    }

}