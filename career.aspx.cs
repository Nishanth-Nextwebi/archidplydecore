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

public partial class career : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnApplyJob(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                var rPath = CheckPDFFormat();
                if (rPath == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid Document',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                string uid = HttpContext.Current.Request.Cookies["arch_i"] != null ? HttpContext.Current.Request.Cookies["arch_i"].Value : HttpContext.Current.Request.Cookies["arch_v"] != null ? HttpContext.Current.Request.Cookies["arch_v"].Value : "";
                JobApplications cat = new JobApplications();
                cat.Name = txtFullName.Text.Trim();
                cat.Email = txtEmail.Text.Trim();
                cat.UserGuid = uid;
                cat.Phone = txtPhone.Text.Trim();
                cat.Exp = txtExp.Text.Trim();
                cat.City = txtCity.Text.Trim();
                cat.CoverLetter = UploadPDFFormat();
                cat.InterestedField = txtInterst.Text.Trim();
                if (cat.CoverLetter == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please upload required document',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                int result = JobApplications.InserJobApplications(conAP, cat);
                if (result > 0)
                {
                    Emails.SendJobApply(txtFullName.Text, txtEmail.Text);
                    Emails.SendJobApplyToAdmin(txtFullName.Text, txtEmail.Text, txtInterst.Text, txtExp.Text, txtPhone.Text, txtCity.Text, cat.CoverLetter);
                    txtFullName.Text = txtEmail.Text = txtInterst.Text = txtExp.Text = txtPhone.Text=txtCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Applied successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }

    }
     private string CheckPDFFormat()
    {
        #region PDF
        string UploadPdf = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()), ImageGuid1 = CommonModel.seourl(txtFullName.Text.Trim()).ToLower();
                if ((fileExtension == ".pdf" || fileExtension == ".doc"|| fileExtension == ".jpg"|| fileExtension == ".png"|| fileExtension == ".jpeg"|| fileExtension == ".docx"))
                {

                    string iconPath = Server.MapPath(".") + "\\../Uploadpdf\\" + ImageGuid1 + fileExtension;
                }
                else
                {
                    return "Format";
                }
            }
            catch (Exception ex)
            {
                ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckPDFFormat", ex.Message);

            }
        }
        #endregion
        return UploadPdf;
    }

   public string UploadPDFFormat()
    {
        #region upload file
        string thumbfile = "";
        try
        {
            if (FileUpload1.HasFile)
            {
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()),
                ImageGuid1 = Guid.NewGuid().ToString() + "_resume".Replace(" ", "-").Replace(".", "");
                string iconPath = Server.MapPath(".") + "\\./UploadImages\\" + ImageGuid1 + "" + fileExtension;
                FileUpload1.SaveAs(iconPath);
                thumbfile = "/UploadImages/" + ImageGuid1 + "" + fileExtension;
            }
            else
            {
                thumbfile = lblpdf.Text;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UploadPDFFormat", ex.Message);

        }

        #endregion
        return thumbfile;
    }
}