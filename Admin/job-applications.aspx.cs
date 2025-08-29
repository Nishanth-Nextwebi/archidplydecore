using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web;

public partial class Admin_job_applications : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strRequests = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllContactRequests();
    }
    public void GetAllContactRequests()
    {
        try
        {
            List<JobApplications> lcs = JobApplications.GetAllJobApplications(conAP);
            int i = 0;
            foreach (JobApplications pro in lcs)
            {
                strRequests += @"<tr>   
                                <td>" + (i + 1) + @"</td>                                           
                                <td>" + pro.Name + @"</td>
                               <td><a href='" + pro.CoverLetter + @"' Download/><img src='assets/images/pdf.png' style='height:60px;' /></td>
                               <td><a href='CompanyEmail:" + pro.Email + "'>" + pro.Email + @"</a></td>
                                <td>" + pro.Phone + @"</td> 
                                <td>" + pro.InterestedField + @"</td> 
                                <td>" + pro.Exp + @"</td> 
                                <td>" + pro.AddedOn.ToString("dd/MM/yyyy hh:mm tt") + @"</td> 
                                <td class='text-center'>
                                <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + pro.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                <i class='mdi mdi-trash-can-outline'></i></a></td>
                                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(Request.Url.PathAndQuery, "GetAllContactRequests", ex.Message);
        }
    }
    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            JobApplications BD = new JobApplications();
            BD.Id = Convert.ToInt32(id);
            int exec = JobApplications.DeleteJobApplications(conAP, BD);
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
