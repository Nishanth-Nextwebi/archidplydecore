using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_change_password : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please wait..'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
    }

    public void ClearAllUserSession()
    {
        try
        {
            Session.Abandon();
            Session.Clear();
            if (Request.Cookies["ap_aid"] != null)
            {
                Response.Cookies["ap_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
            } 
            if (Request.Cookies["ap_apkv"] != null)
            {
                Response.Cookies["ap_apkv"].Expires = TimeStamps.UTCTime().AddDays(-10);
            }
        }
        catch (Exception ex)
        {

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
                if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                {

                    CreateUser inputs = new CreateUser();
                    inputs.UserId = Request.Cookies["ap_aid"].Value;
                    inputs.Password = CommonModel.Encrypt(txtCurrent.Text.Trim());
                    CreateUser logins = CreateUser.Login2(conAP, inputs);
                    if (logins.UserGuid != null)
                    {
                        if (logins.Status == "Active")
                        {
                            string status = CreateUser.ChangePassword(conAP, logins.UserGuid, CommonModel.Encrypt(txtNew.Text.Trim()));
                            if (status == "Success")
                            {
                                ClearAllUserSession();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Password changed successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Current Password incorrect',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
}