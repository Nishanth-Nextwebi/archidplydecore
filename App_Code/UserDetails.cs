using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class UserDetails 
{
    public int Id { set; get; }
    public string UserGuid { get; set; }
    public string UserType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImageUrl { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string ContactNo { get; set; }
    public string Gender { get; set; }
    public string City { get; set; }
    public string DOB { get; set; }
    public string CustomerId { get; set; }
    public string ForgotId { get; set; }
    public DateTime LastLoggedIn { get; set; }
    public string LastLoggedIp { get; set; }
    public List<UserAddress> Address { get; set; }
    public DateTime AddedOn { set; get; }
    public string AddedIp { set; get; }
    public string AddedBy { set; get; }
    public string Status { set; get; }
    public string BirthDate { get; set; }
    public string BirthMonth { get; set; }
    public string AnniversaryDate { get; set; }
    public string PassKey { get; set; }
    public string AnniversaryMonth { get; set; }

    #region UserDetails Methods
    public static UserDetails CheckUserByEmail(SqlConnection conAP, string eMail)
    {
        UserDetails details = new UserDetails();
        try
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("select FirstName,UserGuid,Status,EmailId from Customers where EmailId=@EmailId and Status != 'Deleted'", conAP))
            {
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = eMail;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    details.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    details.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                    details.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                    details.Status = Convert.ToString(dt.Rows[0]["Status"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckUserByEmail", ex.Message);
        }
        return details;
    }

    public static string GetMaxId(SqlConnection conAP)
    {
        string x = "";
        try
        {
            SqlCommand cmd3 = new SqlCommand("Select Max(try_convert(decimal, Id)) as mid from Customers", conAP);
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                string cc = Convert.ToString(dt3.Rows[0]["mid"]);
                if (cc == "")
                {
                    cc = "0000";
                }
                x = (Convert.ToInt32(cc) + 1).ToString();
                if (x.Length <= 4)
                {
                    x = Convert.ToInt32(x).ToString("0000");
                }
            }
        }
        catch (Exception ex)
        {
            x = "0001";
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOMax", ex.Message);
        }
        return x;
    }

    public static UserDetails CheckUserByMobile(SqlConnection conAP, string conTact)
    {
        UserDetails details = new UserDetails();
        try
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("select FirstName,UserGuid,Status,EmailId,ContactNo from Customers where ContactNo=@ContactNo and Status != 'Deleted'", conAP))
            {
                cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = conTact;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    details.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    details.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                    details.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"]);
                    details.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                    details.Status = Convert.ToString(dt.Rows[0]["Status"]);
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckUserByEmail", ex.Message);
        }
        return details;
    }
    public static UserDetails ChangeLogin(SqlConnection conAP, UserDetails loginInputs)
    {
        UserDetails login = new UserDetails();
        try
        {
            string query = "Select * from Customers where UserGuid=@UserGuid and Pwd=@Pwd and Status!='Deleted'";
            SqlCommand cmd = new SqlCommand(query, conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = loginInputs.UserGuid;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = loginInputs.Password;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                login.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ChangeLogin", ex.Message);
        }
        return login;
    }
    public static int CreateUser(SqlConnection conAP, UserDetails userDetails)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("insert into Customers (UserGuid,Gender,FirstName,LastName,EmailId,ContactNo,Pwd,Status,CustomerId,ForgotId,AddedOn,AddedIp,PassKey) values(@UserGuid,@Gender,@FirstName,@LastName,@EmailId,@ContactNo,@Pwd,@Status,@CustomerId,@ForgotId,@AddedOn,@AddedIp,@PassKey)", conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = userDetails.UserGuid;
            cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = userDetails.FirstName;
            cmd.Parameters.AddWithValue("@Gender", SqlDbType.NVarChar).Value = userDetails.Gender;
            cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = userDetails.LastName;
            cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = userDetails.EmailId;
            cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = userDetails.ContactNo;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = userDetails.Password;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = userDetails.Status;
            cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = userDetails.CustomerId;
            cmd.Parameters.AddWithValue("@ForgotId", SqlDbType.NVarChar).Value = userDetails.ForgotId;
            cmd.Parameters.AddWithValue("@PassKey", SqlDbType.NVarChar).Value = userDetails.PassKey;
            cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
            cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "RegisterUser", ex.Message);
        }
        return x;
    }
    //public static UserDetails UserLoginWithMobile(SqlConnection conAP, string uid, string pwd)
    //{
    //    UserDetails result = new UserDetails();
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        SqlCommand cmd = new SqlCommand("select Pwd,UserGuid,Status,FirstName from Customers where ContactNo=@ContactNo and Pwd=@Pwd and Status != 'Deleted'", conAP);
    //        cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = uid;
    //        cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = pwd;
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            result.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
    //            result.Status = Convert.ToString(dt.Rows[0]["Status"]);
    //            result.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UserLogin", ex.Message);
    //    }
    //    return result;
    //}
    public static UserDetails UserLoginWithEmail(SqlConnection conAP, string EmailId, string pwd)
    {
        UserDetails result = new UserDetails();
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select Pwd,UserGuid,Status,FirstName,PassKey from Customers where EmailId=@EmailId and Pwd=@Pwd and Status != 'Deleted'", conAP);
            cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = EmailId;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = pwd;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                result.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                result.Status = Convert.ToString(dt.Rows[0]["Status"]);
                result.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                result.PassKey = Convert.ToString(dt.Rows[0]["PassKey"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UserLogin", ex.Message);
        }
        return result;
    }
    public static UserDetails UserLogin(SqlConnection conAP, string uid, string pwd)
    {
        UserDetails result = new UserDetails();
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select Pwd,UserGuid,Status,FirstName from Customers where EmailId=@EmailId and Pwd=@Pwd and Status != 'Deleted'", conAP);
            cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = uid;
            cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = pwd;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                result.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                result.Status = Convert.ToString(dt.Rows[0]["Status"]);
                result.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UserLogin", ex.Message);
        }
        return result;
    }
    public static int UpdateLastLogDetails(SqlConnection conAP, string uid)
    {
        int x = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand("Update Customers Set LastLoggedIn=@LastLoggedIn,LastLoggedIp=@LastLoggedIp Where UserGuid=@UserGuid ", conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                cmd.Parameters.AddWithValue("@LastLoggedIn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@LastLoggedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateLastLogDetails", ex.Message);
        }
        return x;
    }
    public static int SetForgotId(SqlConnection conAP, string uid, string fId)
    {
        int x = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand("Update Customers Set ForgotId=@ForgotId where UserGuid=@UserGuid ", conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                cmd.Parameters.AddWithValue("@ForgotId", SqlDbType.NVarChar).Value = fId;
                cmd.Parameters.AddWithValue("@AddedDateTime", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateLastLogDetails", ex.Message);
        }
        return x;
    }
    public static List<UserDetails> GetAllCustomers(SqlConnection conAP, string uid)
    {
        List<UserDetails> details = new List<UserDetails>();
        try
        {
            string query = "select * from Customers where (UserGuid=@uid or @uid='') Order by Id Desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = uid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                details = (from DataRow dr in dt.Rows
                           select new UserDetails()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               Address = GetLoggedUserAddress(conAP, Convert.ToString(dr["UserGuid"])),
                               ContactNo = Convert.ToString(dr["ContactNo"]),
                               Gender = Convert.ToString(dr["Gender"]),
                               AddedIp = Convert.ToString(dr["AddedIP"]),
                               DOB = Convert.ToString(dr["DOB"]),
                               EmailId = Convert.ToString(dr["EmailId"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               ForgotId = Convert.ToString(dr["ForgotId"]),
                               LastLoggedIn = Convert.ToString(dr["LastLoggedIn"]) != "" ? Convert.ToDateTime(Convert.ToString(dr["LastLoggedIn"])) : Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               LastLoggedIp = Convert.ToString(dr["LastLoggedIp"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUser", ex.Message);
        }
        return details;
    }
    public static UserDetails GetUserDetailsByID(SqlConnection conAP, string uGuid)
    {
        UserDetails ud = new UserDetails();
        try
        {
            string query = "select Top 1 * from Customers where UserGuid=@UserGuid and Status!='Deleted'";
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
                    ud.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"]);
                    ud.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                    ud.PassKey = Convert.ToString(dt.Rows[0]["PassKey"]);
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

    public static UserDetails GetAllCustomerIds(SqlConnection conAP, string custid)
    {
        UserDetails ud = new UserDetails();
        try
        {
            string query = "select Top 1 * from Customers where CustomerId=@CustomerId or @CustomerId='' and Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.NVarChar).Value = custid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ud.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                    ud.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                    ud.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"]);
                    ud.UserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
                    ud.PassKey = Convert.ToString(dt.Rows[0]["PassKey"]);
                    ud.CustomerId = Convert.ToString(dt.Rows[0]["CustomerId"]);
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


    public static List<UserAddress> GetLoggedUserAddress(SqlConnection conAP, string uGuid)
    {
        List<UserAddress> details = new List<UserAddress>();
        try
        {
            string query = "Select cadd.*, c.ContactNo,c.CustomerId, c.EmailId,c.Pwd,c.Gender,c.ImageUrl from CustomerAddress as cadd inner join Customers as c on c.UserGuid = cadd.UserGuid where c.UserGuid=@UserGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                details = (from DataRow dr in dt.Rows
                           select new UserAddress()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               AddressLine1 = Convert.ToString(dr["Address1"]),
                               AddressLine2 = Convert.ToString(dr["Address2"]),
                               City = Convert.ToString(dr["City"]),
                               Country = Convert.ToString(dr["Country"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               ShortName = Convert.ToString(dr["ShortName"]),
                               Zip = Convert.ToString(dr["Zip"]),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               Status = Convert.ToString(dr["Status"]),
                               State = Convert.ToString(dr["State"]),
                               Email = Convert.ToString(dr["EmailId"]),
                               Phone = Convert.ToString(dr["ContactNo"]),
                               Landmark = Convert.ToString(dr["Landmark"]),
                               Password = Convert.ToString(dr["Pwd"]),
                               Gender = Convert.ToString(dr["Gender"]),
                               ImageUrl = Convert.ToString(dr["ImageUrl"]),
                               CustomerId = Convert.ToString(dr["CustomerId"])
                           }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUser", ex.Message);
        }
        return details;
    }
    public static int UpdateUserDetails(SqlConnection conAP, UserDetails uds)
    {
        int x = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand("Update Customers Set FirstName=@FirstName,LastName=@LastName,ImageUrl=@ImageUrl,EmailId=@EmailId,Gender=@Gender,ContactNo=@ContactNo Where UserGuid=@UserGuid ", conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uds.UserGuid;
                cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = uds.FirstName;
                cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = uds.LastName;
                cmd.Parameters.AddWithValue("@Gender", SqlDbType.NVarChar).Value = uds.Gender;
                cmd.Parameters.AddWithValue("@ContactNo", SqlDbType.NVarChar).Value = uds.ContactNo;
                cmd.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = uds.EmailId;
                cmd.Parameters.AddWithValue("@ImageUrl", SqlDbType.NVarChar).Value = uds.ImageUrl==""?"": uds.ImageUrl;
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateLastLogDetails", ex.Message);
        }
        return x;
    }
    public static int UpdateUserAddress(SqlConnection conAP, UserAddress uds)
    {
        int x = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand("Update CustomerAddress Set FirstName=@FirstName,LastName=@LastName,Address1=@Address1,Address2=@Address2,City=@City,State=@State,Country=@Country,Zip=@Zip,ShortName=@ShortName,Landmark=@Landmark,Phone=@Phone,Email=@Email Where UserGuid=@UserGuid ", conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uds.UserGuid;
                cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = uds.FirstName;
                cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = uds.LastName;
                cmd.Parameters.AddWithValue("@Address1", SqlDbType.NVarChar).Value = uds.AddressLine1;
                cmd.Parameters.AddWithValue("@Address2", SqlDbType.NVarChar).Value = uds.AddressLine2;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = uds.City;
                cmd.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = uds.State;
                cmd.Parameters.AddWithValue("@Country", SqlDbType.NVarChar).Value = uds.Country;
                cmd.Parameters.AddWithValue("@Zip", SqlDbType.NVarChar).Value = uds.Zip;
                cmd.Parameters.AddWithValue("@ShortName", SqlDbType.NVarChar).Value = uds.ShortName;
                cmd.Parameters.AddWithValue("@Landmark", SqlDbType.NVarChar).Value = uds.Landmark;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = uds.Email;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = uds.Phone;
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUserAddress", ex.Message);
        }
        return x;
    }
    public static int UpdateUserProfileAddress(SqlConnection conAP, UserAddress uds)
    {
        int x = 0;
        try
        {
            using (SqlCommand cmd = new SqlCommand("Update CustomerAddress Set FirstName=@FirstName,LastName=@LastName,Address1=@Address1,Address2=@Address2,City=@City,State=@State,Country=@Country,Zip=@Zip,Phone=@Phone,Email=@Email Where UserGuid=@UserGuid ", conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uds.UserGuid;
                cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = uds.FirstName;
                cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = uds.LastName;
                cmd.Parameters.AddWithValue("@Address1", SqlDbType.NVarChar).Value = uds.AddressLine1;
                cmd.Parameters.AddWithValue("@Address2", SqlDbType.NVarChar).Value = uds.AddressLine2;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = uds.City;
                cmd.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = uds.State;
                cmd.Parameters.AddWithValue("@Country", SqlDbType.NVarChar).Value = uds.Country;
                cmd.Parameters.AddWithValue("@Zip", SqlDbType.NVarChar).Value = uds.Zip;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = uds.Email;
                cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = uds.Phone;
                conAP.Open();
                x = cmd.ExecuteNonQuery();
                conAP.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUserAddress", ex.Message);
        }
        return x;
    }
    public static string ChangePassword(SqlConnection conAP, string UserGuid, string Pwd)
    {
        string change = "";
        try
        {
            string query = "Update Customers Set Pwd=@Pwd,PassKey=@PassKey  Where UserGuid=@UserGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = Pwd;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = UserGuid;
                cmd.Parameters.AddWithValue("@PassKey", SqlDbType.NVarChar).Value = Convert.ToString(Guid.NewGuid());
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckResetLink", ex.Message);
        }
        return change;
    }
    public static string ChangePasswordOld(SqlConnection conAP, UserDetails details, string nPassword)
    {
        string x = "";
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select Pwd from Customers where UserGuid=@UserGuid", conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = details.UserGuid;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string pwd = CommonModel.Decrypt(dt.Rows[0]["Pwd"].ToString());
                if (pwd == CommonModel.Encrypt(details.Password))
                {
                    SqlCommand cmdUpdate = new SqlCommand("Update Customers Set Pwd=@Pwd  Where UserGuid=@UserGuid ", conAP);
                    cmdUpdate.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = details.UserGuid;
                    cmdUpdate.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = CommonModel.Encrypt(nPassword);
                    conAP.Open();
                    int exec = cmdUpdate.ExecuteNonQuery();
                    conAP.Close();
                    x = exec > 0 ? "Success" : "Error";
                }
                else
                {
                    x = "MisMatch";
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ChangePassword", ex.Message);
            x = "Error";
        }
        return x;
    }
    public static int AddUserAddress(SqlConnection conAP, UserAddress address)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("insert into CustomerAddress (UserGuid,FirstName,LastName,Address1,Address2,City,Country,Zip,ShortName,Status,AddedOn,AddedIp,State,Email,Phone) values (@UserGuid,@FirstName,@LastName,@Address1,@Address2,@City,@Country,@Zip,@ShortName,@Status,@AddedOn,@AddedIp,@State,@Email,@Phone)", conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = address.UserGuid;
            cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = address.FirstName;
            cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = address.LastName;
            cmd.Parameters.AddWithValue("@Address1", SqlDbType.NVarChar).Value = address.AddressLine1;
            cmd.Parameters.AddWithValue("@Address2", SqlDbType.NVarChar).Value = address.AddressLine2;
            cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = address.Email;
            cmd.Parameters.AddWithValue("@Phone", SqlDbType.NVarChar).Value = address.Phone;
            cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = address.City;
            cmd.Parameters.AddWithValue("@State", SqlDbType.NVarChar).Value = address.State;
            cmd.Parameters.AddWithValue("@Country", SqlDbType.NVarChar).Value = address.Country;
            cmd.Parameters.AddWithValue("@Zip", SqlDbType.NVarChar).Value = address.Zip;
            cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
            cmd.Parameters.AddWithValue("@ShortName", SqlDbType.NVarChar).Value = address.ShortName;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = address.Status;
            conAP.Open();
            x = cmd.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddUserAddress", ex.Message);
        }
        return x;
    }
    public static List<UserDetails> GetLoggedUser(SqlConnection conAP, string uGuid)
    {
        List<UserDetails> details = new List<UserDetails>();
        try
        {
            string query = "select * from Customers where UserGuid=@UserGuid";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                details = (from DataRow dr in dt.Rows
                           select new UserDetails()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               Address = GetLoggedUserAddress(conAP, uGuid),
                               ContactNo = Convert.ToString(dr["ContactNo"]),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               EmailId = Convert.ToString(dr["EmailId"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               ForgotId = Convert.ToString(dr["ForgotId"]),
                               LastLoggedIn = Convert.ToString(dr["LastLoggedIn"]) != "" ? Convert.ToDateTime(Convert.ToString(dr["LastLoggedIn"])) : CommonModel.UTCTime(),
                               LastLoggedIp = Convert.ToString(dr["LastLoggedIp"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUser", ex.Message);
        }
        return details;
    }
    public static int UpdateAsVerifed(SqlConnection conAP, string uGuid)
    {
        int x = 0;
        try
        {
            SqlCommand cmd1 = new SqlCommand("Update Customers Set Status='Verified' Where UserGuid=@UserGuid", conAP);
            cmd1.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            conAP.Open();
            x = cmd1.ExecuteNonQuery();
            conAP.Close();
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "RegisterUser", ex.Message);
        }
        return x;
    }
    public static string CheckPasswordResetId(SqlConnection conAP, string ForgotId)
    {
        string res = "";
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select UserGuid from Customers where ForgotId=@ForgotId", conAP);
            cmd.Parameters.AddWithValue("@ForgotId", SqlDbType.NVarChar).Value = ForgotId;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                res = Convert.ToString(dt.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckPasswordResetId", ex.Message);
        }
        return res;
    }  
    public static string GetPasswordWithUserGuid(SqlConnection conAP, string UserGuid)
    {
        string res = "";
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select Pwd from Customers where UserGuid=@UserGuid", conAP);
            cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = UserGuid;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                res = Convert.ToString(dt.Rows[0]["Pwd"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckPasswordResetId", ex.Message);
        }
        return res;
    }
    public static string PasswordReset(SqlConnection conAP, string cPassword, string uGuid)
    {
        string res = "";
        try
        {
            SqlCommand cmdUpdate = new SqlCommand("Update Customers Set Pwd=@Pwd,ForgotId=@ForgotId, Status='Verified' Where UserGuid=@UserGuid ", conAP);
            cmdUpdate.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uGuid;
            cmdUpdate.Parameters.AddWithValue("@ForgotId", SqlDbType.NVarChar).Value = "";
            cmdUpdate.Parameters.AddWithValue("@Pwd", SqlDbType.NVarChar).Value = cPassword;
            conAP.Open();
            int exec = cmdUpdate.ExecuteNonQuery();
            conAP.Close();
            res = exec > 0 ? "Updated" : "Error";
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ChangePassword", ex.Message);
            res = ex.Message;
        }
        return res;
    }
    public static List<UserDetails> GetAllCustomers(SqlConnection conAP)
    {
        List<UserDetails> details = new List<UserDetails>();
        try
        {
            string query = "select * from Customers where Status!= 'Deleted' Order by Id Desc";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                details = (from DataRow dr in dt.Rows
                           select new UserDetails()
                           {
                               Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                               AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               ContactNo = Convert.ToString(dr["ContactNo"]),
                               BirthDate = Convert.ToString(dr["BirthDate"]),
                               BirthMonth = Convert.ToString(dr["BirthMonth"]),
                               AnniversaryDate = Convert.ToString(dr["AnniversaryDate"]),
                               AnniversaryMonth = Convert.ToString(dr["AnniversaryMonth"]),
                               AddedIp = Convert.ToString(dr["AddedIp"]),
                               DOB = Convert.ToString(dr["DOB"]),
                               Gender = Convert.ToString(dr["Gender"]),
                               EmailId = Convert.ToString(dr["EmailId"]),
                               FirstName = Convert.ToString(dr["FirstName"]),
                               ForgotId = Convert.ToString(dr["ForgotId"]),
                               LastLoggedIn = Convert.ToString(dr["LastLoggedIn"]) != "" ? Convert.ToDateTime(Convert.ToString(dr["LastLoggedIn"])) : Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                               LastLoggedIp = Convert.ToString(dr["LastLoggedIp"]),
                               LastName = Convert.ToString(dr["LastName"]),
                               CustomerId = Convert.ToString(dr["CustomerId"]),
                               UserGuid = Convert.ToString(dr["UserGuid"]),
                               Status = Convert.ToString(dr["Status"])
                           }).ToList();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLoggedUser", ex.Message);
        }
        return details;
    }
    public static string GetUserGuidWithEmail(SqlConnection conAP, string email)
    {
        string result = "";
        try
        {
            string cmdText = "Select * from Customers where EmailId=@EmailId and Status='Verified' ";
            SqlCommand sqlCommand = new SqlCommand(cmdText, conAP);
            sqlCommand.Parameters.AddWithValue("@EmailId", SqlDbType.NVarChar).Value = email;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                result = Convert.ToString(dataTable.Rows[0]["UserGuid"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ResetPassword", ex.Message);
        }

        return result;
    } 
    public static string GetUserNameWithGuid(SqlConnection conAP, string UserGuid)
    {
        string result = "";
        try
        {
            string cmdText = "Select * from Customers where UserGuid=@UserGuid and Status='Verified' ";
            SqlCommand sqlCommand = new SqlCommand(cmdText, conAP);
            sqlCommand.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = UserGuid;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                result = Convert.ToString(dataTable.Rows[0]["FirstName"])+" "+ Convert.ToString(dataTable.Rows[0]["LastName"]);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ResetPassword", ex.Message);
        }

        return result;
    }
    public static int SetForgotUserId(SqlConnection conAP, string uid, string ForgotId)
    {
        int r = 0;
        try
        {
            string query = "Update Customers Set ForgotId=@ForgotId,resetOn=@resetOn where UserGuid=@UserGuid ";
            using (SqlCommand cmd = new SqlCommand(query, conAP))
            {
                cmd.Parameters.AddWithValue("@resetOn", SqlDbType.NVarChar).Value = CommonModel.UTCTime();
                cmd.Parameters.AddWithValue("@ForgotId", SqlDbType.NVarChar).Value = ForgotId;
                cmd.Parameters.AddWithValue("@UserGuid", SqlDbType.NVarChar).Value = uid;
                conAP.Open();
                r = cmd.ExecuteNonQuery();
                conAP.Close();

            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SetRestId", ex.Message);
        }
        return r;
    }
    #endregion
}

public class UserAddress
{
    public int Id { get; set; }
    public string UserGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Zip { get; set; }
    public string ShortName { get; set; }
    public string Landmark { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public string ImageUrl { get; set; }
    public string CustomerId { get; set; }

}