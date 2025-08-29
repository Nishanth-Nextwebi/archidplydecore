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
public partial class Admin_manage_client_stories : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strStories = "", strImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetAllStories();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetStories();
            }
            else
            {

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
               string pageName = Path.GetFileName(Request.Path);
                /*   string resmsg = CheckImageFormat();
                  if (resmsg == "Format")
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                      return;
                  }
                  else if (resmsg == "Size")
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Image size should be 300*300 px',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                      return;
                  }*/
                ClientStories cs = new ClientStories();
                if (btnSave.Text == "Update")
                {
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.Name = txtName.Text.Trim();
                    cs.Details = txtDetails.Text.Trim();
                    cs.Designation = txtDesignation.Text.Trim();
                    cs.Image = "";//UploadImage();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;

                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        int result = ClientStories.UpdateClientStories(conAP, cs);
                        if (result > 0)
                        {
                            GetAllStories();
                            GetStories();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Client Story updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
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
                    cs.Name = txtName.Text.Trim();
                    cs.Details = txtDetails.Text.Trim();
                    cs.Designation = txtDesignation.Text.Trim();
                    cs.Image = "";// UploadImage();
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;
                   /* if (cs.Image == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Upload Image',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }*/

                    if (CreateUser.CheckAccess(conAP, pageName, "add", Request.Cookies["ap_aid"].Value))
                    {
                        int result = ClientStories.InsertClientStories(conAP, cs);
                        if (result > 0)
                        {
                            txtName.Text = txtDetails.Text = "";
                            GetAllStories();
                            GetStories();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Client Story added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
   /* public string CheckImageFormat()
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
    }*/
    public void GetAllStories()
    {
        try
        {
            strStories = "";
            List<ClientStories> cas = ClientStories.GetAllClientStories(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (ClientStories nb in cas)
            {

                strStories += @"<tr>
                                 <td>" + (i + 1) + @"</td>  
                                 <td>" + nb.Name + @"</td>
                                 <td>" + nb.Designation + @"</td>
                                 <td class='text-wrap-auto'>" + nb.Details + @"</td>
                                 <td><a href='javascript:void();' class='bs-tooltip' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title=''>" + nb.AddedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                 <td class='text-center'>
                                        <a href='manage-client-stories.aspx?id=" + nb.Id + @"' class='bs-tooltip fs-18 link-info' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                           <i class='mdi mdi-pencil'></i></a>
                                         <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title='Delete'>
                                            <i class='mdi mdi-trash-can-outline'></i></a></td>
                                  </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllClientStories", ex.Message);
        }
    }
    public void GetStories()
    {
        try
        {
            ClientStories PD = ClientStories.GetClientStories(conAP, Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (PD != null)
            {

                btnSave.Text = "Update";
                btnNew.Visible = true;
                txtName.Text = PD.Name;
                txtDesignation.Text = PD.Designation;
                txtDetails.Text = PD.Details;

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetClientStories", ex.Message);
        }
    }
    protected void addNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("manage-client-stories.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteStories(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "manage-client-stories.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ClientStories cat = new ClientStories();
                cat.Id = Convert.ToInt32(id);
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                int exec = ClientStories.DeleteClientStories(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteStories", ex.Message);
        }
        return x;
    }

}