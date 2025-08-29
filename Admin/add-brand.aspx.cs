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

public partial class add_brand : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strCatImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetBrand();
            }
        }
    }
    public void GetBrand()
    {
        try
        {
            List<Brand> categories = Brand.GetBrand(conAP, Request.QueryString["id"]).ToList();
            if (categories.Count > 0)
            {
                btnSave.Text = "Update";
                txtBrand.Text = categories[0].BrandName;
                txtURL.Text = categories[0].BrandUrl;
                txtMetaDesc.Text = categories[0].MetaDesc;
                txtMetaKeys.Text = categories[0].MetaKey;
                txtPageTitle.Text = categories[0].PageTitle;
                txtBrandDesc.Text = categories[0].FullDesc;
                txtShortDesc.Text = categories[0].ShortDesc;
                chkDisplayHome.Checked = categories[0].DisplayHome == "Yes" ? true : false;
                txtDisplayOrder.Text = categories[0].DisplayOrder;
                btnNew.Visible = true;
                if (categories[0].ImageUrl != "")
                {
                    lblThumb.Text = categories[0].ImageUrl;
                    strCatImage = @"<img src='/" + categories[0].ImageUrl + @"' class='img-responsive' width='20%' height='20%' title='" + categories[0].BrandName + @"'/>";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "GetBrand", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                string aid = Request.Cookies["ap_aid"].Value;

                string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Brand image size should be 300 * 300 px.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }

                Brand cat = new Brand();

                cat.BrandName = txtBrand.Text.Trim();
                cat.Url = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                cat.DisplayOrder = txtDisplayOrder.Text == "" ? "1000" : txtDisplayOrder.Text;
                cat.DisplayHome = chkDisplayHome.Checked ? "Yes" : "No";
                cat.PageTitle = txtPageTitle.Text.Trim();
                cat.MetaKey = txtMetaKeys.Text.Trim();
                cat.MetaDesc = txtMetaDesc.Text.Trim();
                cat.FullDesc = txtBrandDesc.Text.Trim();
                cat.ShortDesc = txtShortDesc.Text.Trim();
                cat.ImageUrl = UploadImage();
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                cat.AddedBy = aid;
                cat.Status = "Active";
                List<Brand> res = Brand.GetBrandByName(conAP, txtBrand.Text.Trim().ToLower());//.Where(s => s.BrandName.ToLower() == cat.BrandName.ToLower()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (!CreateUser.CheckAccess(conAP, pageName, "Edit", aid))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }

                    if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Brand already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                    else
                    {
                        cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                        int result = Brand.UpdateBrand(conAP, cat);
                        if (result > 0)
                        {
                            GetBrand();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Brand updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                    }
                }
                else
                {
                    if (!CreateUser.CheckAccess(conAP, pageName, "Add", aid))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }

                    if (res.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Brand already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                    else
                    {
                        int result = Brand.InsertBrand(conAP, cat);
                        if (result > 0)
                        {
                            txtBrand.Text = txtDisplayOrder.Text = txtBrandDesc.Text = txtMetaDesc.Text = txtShortDesc.Text = txtMetaKeys.Text = txtPageTitle.Text = txtURL.Text = "";
                            chkDisplayHome.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Brand added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-brand.aspx", false);
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
                    if (bmpPostedImageBig.Height != 300 && bmpPostedImageBig.Width != 300)
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
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-catimg".Replace(" ", "-").Replace(".", "");
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

}