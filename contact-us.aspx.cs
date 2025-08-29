using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class contact_us : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                ContactUs cat = new ContactUs();
                cat.UserName = txtName.Text.Trim();
                cat.EmailId = txtEmail.Text.Trim();
                cat.ContactNo = txtPhone.Text.Trim();
                cat.Message = txtMessage.Text.Trim();
                int result = ContactUs.InserContactUs(conAP, cat);
                if (result > 0)
                {
                    txtName.Text = txtEmail.Text  = txtMessage.Text = "";
                    Emails.ContactRequest(cat);
                    Emails.ContactUSRequestToCustomer(txtName.Text.Trim(), txtEmail.Text.Trim());
                    Response.Redirect("/thank-you");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your query posted successfully. We will get back you soon...',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

}