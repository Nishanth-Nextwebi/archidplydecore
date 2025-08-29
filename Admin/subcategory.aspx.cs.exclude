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

public partial class Admin_subcategory : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strCategory = "", strSubCatImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllCategory();

            if (Request.QueryString["id"] != null)
            {
                GetSubCategory();
            }
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
                    if (bmpPostedImageBig.Height != 500 && bmpPostedImageBig.Width != 500)
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
    public void GetSubCategory()
    {
        try
        {
            List<SubCategory> categories = SubCategory.GetSubCategory(conAP, Request.QueryString["id"]);
            if (categories.Count > 0)
            {
                btnSave.Text = "Update";
                txtSubCategory.Text = categories[0].SubCategoryName;
                txtURL.Text = categories[0].Url;
                txtMetaDesc.Text = categories[0].MetaDesc;
                txtMetaKeys.Text = categories[0].MetaKey;
                txtPageTitle.Text = categories[0].PageTitle;
                txtSubCategoryDesc.Text = categories[0].FullDesc;
                addSubCategoryBtn.Visible = true;
                chbDispHome.Checked = categories[0].DisplayHome == "Yes" ? true : false;
                txtDisplayOrder.Text = categories[0].DisplayOrder;
                txtShortDesc.Text = categories[0].ShortDesc;
                ddlCateory.SelectedIndex = ddlCateory.Items.IndexOf(ddlCateory.Items.FindByValue(categories[0].Category));
                if (categories[0].ImageUrl != "")
                {
                    lblThumb.Text = categories[0].ImageUrl;
                    strSubCatImage = @"<img src='/" + categories[0].ImageUrl + @"' class='img-responsive' width='20%' height='20%' title='" + categories[0].SubCategoryName + @"'/>";
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetSubCategory", ex.Message);
        }
    }

    public void GetAllCategory()
    {
        try
        {
            List<Category> categories = Category.GetAllCategory(conAP);
            if (categories.Count > 0)
            {
                ddlCateory.Items.Clear();
                ddlCateory.DataSource = categories;
                ddlCateory.DataTextField = "CategoryName";
                ddlCateory.DataValueField = "Id";
                ddlCateory.DataBind();
            }
            ddlCateory.Items.Insert(0, new ListItem("Select Category", ""));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblStatus.Visible = true;
                string pageName = Path.GetFileName(Request.Path);
                string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Sub Category image size should be 500*500 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;

                }
                SubCategory cat = new SubCategory();
                cat.SubCategoryName = txtSubCategory.Text.Trim();
                cat.Url = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                cat.Category = ddlCateory.SelectedValue;
                cat.PageTitle = txtPageTitle.Text.Trim();
                cat.MetaKey = txtMetaKeys.Text.Trim();
                cat.MetaDesc = txtMetaDesc.Text.Trim();
                cat.FullDesc = txtSubCategoryDesc.Text.Trim();
                cat.ShortDesc = txtShortDesc.Text.Trim();
                cat.DisplayHome = chbDispHome.Checked ? "Yes" : "No";
                cat.DisplayOrder = txtDisplayOrder.Text == "" ? "1000" : txtDisplayOrder.Text;
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                cat.ImageUrl = UploadImage();

                List<SubCategory> res = SubCategory.GetSubCategoryByCatAndSub(conAP, txtSubCategory.Text.Trim(), ddlCateory.SelectedValue);//.Where(s => s.SubCategoryName.ToLower() == cat.SubCategoryName.ToLower() && s.Category.ToLower() == cat.Category.ToLower()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'SubCategory already exist...',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = SubCategory.UpdateSubCategory(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'SubCategory updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                                GetSubCategory();
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
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'SubCategory already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                        else
                        {
                            cat.UpdatedOn = CommonModel.UTCTime();
                            cat.UpdatedIp = CommonModel.IPAddress();
                            int result = SubCategory.InsertSubCategory(conAP, cat);
                            if (result > 0)
                            {
                                txtSubCategory.Text = txtDisplayOrder.Text = txtURL.Text
                                    = txtMetaDesc.Text = txtPageTitle.Text = txtMetaKeys.Text
                                    = txtSubCategoryDesc.Text = ddlCateory.SelectedValue = "";
                                chbDispHome.Checked = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'SubCategory added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteSubCategory(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "subcategory.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                SubCategory cat = new SubCategory();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = SubCategory.DeleteSubCategory(conAP, cat);
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteSubCategory", ex.Message);
        }
        return x;
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        Response.Redirect("subcategory.aspx");
    }
}