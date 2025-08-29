using RestSharp.Extensions;
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
public partial class Admin_manage_our_reports : System.Web.UI.Page

{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strInvesterRelations = "", strPDF = "", strInvesterTitle = "", strInvesterId = "";
    public List<InvesterRelations> InvesterRelationss = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetInvesterRelType();
        if (Request.QueryString["id"] != null)
        {
            GetAllInvesterRelationss();
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["fid"] != null)
            {
                GetInvestorType();
            }
        }
    }

    public void GetAllInvesterRelationss()
    {
        try
        {
            strInvesterRelations = "";
            InvesterRelationss = InvesterRelations.GetInvesterRelations(conAP, Request.QueryString["id"]);
            int i = 0;
            foreach (InvesterRelations cat in InvesterRelationss)
            {
                strInvesterRelations += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + cat.Title + @"</td> 
                                                <td>" + cat.DisplayOrder + @"</td> 
                                                <td><a href='/" + cat.PDF + @"' download target='_blank'/><img src='assets/images/pdf.png' style='height:60px;' /></td>
                                                <td>" + cat.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                                                <td class='text-center'>
                                                <a href='add-Investor-relations.aspx?fid=" + cat.Id + "&id=" + Request.QueryString["id"] + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvesterRelationss", ex.Message);
        }
    }
    public string UploadPDFFormat()
    {
        #region upload file
        string thumbfile = "";
        try
        {
            if (UploadPDF.HasFile)
            {
                string fileExtension = Path.GetExtension(UploadPDF.PostedFile.FileName.ToLower()),
                ImageGuid1 = Guid.NewGuid().ToString() + "_Reports".Replace(" ", "-").Replace(".", "");
                string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
                UploadPDF.SaveAs(iconPath);
                thumbfile = "UploadImages/" + ImageGuid1 + "" + fileExtension;
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
    private string CheckPDFFormat()
    {
        #region PDF
        string UploadPdf = "";
        if (UploadPDF.HasFile)
        {
            try
            {
                string fileExtension = Path.GetExtension(UploadPDF.PostedFile.FileName.ToLower()), ImageGuid1 = CommonModel.seourl(txtTitle.Text.Trim()).ToLower();
                if ((fileExtension == ".pdf" || fileExtension == ".doc"|| fileExtension == ".jpg"|| fileExtension == ".xls"|| fileExtension == ".jpeg"|| fileExtension == ".png"))
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
    public void GetInvestorType()
    {
        try
        {
            List<InvesterRelations> faqs = InvesterRelations.GetInvesterRelationsById(conAP, Request.QueryString["fid"]);//.Where(x => x.Id == Convert.ToInt32(Request.QueryString["fid"])).ToList();
            if (faqs.Count > 0)
            {
                btnSave.Text = "Update";
                txtTitle.Text = faqs[0].Title;
                txtOrder.Text = faqs[0].DisplayOrder;
                if (faqs[0].PDF != "")
                {
                    divpdf.Visible = true;
                    strPDF = faqs[0].PDF;
                }

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelations", ex.Message);
        }
    }

    public void GetInvesterRelType()
    {
        try
        {
            InvesterRelType pro = InvesterRelType.GetInvesterRelTypeByGuid(conAP, Request.QueryString["id"]);
            if (pro != null)
            {
                strInvesterId = pro.InversterGuid.ToString();
                strInvesterTitle = pro.Title;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelType", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            var rPath = CheckPDFFormat();
            if (rPath == "Format")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid PDF formate.Please upload correct document',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }
            string pageName = Path.GetFileName(Request.Path);
            if (Page.IsValid)
            {
                InvesterRelations cat = new InvesterRelations();
                if (btnSave.Text == "Update")
                {

                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        cat.Id = Convert.ToInt32(Request.QueryString["fid"]);
                        cat.InversterGuid = strInvesterId;
                        cat.Title = txtTitle.Text.Trim();
                        cat.DisplayOrder = txtOrder.Text.Trim();
                        cat.PDF = UploadPDFFormat();    
                        cat.AddedBy = Request.Cookies["ap_aid"].Value;
                        cat.AddedOn = CommonModel.UTCTime();
                        cat.Status = "Active";
                        int result = InvesterRelations.UpdateInvesterRelations(conAP, cat);
                        if (result > 0)
                        {
                            GetAllInvesterRelationss();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Investor Relations updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
                        cat.InversterGuid = strInvesterId;
                        cat.Title = txtTitle.Text.Trim();
                        cat.DisplayOrder = txtOrder.Text.Trim();
                        cat.PDF = UploadPDFFormat();
                        cat.AddedBy = Request.Cookies["ap_aid"].Value;
                        cat.AddedOn = CommonModel.UTCTime();
                        cat.Status = "Active";
                        int result = InvesterRelations.InsertInvesterRelations(conAP, cat);
                            if (result > 0)
                        {
                            GetAllInvesterRelationss();
                            txtTitle.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Investor Relations added successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "add-Investor-relations.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                InvesterRelations cat = new InvesterRelations();
                cat.Id = Convert.ToInt32(id);
                cat.AddedOn = CommonModel.UTCTime();
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = InvesterRelations.DeleteInvesterRelations(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteP", ex.Message);
        }
        return x;
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        var url = Request.QueryString["id"];
        Response.Redirect("add-Investor-relations.aspx?id="+ url + "");
    }

}





































/*{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strAnualReports = "", strPDF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetAllInvesterRelations();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetInvesterRelations();
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
                var rPath = CheckPDFFormat();
                if (rPath == "Format")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid PDF formate.Please upload correct document',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    return;
                }
                string pageName = Path.GetFileName(Request.Path);
                InvesterRelations cs = new InvesterRelations();
                if (btnSave.Text == "Update")
                {
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.InversterGuid = Request.QueryString["InversterGuid"];
                    cs.Title = txtTitle.Text.Trim();
                    cs.PDF = UploadPDFFormat();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;

                    if (CreateUser.CheckAccess(conAP, pageName, "edit", Request.Cookies["ap_aid"].Value))
                    {
                        int result = InvesterRelations.UpdateInvesterRelations(conAP, cs);
                        if (result > 0)
                        {
                            GetAllInvesterRelations();
                            GetInvesterRelations();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Reports updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);


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
                    cs.Title = txtTitle.Text.Trim();
                    cs.PDF = UploadPDFFormat();
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;
                    if (cs.PDF == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please Upload PDF',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    if (CreateUser.CheckAccess(conAP, pageName, "add", Request.Cookies["ap_aid"].Value))
                    {
                        int result = InvesterRelations.InsertInvesterRelations(conAP, cs);
                        if (result > 0)
                        {
                            txtTitle.Text = "";
                            GetAllInvesterRelations();
                            GetInvesterRelations();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Reports added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
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
    public void GetAllInvesterRelations()
    {
        try
        {
            strAnualReports = "";
            List<InvesterRelations> cas = InvesterRelations.GetAllInvesterRelations(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (InvesterRelations nb in cas)
            {

                strAnualReports += @"<tr>
                                 <td>" + (i + 1) + @"</td>  
                                 <td>" + nb.Title + @"</td>
                                 <td><a href='/" + nb.PDF + @"' target='_blank'/><img src='assets/images/pdf.png' style='height:60px;' /></td>
                                 <td><a href='javascript:void();' class='bs-tooltip' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title=''>" + nb.AddedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                 <td class='text-center'>
                                        <a href='anual-reports.aspx?id=" + nb.Id + @"' class='bs-tooltip fs-18 link-info' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                           <i class='mdi mdi-pencil'></i></a>
                                         <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title='Delete'>
                                            <i class='mdi mdi-trash-can-outline'></i></a></td>
                                  </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvesterRelations", ex.Message);
        }
    }
    public void GetInvesterRelations()
    {
        try
        {
            InvesterRelations PD = InvesterRelations.GetInvesterRelations(conAP, Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (PD != null)
            {

                btnSave.Text = "Update";
                btnNew.Visible = true;
                txtTitle.Text = PD.Title;
                if (PD.PDF != "")
                {
                    divpdf.Visible = true;
                    strPDF = PD.PDF;
                }

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelations", ex.Message);
        }
    }
    protected void addNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("anual-reports.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteInvesterRelations(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "anual-reports.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                InvesterRelations cat = new InvesterRelations();
                cat.Id = Convert.ToInt32(id);
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                int exec = InvesterRelations.DeleteInvesterRelations(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteInvesterRelations", ex.Message);
        }
        return x;
    }
    private string CheckPDFFormat()
    {
        #region PDF
        string UploadPdf = "";
        if (UploadPDF.HasFile)
        {
            try
            {
                string fileExtension = Path.GetExtension(UploadPDF.PostedFile.FileName.ToLower()), ImageGuid1 = CommonModel.seourl(txtTitle.Text.Trim()).ToLower();
                if ((fileExtension == ".pdf" || fileExtension == ".doc"))
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
            if (UploadPDF.HasFile)
            {
                string fileExtension = Path.GetExtension(UploadPDF.PostedFile.FileName.ToLower()),
                ImageGuid1 = Guid.NewGuid().ToString() + "_Reports".Replace(" ", "-").Replace(".", "");
                string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
                UploadPDF.SaveAs(iconPath);
                thumbfile = "UploadImages/" + ImageGuid1 + "" + fileExtension;
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
*/