using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_space : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strTag, strImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllTags();
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllTags();
            if (Request.QueryString["id"] != null)
            {
                GetTag();
            }
        }
    }
    public void GetAllTags()
    {
        try
        {
            strTag = "";
            List<Tags> categories = Tags.GetAllTags(conAP).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (Tags cat in categories)
            {
                strTag += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td><img src='/" + cat.TagImage + @"' style='max-height:50px;' /></td>
<td>" + cat.Title + @"</td>
                                                <td>" + (cat.DisplayHome.ToLower() == "yes" ? "<span class='badge badge-outline-success'>Yes</span>" : "<span class='badge badge-outline-danger'>No</span>") + @"</td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + cat.UpdatedBy + @"' >" + cat.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'>
                                                   <a href='Tags.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
    public void GetTag()
    {
        try
        {
            Tags categories = Tags.GetTags(conAP, Request.QueryString["id"]).FirstOrDefault();
            if (categories != null)
            {
                btnSave.Text = "Update";
                AddTags.Visible = true;
                txtTag.Text = categories.Title;
                txtDisplayOrder.Text = categories.DisplayOrder;
                chbDispHome.Checked = categories.DisplayHome == "Yes" ? true : false;
                txtUrl.Text = categories.TagURL;
                if (categories.TagImage != "")
                {
                    strImage = "<img src='/" + categories.TagImage + @"' style='max-height:60px;' />";
                    lblThumb.Text = categories.TagImage;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTag", ex.Message);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Tag image size should be 300*300 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    return;

                }
                Tags cat = new Tags();
                cat.Title = txtTag.Text.Trim();
                cat.TagURL = txtUrl.Text.Trim();
                cat.DisplayHome = chbDispHome.Checked ? "Yes" : "No";
                cat.DisplayOrder = txtDisplayOrder.Text.Trim() == "" ? "1000" : txtDisplayOrder.Text.Trim();
                cat.TagImage = UploadImage();
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;
                List<Tags> res = Tags.GetTagsByName(conAP, txtTag.Text.Trim());//.Where(s => s.Title.ToLower() == txtTag.Text.Trim().ToLower()).ToList();
                if (cat.TagImage == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Upload Image',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'Tag already exist...',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = Tags.UpdateTags(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Tag updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text:'Tag already exist...',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            cat.UpdatedIp = CommonModel.IPAddress();
                            cat.UpdatedOn = CommonModel.UTCTime();
                            int result = Tags.InsertTags(conAP, cat);
                            if (result > 0)
                            {
                                txtTag.Text = txtUrl.Text = "";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Tag added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
            GetAllTags();
            GetTag();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteTags(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "Tags.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                Tags cat = new Tags();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = Tags.DeleteTags(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteTags", ex.Message);
        }
        return x;
    }

    protected void AddTags_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tags.aspx");
    }
}