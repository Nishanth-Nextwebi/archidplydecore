<%@ WebHandler Language="C#" Class="enquiry_product_gallery" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public class enquiry_product_gallery : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            string guid = context.Request["guid"];
            string aid = HttpContext.Current.Request.Cookies["ap_aid"].Value;

            if (!CreateUser.CheckAccess(conAP, "enquiry-product-gallery.aspx", "Add", aid))
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
                    else if (fileExtension == ".png")
                    {
                        System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                        if ((bmpPostedImageBig.PhysicalDimension.Height != 1000) && (bmpPostedImageBig.PhysicalDimension.Width != 1000))
                        {
                            //context.Response.ContentType = "text/plain";
                            //context.Response.Write("Size|");
                            context.Response.Write("Image size should be " + 1000 + "*" + 1000 + " Px ");
                            return;
                        }
                        else
                        {
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                            CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                        }

                    }
                    else
                    {
                        System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                        if ((bmpPostedImageBig.PhysicalDimension.Height != 1000) && (bmpPostedImageBig.PhysicalDimension.Width != 1000))
                        {
                            //context.Response.ContentType = "text/plain";
                            //context.Response.Write("Size|");
                            context.Response.Write("Image size should be " + 1000 + "*" + 1000 + " Px ");
                            return;
                        }
                        else
                        {
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                            CommonModel.SaveJpeg(iconPath, objImagesmallBig, 80);
                        }
                    }
                    string bImage = "UploadImages/" + ImageGuid1 + "_gallery" + fileExtension;

                    EnquiryProductGallery pi = new EnquiryProductGallery();
                    pi.GalleryOrder = "1000";
                    pi.Images = bImage;
                    pi.AddedOn = added_on;
                    pi.AddedIp = added_ip;
                    pi.AddedBy = aid;
                    pi.UpdatedOn = TimeStamps.UTCTime();
                    pi.UpdatedIp = CommonModel.IPAddress();
                    pi.UpdatedBy = aid;
                    pi.Status = "Active";
                    pi.ProductGuid = guid;

                    x += EnquiryProductGallery.InsertEnquiryProductGallery(conAP, pi);
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