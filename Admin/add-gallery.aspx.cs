using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Admin_add_gallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static List<Gallery> GetGalleryImage(string id)
    {
        List<Gallery> tr = new List<Gallery>();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            tr = Gallery.GetGallery(conAP, id).OrderBy(x => Convert.ToInt32(x.GalleryOrder)).ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGalleryImage", ex.Message);
        }
        return tr;
    }

    [WebMethod(EnableSession = true)]
    public static string ImageOrderUpdate(string id)
    {
        string x = "";
        try
        {
            string[] str = id.Split('|');
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "add-gallery.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var added_on = TimeStamps.UTCTime();
            var added_ip = CommonModel.IPAddress();

            for (int i = 0; i < str.Length; i++)
            {
                Gallery catG = new Gallery();
                catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                catG.GalleryOrder = Convert.ToString(i);
                catG.AddedOn = added_on;
                catG.AddedIp = added_ip;
                catG.AddedBy = aid;

                int res = Gallery.UpdateGalleryOrder(conAP, catG);
                if (res > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ImageOrderUpdate", ex.Message);
            x = "W";
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string InsertGallery(string pid, string lnk, string tp)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;
            if (!CreateUser.CheckAccess(conAP, "add-gallery.aspx", "Add", aid))
            {
                x = "permission";
                return x;
            }

            Gallery pi = new Gallery();
            pi.GalleryOrder = "1000";
            pi.Images = lnk;
            pi.AddedOn = TimeStamps.UTCTime();
            pi.AddedIp = CommonModel.IPAddress();
            pi.AddedBy = aid;
            pi.Status = "Active";
            int res = Gallery.InsertGallery(conAP, pi);
            if (res > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            x = "";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertGallery", ex.Message);
        }

        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteGallery(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "add-gallery.aspx", "Delete", aid))
            {
                x = "Permission";
                return x;
            }

            Gallery gallery = new Gallery();
            gallery.Id = Convert.ToInt32(id);
            gallery.AddedOn = TimeStamps.UTCTime();
            gallery.AddedIp = CommonModel.IPAddress();
            gallery.AddedBy = aid;
            gallery.Status = "Deleted";
            int exec = Gallery.DeleteGallery(conAP, gallery);
            if (exec > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteGallery", ex.Message);
        }
        return x;
    }
}