using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_enquiry_products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strThumbImage = "", strMobileImage = "", strCourseSylabus = "", strInternImage1 = "", strInternImage2 = "", strCertImage = "", strCourseImage = "", strBannerImage = "", strCategory = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllFeatures();
            // GetAllTags();
            BindCategories();
            // BindBrand();
            if (Request.QueryString["id"] != null)
            {
                GetEnquiryEnquiryProduct();
            }
        }
    }
    public void GetEnquiryEnquiryProduct()
    {
        try
        {
            EnquiryProduct pds = EnquiryProduct.GetEnquiryProductById(conAP, Convert.ToInt32(Request.QueryString["id"]));
            if (pds != null)
            {
                strCategory = pds.CategoryName;
                btnSave.Text = "Update";
                addProduct.Visible = true;
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(pds.Category));
                ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(ddlBrand.Items.FindByValue(pds.Brand));
                txtProdName.Text = pds.ProductName;
                txtURL.Text = pds.ProductUrl;
                txtShort.Text = pds.ShortDesc;
                txtPTitle.Text = pds.PageTitle;
                txtMetaDesc.Text = pds.MetaDesc;
                txtMKeys.Text = pds.MetaKey;
                txtOrder.Text = pds.DisplayOrder;
                txtItemNum.Text = pds.ItemNumber;
                chbDispHome.Checked = pds.Status == "Active" ? true : false;
                //chkFeatured.Checked = pds.Featured == "Yes" ? true : false;
                foreach (string vs in pds.Features.Split('|'))
                {
                    foreach (ListItem li in ddlFeatures.Items)
                    {
                        if (li.Text.Trim() == vs.Trim())
                        {
                            li.Selected = true;
                        }
                    }
                }
                foreach (string vs in pds.ProductTags.Split('|'))
                {
                    foreach (ListItem li in drpTag.Items)
                    {
                        if (li.Text.Trim() == vs.Trim())
                        {
                            li.Selected = true;
                        }
                    }
                }
                if (pds.ProductImage != "")
                {
                    strBannerImage = "<img src='/" + pds.ProductImage + "' style='max-height:60px;margin-bottom:10px;' />";
                    lblThumb.Text = pds.ProductImage;
                }
                //  BindSubCategories(pds.Category);
                ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(pds.SubCategory));
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string resmsg = CheckImageFormat();
                if (resmsg == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: '.png, .jpeg, .jpg, .gif, .webp formats to be uploaded!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Image size should be 500 × 500 px.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                string pageName = Path.GetFileName(Request.Path);
                EnquiryProduct cat = new EnquiryProduct();
                cat.Category = ddlCategory.SelectedItem.Value;
                cat.SubCategory = "";
                cat.Brand = "";
                // var subCat = SubCategory.GetSubCategoryByID(conAP, ddlSubCategory.SelectedItem.Value);
                // cat.ProductName = Convert.ToString(subCat[0].SubCategoryName);
                // cat.ProductUrl = Convert.ToString(subCat[0].Url);
                cat.ProductName = txtProdName.Text.Trim();
                cat.ProductUrl = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                cat.ShortDesc = txtShort.Text.Trim();
                cat.FullDesc = "";
                cat.Featured = "";
                cat.Status = chbDispHome.Checked ? "Active" : "Draft";
                cat.PageTitle = txtPTitle.Text.Trim();
                cat.ItemNumber = txtItemNum.Text.Trim();
                cat.MetaKey = txtMKeys.Text.Trim();
                cat.MetaDesc = txtMetaDesc.Text.Trim();
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                cat.ProductImage = UploadImage();
                cat.RelatedProducts = "";
                cat.DisplayOrder = txtOrder.Text.Trim();
                string tags = "";
                #region Product Tags
                foreach (ListItem li in drpTag.Items)
                {
                    if (li.Selected)
                    {
                        tags += li.Value + "|";
                    }
                }
                tags = tags.TrimEnd('|');
                #endregion
                string features = "";
                #region Product Features
                foreach (ListItem li in ddlFeatures.Items)
                {
                    if (li.Selected)
                    {
                        features += li.Value + "|";
                    }
                }
                features = features.TrimEnd('|');
                #endregion

                cat.ProductTags = tags;
                cat.Features = features;
                #region NameExist
                List<EnquiryProduct> res = EnquiryProduct.GetAllEnquiryProductBySubCategory(conAP, cat.ProductName).ToList();
                #endregion
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Already Exist For this Subcategory Please Check',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            return;
                        }
                        cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                        int result = EnquiryProduct.UpdateEnquiryProduct(conAP, cat);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
                        if (!FileUpload1.HasFile)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please select a product image to upload!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                            return;
                        }
                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Already Exist For this Subcategory Please Check',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            return;
                        }
                        string pGuid = Guid.NewGuid().ToString();
                        cat.ProductGuid = pGuid;
                        cat.UpdatedOn = CommonModel.UTCTime();
                        cat.UpdatedIp = CommonModel.IPAddress();
                        int result = EnquiryProduct.InsertEnquiryProduct(conAP, cat);
                        if (result > 0)
                        {
                            cat.Id = result;
                            ddlCategory.ClearSelection();
                            ddlFeatures.ClearSelection();
                            txtProdName.Text = txtURL.Text = txtOrder.Text = txtPTitle.Text = txtMetaDesc.Text = txtShort.Text = txtItemNum.Text = txtMKeys.Text = txtMetaDesc.Text = txtMetaDesc.Text = "";
                            chbDispHome.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Added successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator,actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
                GetEnquiryEnquiryProduct();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
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
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()),
                ImageGuid1 = Guid.NewGuid().ToString() + "-prod".Replace(" ", "-").Replace(".", "");
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
    public void BindBrand()
    {
        try
        {
            List<Brand> comps = Brand.GetAllBrand(conAP);
            if (comps.Count > 0)
            {
                ddlBrand.Items.Clear();
                ddlBrand.DataSource = comps;
                ddlBrand.DataValueField = "Id";
                ddlBrand.DataTextField = "BrandName";
                ddlBrand.DataBind();
            }
            ddlBrand.Items.Insert(0, new ListItem("- Select Brand -", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindBrand", ex.Message);
        }
    }
    public void BindCategories()
    {
        try
        {
            List<Category> comps = Category.GetAllEnquiryCategory(conAP);
            if (comps.Count > 0)
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = comps;
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("- Select Category -", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindCategories", ex.Message);
        }
    }

    protected void addProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-enquiry-products.aspx");

    }
    /* protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
     {
         BindSubCategories(ddlCategory.SelectedItem.Value);
     }*/
    /*  public void BindSubCategories(string category)
      {
          try
          {
              ddlSubCategory.Items.Clear();
              var subCat = SubCategory.GetAllSubCategoryByCategory(conAP, category);
              if (subCat.Rows.Count > 0)
              {
                  ddlSubCategory.DataSource = subCat;
                  ddlSubCategory.DataValueField = "Id";
                  ddlSubCategory.DataTextField = "SubCategory";
                  ddlSubCategory.DataBind();
              }
              ddlSubCategory.Items.Insert(0, new ListItem("- Select Sub Category -", ""));
          }
          catch (Exception ex)
          {
              ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindSubCategories", ex.Message);
          }
      }*/
    public void GetAllTags()
    {
        try
        {
            drpTag.Items.Clear();
            List<Tags> cats = Tags.GetAllTags(conAP);
            if (cats.Count > 0)
            {
                drpTag.DataSource = cats;
                drpTag.DataTextField = "Title";
                drpTag.DataValueField = "Title";
                drpTag.DataBind();
            }
            drpTag.Items.Insert(0, new ListItem("Select Tags", ""));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSpacress", ex.Message);
        }
    }

    public void GetAllFeatures()
    {
        try
        {
            ddlFeatures.Items.Clear();
            List<ProductFeatures> cats = ProductFeatures.GetAllProductFeatures(conAP);
            if (cats.Count > 0)
            {
                ddlFeatures.DataSource = cats;
                ddlFeatures.DataTextField = "Title";
                ddlFeatures.DataValueField = "Id";
                ddlFeatures.DataBind();
            }
            ddlFeatures.Items.Insert(0, new ListItem("Select Features", ""));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllFeatures", ex.Message);
        }
    }
}