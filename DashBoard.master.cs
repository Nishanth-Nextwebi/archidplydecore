using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashBoard : System.Web.UI.MasterPage
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProfileimg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPassKeyChanged();
        GetUserDetailsByUid();
    }
     public void GetUserDetailsByUid()
    {
        try
        {
            List<UserAddress> Details = UserDetails.GetLoggedUserAddress(conAP, Convert.ToString(Request.Cookies["arch_i"].Value)).ToList();
            if (Details.Count>0)
            {
                if (Details[0].ImageUrl != "")
                {
                    strProfileimg = "<img src='/" + Details[0].ImageUrl + "' style='height:40px; width:40px;' class='rounded-pill'/>";
                }
                /*else
                {
                    strProfileimg = "<img src='../assets/imgs/avatar-1.png' style='height:40px; width:40px;' class='rounded-pill'/>";
                }*/

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllMemberDetails", ex.Message);
        }
    }
    public void CheckPassKeyChanged()
    {
        try
        {
            if (Request.Cookies["arch_pkey"] != null)
            {
                string stored_pass_key = Request.Cookies["arch_pkey"].Value;
                var udetails = UserDetails.GetUserDetailsByID(conAP, Request.Cookies["arch_i"].Value);
                if (udetails != null)
                {
                    string current_pass_key = udetails.PassKey;

                    if (current_pass_key != stored_pass_key)
                    {
                        Session.Abandon();
                        Session.Clear();
                        if (Request.Cookies["arch_i"] != null)
                        {
                            Response.Cookies["arch_i"].Expires = TimeStamps.UTCTime().AddDays(-10);
                        }
                        if (Request.Cookies["arch_pkey"] != null)
                        {
                            Response.Cookies["arch_pkey"].Expires = TimeStamps.UTCTime().AddDays(-10);
                        }
                        Response.Redirect("/");
                    }
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    if (Request.Cookies["arch_i"] != null)
                    {
                        Response.Cookies["arch_i"].Expires = TimeStamps.UTCTime().AddDays(-10);
                    }
                    if (Request.Cookies["arch_pkey"] != null)
                    {
                        Response.Cookies["arch_pkey"].Expires = TimeStamps.UTCTime().AddDays(-10);
                    }
                    Response.Redirect("/");
                }
            }
            else
            {
                Session.Abandon();
                Session.Clear();
                if (Request.Cookies["arch_i"] != null)
                {
                    Response.Cookies["arch_i"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                if (Request.Cookies["arch_pkey"] != null)
                {
                    Response.Cookies["arch_pkey"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                Response.Redirect("/");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckPassKeyChanged", ex.Message);

        }
    }
}
