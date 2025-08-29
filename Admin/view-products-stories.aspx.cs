using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_products_stories : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    public string strstoriess, strImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BindProductStories();
    }
    private void BindProductStories()
    {
        try
        {
            var stories_lst = ProductStories.GetAllProductStoriess(conAP).ToList();
            if (stories_lst.Count > 0)
            {
                strstoriess = "";
                int i = 0;
                foreach (var v in stories_lst)
                {
                    strstoriess += @"<tr>
                                        <td>" + (i + 1) + @"</td>
                                       <td><a href='/" + v.Image + @"'/><img src='/" + v.Image + @"' style='height:60px;' /></td>
                                        <td>" + v.Title + @"</td>
                                        <td>" + v.Link + @"</td>
                                        <td>" + v.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                                        <td>" + v.Featured + @"</td>
                                         <td class='gap-3'> 
                                          <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-success AddGalleryBtn' data-id='" + v.Id + @"'  data-name='Add Gallery' data-bs-toggle='modal' data-bs-target='#GalleryModal' data-placement='top' aria-label='Add ProductStories Photos'>
                                              <i class='mdi mdi-image-plus'></i></a>
                                          <a href='add-products-stories.aspx?id=" + v.Id + @"' class='bs-tooltip  fs-18' data-bs-toggle='tooltip' data-placement='top' aria-label='Edit ProductStories Details'>
                                              <i class='mdi mdi-pencil'></i></a>
                                          <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + v.Id + @"' data-bs-toggle='tooltip' data-placement='top' aria-label='Delete'>
                                              <i class='mdi mdi-trash-can-outline'></i></a> 
                                    </td>";
                    i++;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindProductStories", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-products-stories.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                ProductStories delete_ProductStories = new ProductStories()
                {
                    Id = Convert.ToInt32(id),
                    Status = "Deleted",
                    UpdatedOn = TimeStamps.UTCTime(),
                    UpdatedIP = CommonModel.IPAddress(),
                    UpdatedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value,
                };
                int exec = ProductStories.DeleteProductStoriesInfo(conAP, delete_ProductStories);
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }

    [WebMethod]
    public static List<StoriesGallery> GetGalleryImage(string id)
    {
        List<StoriesGallery> tr = new List<StoriesGallery>();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            tr = StoriesGallery.GetGallery(conAP, id).OrderBy(x => Convert.ToInt32(x.GalleryOrder)).ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGalleryImage", ex.Message);
        }
        return tr;
    }

    [WebMethod(EnableSession = true)]
    public static string DeletePackageGallery(string id)
    {
        string x = "";
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-products-stories.aspx", "Delete", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                StoriesGallery productGallery = new StoriesGallery();
                productGallery.Id = Convert.ToInt32(id);
                productGallery.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                int exec = StoriesGallery.DeleteGallery(conAP, productGallery);
                if (exec > 0)
                {
                    x = "Success";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string ImageOrderUpdate(string id)
    {
        string x = "";
        try
        {
            string[] str = id.Split('|');
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            if (CreateUser.CheckAccess(conAP, "view-products-stories.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    StoriesGallery catG = new StoriesGallery();
                    catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                    catG.GalleryOrder = Convert.ToString(i);
                    catG.AddedBy = HttpContext.Current.Request.Cookies["ap_aid"].Value;
                    int res = StoriesGallery.UpdateGalleryOrder(conAP, catG);
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
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ImageOrderUpdate", ex.Message);
            x = "W";
        }

        return x;
    }
}