using DocumentFormat.OpenXml.Drawing.Charts;
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


public partial class Admin_assets_add_products : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strThumbImage = "", strMobileImage = "", strCourseSylabus = "", strInternImage1 = "", strInternImage2 = "", strCertImage = "", strCourseImage = "", strBannerImage = "", strCategory = "";
    public string strProdSeo = "", strProdFaqs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        btnSeo.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSeo, null) + ";");
        if (!IsPostBack)
        {
            BindSubCategories();
           // BindBrand();
            //GetAllTags();
            idPid.Value = Guid.NewGuid().ToString();
            if (Request.QueryString["id"] != null)
            {
                idTabPrices.Visible = idTabGallery.Visible = idTabSeo.Visible = true;
                idPid.Value = Request.QueryString["id"];
                GetProductDetails();
            }
        }
    }

    #region Product region
   /* protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategories(ddlCategory.SelectedItem.Value);
        if (Request.QueryString["id"] != null)
        {
            idTabPrices.Visible = idTabSeo.Visible = idTabGallery.Visible =  true;
            idPid.Value = Request.QueryString["id"];
            GetEditProductDetails();
        }
    }*/
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
            List<Category> comps = Category.GetAllCategory(conAP);
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
                drpTag.DataValueField = "TagURL";
                drpTag.DataBind();
            }
            drpTag.Items.Insert(0, new ListItem("- Select Tag -", "0"));
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSpacress", ex.Message);
        }
    }
    public void GetProductDetails()
    {
        try
        {
            ProductDetails pds = ProductDetails.GetProductDetailsById(conAP, Convert.ToInt32(Request.QueryString["id"]));
            if (pds != null)
            {
                strCategory = pds.CategoryName;
                btnSave.Text = "Update";
                addProduct.Visible = true;
                ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(pds.SubCategory));
                //ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(ddlBrand.Items.FindByValue(pds.Brand));
                txtProdName.Text = pds.ProductName;
                txtProductOrder.Text = pds.DisplayOrder;
                txtURL.Text = pds.ProductUrl;
                txtShort.Text = pds.ShortDesc;
                txtProductDesc.Text = pds.FullDesc;
                txtPTitle.Text = pds.PageTitle;
                txtMetaDesc.Text = pds.MetaDesc;
                txtMKeys.Text = pds.MetaKey;
                txtSKUCode.Text = pds.SKUCode;
                txtOrigin.Text = pds.Origin;
                txtIngr.Text = pds.Ingredients;
                txtReview.Text = pds.ReviewKeyword;
                txtDelDate.Text = pds.DeliveryDays;
                txtItemNum.Text = pds.ItemNumber;
                chkInStock.Checked = pds.InStock == "Yes" ? true : false;
                chbDispHome.Checked = pds.Status == "Active" ? true : false;
                chkFeatured.Checked = pds.Featured == "Yes" ? true : false;
                chkBestSeller.Checked = pds.BestSeller == "Yes" ? true : false;
                chkShop.Checked = pds.Shop == "Yes" ? true : false;
                chkEnquiry.Checked = pds.Enquiry == "Yes" ? true : false;
                drpTag.SelectedValue = pds.ProductTags;
                //#region Product Tags
                //foreach (string vs in pds.ProductTags.Split('|'))
                //{
                //    foreach (ListItem li in drpTag.Items)
                //    {
                //        if (li.Text.Trim() == vs.Trim())
                //        {
                //            li.Selected = true;
                //        }
                //    }
                //}
                //#endregion

                //#region Product Demand
                //foreach (string Ls in pds.Demand.Split('|'))
                //{
                //    foreach (ListItem li in lstdmnd.Items)
                //    {
                //        if (li.Text.Trim() == Ls.Trim())
                //        {
                //            li.Selected = true;
                //        }
                //    }

                //}

                //#endregion
                if (pds.ProductImage != "")
                {
                    strBannerImage = "<img src='/" + pds.ProductImage + "' style='max-height:60px;margin-bottom:10px;' />";
                    lblThumb.Text = pds.ProductImage;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategory", ex.Message);
        }
    }
    public void BindSubCategories()
    {
        try
        {
            ddlSubCategory.Items.Clear();
            var subCat = SubCategory.GetAllSubCategoryByCategory(conAP);
            if (subCat.Rows.Count > 0)
            {
                ddlSubCategory.DataSource = subCat;
                ddlSubCategory.DataValueField = "Id";
                ddlSubCategory.DataTextField = "SubCategory";
                ddlSubCategory.DataBind();
            }
            ddlSubCategory.Items.Insert(0, new ListItem("- Select Category -", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindSubCategories", ex.Message);
        }
    }
    public void GetEditProductDetails()
    {
        try
        {
            ProductDetails pds = ProductDetails.GetProductDetailsById(conAP, Convert.ToInt32(Request.QueryString["id"]));
            if (pds != null)
            {
                if (pds.ProductImage != "")
                {
                    strBannerImage = "<img src='/" + pds.ProductImage + "' style='max-height:60px;margin-bottom:10px;' />";
                    lblThumb.Text = pds.ProductImage;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEditProductDetails", ex.Message);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: '.png, .jpeg, .jpg, .gif, .webp formats to be uploaded!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Image size should be 500 × 500 px.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                string pageName = Path.GetFileName(Request.Path);
                ProductDetails cat = new ProductDetails();
                cat.Category = "";
                cat.SubCategory = ddlSubCategory.SelectedItem.Value;
                cat.Brand = "";
                cat.ProductName = txtProdName.Text.Trim();
                cat.SKUCode = txtSKUCode.Text.Trim();
                cat.TaxGroup = "18";
                cat.ProductUrl = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                cat.ShortDesc = txtShort.Text.Trim();
                cat.FullDesc = txtProductDesc.Text.Trim();
                cat.InStock = chkInStock.Checked ? "Yes" : "No";
                cat.Featured = chkFeatured.Checked ? "Yes" : "No";
                cat.Enquiry = "";
                cat.Shop = "";
                cat.BestSeller = "";
                cat.Status = chbDispHome.Checked ? "Active" : "Draft";
                cat.PageTitle = txtPTitle.Text.Trim();
                cat.ReviewKeyword = "";
                cat.ItemNumber = txtItemNum.Text.Trim();
                cat.MetaKey = txtMKeys.Text.Trim();
                cat.MetaDesc = txtMetaDesc.Text.Trim();
                cat.DisplayOrder = txtProductOrder.Text.Trim()==""?"1000": txtProductOrder.Text.Trim();
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                cat.Origin = txtOrigin.Text.Trim();
                cat.Ingredients = txtIngr.Text.Trim();
                cat.ProductImage = UploadImage();
                cat.DeliveryDays = txtDelDate.Text.Trim()==""?"": txtDelDate.Text.Trim();

                string tags = "";
                /*#region Product Tags
                foreach (ListItem li in drpTag.Items)
                {
                    if (li.Selected)
                    {
                        tags += li.Value + "|";
                    }
                }
                tags = tags.TrimEnd('|');
                #endregion*/


                cat.ProductTags = "";

                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        cat.Id = Convert.ToInt32(idPid.Value);
                        int result = ProductDetails.UpdateProductDetails(conAP, cat);
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
                        string pGuid = Guid.NewGuid().ToString();
                        cat.ProductGuid = pGuid;
                        cat.UpdatedOn = CommonModel.UTCTime();
                        cat.UpdatedIp = CommonModel.IPAddress();
                        int result = ProductDetails.InsertProductDetails(conAP, cat);
                        if (result > 0)
                        {
                            cat.Id = result;
                            ddlCategory.ClearSelection(); 
                            txtProdName.Text = txtURL.Text = txtPTitle.Text = txtMetaDesc.Text = txtMetaDesc.Text = txtDelDate.Text = "";
                            chbDispHome.Checked = false; chkInStock.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product Added successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            Response.Redirect("add-products.aspx?id=" + result);
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
            }
            GetProductDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    //public string getLastId()
    //{
    //    string id = "";
    //    int lid = ProductDetails.GetProductLastId(conAP);
    //    id = "D2C" + (lid.ToString().Length < 3 ? lid.ToString("000") : lid.ToString());
    //    return id;
    //}

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
                ImageGuid1 = Guid.NewGuid().ToString().Replace(" ", "-").Replace(".", "");
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

    #endregion

    #region Product Prices
    [WebMethod(EnableSession = true)]
    public static List<ProductPrices> GetEditedProductPrices(string idPid)
    {
        List<ProductPrices> lpps = null;
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            lpps = ProductPrices.GetProductPriceByPId(conAP, idPid).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEditedProductPrices", ex.Message);
        }
        return lpps;
    }



    [WebMethod(EnableSession = true)]
    public static string AddPrices(string pdid, string prid, string sz, string act, string dis, string legth)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;
            ProductPrices cat = new ProductPrices();

            cat.ProductId = pdid;
            cat.ProductSize = sz;
            cat.ActualPrice = act;
            cat.DiscountPrice = dis;
            cat.ProductThickness = legth;
            cat.ProductLength = "";
            cat.AddedBy = aid;
            cat.AddedIp = CommonModel.IPAddress();
            cat.AddedOn = CommonModel.UTCTime();
            cat.Status = "Active";

            if (prid != "")
            {
                if (!CreateUser.CheckAccess(conAP, "add-products.aspx", "Edit", aid))
                {
                    x = "permission";
                    return x;
                }
                List<ProductPrices> Upexist = ProductPrices.GetProductPriceIdByDetails(conAP, pdid, act, sz, legth, prid);
                if (Upexist.Count > 0)
                {
                    x = "exist";
                    return x;
                }
                cat.Id = Convert.ToInt32(prid);
                int result = ProductPrices.UpdateProductPrice(conAP, cat);
                if (result > 0)
                {
                    x = "Updated";
                }
            }
            else
            {
                if (!CreateUser.CheckAccess(conAP, "add-products.aspx", "Add", aid))
                {
                    x = "permission";
                    return x;
                }
                List<ProductPrices> Inexist = ProductPrices.GetProductPriceIdByDetails(conAP, pdid, act, sz, legth);
                if (Inexist.Count > 0)
                {
                    x = "exist";
                    return x;
                }
                int result = ProductPrices.InsertProductPrice(conAP, cat);
                if (result > 0)
                {
                    x = "Inserted";
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddPrices", ex.Message);
            return "";
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteProductPrices(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "add-products.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductPrices pro = new ProductPrices();
                pro.Id = Convert.ToInt32(id);
                pro.Status = "Deleted";
                pro.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ProductPrices.DeleteProductPrice(conAP, pro);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductPrices", ex.Message);
        }
        return x;
    }
    #endregion

    #region product Seo
    protected void btnSeo_Click(object sender, EventArgs e)
    {
        lblProdSeo.Visible = true;
        try
        {
            string pageName = Path.GetFileName(Request.Path);
            ProductDetails cat = new ProductDetails();
            if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
            {
                cat.Id = Convert.ToInt32(idPid.Value);
                cat.PageTitle = txtPTitle.Text;
                cat.MetaKey = txtMKeys.Text;
                cat.MetaDesc = txtMetaDesc.Text;
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                int result = ProductDetails.UpdateProductSeoDetails(conAP, cat);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Product SEO Updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    // lblProdSeo.Text = "Product SEO Updated successfully.";
                    //lblProdSeo.Attributes.Add("class", "alert alert-success");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    //lblProdSeo.Text = "There is some problem now. Please try after some time";
                    // lblProdSeo.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator,actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    #endregion

    #region product images

    [WebMethod]
    public static List<ProductGallery> GetGalleryImage(string id)
    {
        List<ProductGallery> tr = new List<ProductGallery>();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            tr = ProductGallery.GetProductGallery(conAP, id).OrderBy(x => Convert.ToInt32(x.GalleryOrder)).ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGalleryImage", ex.Message);
        }
        return tr;
    }

    [WebMethod(EnableSession = true)]
    public static string ImageOrderUpdate(string id)
    {
        string x = "";
        try
        {
            string[] str = id.Split('|');
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "add-product.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var added_on = TimeStamps.UTCTime();
            var added_ip = CommonModel.IPAddress();

            for (int i = 0; i < str.Length; i++)
            {
                ProductGallery catG = new ProductGallery();
                catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                catG.GalleryOrder = Convert.ToString(i);
                catG.AddedOn = added_on;
                catG.AddedIp = added_ip;
                catG.AddedBy = aid;

                int res = ProductGallery.UpdateProductGalleryOrder(conAP, catG);
                if (res > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ImageOrderUpdate", ex.Message);
            x = "W";
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string InsertGallery(string pid, string lnk, string tp)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;
            if (!CreateUser.CheckAccess(conAP, "add-products.aspx", "Add", aid))
            {
                x = "permission";
                return x;
            }

            ProductGallery pi = new ProductGallery();
            pi.ProductId = Convert.ToString(pid);
            pi.GType = tp;
            pi.GalleryOrder = "1000";
            pi.Images = lnk;
            pi.AddedOn = TimeStamps.UTCTime();
            pi.AddedIp = CommonModel.IPAddress();
            pi.AddedBy = aid;
            pi.Status = "Active";
            int res = ProductGallery.InsertProductGallery(conAP, pi);
            if (res > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            x = "";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertGallery", ex.Message);
        }

        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteGallery(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "add-products.aspx", "Delete", aid))
            {
                x = "Permission";
                return x;
            }

            ProductGallery productGallery = new ProductGallery();
            productGallery.Id = Convert.ToInt32(id);
            productGallery.AddedOn = TimeStamps.UTCTime();
            productGallery.AddedIp = CommonModel.IPAddress();
            productGallery.AddedBy = aid;
            productGallery.Status = "Deleted";
            int exec = ProductGallery.DeleteProductGallery(conAP, productGallery);
            if (exec > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteGallery", ex.Message);
        }
        return x;
    }
    #endregion


    protected void addProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-products.aspx");
    }
}