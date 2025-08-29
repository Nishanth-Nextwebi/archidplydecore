using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reset_password : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["c"] == null)
            {
                Response.Redirect("/404");
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;
                string ForgotId = Request.QueryString["c"];
                string UserGuid = UserDetails.CheckPasswordResetId(conAP, ForgotId);


                if (UserGuid != "")
                {
                    string Pwd = UserDetails.GetPasswordWithUserGuid(conAP, UserGuid);
                    if (Pwd != "")
                    {
                        string oldPwd = CommonModel.Decrypt(Pwd);
                        if (txtPwd.Text.Trim() == oldPwd)
                        {
                            lblStatus.Text = "New password can't be same as the previous password!";
                            lblStatus.Attributes.Add("class", "alert alert-danger");
                            return;
                        }

                    }


                    string res = UserDetails.PasswordReset(conAP, CommonModel.Encrypt(txtPwd.Text.Trim()), UserGuid);
                    if (res == "Updated")
                    {
                        lblStatus.Text = "Password updated successfully!";
                        lblStatus.Attributes.Add("class", "alert alert-success");
                    }
                    else
                    {
                        lblStatus.Text = "There is some problem. Please try after some time";
                        lblStatus.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    lblStatus.Text = "Invalid link or link as expired please try again!";
                    lblStatus.Attributes.Add("class", "alert alert-danger");
                }


            }
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "There is some problem. Please try after some time";
            lblStatus.Attributes.Add("class", "alert alert-danger");
            CommonModel.CaptureException(Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
        }
    }
}