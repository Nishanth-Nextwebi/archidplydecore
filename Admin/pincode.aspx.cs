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

public partial class Admin_pincode : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strAllPincode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            GetAllPincode();
            if (Request.QueryString["id"] != null)
            {
                GetPincode();
            }
        }
    }
    public void GetAllPincode()
    {
        try
        {
            strAllPincode = "";
            List<PinCode> categories = PinCode.GetAllPincode(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (PinCode cat in categories)
            {
                strAllPincode += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + cat.Pincode + @"</td>
                                                <td>" + cat.City + @"</td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + cat.UpdatedBy + @"' >" + cat.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'>
                                                  <a href='pincode.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='"+ cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPincode", ex.Message);
        }
    }
    public void GetPincode()
    {
        try
        {
            List<PinCode> categories = PinCode.GetPincodeById(conAP, Convert.ToInt32(Request.QueryString["id"]));//.Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (categories.Count > 0)
            {
                btnSave.Text = "Update";
                addPincode.Visible = true;
                txtPinCode.Text = categories[0].Pincode;
                txtcity.Text = categories[0].City;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetPincode", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                PinCode cat = new PinCode();
                cat.Pincode = txtPinCode.Text.Trim();
                cat.ShippingPrice = txtShip.Text.Trim()==""?"": cat.ShippingPrice = txtShip.Text.Trim();
                cat.City = txtcity.Text.Trim();
                cat.UpdatedBy = Request.Cookies["ap_aid"].Value;

                List<PinCode> res = PinCode.GetPincodeByPinCode(conAP, txtPinCode.Text.Trim());//.Where(s => s.Pincode.ToLower() == txtCategory.Text.Trim().ToLower()).ToList();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                        if (res.Count > 0 && res[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Pincode already exist..',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                        else
                        {
                            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                            int result = PinCode.UpdatePincode(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Pincode updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                         
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", Request.Cookies["ap_aid"].Value))
                    {

                        if (res.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Pincode already exist..',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                        else
                        {
                            cat.AddedBy = Request.Cookies["ap_aid"].Value;
                            int result = PinCode.InsertPincode(conAP, cat);
                            if (result > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Pincode added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                                txtPinCode.Text = txtcity.Text = txtShip.Text = "";
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops!There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }

            }
            GetAllPincode();
            GetPincode();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeletePincode(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "pincode.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                PinCode cat = new PinCode();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = PinCode.DeletePincode(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeletePincode", ex.Message);
        }
        return x;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetAllPincode();
    }

    protected void addPincode_Click(object sender, EventArgs e)
    {
        Response.Redirect("pincode.aspx");
    }
}