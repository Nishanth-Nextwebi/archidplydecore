using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CreateUser
/// </summary>
public class CreateUser
{
    public CreateUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region User login management
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string UserRole { get; set; }
    public string EmailId { get; set; }
    public string ContactNo { get; set; }
    public string UserGuid { get; set; }
    public string PassKey { get; set; }
    public string Status { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIP { get; set; }
    public string AddedBy { get; set; }
    public DateTime? log_time { get; set; }
    public string log_ip { get; set; }
    public string ProfileImage { get; set; }
    public string RoleName { get; set; }


    #region manage a user

    public static List<CreateUser> GetAllUser(SqlConnection conAP)
    {
        List<CreateUser> companies = new List<CreateUser>();
        try
        {
            string query = "Select *,(select top 1 RoleName from UserRoles where Id=UserRole and Status!='Deleted')as RoleName, (Select top 1 UserName From CreateUser Where UserGuid=CreateUser.AddedBy) as AddedBy1 from CreateUser where Status !='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                companies = (from DataRow dr in dt.Rows
                             select new CreateUser()
                             {
                                 Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                 EmailId = Convert.ToString(dr["EmailId"]),
                                 UserName = Convert.ToString(dr["UserName"]),
                                 RoleName = Convert.ToString(dr["RoleName"]),
                                 AddedBy = Convert.ToString(dr["AddedBy1"]),
                                 UserRole = Convert.ToString(dr["UserRole"]),
                                 ContactNo = Convert.ToString(dr["ContactNo"]),
                                 AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                 AddedIP = Convert.ToString(dr["AddedIP"]), 
                                 Password = Convert.ToString(dr["Pwd"]),
                                 UserGuid = Convert.ToString(dr["UserGuid"]),
                                 UserId = Convert.ToString(dr["UserId"]),
                                 PassKey = Convert.ToString(dr["pass_key"]),
                                 ProfileImage = Convert.ToString(dr["ProfileImage"]),
                                 log_time = Convert.ToString(dr["log_time"]) == "" ? (DateTime?)null : Convert.ToDateTime(Convert.ToString(dr["log_time"])),
                                 log_ip = Convert.ToString(dr["log_ip"]),
                                 Status = Convert.ToString(dr["Status"])
                             }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUser", ex.Message);
        }
        return companies;
    }
    public static List<CreateUser> GetUserDetailsById(SqlConnection conAP, int id)
    {
        List<CreateUser> companies = new List<CreateUser>();
        try
        {
            string query = "Select *, (Select top 1 UserName From CreateUser Where UserGuid=CreateUser.AddedBy) as AddedBy1 from CreateUser where Status !='Deleted' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                companies = (from DataRow dr in dt.Rows
                             select new CreateUser()
                             {
                                 Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                 EmailId = Convert.ToString(dr["EmailId"]),
                                 UserName = Convert.ToString(dr["UserName"]),
                                 AddedBy = Convert.ToString(dr["AddedBy1"]),
                                 UserRole = Convert.ToString(dr["UserRole"]),
                                 ContactNo = Convert.ToString(dr["ContactNo"]),
                                 AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                 AddedIP = Convert.ToString(dr["AddedIP"]),
                                 Password = Convert.ToString(dr["Pwd"]),
                                 UserGuid = Convert.ToString(dr["UserGuid"]),
                                 UserId = Convert.ToString(dr["UserId"]),
                                 PassKey = Convert.ToString(dr["pass_key"]),
                                 ProfileImage = Convert.ToString(dr["ProfileImage"]),
                                 log_time = Convert.ToString(dr["log_time"]) == "" ? (DateTime?)null : Convert.ToDateTime(Convert.ToString(dr["log_time"])),
                                 log_ip = Convert.ToString(dr["log_ip"]),
                                 Status = Convert.ToString(dr["Status"])
                             }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUser", ex.Message);
        }
        return companies;
    }

    public static CreateUser GetUserDetails(SqlConnection conAP, string uGuid)
    {
        CreateUser ud = new CreateUser();
        try
        {
            string query = "Select ud.*, ur.RoleName from CreateUser as ud inner join UserRoles as ur on ud.UserRole =ur.Id where ud.Status !='Deleted' and ud.UserGuid=@UserGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ud.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    ud.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                    ud.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                    ud.AddedBy = Convert.ToString(dt.Rows[0]["AddedBy"]);
                    ud.UserRole = Convert.ToString(dt.Rows[0]["RoleName"]);
                    ud.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"]);
                    ud.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                    ud.PassKey = Convert.ToString(dt.Rows[0]["pass_key"]);
                    ud.ProfileImage = Convert.ToString(dt.Rows[0]["ProfileImage"]);
                    ud.log_time = Convert.ToString(dt.Rows[0]["log_time"]) == "" ? (DateTime?)null : Convert.ToDateTime(Convert.ToString(dt.Rows[0]["log_time"]));
                    ud.log_ip = Convert.ToString(dt.Rows[0]["log_ip"]);
                    ud.Status = Convert.ToString(dt.Rows[0]["Status"]);
                }
                else
                {
                    ud = null;
                } 
            }
        }
        catch (Exception ex)
        {
            ud = null;
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUser", ex.Message);
        }
        return ud;
    }

    public static int InsertUser(SqlConnection conAP, CreateUser comp)
    {
        int result = 0;

        try
        {
            string query = "Insert Into CreateUser (AddedBy,AddedIP,AddedOn,ContactNo,EmailId,Pwd,Status,UserGuid,UserId,UserName,UserRole, ProfileImage) values(@AddedBy,@AddedIP,@AddedOn,@ContactNo,@EmailId,@Pwd,@Status,@UserGuid,@UserId, @UserName,@UserRole,@ProfileImage)";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = comp.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = comp.AddedIP;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = comp.AddedOn;
                cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = comp.ContactNo;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = comp.EmailId;
                cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = comp.Password;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = comp.Status;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = comp.UserGuid;
                cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = comp.UserId;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = comp.UserName;
                cmd.Parameters.AddWithValue("@UserRole", SqlDbType.NVarChar).Value = comp.UserRole;
                cmd.Parameters.AddWithValue("@ProfileImage", SqlDbType.NVarChar).Value = comp.ProfileImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertUser", ex.Message);
        }
        return result;
    }

    public static int UpdateUser(SqlConnection conAP, CreateUser comp)
    {
        int result = 0;
        try
        {
            string query = "Update CreateUser Set ProfileImage=@ProfileImage,AddedBy=@AddedBy, AddedIP=@AddedIP,AddedOn=@AddedOn,ContactNo=@ContactNo,EmailId=@EmailId,Pwd=@Pwd,Status=@Status,UserId=@UserId, UserName=@UserName,UserRole=@UserRole Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = comp.Id;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = comp.AddedIP;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = comp.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = comp.AddedOn;
                cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = comp.ContactNo;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = comp.EmailId;
                cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = comp.Password;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = comp.Status;
                cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = comp.UserId;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = comp.UserName;
                cmd.Parameters.AddWithValue("@UserRole", SqlDbType.NVarChar).Value = comp.UserRole;
                cmd.Parameters.AddWithValue("@ProfileImage", SqlDbType.NVarChar).Value = comp.ProfileImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUser", ex.Message);
        }
        return result;
    }

    public static int UpdateUserByUser(SqlConnection conAP, CreateUser comp)
    {
        int result = 0;
        try
        {
            string query = "Update CreateUser Set ProfileImage=@ProfileImage,AddedBy=@AddedBy, AddedIP=@AddedIP,AddedOn=@AddedOn,ContactNo=@ContactNo,EmailId=@EmailId,Status=@Status, UserName=@UserName Where UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = comp.UserGuid;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = comp.AddedIP;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = comp.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = comp.AddedOn;
                cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = comp.ContactNo;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = comp.EmailId;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = comp.Status;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = comp.UserName;
                cmd.Parameters.AddWithValue("@ProfileImage", SqlDbType.NVarChar).Value = comp.ProfileImage;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUserByUser", ex.Message);
        }
        return result;
    }


    public static int UnBlockUser(SqlConnection conAP, string id, string addedBy)
    {
        int r = 0;
        try
        {
            string query = "Update CreateUser Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp  where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = addedBy;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress(); ;
                conAP.Open();
                r = cmd.ExecuteNonQuery();
                conAP.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SetRestId", ex.Message);
        }
        return r;
    }

    public static int BlockUser(SqlConnection conAP, string id,string addedBy)
    {
        int r = 0;
        try
        {
            string query = "Update CreateUser Set Status=@Status,AddedOn=@AddedOn,AddedIp=@AddedIp,pass_key=@pass_key,pass_key_on=@pass_key_on   where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Blocked";
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = addedBy;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@pass_key", SqlDbType.NVarChar).Value = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@pass_key_on", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                conAP.Open();
                r = cmd.ExecuteNonQuery();
                conAP.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SetRestId", ex.Message);
        }
        return r;
    }

    public static int DeleteUser(SqlConnection conAP, CreateUser comp)
    {
        int result = 0;

        try
        {
            string query = "Update CreateUser Set Status=@Status,AddedBy=@AddedBy, AddedOn=@AddedOn, AddedIP=@AddedIP Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = comp.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = comp.Status;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = comp.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = comp.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = comp.AddedIP;
                conAP.Open();
                result = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteUser", ex.Message);
        }
        return result;
    }
    #endregion

    public static CreateUser Login(SqlConnection conAP, CreateUser loginInputs)
    {
        CreateUser login = new CreateUser();
        try
        {
            string query = "Select C.*, R.RoleName  from CreateUser as C inner join UserRoles as R on R.Id = C.UserRole where C.UserId=@UserId and C.Pwd=@Pwd";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = loginInputs.UserId;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = loginInputs.Password;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                login.PassKey = Convert.ToString(dt.Rows[0]["pass_key"]);
                login.UserRole = Convert.ToString(dt.Rows[0]["RoleName"]);
                login.Status = Convert.ToString(dt.Rows[0]["Status"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Login", ex.Message);
        }
        return login;
    }

    public static CreateUser Login2(SqlConnection conAP, CreateUser loginInputs)
    {
        CreateUser login = new CreateUser();
        try
        {
            string query = "Select * from CreateUser where UserGuid=@UserId and Pwd=@Pwd";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = loginInputs.UserId;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = loginInputs.Password;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                login.PassKey = Convert.ToString(dt.Rows[0]["pass_key"]);
                login.Status = Convert.ToString(dt.Rows[0]["Status"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Login2", ex.Message);
        }
        return login;
    }

    public static string ResetPassword(SqlConnection conAP, string email)
    {
        string login = "";
        try
        {
            string query = "Select * from CreateUser where EmailId=@EmailId and Status='Active' ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = email;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login = Convert.ToString(dt.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ResetPassword", ex.Message);
        }
        return login;
    }

    public static string CheckPassKey(SqlConnection conAP, string uid)
    {
        string login = "";
        try
        {
            string query = "Select * from CreateUser where UserGuid=@UserGuid ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login = Convert.ToString(dt.Rows[0]["pass_key"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckResetLink", ex.Message);
        }
        return login;
    }

    public static string CheckResetLink(SqlConnection conAP, string uid)
    {
        string login = "";
        try
        {
            string query = "Select * from CreateUser where reset_id=@reset_id ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@reset_id", SqlDbType.NVarChar).Value = uid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login = Convert.ToString(dt.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckResetLink", ex.Message);
        }
        return login;
    }

    public static string GetLoggedUserName(SqlConnection conAP, string uid)
    {
        string login = "";
        try
        {
            string query = "Select * from CreateUser where UserGuid=@UserGuid ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login = Convert.ToString(dt.Rows[0]["UserName"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUserName", ex.Message);
        }
        return login;
    }

    public static void UpdateLastLoginTime(SqlConnection conAP, string uid)
    {
        try
        {
            string query = "Update CreateUser Set log_time=@log_time,log_ip=@log_ip where UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@log_time", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@log_ip", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                conAP.Open();
                cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateLastLoginTime", ex.Message);
        }

    }
    public static string GetLoggedUserId(SqlConnection conAP, string uid)
    {
        string login = "";
        try
        {
            string query = "Select * from CreateUser where UserGuid=@UserGuid ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login = Convert.ToString(dt.Rows[0]["UserId"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUserId", ex.Message);
        }
        return login;
    }

    public static bool CheckIfAdmin(SqlConnection conAP, string uid)
    {
        bool login = false;
        try
        {
            string query = "Select Count(u.Id) as RoleAdmin from CreateUser as u inner join UserRoles as r on  r.Id = u.UserRole Where r.RoleName = 'admin' and u.UserGuid=@UserGuid ";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int x = 0;
                int.TryParse(Convert.ToString(dt.Rows[0]["RoleAdmin"]), out x);
                if (x > 0)
                {
                    login = true;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckIfAdmin", ex.Message);
        }
        return login;
    }

    public static int SetRestId(SqlConnection conAP, string uid, string resetId)
    {
        int r = 0;
        try
        {
            string query = "Update CreateUser Set reset_id=@reset_id,reset_on=@reset_on where UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@reset_on", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@reset_id", SqlDbType.NVarChar).Value = resetId;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                conAP.Open();
                r = cmd.ExecuteNonQuery();
                conAP.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SetRestId", ex.Message);
        }
        return r;
    }

    public static int SetResetTiming(SqlConnection conAP, string resetId, string pwd)
    {
        int x = 0;
        try
        {
            string query = "Update CreateUser Set reset_id=@reset_id,reset_on=@reset_on, pwd=@pwd, pass_key=@pass_key,pass_key_on=@pass_key_on  where reset_id=@reset_id1 ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@reset_id1", SqlDbType.NVarChar).Value = resetId;
                cmd.Parameters.AddWithValue("@reset_on", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@reset_id", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.AddWithValue("@pwd", SqlDbType.NVarChar).Value = pwd;
                cmd.Parameters.AddWithValue("@pass_key", SqlDbType.NVarChar).Value = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@pass_key_on", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SetResetTiming", ex.Message);
        }
        return x;
    }

    public static string ChangePassword(SqlConnection conAP, string aId, string nPwd)
    {
        string change = "";
        try
        {
            string query = "Update CreateUser Set pwd=@pwd, pass_key=@pass_key,pass_key_on=@pass_key_on where UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@pwd", SqlDbType.NVarChar).Value = nPwd;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = aId;
                cmd.Parameters.AddWithValue("@pass_key", SqlDbType.NVarChar).Value = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@pass_key_on", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                conAP.Open();
                int x = cmd.ExecuteNonQuery();
                conAP.Close();
                if (x > 0)
                {
                    change = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ChangePassword", ex.Message);
        }
        return change;
    }
    #endregion

    #region user role management
    public static List<UserAccess> GetAllUserAccess(SqlConnection conAP, string uid)
    {
        List<UserAccess> categories = new List<UserAccess>();
        try
        {
            string query = "Select u.UserGuid, u.UserName,u.UserRole,p.ShowInMenu, r.RoleName, ua.AddAccess, ua.EditAccess,ua.DeleteAccess, ua.ViewAccess, p.Id as PageId, p.PageName, p.PageLink, p.PageGroup, pg.GroupName,pg.Icon as GroupIcon, pg.GroupOrder from CreateUser as u inner join UserRoles as r on r.Id = u.UserRole inner join UserRolesAccess as ua on ua.RoleId = r.Id Inner join PageMaster as p on p.Id  = ua.PageId left join PageGroup as pg on pg.Id = p.PageGroup where u.UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new UserAccess()
                              {
                                  AddAccess = Convert.ToString(dr["AddAccess"]),
                                  DeleteAccess = Convert.ToString(dr["DeleteAccess"]),
                                  ViewAccess = Convert.ToString(dr["ViewAccess"]),
                                  EditAccess = Convert.ToString(dr["EditAccess"]),
                                  PageGroupId = Convert.ToString(dr["PageGroup"]),
                                  GroupName = Convert.ToString(dr["GroupName"]),
                                  PageId = Convert.ToString(dr["PageId"]),
                                  PageLink = Convert.ToString(dr["PageLink"]),
                                  PageName = Convert.ToString(dr["PageName"]),
                                  UserGuid = Convert.ToString(dr["UserGuid"]),
                                  RoleName = Convert.ToString(dr["RoleName"]),
                                  UserName = Convert.ToString(dr["UserName"]),
                                  UserRole = Convert.ToString(dr["UserRole"]),
                                  ShowInMenu = Convert.ToString(dr["ShowInMenu"]),
                                  GroupIcon = Convert.ToString(dr["GroupIcon"]),
                                  GroupOrder = Convert.ToString(dr["GroupOrder"]) == "" ? 0 : Convert.ToInt32(Convert.ToString(dr["GroupOrder"])),
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserAccess", ex.Message);
        }
        return categories;
    }

    public static List<UserAccess> LinkIfAdmin(SqlConnection conAP)
    {
        List<UserAccess> categories = new List<UserAccess>();
        try
        {
            string query = " Select p.Id as PageId, p.PageName, p.PageLink,p.ShowInMenu, p.PageGroup, pg.GroupName,pg.Icon as GroupIcon, pg.GroupOrder from  PageMaster as p left join PageGroup as pg on pg.Id = p.PageGroup where p.Status != 'Deleted' and pg.Status != 'Deleted' ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new UserAccess()
                              {
                                  PageGroupId = Convert.ToString(dr["PageGroup"]),
                                  GroupName = Convert.ToString(dr["GroupName"]),
                                  ShowInMenu = Convert.ToString(dr["ShowInMenu"]),
                                  PageId = Convert.ToString(dr["PageId"]),
                                  PageLink = Convert.ToString(dr["PageLink"]),
                                  PageName = Convert.ToString(dr["PageName"]),
                                  GroupIcon = Convert.ToString(dr["GroupIcon"]),
                                  GroupOrder = Convert.ToString(dr["GroupOrder"]) == "" ? 0 : Convert.ToInt32(Convert.ToString(dr["GroupOrder"])),
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserAccess", ex.Message);
        }
        return categories;
    }

    public static bool CheckAccess(SqlConnection conAP, string pageL, string accessType, string uid)
    {
        bool x = false;
        if (CheckIfAdmin(conAP, uid))
        {
            x = true;
        }
        else
        {
            string pageName = pageL;
            switch (accessType.ToLower())
            {
                case "add":
                    List<UserAccess> ua = GetAllUserAccess(conAP, uid).Where(s => s.PageLink == pageName && s.AddAccess == "1").ToList();
                    if (ua.Count > 0)
                        x = true;
                    break;
                case "edit":
                    List<UserAccess> ue = GetAllUserAccess(conAP, uid).Where(s => s.PageLink == pageName && s.EditAccess == "1").ToList();
                    if (ue.Count > 0)
                        x = true;
                    break;
                case "delete":
                    List<UserAccess> ud = GetAllUserAccess(conAP, uid).Where(s => s.PageLink == pageName && s.DeleteAccess == "1").ToList();
                    if (ud.Count > 0)
                        x = true;
                    break;
                case "view":
                    List<UserAccess> uv = GetAllUserAccess(conAP, uid).Where(s => s.PageLink == pageName && s.ViewAccess == "1").ToList();
                    if (uv.Count > 0)
                        x = true;
                    break;
            }
        }
        return x;
    }

    #endregion


}

public class UserAccess
{
    public string UserGuid { get; set; }
    public string PageId { get; set; }
    public string PageGroupId { get; set; }
    public string PageName { get; set; }
    public string PageDesc { get; set; }
    public string PageLink { get; set; }
    public string GroupName { get; set; }
    public string ViewAccess { get; set; }
    public string AddAccess { get; set; }
    public string EditAccess { get; set; }
    public string DeleteAccess { get; set; }
    public string UserRole { get; set; }
    public string RoleName { get; set; }
    public string UserName { get; set; }
    public int GroupOrder { get; set; }
    public string GroupIcon { get; set; }
    public string ShowInMenu { get; set; }
}
