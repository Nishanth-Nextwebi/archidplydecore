using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class change_password : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnchnagePwd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;
                UserDetails inputs = new UserDetails();
                inputs.UserGuid = Convert.ToString(Request.Cookies["arch_i"].Value);
                inputs.Password = CommonModel.Encrypt(txtCurrent.Text.Trim());
                UserDetails logins = UserDetails.ChangeLogin(conAP, inputs);
                if (logins.UserGuid != null)
                {
                    string status = UserDetails.ChangePassword(conAP, logins.UserGuid, CommonModel.Encrypt(txtNew.Text.Trim()));
                    if (status == "Success")
                    {
                        lblStatus.Text = "Password changed successfully.";
                        lblStatus.Attributes.Add("class", "alert alert-success d-block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Password changed successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
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
                    lblStatus.Text = "Current Password incorrect.";
                    lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Current Password incorrect.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "There is some problem now. Please try after some time.";
            lblStatus.Attributes.Add("class", "alert alert-danger d-block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
        }
    }
}