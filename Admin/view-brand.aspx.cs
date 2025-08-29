using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_brand : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strBrands = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllBrand();
    }
    public void GetAllBrand()
    {
        try
        {
            strBrands = "";
            List<Brand> categories = Brand.GetAllBrand(conAP).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (Brand cat in categories)
            {
                strBrands += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td><a href='javascript:void(0);'><img src='/" + cat.ImageUrl + @"' data-image='/" + cat.ImageUrl + @"' alt='' class='img-thumbnail rounded-circle avatar-sm viewImg'> </a></td>
                                                <td>" + cat.BrandName + @"</td>
                                                <td>" + (cat.DisplayHome.ToLower() == "yes" ? "<span class='badge badge-outline-success'>Yes</span>" : "<span class='badge badge-outline-danger'>No</span>") + @"</td>
                                                <td><span class='badge badge-outline-primary'>" + cat.DisplayOrder + @"</span></td>
                                                <td><a class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By : " + cat.UpdatedBy + @"' >" + cat.UpdatedOn.ToString("dd-MMM-yyyy") + @"</a></td>
                                                 <td>
                                                       <a href='add-brand.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit Brand'><i class='mdi mdi-pencil'></i></a>
                                                       <a href='javascript:void(0);' class='bs-tooltip deleteItem link-danger fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete Brand'><i class='mdi mdi-trash-can-outline'></i></a>
                                                    </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "GetAllBrand", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "view-brand.aspx", "Delete", aid))
            {
                x = "Permission";
                return x;
            }

            Brand cat = new Brand();

            cat.Id = Convert.ToInt32(id);
            cat.UpdatedOn = TimeStamps.UTCTime();
            cat.UpdatedIp = CommonModel.IPAddress();
            cat.UpdatedBy = aid;
            cat.Status = "Deleted";

            int exec = Brand.DeleteBrand(conAP, cat);
            if (exec > 0)
            {
                x = "Success";
            }
            else
            {
                x = "W";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
}