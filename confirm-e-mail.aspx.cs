using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class email_verify : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<UserDetails> ud = UserDetails.GetLoggedUser(conAP, Request.QueryString["u"]);
            if (ud.Count > 0)
            {
                if (ud[0].Status == "Unverified")
                {
                    int exec = UserDetails.UpdateAsVerifed(conAP, Request.QueryString["u"]);
                    if (exec > 0)
                    {
                        Emails.SendRegistered(ud[0].FirstName, ud[0].EmailId);
                        strStatus = "<div class='text-center statusImgBox'><img src='/images_/thank-you-tick.gif' class='img-fluid' /></div><h3 class='main-heading text-success'>Your e-mail has been successfully verified.<br />Please  <a href='/' class='text-danger' >click here</a> to Proceed.</h3>";
                    } 
                    else
                    {
                        strStatus = "<div class='text-center statusImgBox'><img src='/images_/error-img.gif' class='img-fluid' /></div><h3 class='main-heading text-danger'>There is a problem. Please try again after some time.</h3>";
                    }
                }
                else
                {
                    strStatus = "<div class='text-center statusImgBox'><img src='/images_/error-img.gif' class='img-fluid' /></div><h3 class='main-heading text-danger'>Invalid link. Please try another link</h3>";
                }
            }
            else
            {
                strStatus = "<div class='text-center statusImgBox'><img src='/images_/error-img.gif' class='img-fluid' /></div><h3 class='main-heading text-danger'>Invalid link. Please try another link</h3>";
            }
        }
        catch (Exception ex)
        {
            string s=ex.Message;
            strStatus = "<h3 class='main-heading text-danger'>Invalid link. Please try another link</h3>";
        }
    }
}