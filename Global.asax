<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">



    void Application_Start(object sender, EventArgs e)
    {
        RegisterProducts(RouteTable.Routes);
    }
    void RegisterProducts(RouteCollection routes)
    {
        routes.Clear();
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.MapPageRoute("/products-details", "products/{subcaturl}", "~/product-detail.aspx");
        routes.MapPageRoute("/Products", "products-categories/{caturl}", "~/products.aspx");
        routes.MapPageRoute("/Blogs", "blogs", "~/blogs.aspx");
        routes.MapPageRoute("/blog-detail", "blog/{BlogUrl}", "~/blog-detail.aspx");
        routes.MapPageRoute("/shop-products", "shop", "~/shop-products.aspx");
        routes.MapPageRoute("/shop-products-tag", "shop/{tagURL}", "~/shop-products.aspx");
        routes.MapPageRoute("/404", "404", "~/404.aspx");
        routes.MapPageRoute("/shop-products-Details", "shop-products/{ProdUrl}", "~/shop-product-detail.aspx");
        routes.MapPageRoute("/investors-report-detail", "investors-report-detail", "~/investors-report-detail.aspx");
        routes.MapPageRoute("/investors", "investorsList", "~/investors.aspx");
        routes.MapPageRoute("/About-us", "about-us", "~/about-us.aspx");
        routes.MapPageRoute("/Contact-us", "contact-us", "~/contact-us.aspx");
        routes.MapPageRoute("/Thank-you", "thank-you", "~/thank-you.aspx");
        routes.MapPageRoute("/investor-contact", "investor-contact", "~/investor-contact.aspx");
        routes.MapPageRoute("/Cart", "cart", "~/shop-cart.aspx");
        routes.MapPageRoute("/Checkout", "checkout", "~/shop-checkout.aspx");
        routes.MapPageRoute("/My-profile", "my-profile", "~/my-profile.aspx");
        routes.MapPageRoute("/Change-password", "change-password", "~/change-password.aspx");
        routes.MapPageRoute("/Myorders", "myorders", "~/myorders.aspx");
        routes.MapPageRoute("/Factory", "factory", "~/factory.aspx");
        routes.MapPageRoute("/Showroom", "showroom", "~/showroom.aspx");
        routes.MapPageRoute("/Dealers", "dealers", "~/dealers.aspx");
        routes.MapPageRoute("/Career", "career", "~/career.aspx");
        routes.MapPageRoute("/Product-stories", "product-stories", "~/product-stories.aspx");
        routes.MapPageRoute("/Privacy-policy", "privacy-policy", "~/privacy-policy.aspx");
    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
