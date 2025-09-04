using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.TextFormatting;
public partial class Admin_banner_images : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strImages = "", strThumbImage = "", strThumbImage1 = "", strThmb = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Visible = false;
        btnUpload.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnUpload, null) + ";");
        if (!IsPostBack)
        {
            GetAllBannerImages();
            if (Request.QueryString["id"] != null)
            {
                GetBanner();
            }
        }
    }
    public void GetBanner()
    {
        try
        {
            List<BannerImages> banner = BannerImages.GetBannerImageById(conAP, Convert.ToInt32(Request.QueryString["id"]));//.Where(s => s.Id == Convert.ToInt32(Request.QueryString["id"])).SingleOrDefault();
            if (banner.Count>0)
            {
                btnUpload.Text = "Update";
                addBanner.Visible = true;
                txtTitle.Text = banner[0].BannerTitle;
                txtDesc.Text = banner[0].Description;
                txtlink.Text = banner[0].Link;
                if (banner[0].DesktopImage != "")
                {
                    strThumbImage = "<img src='/" + banner[0].DesktopImage + "' style='max-height:60px;' alt='NA' />";
                    lblThumb.Text = banner[0].DesktopImage;
                }
                if (banner[0].MobileImage != "")
                {
                    strThumbImage1 = "<img src='/" + banner[0].MobileImage + "' style='max-height:60px;' />";
                    lblThumb1.Text = banner[0].MobileImage;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBanner", ex.Message);
        }
    }
    public void GetAllBannerImages()
    {
        try
        {
            strImages = "";
            List<BannerImages> imagess = BannerImages.GetBannerImage(conAP).OrderByDescending(s => s.UpdatedOn).ToList();
            int i = 0;
            foreach (BannerImages img in imagess)
            {
                strImages += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + img.BannerTitle + @"</td>
                                                <td><img src='/" + img.DesktopImage + @"' style='max-height:50px;' /></td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + img.UpdatedBy + @"' >" + img.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'> 
<a href='banner-images.aspx?id=" + img.Id + @"' class='bs-tooltip fs-18' data-id='"+ img.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + img.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>

                                                        </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllImages", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "banner-images.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                BannerImages bis = new BannerImages();
                bis.Id = Convert.ToInt32(id);
                bis.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                bis.UpdatedIp = CommonModel.IPAddress();
                bis.UpdatedOn = CommonModel.UTCTime();
                bis.Status = "Deleted";
                int exec = BannerImages.DeleteBannerImage(conAP, bis);
                if (exec > 0)
                {
                    x = "Success";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;

                string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    if (btnUpload.Text == "Update")
                    {
                        GetBanner();
                    }
                    return;

                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Desktop banner image size should be 1520*650 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    if (btnUpload.Text == "Update")
                    {
                        GetBanner();
                    }
                    return;

                }

                //string resmsg1 = CheckImageFormat1();
                //if (resmsg1 == "Format")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                //    if (btnUpload.Text == "Update")
                //    {
                //        GetBanner();
                //    }
                //    return;
                //}
                //else if (resmsg1 == "Size")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Mobile banner image size should be 400*500 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                //    if (btnUpload.Text == "Update")
                //    {Please select a Mobile image to upload!


                //        GetBanner();
                //    }
                //    return;

                //}

                string pageName = Path.GetFileName(Request.Path);
                BannerImages banner = new BannerImages();
                banner.BannerTitle = txtTitle.Text.Trim();
                banner.Link = txtlink.Text.Trim();
                banner.Description = txtDesc.Text.Trim();
                if (btnUpload.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        banner.Id = Convert.ToInt32(Request.QueryString["id"]);
                        banner.UpdatedBy = Request.Cookies["ap_aid"].Value;
                        banner.DesktopImage = UploadImage();
                        banner.MobileImage = "";
                        int result = BannerImages.UpdateBannerImage(conAP, banner);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Banner details updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            GetBanner();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                < img src =
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                        if (!FileUpload1.HasFile)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please select a desktop image to upload!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                            return;
                        }
                        //if (!FileUpload2.HasFile)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please select a Mobile image to upload!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        //    return;
                        //}

                        banner.AddedBy = Request.Cookies["ap_aid"].Value;
                        banner.DesktopImage = UploadImage();
                        banner.MobileImage = "";
                        int result = BannerImages.InsertBannerImage(conAP, banner);
                        if (result > 0)
                        {
                            banner.Id = result;
                            BannerImages.AddBannerImage(conAP, banner);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Banner details added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            txtTitle.Text = txtlink.Text = "";
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
        finally
        {
            GetAllBannerImages();

        }
    }
    public string CheckImageFormat()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload1.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower());

            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".webp"))
            {
                if (fileExtension == ".webp")
                {
                    return thumbImage;
                }
                else
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    if (bmpPostedImageBig.Height == 650 && bmpPostedImageBig.Width == 1520)
                    {
                        return thumbImage;

                    }
                    else
                    {
                        return "Size";

                    }
                }

            }
            else
            {
                return "Format";
            }
        }
        #endregion
        return thumbImage;
    }

    public string UploadImage()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload1.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-deskBanImg".Replace(" ", "-").Replace(".", "");
            string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
            try
            {
                if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblThumb.Text))))
                {
                    File.Delete(Server.MapPath("~/" + Convert.ToString(lblThumb.Text)));
                }
            }
            catch (Exception eeex)
            {
                CommonModel.CaptureException(Request.Url.PathAndQuery, "CategoryUploadImage", eeex.Message);
                return lblThumb.Text;
            }

            if (fileExtension == ".webp")
            {
                FileUpload1.SaveAs(iconPath);

            }
            else if (fileExtension == ".gif")
            {
                FileUpload1.SaveAs(iconPath);

            }
            else
            {
                System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                if (fileExtension == ".png")
                {
                    CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                }
                else { CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99); }
            }
            thumbImage = "UploadImages/" + ImageGuid1 + "" + fileExtension;
        }
        else
        {
            thumbImage = lblThumb.Text;
        }
        #endregion
        return thumbImage;
    }

    public string CheckImageFormat1()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload2.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload2.PostedFile.FileName.ToLower());

            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".webp"))
            {
                if (fileExtension == ".webp")
                {

                    return thumbImage;
                }
                else
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    if (bmpPostedImageBig.Height == 500 && bmpPostedImageBig.Width == 400)
                    {
                        return thumbImage;
                    }
                    else
                    {
                        return "Size";

                    }
                }

            }
            else
            {
                return "Format";
            }
        }
        #endregion
        return thumbImage;
    }

    public string UploadImage1()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload2.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload2.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-mobBanImg".Replace(" ", "-").Replace(".", "");
            string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
            try
            {
                if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblThumb1.Text))))
                {
                    File.Delete(Server.MapPath("~/" + Convert.ToString(lblThumb1.Text)));
                }
            }
            catch (Exception eeex)
            {
                CommonModel.CaptureException(Request.Url.PathAndQuery, "CategoryUploadImage", eeex.Message);
                return lblThumb1.Text;
            }

            if (fileExtension == ".webp")
            {
                FileUpload2.SaveAs(iconPath);

            }
            else if (fileExtension == ".gif")
            {
                FileUpload2.SaveAs(iconPath);

            }
            else
            {
                System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);
                System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                if (fileExtension == ".png")
                {
                    CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                }
                else { CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99); }
            }
            thumbImage = "UploadImages/" + ImageGuid1 + "" + fileExtension;
        }
        else
        {
            thumbImage = lblThumb1.Text;
        }
        #endregion
        return thumbImage;
    }

    protected void addBanner_Click(object sender, EventArgs e)
    {
        Response.Redirect("banner-images.aspx");
    }
}