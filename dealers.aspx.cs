using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class dealers : System.Web.UI.Page
{
    SqlConnection conAP = new SqlConnection(ConfigurationManager.ConnectionStrings["conAP"].ConnectionString);

    public string strDealers= "";
    protected void Page_Load(object sender, EventArgs e)
    {
      /*  BindDealerList("");
        if (!IsPostBack)
        {
            BindDealerList("");
            GetAllCity();
        }*/
    }
   /* public void GetAllCity()
    {
        try
        {
            List<City> comps = City.GetAllCity(conAP);
            if (comps.Count > 0)
            {
                ddlCity.Items.Clear();
                ddlCity.DataSource = comps;
                ddlCity.DataValueField = "Id";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataBind();
            }
            ddlCity.Items.Insert(0, new ListItem("Select a city to filter dealers", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "GetAllCity", ex.Message);
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDealerList(ddlCity.SelectedItem.Value);
    }*/
    public void BindDealerList(string city)
    {
        try
        {
            strDealers = "";
            List<DealersList> dealers = DealersList.GetDealerListByCity(conAP, city);
            if (dealers.Count > 0)
            {
                foreach (DealersList del in dealers)
                {
                    strDealers += @"<div class='col-xl-4'>
       <div class='location-box'>
           <div class='card rounded-4 p-7 mb-7'>
               <div class='card-body p-0'>
                   <h2 class='fs-28px mb-2 mb-md-2'>"+ del.Name+@"</h2>
                   <div class='col-md-12 mb-11'>
                       <div class='d-flex align-items-start'>
                           <div class='d-none'>
                               <svg class='icon fs-2'>
                                   <use xlink:href='#'></use>
                               </svg>
                           </div>
                           <div>
                               <h3 class='fs-20px mb-2 mt-5'>
                                   Address
                               </h3>
                               <div class='fs-6'>
                                   <p class='mb-2 pb-4 fs-6'>"+ del.Address+@"</p>
                               </div>
                           </div>
                       </div>
                   </div>
                   <div class='col-md-6 mb-7'>
                       <div class='d-flex align-items-start'>
                           <div class='d-none'>
                               <svg class='icon fs-2'>
                                   <use xlink:href='#'></use>
                               </svg>
                           </div>
                           <div>
                               <h3 class='fs-5 mb-6'>Contact</h3>
                               <div class='fs-6'>
                                   <p class='mb-3 fs-6'>City :<span class='text-body-emphasis'>"+ del.CityName+@"</span></p><p class='mb-3 fs-6'>Mobile :<span class='text-body-emphasis'>"+ del.Phone+@"</span></p><p class='mb-0 fs-6'>
                                       E-mail: "+ del.EmailId+@"
                                   </p>
                               </div>
                           </div>
                       </div>
                   </div>
               </div>
           </div>
       </div>
   </div>";
                }
            }
        }
        catch (Exception ex)
        {

            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindDealerList", ex.Message);
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                DistributionNetwork cat = new DistributionNetwork();
                cat.Name = txtName.Text.Trim();
                cat.Email = txtEmail.Text.Trim();
                cat.City = txtCity.Text.Trim();
                cat.Phone = txtPhone.Text.Trim();
                cat.State ="";
                cat.Message = txtMessage.Text.Trim();
                int result = DistributionNetwork.InserDistributionNetwork(conAP, cat);
                if (result > 0)
                {
                    txtName.Text = txtEmail.Text = txtMessage.Text = txtCity.Text="";
                    Response.Redirect("/thank-you");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
}