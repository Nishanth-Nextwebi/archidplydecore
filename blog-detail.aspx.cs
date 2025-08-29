using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blog_detail : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string StrImgUrl = "", strBlogUrl = "", StrBlogTitle = "", StrPostedBy = "", StrPostedOn = "", StrDesc, strPstedBy, strShortDesc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strBlogUrl = Convert.ToString(RouteData.Values["BlogUrl"]);
        if (strBlogUrl != "")
        {
            BindBlogs();

        }
    }
    public void BindBlogs()
    {
        try
        {
            Blogs lst = Blogs.GetBlogByName(conAP, strBlogUrl).FirstOrDefault();
            if (lst != null)
            {
                DateTime postedOn = Convert.ToDateTime(lst.PostedOn);
                StrDesc = lst.FullDesc;
                StrBlogTitle = lst.Title;
                StrPostedOn = postedOn.ToString("dd MMMM, yyyy");
                StrPostedBy = lst.PostedBy;
                StrImgUrl = lst.DetailImage;
                strShortDesc = lst.ShortDesc;
                StrImgUrl = lst.DetailImage;
                Page.Title = lst.PageTitle;
                Page.MetaDescription = lst.MetaDesc;
                Page.MetaKeywords = lst.MetaKey;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindBlogs", ex.Message);
        }
    }

}