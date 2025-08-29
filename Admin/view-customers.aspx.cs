using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Razorpay.Api;
using DocumentFormat.OpenXml.Office2010.Excel;

public partial class Admin_view_customers : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strUsers = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BindCustomers();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Page_Load", ex.Message);
        }
    }

    public void BindCustomers()
    {
        try
        {
            strUsers = "";
            if (!CreateUser.CheckAccess(conAP, "view-products.aspx", "View", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }
            else
            {
                var query = @"select * from Customers where Status!= 'Deleted' Order by Id Desc";
                using (SqlCommand cmd = new SqlCommand(query, conAP))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {


                        var id = Convert.ToInt32(dr["id"]);
                        var lgon = Convert.ToString(dr["LastLoggedIn"]) != "" ? Convert.ToDateTime(dr["LastLoggedIn"]).ToString("MMM dd, yyyy hh:mm tt") : "";
                        var guid = Convert.ToString(dr["UserGuid"]);

                        string ft1 = Convert.ToString(dr["Status"]) == "Blocked" ? "checked" : "";
                        string sts1 = Convert.ToString(dr["Status"]) == "Blocked" ? "<span id='sts_" + Convert.ToString(dr["Id"]) + @"' class='badge badge-outline-danger shadow fs-13'>Blocked</span>" : "<span id='sts_" + Convert.ToString(dr["Id"]) + @"' class='badge badge-outline-success shadow fs-13'>Verified</span>";
                        string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox'  role='switch' data-id='" + Convert.ToString(dr["Id"]) + @"' class='form-check-input blockItem' data-guid='" + guid + "' id='chk_" + Convert.ToString(dr["Id"]) + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";

                        strUsers += @"<tr>
                                     <td>" + (i + 1) + @"</td>
                                     <td>" + Convert.ToString(dr["FirstName"]) + @" " + Convert.ToString(dr["LastName"]) + @"</td>
                                     <td>" + Convert.ToString(dr["CustomerId"]) + @"</td> 
                                     <td>" + Convert.ToString(dr["ContactNo"]) + @"</td> 
                                     <td>" + Convert.ToString(dr["EmailId"]) + @"</td>
                                     <td>" + sts1 + @"</td>
                                     <td>" + chk + @"</td>
                                     <td>" + Convert.ToDateTime(dr["AddedOn"]).ToString("MMM dd, yyyy hh:mm tt") + @"</td> 
                                     <td>" + lgon + @"</td>
<td class='text-center'> 
                                            <a href = 'javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'><i class='mdi mdi-trash-can-outline'></i></a> 
                                         </a>
                                        </td>
                                   </tr>";
                        i++;

                    }
                }
            }
               
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindCustomers", ex.Message);
        }
    }

   
    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            if (CreateUser.CheckAccess(conAP, "view-customers.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                Userlogins BD = new Userlogins();
                BD.Id = Convert.ToInt32(id);
                BD.Status = "Deleted";
                int exec = Userlogins.BlockMember(conAP, BD);
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else {
                x = "Permission";
            }

                
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
    [WebMethod(EnableSession = true)]
    public static string VerifyCustomers(string id, string ftr)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-customers.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                var MD = new Userlogins()
                {
                    Id = Convert.ToInt32(id),
                    Status = ftr == "Yes" ? "Blocked" : "Verified",
                    AddedOn = TimeStamps.UTCTime(),
                    AddedIp = CommonModel.IPAddress(),
                    //PassKey = Guid.NewGuid().ToString(),
                };

                int exec = Userlogins.BlockMember(conAP, MD);
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "VerifyCustomers", ex.Message);
        }
        return x;
    }


}