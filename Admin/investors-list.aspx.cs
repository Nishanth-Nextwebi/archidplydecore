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
public partial class Admin_investors_list : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strImages = "", strThumbImage = "", strThumbImage1 = "", strThmb = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Visible = false;
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllInvestorsList();
            if (Request.QueryString["id"] != null)
            {
                GetInvestor();
            }
        }
    }
    public void GetInvestor()
    {
        try
        {
            InvestorsList investor = InvestorsList.GetInvestorList(conAP, Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (investor != null)
            {
                btnSave.Text = "Update";
                AddNew.Visible = true;
                txtTitle.Text = investor.Title;
                txtlink.Text = investor.Link;
                if (investor.Logo != "")
                {
                    strThumbImage = "<img src='/" + investor.Logo + "' style='max-height:60px;' />";
                    lblThumb.Text = investor.Logo;
                }

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvestor", ex.Message);
        }
    }
    public void GetAllInvestorsList()
    {
        try
        {
            strImages = "";
            List<InvestorsList> imagess = InvestorsList.GetAllInvestorList(conAP).OrderByDescending(s => s.AddedOn).ToList();
            int i = 0;
            foreach (InvestorsList img in imagess)
            {
                strImages += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + img.Title + @"</td>
                                                <td><img src='/" + img.Logo + @"' style='max-height:50px;' /></td>
                                                <td><a href='" + img.Link + @"'><u><b>Link</b></u> </a></td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Added By : " + img.AddedOn + @"' >" + img.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'> 
<a href='investors-list.aspx?id=" + img.Id + @"' class='bs-tooltip fs-18' data-id='" + img.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
            if (CreateUser.CheckAccess(conAP, "investors-list.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                InvestorsList bis = new InvestorsList();
                bis.Id = Convert.ToInt32(id);
                bis.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                bis.Status = "Deleted";
                int exec = InvestorsList.DeleteInvestorsList(conAP, bis);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    if (btnSave.Text == "Update")
                    {
                        GetInvestor();
                    }
                    return;

                }
                else if (resmsg == "Size")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Desktop investor image size should be 1600*500 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    if (btnSave.Text == "Update")
                    {
                        GetInvestor();
                    }
                    return;

                }

                string pageName = Path.GetFileName(Request.Path);
                InvestorsList investor = new InvestorsList();
                investor.Title = txtTitle.Text.Trim();
                investor.Link = txtlink.Text.Trim() == "" ? "javascript:void(0)" : txtlink.Text.Trim();
                if (btnSave.Text == "Update")
                {
                    AddNew.Visible = true;
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        investor.Id = Convert.ToInt32(Request.QueryString["id"]);
                        investor.AddedBy = Request.Cookies["ap_aid"].Value;
                        investor.Logo = UploadImage();
                        int result = InvestorsList.UpdateInvestorList(conAP, investor);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Investor details updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            GetInvestor();
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
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {
                        if (!FileUpload1.HasFile)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please select a desktop image to upload!',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                            return;
                        }

                        investor.AddedBy = Request.Cookies["ap_aid"].Value;
                        investor.Logo = UploadImage();

                        int result = InvestorsList.InsertInvestorList(conAP, investor);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Investor details added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
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
            GetAllInvestorsList();

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
                    if (bmpPostedImageBig.Height == 128 && bmpPostedImageBig.Width == 128)
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
                CommonModel.CaptureException(Request.Url.PathAndQuery, "UploadImage", eeex.Message);
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
    protected void addNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("investors-list.aspx");
    }
}