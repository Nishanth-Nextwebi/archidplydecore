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
public partial class Admin_add_Investor_relations : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strInvesterRelType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetAllInvesterRelType();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetInvesterRelType();
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
                InvesterRelType cs = new InvesterRelType();
                if (btnSave.Text == "Update")
                {
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.DisplayOrder = txtOrder.Text.Trim();
                    cs.Title = txtTitle.Text.Trim();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;

                    if (CreateUser.CheckAccess(conAP, pageName, "edit", Request.Cookies["ap_aid"].Value))
                    {
                        int result = InvesterRelType.UpdateInvesterRelType(conAP, cs);
                        if (result > 0)
                        {
                            GetAllInvesterRelType();
                            GetInvesterRelType();
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
                    cs.InversterGuid = Guid.NewGuid().ToString();
                    cs.Title = txtTitle.Text.Trim();
                    cs.DisplayOrder = txtOrder.Text.Trim();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;
                    if (CreateUser.CheckAccess(conAP, pageName, "add", Request.Cookies["ap_aid"].Value))
                    {
                        int result = InvesterRelType.InsertInvesterRelType(conAP, cs);
                        if (result > 0)
                        {
                            txtTitle.Text = "";
                            GetAllInvesterRelType();
                            GetInvesterRelType();
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
    public void GetAllInvesterRelType()
    {
        try
        {
            strInvesterRelType = "";
            List<InvesterRelType> cas = InvesterRelType.GetAllInvesterRelType(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (InvesterRelType nb in cas)
            {

                strInvesterRelType += @"<tr>
                                 <td>" + (i + 1) + @"</td>  
                                 <td>" + nb.Title + @"</td>
                                 <td>" + nb.DisplayOrder + @"</td>
                                 <td><a href='add-Investor-relations.aspx?id=" + nb.InversterGuid + @"'class='btn btn-sm btn-secondary' data-id='"+nb.Id+ @"' data-toggle='tooltip' data-placement='top' title='Manage Investors' data-original-title='Manage Investors' />Click</td>
                                 <td><a href='javascript:void();' class='bs-tooltip' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title=''>" + nb.AddedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                 <td class='text-center'>
                                        <a href='add-Investor-type.aspx?id=" + nb.Id + @"' class='bs-tooltip fs-18 link-info' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                           <i class='mdi mdi-pencil'></i></a>
                                         <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title='Delete'>
                                            <i class='mdi mdi-trash-can-outline'></i></a></td>
                                  </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllInvesterRelType", ex.Message);
        }
    }
    public void GetInvesterRelType()
    {
        try
        {
            InvesterRelType PD = InvesterRelType.GetInvesterRelType(conAP, Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (PD != null)
            {

                btnSave.Text = "Update";
                btnNew.Visible = true;
                txtTitle.Text = PD.Title;
                txtOrder.Text = PD.DisplayOrder;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetInvesterRelType", ex.Message);
        }
    }
    protected void addNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-Investor-type.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteInvesterRelType(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "add-Investor-type.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                InvesterRelType cat = new InvesterRelType();
                cat.Id = Convert.ToInt32(id);
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                int exec = InvesterRelType.DeleteInvesterRelType(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteInvesterRelType", ex.Message);
        }
        return x;
    }

}