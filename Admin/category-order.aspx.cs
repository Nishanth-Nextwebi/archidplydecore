using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Admin_category_order : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    GetAllCategory();
        //}
    }

    //public void GetAllCategory()
    //{
    //    try
    //    {
    //        List<Category> cats = Category.GetAllCategory(conAP);
    //        if (cats.Count > 0)
    //        {
    //            ddlCategory.Items.Clear();
    //            ddlCategory.DataSource = cats;
    //            ddlCategory.DataTextField = "Category";
    //            ddlCategory.DataValueField = "Category";
    //            ddlCategory.DataBind();
    //        }
    //        ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
    //    }
    //}


    [WebMethod(EnableSession = true)]
    public static int CategoryOrderUpdate(string category)
    {
        int status = 1;
        string[] str = category.Split(',');
        SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
        try
        {
            if (CreateUser.CheckAccess(conAP, "category-order.aspx", "Edit", HttpContext.Current.Request.Cookies["ap_aid"].Value))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    Category catG = new Category();
                    catG.Id = str[i] == "" ? 0 : Convert.ToInt32(str[i]);
                    catG.DisplayOrder = Convert.ToString(i);
                    Category.UpdateCategoryOrder(conAP, catG);
                }
                status = 0;
            }
            else
            {
                status = 403;
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CategoryOrderUpdate", ex.Message);
            status = 1;
        }
        return status;
    }


    [WebMethod(EnableSession = true)]
    public static List<Category> CategoryOrder()
    {
        List<Category> fpl = new List<Category>();
        try
        {
            SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);
            List<Category> lpds = Category.GetAllCategory(conAP).OrderBy(x => x.DisplayOrder).ToList();
            foreach (var cat in lpds)
            {
                fpl.Add(new Category() { CategoryName = cat.CategoryName, Id = cat.Id, ImageUrl = cat.ImageUrl });
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CategoryOrder", ex.Message);
        }
        return fpl;
    }

    
}