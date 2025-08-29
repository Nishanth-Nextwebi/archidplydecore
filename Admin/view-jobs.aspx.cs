using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_job : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strJobs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllJobs();
    }
    public void GetAllJobs()
    {
        try
        {
            strJobs = "";
            List<JobDetails> blog = JobDetails.GetAllJobs(conAP).OrderByDescending(s => s.AddedOn).ToList();
            int i = 0;
            foreach (JobDetails pro in blog)
            {
                string ft1 = pro.Status == "Active" ? "checked" : "";
                string sts = pro.Status == "Active" ? "<span id='sts_" + pro.Id + @"' class='badge badge-outline-success shadow fs-13'>Published</span>" : "<span id='sts_" + pro.Id + @"' class='badge badge-outline-warning shadow fs-13'>Draft</span>";
                string chk = @"<div class='text-center form-check form-switch form-switch-lg form-switch-success'>
                               <input type='checkbox' data-id='" + pro.Id + @"' class='form-check-input PublishJob' id='chk_" + pro.Id + @"' " + ft1 + @">
                               <span class='slider round'></span>
                              </div>";



                strJobs += @"<tr>
                                <td>" + (i + 1) + @"</td>    
                                <td>" + pro.JobTitle + @"</td>    
                                <td>" + pro.Location + @"</td>    
                                <td>" + pro.NoOfPositions + @"</td> 
                                <td>" + pro.PostedOn.ToString("dd/MM/yyyy") + @"</td> 
 <td>" + pro.Salary + @"</td> 
                                <td class='text-center'> 
<a href='add-job.aspx?id=" + pro.Id + @"' class='bs-tooltip fs-18' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllJobs", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-jobs.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                JobDetails pro = new JobDetails();
                pro.Id = Convert.ToInt32(id);
                pro.Status = "Deleted";
                int exec = JobDetails.DeleteJob(conAP, pro);
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
}