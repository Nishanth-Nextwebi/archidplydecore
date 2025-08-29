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

public partial class Admin_view_subcategory : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strCategory = "", strSubCatImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllSubCategory();

        }
    }


    public void GetAllSubCategory()
    {
        try
        {
            strCategory = "";
            List<SubCategory> categories = SubCategory.GetAllSubCategory(conAP).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (SubCategory cat in categories)
            {
                strCategory += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td><a href='javascript:void(0);' ><img src='/" + cat.ImageUrl + @"' alt='' data-image='/"+cat.ImageUrl+@"' class='img-thumbnail rounded-circle avatar-sm viewImg'> </a></td>
                                                <td>" + cat.Category + @"</td>
                                                <td>" + cat.SubCategoryName + @"</td>
                                                <td>" + (cat.DisplayHome.ToLower() == "yes" ? "<span class='badge badge-outline-success'>Yes</span>" : "<span class='badge badge-outline-danger'>No</span>") + @"</td>
                                                <td><span class='badge badge-outline-primary'>" + cat.DisplayOrder + @"</span></td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + cat.UpdatedBy + @"' >" + cat.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</a></td>
                                                <td class='text-center'>
                                                <a href='subcategory.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllSubCategory", ex.Message);
        }
    }



    [WebMethod(EnableSession = true)]
    public static string DeleteSubCategory(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-subcategory.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                SubCategory cat = new SubCategory();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = SubCategory.DeleteSubCategory(conAP, cat);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteSubCategory", ex.Message);
        }
        return x;
    }
}