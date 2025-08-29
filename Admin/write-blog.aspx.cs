using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_write_blog : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strThumbImage = "", strThumbImage2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Visible = false;
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetBlogDetails();
            }
        }
    }

    public void GetBlogDetails()
    {
        try
        {
            List<Blogs> pro = Blogs.GetBlogById(conAP, Convert.ToInt32(Request.QueryString["id"]));
            if (pro.Count > 0)
            {
                btnSave.Text = "Update";
                addBlog.Visible = true;
                txtBlogName.Text = pro[0].Title;
                txtURL.Text = pro[0].Url;
                txtDesc.Text = pro[0].FullDesc;
                txtPostedBy.Text = pro[0].PostedBy;
                txtPageTitle.Text = pro[0].PageTitle;
                txtMetaKeys.Text = pro[0].MetaKey;
                txtMetaDesc.Text = pro[0].MetaDesc;
                txtShortDesc.Text = pro[0].ShortDesc;
                txtDesc.Text = pro[0].FullDesc;
                txtDate.Text = pro[0].PostedOn.ToString("dd/MMM/yyyy");
                if (pro[0].ImageUrl.Trim() != "")
                {
                    strThumbImage = "<img src='/" + pro[0].ImageUrl.Trim() + "' style='max-height:60px;' />";
                    lblThumb.Text = pro[0].ImageUrl.Trim();
                    reqFileUpload1.Visible = false;
                }
                if (pro[0].DetailImage.Trim() != "")
                {
                    strThumbImage2 = "<img src='/" + pro[0].DetailImage.Trim() + "' style='max-height:60px;' />";
                    lblThumb1.Text = pro[0].DetailImage.Trim();
                    reqFileUpload2.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlogDetails", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;
                string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Thumb image size should be 1000*800 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;

                }

                string resmsg1 = CheckImageFormat1();
                if (resmsg1 == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;
                }
                else if (resmsg1 == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Detail image size should be 1200*900 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;

                }

                string pageName = Path.GetFileName(Request.Path);
                Blogs pro = new Blogs();
                pro.Title = txtBlogName.Text.Trim();
                pro.Url = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                pro.PostedBy = txtPostedBy.Text.Trim();
                pro.PostedOn = txtDate.Text == "" ? CommonModel.UTCTime() : Convert.ToDateTime(txtDate.Text);
                pro.PageTitle = txtPageTitle.Text;
                pro.MetaKey = txtMetaKeys.Text;
                pro.MetaDesc = txtMetaDesc.Text;
                pro.FullDesc = txtDesc.Text;
                pro.ShortDesc = txtShortDesc.Text;
                pro.ImageUrl = UploadImage();
                pro.DetailImage = UploadImage1();
                string aid = Request.Cookies["ap_aid"].Value;
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", aid))
                    {
                        pro.Id = Convert.ToInt32(Request.QueryString["id"]);
                        pro.UpdatedBy = aid;
                        int result = Blogs.UpdateBlog(conAP, pro);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Blog updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", aid))
                    {
                        pro.AddedBy = aid;
                        pro.Status = "Active";
                        pro.AddedIp = CommonModel.IPAddress();
                        pro.AddedOn = CommonModel.UTCTime();
                        int result = Blogs.WriteBlog(conAP, pro);
                        if (result > 0)
                        {
                            //pro.Id = result;
                            txtBlogName.Text = txtDesc.Text = txtPostedBy.Text = txtDate.Text = txtMetaDesc.Text = txtMetaKeys.Text = txtPageTitle.Text = txtShortDesc.Text = txtURL.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Blog added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
                GetBlogDetails();
            }
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
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
                    if (bmpPostedImageBig.Height != 800 && bmpPostedImageBig.Width != 1000)
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
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-blogThumb".Replace(" ", "-").Replace(".", "");
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
                    if (bmpPostedImageBig.Height != 900 && bmpPostedImageBig.Width != 1200)
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
            string fileExtension = Path.GetExtension(FileUpload2.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-blogDetail".Replace(" ", "-").Replace(".", "");
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


    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("write-blog.aspx");
    }
}