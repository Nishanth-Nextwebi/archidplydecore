using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_enquiry_product_gallery : System.Web.UI.Page
{
    public string strGuid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static List<EnquiryProductGallery> GetGalleryImage(string id)
    {
        List<EnquiryProductGallery> tr = new List<EnquiryProductGallery>();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            tr = EnquiryProductGallery.GetEnquiryProductGallery(conAP, id).ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetEnquiryProductGalleryImage", ex.Message);
        }
        return tr;
    }

    [WebMethod(EnableSession = true)]
    public static string ImageOrderUpdate(string Id)
    {
        string x = "";
        try
        {
            string[] str = Id.Split('|');
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "enquiry-product-gallery.aspx", "Edit", aid))
            {
                x = "Permission";
                return x;
            }

            var added_on = TimeStamps.UTCTime();
            var added_ip = CommonModel.IPAddress();

            for (int i = 0; i < str.Length; i++)
            {
                EnquiryProductGallery catG = new EnquiryProductGallery();
                catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                catG.GalleryOrder = Convert.ToString(i);
                catG.AddedOn = added_on;
                catG.AddedIp = added_ip;
                catG.AddedBy = aid;

                int res = EnquiryProductGallery.UpdateEnquiryProductGalleryOrder(conAP, catG);
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
    public static string InsertGallery(string lnk, string guid)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;
            if (!CreateUser.CheckAccess(conAP, "enquiry-product-gallery.aspx", "Add", aid))
            {
                x = "permission";
                return x;
            }

            EnquiryProductGallery pi = new EnquiryProductGallery();
            pi.ProductGuid = guid;
            pi.Images = lnk;
            pi.AddedOn = TimeStamps.UTCTime();
            pi.AddedIp = CommonModel.IPAddress();
            pi.AddedBy = aid;
            pi.UpdatedOn = TimeStamps.UTCTime();
            pi.UpdatedIp = CommonModel.IPAddress();
            pi.UpdatedBy = aid;
            pi.Status = "Active";
            int res = EnquiryProductGallery.InsertEnquiryProductGallery(conAP, pi);
            if (res > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            x = "";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertEnquiryProductGallery", ex.Message);
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

            if (!CreateUser.CheckAccess(conAP, "enquiry-product-gallery.aspx", "Delete", aid))
            {
                x = "Permission";
                return x;
            }

            EnquiryProductGallery epg = new EnquiryProductGallery();
            epg.Id = Convert.ToInt32(id);
            epg.AddedOn = TimeStamps.UTCTime();
            epg.AddedIp = CommonModel.IPAddress();
            epg.AddedBy = aid;
            epg.Status = "Deleted";
            int exec = EnquiryProductGallery.DeleteEnquiryProductGallery(conAP, epg);
            if (exec > 0)
            {
                x = "Success";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnquiryProductGallery", ex.Message);
        }
        return x;
    }
}