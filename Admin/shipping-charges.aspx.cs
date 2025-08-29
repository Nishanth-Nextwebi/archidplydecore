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

public partial class Admin_shipping_charges : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strColors = "", strUKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            Getcharges();
        }
    }
    public void Getcharges()
    {
        try
        {
            List<ShippingCharges> charges = ShippingCharges.GetchargesByID(conAP, 1);//.Where(s => s.Id == Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (charges.Count > 0)
            {
                btnSave.Text = "Update";
                txtMinCart.Text = Convert.ToString(charges[0].MinCartPrice);
                txtCharge.Text = Convert.ToString(charges[0].ShippingCharge);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Getcharges", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                ShippingCharges charges = new ShippingCharges();
                charges.ShippingCharge = Convert.ToDecimal(txtCharge.Text);
                charges.MinCartPrice = Convert.ToDecimal(txtMinCart.Text);
                charges.UpdatedBy = Request.Cookies["ap_aid"].Value;
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", Request.Cookies["ap_aid"].Value))
                    {
                            charges.Id = 1;   // Convert.ToInt32(Request.QueryString["id"]);
                            int result = ShippingCharges.UpdateShippingCharges(conAP, charges);
                            if (result > 0)
                            {
                            Getcharges();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'charges updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                            }
                            else
                            {
                            Getcharges();
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
                            charges.AddedBy = Request.Cookies["ap_aid"].Value;
                            int result = ShippingCharges.InserShippingCharges(conAP, charges);
                            if (result > 0)
                            {
                            Getcharges();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'charges updated successfully!',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                            }
                            else
                            {
                            Getcharges();
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
            if (CreateUser.CheckAccess(conAP, "shipping-charges.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ShippingCharges cat = new ShippingCharges();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = ShippingCharges.DeleteShippingCharges(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }

    protected void addcharges_Click(object sender, EventArgs e)
    {
        Response.Redirect("shipping-charges.aspx");
    }
    /*public void GetAllcharges()
    {
        try
        {
            strColors = "";
            List<ShippingCharges> chargesCodes = ShippingCharges.GetAllcharges(conAP).OrderByDescending(s => Convert.ToDateTime(s.AddedOn)).ToList();
            int i = 0;
            foreach (ShippingCharges charges in chargesCodes)
            {
                strColors += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td>" + charges.MinCartPrice + @"</td><td>" + charges.ShippingCharge + @"</td>
                                                  <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Added By : " + charges.AddedBy + @"' >" + charges.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>  
                                                <td class='text-center'>
 <a href='shipping-charges.aspx?id=" + charges.Id + @"' class='bs-tooltip fs-18' data-id='" + charges.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + charges.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>

                                                     </td>

                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllcharges", ex.Message);
        }
    }*/
}