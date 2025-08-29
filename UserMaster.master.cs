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

public partial class UserMaster : System.Web.UI.MasterPage
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strDeskMenu, strMobMenu, strUserLoginMobile,strpureply, strUserLoggedIn, strUserLoggedOut, strFooterCat, strFooterbrand, strUserLoginStatus, strGurjan, strdec, strlam, strviva = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginCheck();
        if (!IsPostBack)
        {
            LoginCheck();
        }
        if (Request.Cookies["arch_i"] == null)
        {
            CreateVisitedUser();
        }
        bindMenu();
    }
    public void bindMenu()
    {
        try
        {
            strDeskMenu = "";
            strMobMenu = "";
            List<Category> categories = Category.GetAllCategory(conAP).Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
            if (categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    strFooterCat += @"<li class='pt-3 mb-4'><a href='/products-categories/" + category.CategoryUrl + @"'>" + category.CategoryName + @"</a></li>";
                    string subCat = "";
                    string subMobilCat = "";
                    List<EnquiryProduct> ep = EnquiryProduct.GetAllEnquiryProductByCategory(conAP, Convert.ToString(category.Id)).ToList();//OrderBy(s => s.DisplayOrder).ToList();
                    if (ep.Count > 0)
                    {
                        foreach (EnquiryProduct e in ep)
                        {
                            subCat += @"<ul class='list-unstyled mb-0'><li><a href='/products/" + e.ProductUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + e.ProductName + @"</span></a></li></ul>";
                            subMobilCat += @"<ul><li><a href='/products/" + e.ProductUrl + @"' class='border-hover text-decoration-none py-3 d-block' ><span class='border-hover-target'>" + e.ProductName + @"</span></a></li></ul>";
                        }
                    }
                    subCat = "<div class='col'><a href='/products-categories/" + category.CategoryUrl + @"'><h6 class='fs-18px'>" + category.CategoryName + @"</h6></a>" + subCat + "</div>";
                    subMobilCat = "<div class='col'><a href='/products-categories/" + category.CategoryUrl + @"'><h6>" + category.CategoryName + @"</h6></a>" + subMobilCat + "</div>";
                    strDeskMenu += subCat;
                    strMobMenu += subMobilCat;
                }

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindMenu()", ex.Message);

        }


    }

    //public void bindMenu()
    //{
    //    try
    //    {
    //        strDeskMenu = "";
    //        strMobMenu = "";
    //        List<Category> categories = Category.GetAllCategory(conAP).Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
    //        if (categories.Count > 0)
    //        {
    //            foreach (Category category in categories)
    //            {
    //                strFooterCat += @"<li class='pt-3 mb-4'><a href='/products-categories/" + category.CategoryUrl + @"'>" + category.CategoryName + @"</a></li>";
    //                string subCatgurjan = "";
    //                string subCatdecorative = "";
    //                string subCatlaminated = "";
    //                string subCatvivant = "";
    //                string subCatpurely = "";
    //                List<SubCategory> subCategories = SubCategory.GetSubCategoryByCat(conAP, Convert.ToString(category.Id)).Where(s => s.DisplayHome == "Yes").OrderBy(s => s.DisplayOrder).ToList();
    //                if (subCategories.Count > 0)
    //                {
    //                    foreach (SubCategory subCategory in subCategories)
    //                    {
    //                        if (subCategory.CategoryName.ToLower() == "gurjan based plywood")
    //                        {
    //                            subCatgurjan += @"<ul class='list-unstyled mb-0'><li><a href='/products-categories/" + category.CategoryUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + subCategory.SubCategoryName + @"</span></a></li></ul>";
    //                        }
    //                        else if (subCategory.CategoryName.ToLower() == "the decorative veneer collection")
    //                        {
    //                            subCatdecorative += @"<ul class='list-unstyled mb-0'><li><a href='/products-categories/" + category.CategoryUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + subCategory.SubCategoryName + @"</span></a></li></ul>";
    //                        }
    //                        else if (subCategory.CategoryName.ToLower() == "archidply decor's pre-laminated boards range")
    //                        {
    //                            subCatlaminated += @"<ul class='list-unstyled mb-0'><li><a href='/products-categories/" + category.CategoryUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + subCategory.SubCategoryName + @"</span></a></li></ul>";
    //                        }
    //                        else if (subCategory.CategoryName.ToLower() == "bon vivant")
    //                        {
    //                            subCatvivant += @"<ul class='list-unstyled mb-0'><li><a href='/products-categories/" + category.CategoryUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + subCategory.SubCategoryName + @"</span></a></li></ul>";
    //                        }
    //                        else if (subCategory.CategoryName.ToLower() == "pureply")
    //                        {
    //                            subCatpurely += @"<ul class='list-unstyled mb-0'><li><a href='/products-categories/" + category.CategoryUrl + @"' class='border-hover text-decoration-none py-3 d-block'><span class='border-hover-target'>" + subCategory.SubCategoryName + @"</span></a></li></ul>";
    //                        }
    //                        else
    //                        {
    //                        }
    //                    }
    //                    strGurjan += subCatgurjan;
    //                    strdec += subCatdecorative;
    //                    strlam += subCatlaminated;
    //                    strviva += subCatvivant;
    //                    strpureply += subCatpurely;
    //                }
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindMenu()", ex.Message);

    //    }
    //}
    public void LoginCheck()
    {
        try
        {
            if (Request.Cookies["arch_i"] == null)
            {
                strUserLoginStatus = @"<div class='px-5 d-none d-xl-inline-block'>
                                        <a class='lh-1 color-inherit text-decoration-none'
                                            href='/login.aspx'>
                                            <svg class='icon icon-user-light'>
                                                <use xlink:href='#icon-user-light'></use>
                                            </svg>
                                        </a>
                                    </div>";

                strUserLoginMobile = @"<a class=""lh-1 color-inherit text-decoration-none"" href=""/login.aspx"">
                                        <svg class=""icon icon-user-light"">
                                            <use xlink:href=""#icon-user-light""></use>
                                        </svg>
                                    </a>";
            }
            else
            {
                strUserLoginStatus = @"<div class='px-5 d-none d-xl-inline-block'>
                                        <a class='lh-1 color-inherit text-decoration-none'
                                            href='/my-profile'>
                                            <svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='green' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-user-check'>
                                                        <path d='M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2' />
                                                        <circle cx='8.5' cy='7' r='4' />
                                                        <polyline points='17 11 19 13 23 9' />
                                                    </svg>
                                        </a>
                                    </div>";

                strUserLoginMobile = @"<a class=""lh-1 color-inherit text-decoration-none"" href='/my-profile'>
                                         <svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='green' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-user-check'>
                                                        <path d='M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2' />
                                                        <circle cx='8.5' cy='7' r='4' />
                                                        <polyline points='17 11 19 13 23 9' />
                                                    </svg>
                                    </a>";
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "bindMenu()", ex.Message);

        }


    }
    public void CreateVisitedUser()
    {
        if (HttpContext.Current.Request.Cookies["arch_v"] == null || HttpContext.Current.Request.Cookies["arch_v"].Value == "")
        {
            HttpCookie cookie = new HttpCookie("arch_v");
            cookie.Value = Guid.NewGuid().ToString();
            cookie.Expires = CommonModel.UTCTime().AddDays(30);
            Response.Cookies.Add(cookie);
        }
    }
    //public bool CheckEmail()
    //{
    //    bool result = false;
    //    try
    //    {
    //        lblSighUpStatus.Visible = true;
    //        UserDetails ud = UserDetails.CheckUserByEmail(conAP, txtSignUpEmail.Text.Trim());
    //        if (ud.UserGuid != null)
    //        {
    //            if (ud.Status == "Unverified")
    //            {
    //                hdnModalVisibility.Value = "true";
    //                lblSighUpStatus.Text = "Please confirm your email address to complete sign up process.";
    //                lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please confirm your email address to complete sign up process.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                Emails.SendEmailVerifyLink(txtSignUpEmail.Text.Trim(), ud.FirstName, ConfigurationManager.AppSettings["domain"] + "/confirm-e-mail.aspx?u=" + ud.UserGuid);
    //                result = true;
    //            }
    //            else
    //            {
    //                hdnModalVisibility.Value = "true";
    //                lblSighUpStatus.Text = "Email Already registered. Try with a different email id.";
    //                lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Email Already registered. Try with a different email id.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                result = true;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindYears", ex.Message);
    //    }
    //    return result;
    //}
    //protected void btnLogin_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            lblLoginStatus.Visible = true;
    //            UserDetails ud = UserDetails.UserLoginWithEmail(conAP, txtEmail.Text.Trim(), CommonModel.Encrypt(txtPassword.Text.Trim()));
    //            if (ud.UserGuid != null)
    //            {
    //                if (ud.Status == "Verified")
    //                {
    //                    string uid = HttpContext.Current.Request.Cookies["arch_v"].Value;
    //                    UserDetails.UpdateLastLogDetails(conAP, ud.UserGuid);
    //                    hdnModalVisibility1.Value = "true";
    //                    lblLoginStatus.Text = "Logged-in successfully.";
    //                    lblLoginStatus.Attributes.Add("class", "alert alert-success d-block");
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Logged-in successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
    //                    HttpCookie cookie = new HttpCookie("arch_i");

    //                    cookie.Value = Convert.ToString(ud.UserGuid);
    //                    if (chkStaySignedIn.Checked)
    //                    {
    //                        cookie.Expires = CommonModel.UTCTime().AddDays(30);

    //                    }

    //                    HttpCookie cookie_pass_key = new HttpCookie("arch_pkey");
    //                    cookie_pass_key.Value = Convert.ToString(ud.PassKey);
    //                    Response.Cookies.Add(cookie_pass_key);
    //                    Response.Cookies.Add(cookie);
    //                    CartDetails.UpdateCartAfterLogin(conAP, uid, ud.UserGuid);
    //                    Response.Redirect("/my-profile");
    //                }
    //                else if (ud.Status == "Blocked")
    //                {
    //                    hdnModalVisibility1.Value = "true";
    //                    lblLoginStatus.Text = "Your profile is temporarily blocked. Please contact admin.";
    //                    lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your profile is temporarily blocked. Please contact admin.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                }
    //                else
    //                {
    //                    hdnModalVisibility1.Value = "true";
    //                    lblLoginStatus.Text = "Your email address is not verified.";
    //                    lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Your email address is not verified.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                }
    //            }
    //            else
    //            {

    //                hdnModalVisibility1.Value = "true";
    //                lblLoginStatus.Text = "E-mail address or password is incorrect.";
    //                lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'E-mail address or password is incorrect.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        hdnModalVisibility1.Value = "true";
    //        lblLoginStatus.Text = "There is some problem now. Please try after some time.";
    //        lblLoginStatus.Attributes.Add("class", "alert alert-danger d-block");

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //        CommonModel.CaptureException(Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
    //    }
    //}
    //protected void btnSignUp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            lblSighUpStatus.Visible = true;
    //            if (!chkAgreePolicy.Checked)
    //            {
    //                hdnModalVisibility.Value = "true";
    //                lblSighUpStatus.Text = "Please accept the terms and condition.";
    //                lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please accept the terms and condition.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                return;
    //            }
    //            Random rnd = new Random();
    //            int otp = rnd.Next(100000, 999999);
    //            Session["otp"] = otp;
    //            string uGuid = Guid.NewGuid().ToString();
    //            if (CheckEmail() == false)
    //            {
    //                txtUGuid.Value = uGuid;
    //                UserDetails ud = new UserDetails();
    //                ud.UserGuid = uGuid;
    //                ud.Gender = "";
    //                ud.FirstName = txtFirstName.Text.Trim();
    //                ud.LastName = txtLastName.Text.Trim();
    //                ud.ContactNo = txtSignUpMobileNo.Text.Trim();
    //                ud.EmailId = txtSignUpEmail.Text.Trim();
    //                ud.Password = CommonModel.Encrypt(txtSignUpPassword.Text.Trim());
    //                ud.ForgotId = "";
    //                ud.Subscribed = "";
    //                ud.PassKey = Guid.NewGuid().ToString();
    //                ud.Status = "Unverified";
    //                int exUD = UserDetails.CreateUser(conAP, ud);
    //                if (exUD > 0)
    //                {
    //                    UserAddress ua = new UserAddress();
    //                    ua.FirstName = txtFirstName.Text.Trim();
    //                    ua.LastName = txtLastName.Text.Trim();
    //                    ua.Phone = txtSignUpMobileNo.Text.Trim();
    //                    ua.Email = txtSignUpEmail.Text.Trim();
    //                    ua.ShortName = "My Address";
    //                    ua.Status = "Active";
    //                    ua.UserGuid = uGuid;
    //                    ua.Zip = "";
    //                    ua.AddressLine1 = "";
    //                    ua.AddressLine2 = "";
    //                    ua.City = "";
    //                    ua.State = "";
    //                    ua.Country = "India";
    //                    int exAD = UserDetails.AddUserAddress(conAP, ua);
    //                    if (exAD > 0)
    //                    {
    //                        hdnModalVisibility.Value = "true";
    //                        lblSighUpStatus.Text = "Email verification link is sent Please confirm your email address to complete sign up process.";
    //                        lblSighUpStatus.Attributes.Add("class", "alert alert-success d-block");
    //                        Emails.SendEmailVerifyLink(txtSignUpEmail.Text, txtFirstName.Text, ConfigurationManager.AppSettings["domain"] + "/confirm-e-mail.aspx?u=" + uGuid);
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Email verification link is sent Please confirm your email address to complete sign up process.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
    //                        txtFirstName.Text = txtSignUpEmail.Text = txtSignUpMobileNo.Text = txtLastName.Text = txtSignUpPassword.Text = "";

    //                    }
    //                    else
    //                    {
    //                        hdnModalVisibility.Value = "true";
    //                        lblSighUpStatus.Text = "There is some problem now. Please try after some time.";
    //                        lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //                    }
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        hdnModalVisibility.Value = "true";
    //        lblSighUpStatus.Text = "There is some problem now. Please try after some time.";
    //        lblSighUpStatus.Attributes.Add("class", "alert alert-danger d-block");
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
    //    }
    //}

}
