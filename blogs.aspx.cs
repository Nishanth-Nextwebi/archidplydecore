using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static List<Blogs> allBlogs(string pno)
    {
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        List<Blogs> blogs = null;
        try
        {
            blogs = Blogs.GetAllListBlogs(conAP, Convert.ToInt32(pno));
        }
        catch (Exception ex)
        {

            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "allBlogs", ex.Message);

        }
        return blogs;
    }

}