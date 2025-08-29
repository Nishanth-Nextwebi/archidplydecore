<%@ WebHandler Language="C#" Class="product_stories_images" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public class product_stories_images : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

            string strVid = context.Request["Vid"];
            if (CreateUser.CheckAccess(conAP, "view-products-stories.aspx", "Add", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                HttpFileCollection files = context.Request.Files;
                int x = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fileExtension = Path.GetExtension(file.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString();
                    string iconPath = context.Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "_storygallery" + fileExtension;
                    if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".webp" || fileExtension == ".gif"))
                    {
                        if (fileExtension == ".webp")
                        {
                            file.SaveAs(iconPath);
                        }
                        else
                        {
                            if (fileExtension == ".png")
                            {
                                System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                                if ((bmpPostedImageBig.PhysicalDimension.Height != 500) && (bmpPostedImageBig.PhysicalDimension.Width != 800))
                                {
                                    context.Response.Write("Image size should be " + 500 + "*" + 800 + " Px ");
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
                                if ((bmpPostedImageBig.PhysicalDimension.Height != 500) && (bmpPostedImageBig.PhysicalDimension.Width != 800))
                                {
                                    context.Response.Write("Image size should be " + 500 + "*" + 800 + " Px ");
                                    return;
                                }
                                else
                                {
                                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                                    CommonModel.SaveJpeg(iconPath, objImagesmallBig, 80);
                                }

                            }
                        }

                        string bImage = "UploadImages/" + ImageGuid1 + "_storygallery" + fileExtension;
                        StoriesGallery pi = new StoriesGallery();
                        pi.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                        pi.StoryGuid = Convert.ToString(strVid);
                        pi.GalleryOrder = "1000";
                        pi.Images = bImage;
                        pi.Status = "Active";
                        x += StoriesGallery.InsertGallery(conAP, pi);
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
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Permission|");
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