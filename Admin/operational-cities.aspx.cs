using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_operational_cities : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strCities = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        GetAllOperationalCities();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetOperationalCities();
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
                string aid = Request.Cookies["ap_aid"].Value;
                List<OperationalCities> res = OperationalCities.GetOperationalCitiesByOpCity(conAP, txtCityName.Text.Trim().ToLower());
                OperationalCities cs = new OperationalCities();
                if (btnSave.Text == "Update")
                {
                    cs.Id = Convert.ToInt32(Request.QueryString["id"]);
                    cs.CityName = txtCityName.Text.Trim();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;

                    if (!CreateUser.CheckAccess(conAP, pageName, "Edit", aid))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'City already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    else
                    {
                        int result = OperationalCities.UpdateOperationalCities(conAP, cs);
                        if (result > 0)
                        {
                            GetAllOperationalCities();
                            GetOperationalCities();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'City updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                    }

                }
                else
                {
                    if (!CreateUser.CheckAccess(conAP, pageName, "Add", aid))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    cs.AddedBy = Request.Cookies["ap_aid"].Value;
                    if (res.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'City already exist...',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    cs.CityName = txtCityName.Text.Trim();
                    cs.AddedIp = CommonModel.IPAddress();
                    cs.AddedOn = TimeStamps.UTCTime();
                    cs.Status = "Active";

                    int result = OperationalCities.InsertOperationalCities(conAP, cs);
                    if (result > 0)
                    {
                        txtCityName.Text = "";
                        GetAllOperationalCities();
                        GetOperationalCities();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'City added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
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
    public void GetAllOperationalCities()
    {
        try
        {
            strCities = "";
            List<OperationalCities> cas = OperationalCities.GetAllOperationalCities(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (OperationalCities nb in cas)
            {

                strCities += @"<tr>
                                 <td>" + (i + 1) + @"</td>  
                                 <td>" + nb.CityName + @"</td>
                                 <td><a href='javascript:void();' class='bs-tooltip' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title=''>" + nb.AddedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                 <td class='text-center'>
                                        <a href='operational-cities.aspx?id=" + nb.Id + @"' class='bs-tooltip fs-18 link-info' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                           <i class='mdi mdi-pencil'></i></a>
                                         <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + nb.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title='Delete'>
                                            <i class='mdi mdi-trash-can-outline'></i></a></td>
                                  </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllOperationalCities", ex.Message);
        }
    }
    public void GetOperationalCities()
    {
        try
        {
            OperationalCities PD = OperationalCities.GetOperationalCities(conAP, Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (PD != null)
            {

                btnSave.Text = "Update";
                btnNew.Visible = true;
                txtCityName.Text = PD.CityName;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOperationalCities", ex.Message);
        }
    }
    protected void addNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("operational-cities.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteCity(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "operational-cities.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                OperationalCities cat = new OperationalCities();
                cat.Id = Convert.ToInt32(id);
                cat.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                int exec = OperationalCities.DeleteOperationalCities(conAP, cat);
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteOperationalCities", ex.Message);
        }
        return x;
    }

}