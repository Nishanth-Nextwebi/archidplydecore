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

public partial class Admin_create_category : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strCategory = "", strCatImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetCategory();
            }
        }
    }
    public void GetCategory()
    {
        try
        {
            List<Category> categories = Category.GetCategory(conAP, Request.QueryString["id"]).ToList();
            if (categories.Count > 0)
            {
                btnSave.Text = "Update";
                txtCategory.Text = categories[0].CategoryName;
                txtURL.Text = categories[0].CategoryUrl;
                txtMetaDesc.Text = categories[0].MetaDesc;
                txtMetaKeys.Text = categories[0].MetaKey;
                txtPageTitle.Text = categories[0].PageTitle;
                txtCategoryDesc.Text = categories[0].FullDesc;
                chbDispHome.Checked = categories[0].DisplayHome == "Yes" ? true : false;
                txtDisplayOrder.Text = categories[0].DisplayOrder;
                txtShortDesc.Text = categories[0].ShortDesc;
                btnAddCategory.Visible = true;
               /* if (categories[0].ImageUrl != "")
                {
                    lblThumb.Text = categories[0].ImageUrl;
                    strCatImage = @"<img src='/" + categories[0].ImageUrl + @"' class='img-responsive' width='20%' height='20%' title='" + categories[0].CategoryName + @"'/>";
                }*/
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
    }
    /*public string CheckImageFormat()
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
    }*/
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;
                string pageName = Path.GetFileName(Request.Path);
              /*  string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Category image size should be 300*300 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    
                    return;
                }*/
                Category cat = new Category();
                cat.CategoryName = txtCategory.Text.Trim();
                cat.Url = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                cat.DisplayOrder = txtDisplayOrder.Text == "" ? "1000" : txtDisplayOrder.Text;
                cat.DisplayHome = chbDispHome.Checked ? "Yes" : "No";
                cat.PageTitle = txtPageTitle.Text.Trim();
                cat.MetaKey = txtMetaKeys.Text.Trim();
                cat.MetaDesc = txtMetaDesc.Text.Trim();
                cat.FullDesc = txtCategoryDesc.Text.Trim();
                cat.ShortDesc = txtShortDesc.Text.Trim();
                cat.ImageUrl = "";
                cat.AddedBy = Request.Cookies["ap_aid"].Value;

                List<Category> res = Category.GetCategoryByCategory(conAP, txtCategory.Text.Trim());//.Where(s => s.CategoryName.ToLower() == cat.CategoryName.ToLower()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Category already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                        else
                        {
                            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = Category.UpdateCategory(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Category updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                              
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                       /* if (!FileUpload1.HasFile)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please select a category image to upload!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            return;
                        }*/
                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Category already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                        else
                        {
                            int result = Category.InsertCategory(conAP, cat);
                            if (result > 0)
                            {
                                txtCategory.Text = txtDisplayOrder.Text = txtCategoryDesc.Text = txtMetaDesc.Text
                                    = txtMetaKeys.Text = txtPageTitle.Text = txtURL.Text = "";
                                chbDispHome.Checked = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Category added successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
            }
            GetCategory();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("category.aspx");
    }
}