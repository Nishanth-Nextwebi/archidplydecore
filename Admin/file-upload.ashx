<%@ WebHandler Language="C#" Class="file_upload" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;

public class file_upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                HttpPostedFile file = files[0];
                string fileExtension = Path.GetExtension(file.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString();
                string iconPath = context.Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "_resource" + fileExtension;
                file.SaveAs(iconPath);
                string bImage = "/UploadImages/" + ImageGuid1 + "_resource" + fileExtension; 
                context.Response.ContentType = "text/json";
                TinyMCEImage img = new TinyMCEImage();
                img.location = bImage;
                context.Response.Write(new JavaScriptSerializer().Serialize(img));
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProcessRequest", ex.Message);

        }
    }

    public class TinyMCEImage
    {
        public string location { get; set; }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}