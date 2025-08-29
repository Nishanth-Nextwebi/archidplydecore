using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_blogs : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strBlogs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllBlogs();
    }
    public void GetAllBlogs()
    {
        try
        {
            strBlogs = "";
            List<Blogs> blog = Blogs.GetAllBlogs(conAP).OrderByDescending(s => s.UpdatedOn).ToList();
            int i = 0;
            foreach (Blogs pro in blog)
            {
                string pdby = pro.PostedBy == "" ? pro.AddedBy : pro.PostedBy;
                string ft1 = pro.Status == "Active" ? "checked" : "";
                string sts = pro.Status == "Active" ? "<span id='sts_" + pro.Id + @"' class='badge badge-outline-success shadow fs-13'>Published</span>" : "<span id='sts_" + pro.Id + @"' class='badge badge-outline-warning shadow fs-13'>Draft</span>";
                string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox' data-id='" + pro.Id + @"' class='form-check-input PublishBlog' id='chk_" + pro.Id + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";



                strBlogs += @"<tr>
                                <td>" + (i + 1) + @"</td>    
 <td><a href='/" + pro.BlogImg + @"'/><img src='/" + pro.BlogImg + @"' style='height:60px;' /></td>
                                        <td><a href='/" + pro.DetailImage + @"'/><img src='/" + pro.DetailImage + @"' style='height:60px;' /></td>
                                        
                                <td><a target='_blank' class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Preview Journal' href='/blog/" + pro.Url + @"'>" + pro.Title + @"</a></td>
                               

                                <td>" + pdby + @"</td> 
                                <td>" + pro.PostedOn.ToString("dd/MM/yyyy hh:mm tt") + @"</td> 
 <td>"+ sts + @"</td> 
 <td>" + chk + @"</td> 
                                <td class='text-center'> 

<a href='write-blog.aspx?id=" + pro.Id + @"' class='bs-tooltip fs-18' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                      </td>
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogs", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-blogs.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                Blogs pro = new Blogs();
                pro.Id = Convert.ToInt32(id);
                pro.Status = "Deleted";
                pro.UpdatedOn = CommonModel.UTCTime();
                pro.UpdatedIp = CommonModel.IPAddress();
                pro.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = Blogs.DeleteBlog(conAP, pro);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
    [WebMethod(EnableSession = true)]
    public static string PublishBlog(string id, string ftr)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-blogs.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                Blogs b = new Blogs();
                b.Id = Convert.ToInt32(id);
                b.Status = ftr == "Yes" ? "Active" : "Draft";
                b.UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = Blogs.PublishBlogs(conAP, b);
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "PublishBlog", ex.Message);
        }
        return x;
    }
}