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

public partial class Admin_product_features : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strFeatures, strImg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllProductFeatures();
            if (Request.QueryString["id"] != null)
            {
                GetFeatures();
            }
        }
    }
    public void GetAllProductFeatures()
    {
        try
        {
            strFeatures = "";
            List<ProductFeatures> features = ProductFeatures.GetAllProductFeatures(conAP).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (ProductFeatures cat in features)
            {
                strFeatures += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td><a href='javascript:void(0);' ><img src='/" + cat.Image + @"' alt='' data-image='/" + cat.Image + @"' class='img-thumbnail rounded-circle avatar-sm viewImg'> </a></td> 
                                                <td>" + cat.Title + @"</td>
                                                    <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + cat.UpdatedBy + @"' >" + cat.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'>
                                                   <a href='product-features.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a> 
</td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSpace", ex.Message);
        }
    }
    public void GetFeatures()
    {
        try
        {
            ProductFeatures feat = ProductFeatures.GetProductFeatures(conAP, Request.QueryString["id"]).FirstOrDefault();
            if (feat != null)
            {
                btnSave.Text = "Update";
                AddFeatures.Visible = true;
                txtTag.Text = feat.Title;
                if (feat.Image != "")
                {
                    lblThumb.Text = feat.Image;
                    strImg = @"<img src='/" + feat.Image + @"' class='img-responsive' width='60px' height='60px' title='" + feat.Title + @"'/>";
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTag", ex.Message);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Image size should be 128 × 128 px.', actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                lblStatus.Visible = true;
                string pageName = Path.GetFileName(Request.Path);
                ProductFeatures cat = new ProductFeatures();
                cat.Title = txtTag.Text.Trim();
                cat.Image = UploadImage();
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                List<ProductFeatures> res = ProductFeatures.GetAllProductFeaturesByTitle(conAP, txtTag.Text.Trim());//.Where(s => s.Title.ToLower() == txtTag.Text.Trim().ToLower()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'Feature already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                                GetAllProductFeatures();
                            return;
                        }
                        else
                        {
                            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = ProductFeatures.UpdateProductFeatures(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Feature updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                                GetAllProductFeatures();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                                return;
                            }
                        }
                        GetFeatures();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        GetAllProductFeatures();
                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'Feature already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                                GetAllProductFeatures();
                            return;
                        }
                        else
                        {
                            cat.UpdatedIp = CommonModel.IPAddress();
                            cat.UpdatedOn = CommonModel.UTCTime();
                            int result = ProductFeatures.InsertProductFeatures(conAP, cat);
                            if (result > 0)
                            {
                                txtTag.Text = ""; ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Feature added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                                GetAllProductFeatures();
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
                    GetAllProductFeatures();
                }

            }
            GetAllProductFeatures();
            GetFeatures();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
                    if (bmpPostedImageBig.Height != 128 && bmpPostedImageBig.Width != 128)
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
    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "product-features.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductFeatures cat = new ProductFeatures();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ProductFeatures.DeleteProductFeatures(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductFeatures", ex.Message);
        }
        return x;
    }

    protected void AddFeatures_Click(object sender, EventArgs e)
    {
        Response.Redirect("product-features.aspx");
    }
}