using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using NLog.Fluent;

/// <summary>
/// Summary description for JobDetails
/// </summary>
public class JobDetails
{
    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string JobUrl { get; set; }
    public DateTime PostedOn { get; set; }
    public string NoOfPositions { get; set; }
    public string ShortDesc { get; set; }
    public string FullDesc { get; set; }
    public string KeySkills { get; set; }
    public string Location { get; set; }
    public string ExperienceYears { get; set; }
    public string Salary { get; set; }
    public string AddedIp { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }
    public static int AddJobDetails(SqlConnection conAP, JobDetails Job)
    {
        int result = 0;
        try
        {
            string query = "Insert Into JobDetails (JobTitle,JobUrl,PostedOn,KeySkills,Salary,ExperienceYears,NoOfPositions,Location,FullDesc,AddedOn,AddedIp,Status,AddedBy,ShortDesc) values(@JobTitle,@JobUrl,@PostedOn,@KeySkills,@Salary,@ExperienceYears,@NoOfPositions,@Location,@FullDesc,@AddedOn,@AddedIp,@Status,@AddedBy,@ShortDesc)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = Job.PostedOn;
                cmd.Parameters.AddWithValue("@JobTitle", SqlDbType.NVarChar).Value = Job.JobTitle;
                cmd.Parameters.AddWithValue("@KeySkills", SqlDbType.NVarChar).Value = Job.KeySkills;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = Job.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = Job.ShortDesc;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = Job.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = Job.AddedIp;
                cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = Job.Location;
                cmd.Parameters.AddWithValue("@JobUrl", SqlDbType.NVarChar).Value = Job.JobUrl;
                cmd.Parameters.AddWithValue("@Salary", SqlDbType.NVarChar).Value = Job.Salary;
                cmd.Parameters.AddWithValue("@NoOfPositions", SqlDbType.NVarChar).Value = Job.NoOfPositions;
                cmd.Parameters.AddWithValue("@ExperienceYears", SqlDbType.NVarChar).Value = Job.ExperienceYears;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = Job.AddedOn;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = Job.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddJobDetails", ex.Message);
        }
        return result;
    }

    public static int UpdateJobDetails(SqlConnection conAP, JobDetails Job)
    {
        int result = 0;
        try
        {
            string query = "Update JobDetails Set JobTitle=@JobTitle,JobUrl=@JobUrl,PostedOn=@PostedOn,KeySkills=@KeySkills,Salary=@Salary,ExperienceYears=@ExperienceYears,NoOfPositions=@NoOfPositions,Location=@Location,FullDesc=@FullDesc,AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status,AddedBy=@AddedBy,ShortDesc=@ShortDesc Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = Job.Id;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = Job.PostedOn;
                cmd.Parameters.AddWithValue("@JobTitle", SqlDbType.NVarChar).Value = Job.JobTitle;
                cmd.Parameters.AddWithValue("@KeySkills", SqlDbType.NVarChar).Value = Job.KeySkills;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = Job.FullDesc;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = Job.ShortDesc;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = Job.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = Job.Location;
                cmd.Parameters.AddWithValue("@JobUrl", SqlDbType.NVarChar).Value = Job.JobUrl;
                cmd.Parameters.AddWithValue("@Salary", SqlDbType.NVarChar).Value = Job.Salary;
                cmd.Parameters.AddWithValue("@NoOfPositions", SqlDbType.NVarChar).Value = Job.NoOfPositions;
                cmd.Parameters.AddWithValue("@ExperienceYears", SqlDbType.NVarChar).Value = Job.ExperienceYears;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = Job.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateJobDetails", ex.Message);
        }
        return result;
    }
    public static List<JobDetails> GetJobDetailsById(SqlConnection conAP, int id)
    {
        List<JobDetails> jbs = new List<JobDetails>();
        try
        {
            string query = "Select * from JobDetails Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                jbs = (from DataRow dr in dt.Rows
                         select new JobDetails()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             JobTitle = Convert.ToString(dr["JobTitle"]),
                             JobUrl = Convert.ToString(dr["JobUrl"]),
                             Location = Convert.ToString(dr["Location"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             Salary = Convert.ToString(dr["Salary"]),
                             KeySkills = Convert.ToString(dr["KeySkills"]),
                             ExperienceYears = Convert.ToString(dr["ExperienceYears"]),
                             NoOfPositions = Convert.ToString(dr["NoOfPositions"]),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetJobDetailsById", ex.Message);
        }
        return jbs;
    }
    public static List<JobDetails> GetAllJobs(SqlConnection conAP)
    {
        List<JobDetails> logs = new List<JobDetails>();
        try
        {
            string query = "Select * from JobDetails where Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                logs = (from DataRow dr in dt.Rows
                         select new JobDetails()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             JobTitle = Convert.ToString(dr["JobTitle"]),
                             JobUrl = Convert.ToString(dr["JobUrl"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             Location = Convert.ToString(dr["Location"]),
                             NoOfPositions = Convert.ToString(dr["NoOfPositions"]),
                             ExperienceYears = Convert.ToString(dr["ExperienceYears"]),
                             Salary = Convert.ToString(dr["Salary"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIp = Convert.ToString(dr["AddedIp"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllJobs", ex.Message);
        }
        return logs;
    }
    public static int DeleteJob(SqlConnection conAP, JobDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update JobDetails Set Status=@Status Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteJob", ex.Message);
        }
        return result;
    }
}