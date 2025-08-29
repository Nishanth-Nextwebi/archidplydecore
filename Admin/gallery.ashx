<%@ WebHandler Language="C#" Class="gallery" %>
using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public class gallery : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            string strPid = context.Request["pid"];
            string strType = context.Request["tp"];
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "add-gallery.aspx", "Add", aid))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Permission|");
                return;
            }

            HttpFileCollection files = context.Request.Files;
            int x = 0;

            var added_on = TimeStamps.UTCTime();
            var added_ip = CommonModel.IPAddress();

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string fileExtension = Path.GetExtension(file.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString();
                string iconPath = context.Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "_gallery" + fileExtension;
                if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".webp" || fileExtension == ".gif"))
                {
                    if (fileExtension == ".webp")
                    {
                        file.SaveAs(iconPath);
                    }
                    else
                    {
                        System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                        System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);

                        if (fileExtension == ".png")
                        {
                            CommonModel.SavePNG(iconPath, objImagesmallBig, 80);
                        }
                        else if (fileExtension == ".gif")
                        {
                            file.SaveAs(iconPath);
                        }
                        else
                        {
                            CommonModel.SaveJpeg(iconPath, objImagesmallBig, 80);
                        }
                    }

                    string bImage = "UploadImages/" + ImageGuid1 + "_gallery" + fileExtension;

                    Gallery pi = new Gallery();
                    pi.GalleryOrder = "1000";
                    pi.Images = bImage;
                    pi.AddedOn = added_on;
                    pi.AddedIp = added_ip;
                    pi.AddedBy = aid;
                    pi.Status = "Active";

                    x += Gallery.InsertGallery(conAP, pi);
                }
            }
            if (x > 0)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Success|" + x);
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error|");
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
