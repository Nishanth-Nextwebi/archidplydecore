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


public partial class my_profile : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strProfileimg="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["arch_i"] == null)
        {
            Response.Redirect("/");
        }
        if (!IsPostBack)
        {
            GetUserDetailsByUid();
        }
    }
    public void GetUserDetailsByUid()
    {
        try
        {
            List<UserAddress> Details = UserDetails.GetLoggedUserAddress(conAP, Convert.ToString(Request.Cookies["arch_i"].Value)).ToList();
            if (Details.Count>0)
            {
                txtFirstName.Text = Details[0].FirstName;
                txtLastName.Text = Details[0].LastName;
                txtEmail.Text = Details[0].Email;
                txtPhone.Text = Details[0].Phone;
                txtAdd.Text = Details[0].AddressLine1;
                if (Details[0].ImageUrl != "")
                {
                    strProfileimg = "<img src='/" + Details[0].ImageUrl + "' style='height:196px; width:196px;' class='mb-9 rounded-pill'/>";
                }
              /*  else
                {
                    strProfileimg = "<img src='../assets/imgs/avatar-1.png' style='height:196px; width:196px;' class='mb-9 rounded-pill'/>";

                }*/

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllMemberDetails", ex.Message);
        }
    }
  
    protected void UpdateProfile_Click(object sender, EventArgs e)
    {
        try
        {
            
                #region upload  Size  Profile Photo 
                string profilephoto = "";
                if (fileUploadPhoto.HasFile)
                {
                    string fileExtension = Path.GetExtension(fileUploadPhoto.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString();
                    string iconPath = Server.MapPath(".") + "\\./UploadImages\\" + ImageGuid1 + "_prof" + fileExtension;
                    if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".webp"))
                    {
                        try
                        {
                            if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblProfile.Text))))
                            {
                                File.Delete(Server.MapPath("~/" + Convert.ToString(lblProfile.Text)));
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        if (fileExtension == ".png")
                        {
                            System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(fileUploadPhoto.PostedFile.InputStream);
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                            CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                        }
                        else if (fileExtension == ".webp")
                        {
                            fileUploadPhoto.SaveAs(iconPath);
                        }
                        else
                        {
                            System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(fileUploadPhoto.PostedFile.InputStream);
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                            CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99);
                        }
                        profilephoto = "UploadImages/" + ImageGuid1 + "_prof" + fileExtension;
                    }
                    else
                    {
                        GetUserDetailsByUid();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid Profile Photo format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                }
                else
                {
                    profilephoto = lblProfile.Text;
                }
                #endregion
                UserDetails CD = new UserDetails();
                UserAddress CD1 = new UserAddress();

                CD.UserGuid = Convert.ToString(Request.Cookies["arch_i"].Value);
                CD.FirstName = txtFirstName.Text.Trim();
                CD.LastName = txtLastName.Text.Trim();
                CD.EmailId = txtEmail.Text.Trim();
                CD.ContactNo = txtPhone.Text.Trim();
                CD.Gender = "";
                CD.ImageUrl = profilephoto;
                CD.LastLoggedIn = TimeStamps.UTCTime();
                CD.LastLoggedIp = CommonModel.IPAddress();

                CD1.UserGuid = Convert.ToString(Request.Cookies["arch_i"].Value);
                CD1.FirstName = txtFirstName.Text.Trim();
                CD1.LastName = txtLastName.Text.Trim();
                CD1.Email = txtEmail.Text.Trim();
                CD1.Phone = txtPhone.Text.Trim();
                CD1.AddedOn = TimeStamps.UTCTime();
                CD1.AddedIp = CommonModel.IPAddress();
                CD1.AddressLine1 = txtAdd.Text;
                CD1.AddressLine2 = txtAdd.Text;
                CD1.City = "";
                CD1.State = "";
                CD1.Country = "India";
                CD1.Zip = "";

                int res = UserDetails.UpdateUserDetails(conAP, CD);
                if (res > 0)
                {
                    int res1 = UserDetails.UpdateUserProfileAddress(conAP, CD1);
                    if (res1 > 0)
                    {
                        GetUserDetailsByUid();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Profile Details Updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnUpdate_Click", ex.Message);

        }
    }

    [WebMethod(EnableSession = true)]
    public static string Logout()
    {
        try
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            if (HttpContext.Current.Request.Cookies["arch_i"] != null)
            {
                var archICookie = new HttpCookie("arch_i")
                {
                    Expires = DateTime.UtcNow.AddDays(-10)
                };
                HttpContext.Current.Response.Cookies.Add(archICookie);
            }
            if (HttpContext.Current.Request.Cookies["arch_pkey"] != null)
            {
                var archVCookie = new HttpCookie("arch_pkey")
                {
                    Expires = DateTime.UtcNow.AddDays(-10)
                };
                HttpContext.Current.Response.Cookies.Add(archVCookie);
            }

            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Logout", ex.Message);
            return "Error";
        }
    }
}