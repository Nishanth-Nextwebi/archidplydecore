using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_job : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Visible = false;
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetJobDetails();
            }
        }
    }
    public void GetJobDetails()
    {
        try
        {
            List<JobDetails> pro = JobDetails.GetJobDetailsById(conAP, Convert.ToInt32(Request.QueryString["id"]));
            if (pro.Count > 0)
            {
                btnSave.Text = "Update";
                addNew.Visible = true;
                txtTitle.Text = pro[0].JobTitle;
                txtNoOfPositions.Text = pro[0].NoOfPositions;
                txtSkill.Text = pro[0].KeySkills;
                txtURL.Text = pro[0].JobUrl;
                txtDesc.Text = pro[0].FullDesc;
                txtExperience.Text = pro[0].ExperienceYears;
                txtLocation.Text = pro[0].Location;
                txtSalary.Text = pro[0].Salary;
                txtShortDesc.Text = pro[0].ShortDesc;
                txtShortDesc.Text = pro[0].ShortDesc;
                txtShortDesc.Text = pro[0].ShortDesc;
                txtDesc.Text = pro[0].FullDesc;
                txtDate.Text = pro[0].PostedOn.ToString("dd/MMM/yyyy");
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetJobDetails", ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                JobDetails pro = new JobDetails();
                pro.JobTitle = txtTitle.Text.Trim();
                pro.JobUrl = Regex.Replace(txtURL.Text.Trim(), @"[^0-9a-zA-Z-]+", "-");
                pro.Location = txtLocation.Text.Trim();
                pro.NoOfPositions = txtNoOfPositions.Text.Trim();
                pro.PostedOn = txtDate.Text == "" ? CommonModel.UTCTime() : Convert.ToDateTime(txtDate.Text);
                pro.ExperienceYears = txtExperience.Text;
                pro.Salary = txtSalary.Text;
                pro.KeySkills = txtSkill.Text;
                pro.FullDesc = txtDesc.Text;
                pro.ShortDesc = txtShortDesc.Text;
                string aid = Request.Cookies["ap_aid"].Value;
                pro.AddedBy = aid;
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conAP, pageName, "Edit", aid))
                    {
                        pro.Id = Convert.ToInt32(Request.QueryString["id"]);
                        pro.AddedBy = aid;
                        pro.Status = "Status";
                        int result = JobDetails.UpdateJobDetails(conAP, pro);
                        if (result > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Job updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
                    if (CreateUser.CheckAccess(conAP, pageName, "Add", aid))
                    {
                        pro.AddedBy = aid;
                        pro.Status = "Active";
                        pro.AddedIp = CommonModel.IPAddress();
                        pro.AddedOn = CommonModel.UTCTime();
                        int result = JobDetails.AddJobDetails(conAP, pro);
                        if (result > 0)
                        {
                            txtTitle.Text = txtDesc.Text = txtURL.Text = txtDate.Text = txtLocation.Text = txtExperience.Text = txtDesc.Text = txtShortDesc.Text = txtSkill.Text = txtSalary.Text= txtNoOfPositions.Text="";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Job added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

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
                GetJobDetails();
            }
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-Job.aspx");
    }
}